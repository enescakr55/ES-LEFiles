import { ApplicationRef, ComponentRef, createComponent, Injectable, Type } from '@angular/core';
import { ComponentModalComponent } from '../../components/component-modal/component-modal.component';

@Injectable({
  providedIn: 'root'
})

export class ViewComponentModalService {
  private appRef:ApplicationRef;
  constructor() { }
  public setApplicationRef(applicationRef) {
    this.appRef = applicationRef;
  }
  showModal(title:string,type:string,content:any,componentObject:any=null,modalSettings:any = null){
    return new Promise<boolean>((resolve,reject)=>{
      const componentRef = createComponent(ComponentModalComponent,{environmentInjector:this.appRef.injector});
      componentRef.instance.titleTranslationKey = title;
      componentRef.instance.type = type;
      componentRef.instance.content = content;
      componentRef.instance.object = componentObject;
      componentRef.instance.modalSettings = modalSettings;
      this.appRef.attachView(componentRef.hostView);
      componentRef.location;
      document.body.append((<any>componentRef.hostView).rootNodes[0]);
      componentRef.instance.response.subscribe((x)=>{
        resolve(x);
        this.removeDynamicComponent(componentRef);
      })
    });
  }
  showModalWithComponent<T>(component:Type<T>,title:any,componentObject=null){
    const componentRef = createComponent(component,{environmentInjector:this.appRef.injector});
    if(componentObject != null){
      console.log(Object.keys(componentObject));
      Object.keys(componentObject).forEach(key=>{
        componentRef.instance[key] = componentObject[key];
      })
    }
    this.appRef.attachView(componentRef.hostView);
    return (<any>componentRef.hostView).rootNodes[0];
  }
  removeDynamicComponent(component:ComponentRef<ComponentModalComponent>){
    component.destroy();
  }
}
