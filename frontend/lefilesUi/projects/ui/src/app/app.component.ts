import { ApplicationRef, Component, OnInit, ViewContainerRef } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { ViewComponentModalService } from 'projects/corelib/src/lib/services/componentModal/view-component-modal.service';
import { ErrorHandlerService } from 'projects/corelib/src/lib/services/handler/error-handler.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: false
})
export class AppComponent implements OnInit {
  title = 'ui';
  constructor(private translateService:TranslateService,private applicationRef: ApplicationRef,private componentModal:ViewComponentModalService, private containerRef: ViewContainerRef,private toastrService:ToastrService,private router:Router){}
  
  hideMenuUrls:string[] = ["/login","/shared"];
  menuVisibility:boolean = true;
  showProgressViewer:boolean = false;
  ngOnInit(): void {
    this.componentModal.setApplicationRef(this.applicationRef);
    
    this.translateService.addLangs(["tr-TR"]);
    this.translateService.use("tr-TR");
    ErrorHandlerService.toastrService = this.toastrService;
    ErrorHandlerService.translateService = this.translateService;
    this.startUrlListener();
  }
  startUrlListener(){
    var currentUrl = window.location.pathname;
    this.updateMenuVisibility(currentUrl);
    console.log(currentUrl);
    this.router.events.subscribe({
      next:(route)=>{

        if(route instanceof NavigationEnd){
          var url = route.url;
          this.updateMenuVisibility(url);
        }
      }
    })
  }

  updateMenuVisibility(url:string){
    console.log("--"+url)
    var hideMenu = false;
    this.hideMenuUrls.forEach(x=>{
      if(url.startsWith(x)){
        hideMenu = true;
      }
    })
    this.menuVisibility = !hideMenu;
  }
  
}
