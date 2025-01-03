import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DomSanitizer, SafeHtml, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
import { ViewComponentModalService } from '../../services/componentModal/view-component-modal.service';

declare var bootstrap: any;
@Component({
  selector: 'lib-component-modal',
  templateUrl: './component-modal.component.html',
  styleUrls: ['./component-modal.component.css']
})
export class ComponentModalComponent implements OnInit, AfterViewInit {
  @Input() titleTranslationKey: string;
  @Input() content: any;
  safeContent: SafeResourceUrl;
  safeHml: SafeHtml;
  @Input() type: string;
  @Output() response: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Input() footerHtml?: string = undefined;
  safeFooterHTML?: SafeHtml = undefined;
  openedModal: any;
  loaded: boolean = false;
  @Input() object:any = null;
  @Input() modalSettings:any; //customWidth:number;
  classes:string = "";
  constructor(private domSanitizer: DomSanitizer, private viewComponentModalService: ViewComponentModalService) {

   }
  ngAfterViewInit(): void {
    if (this.type == 'component') {


      var componentItem = this.viewComponentModalService.showModalWithComponent<any>(this.content, "",this.object);
      if(this.object != null){

      }
      var bodyItems = document.getElementsByClassName("modal-body")
      bodyItems[0].innerHTML = "";
      bodyItems[0].appendChild(componentItem);
    }
    setTimeout(() => {
      var modal = document.getElementById("componentModal");
      console.log(modal)
      const createdModal = new bootstrap.Modal(modal, { keyboard: false });
      createdModal.show();
      this.openedModal = createdModal;
      modal.addEventListener("click",(ev)=>{
        var clickedElem = ev.target as HTMLElement;
        if(clickedElem.getAttribute("data-bs-dismiss") != null && clickedElem.getAttribute("data-bs-dismiss") == "modal"){
          this.close();
          console.log("closed");
        }
      })
      /*var qsItems = modal.querySelectorAll('button[data-bs-dismiss="modal"]').forEach(a=>{
        var btn = a as HTMLButtonElement;
        btn.addEventListener("click",()=>{
          this.close();
        })
      });*/
    }, 250)
  }

  setModalWidth(){
    if(this.modalSettings != null){
      if(this.modalSettings["width"] != null){
        switch(this.modalSettings["width"]){
          case "large":
            this.classes += "modal-xl";
            break;
          case "medium":
            this.classes += "modal-lg";
            break;
          case "small":
            this.classes += "modal-sm";
            break;
        }
      }
    }
  }
  ngOnInit(): void {
    this.setModalWidth();
    if (this.type == 'link') {
      this.safeContent = this.domSanitizer.bypassSecurityTrustResourceUrl(this.content);
      console.log(this.safeContent);
      //this.safeContent = {changingThisBreaksApplicationSecurity:this.content};
      //console.log(this.safeContent);
    } else if (this.type == 'html') {
      this.safeHml = this.domSanitizer.bypassSecurityTrustHtml(this.content);
    } 
    if (this.footerHtml != undefined) {
      this.safeFooterHTML = this.domSanitizer.bypassSecurityTrustHtml(this.footerHtml);
    }
  }
  onLoadFrame($ev: any) {
    var o = $ev.target;
    try {
      setTimeout(() => {
        var ifHeight = (o.contentDocument || o.contentWindow.document).body.scrollHeight;
        if (ifHeight > 0) {
          o.style.height = ifHeight + "px";
          this.loaded = true;
        } else {
          this.onLoadFrame($ev);
          return;
        }
      }, 1000);

    } catch {
      this.onLoadFrame($ev);
    }




  }
  close() {
    try {
      (document.getElementById("modalFrame") as HTMLIFrameElement).src = "about:blank"
    } catch {

    }

    this.response.emit(true);
  }

}
