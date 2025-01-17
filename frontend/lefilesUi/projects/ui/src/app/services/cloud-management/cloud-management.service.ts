import { HttpClient } from '@angular/common/http';
import { APP_ID, Injectable } from '@angular/core';
import { DataResponseModel, ResponseModel } from 'projects/corelib/src/lib/models/results/responseModel';
import { environment } from 'projects/ui/src/environments/environment.prod';
import { FileAndFoldersResponse, FileSystemEntryItemResponse } from '../../models/file-management/fileSystemEntryItemResponse';
import { CreateFolderRequest } from '../../models/cloud-management/createFolderRequest';
import { UpdateFolderRequest } from '../../models/cloud-management/updateFolderRequest';
import { UpdateFolderContentsResponse } from '../../models/cloud-management/updateFolderContentsResponse';
import { FileItemDetailsResponse } from '../../models/file-management/fileItemDetailsResponse';
import { MoveFilesRequest } from '../../models/cloud-management/moveFilesRequest';
import { CopyFilesRequest } from '../../models/cloud-management/copyFilesRequest';
import { FolderItemDetailsResponse } from '../../models/file-management/folderItemDetailsResponse';
import { MoveFolderRequest } from '../../models/cloud-management/moveFolderRequest';
import { FilesystemSearchResult } from '../../models/cloud-management/filesystemSearchResult';


@Injectable({
  providedIn: 'root'
})
export class CloudManagementService {

  constructor(private httpClient:HttpClient) { }
  getFileSystemEntries(parentFolderId:string|null = null,options:any=null){
    var apiUrl = environment.apiUrl;
    var query = "?";

    if(parentFolderId != null){
      query += 'parentId='+parentFolderId;
    }

    if(options != null && options["filters"] != null && options["filters"].length > 0){
      query += (query == '?' ? `filters=${options["filters"]}` : `&filters=${options["filters"]}`);
    }

    if(options != null && options["viewtype"] != null){
      query += (query == '?' ? `viewtype=${options["viewtype"]}` : `&viewtype=${options["viewtype"]}`);
    }
    console.log(options);
    return this.httpClient.get<DataResponseModel<FileAndFoldersResponse>>(`${apiUrl}my-cloud/view-files${query}`);
  }
  createFolder(createFolderRequest:CreateFolderRequest){
    var apiUrl = environment.apiUrl;
    return this.httpClient.post<ResponseModel>(`${apiUrl}my-cloud/folders/create`,createFolderRequest);
  }
  updateFolder(updateFolderRequest:UpdateFolderRequest){
    var apiUrl = environment.apiUrl;
    return this.httpClient.post<ResponseModel>(`${apiUrl}my-cloud/folders/${updateFolderRequest.folderId}/update`,updateFolderRequest);
  }
  updateFolderContents(folderId:string){
    var apiUrl = environment.apiUrl;
    return this.httpClient.get<DataResponseModel<UpdateFolderContentsResponse>>(`${apiUrl}my-cloud/folders/${folderId}/update`);
  }
  deleteFolder(folderId:string,confirm:boolean){
    var request = {folderId:folderId,confirm:confirm};
    var apiUrl = environment.apiUrl;
    return this.httpClient.delete<ResponseModel>(`${apiUrl}my-cloud/folders/${folderId}/delete`,{body:request});
  }
  createFileEntry(folderId:string|undefined){
    var apiUrl = environment.apiUrl;
    var query = "";
    if(folderId != undefined){
      query = "?folder="+folderId;
    }
    return this.httpClient.get<DataResponseModel<string>>(`${apiUrl}my-cloud/upload-file${query}`);
  }
  deleteFile(fileId:string,confirm:boolean){
    var request = {file:fileId,confirm:confirm};
    var apiUrl = environment.apiUrl;
    return this.httpClient.delete<ResponseModel>(`${apiUrl}my-cloud/files/${fileId}/delete`,{body:request});
  }
  uploadFilePart(entryId:string,part:number,file:any,begin?:boolean,end?:boolean,filename?:string){
    var apiUrl = environment.apiUrl;
    var query = `?part=${part}`;
    if(begin != null && begin == true){
      query += `&begin=true`;
    }
    if(end != null && end == true){
      query += `&end=true`;
    }
    if(filename){
      query+="&filename="+filename;
    }
    var formData = new FormData();
    formData.set("file",file);
    return this.httpClient.post<ResponseModel>(`${apiUrl}my-cloud/file-entry/${entryId}/upload${query}`,formData);
  }
  public downloadFile(fileId:string){
    var apiUrl = environment.apiUrl;
    return this.httpClient.get<Blob>(`${apiUrl}my-cloud/files/${fileId}/download`,{observe:'events',responseType:'blob' as 'json',reportProgress:true});
  }
  public saveFile(blob: Blob, filename: string) {
    if (window.navigator && (window.navigator as any).msSaveOrOpenBlob) {
        (window.navigator as any).msSaveBlob(blob);
        console.log("msSaveBlob")
    } else {
      console.log("else e girdi");
        const blobUrl = URL.createObjectURL(blob);
        const aElement = document.createElement('a');
        aElement.href = blobUrl;
        aElement.download = filename;
        aElement.style.display = 'none';
        document.body.appendChild(aElement);
        aElement.click();
        URL.revokeObjectURL(blobUrl);
        aElement.remove();    
        console.log("else e girdi2");
    }
}
public getThumbnail(fileId:string){
  var apiUrl = environment.apiUrl;
  return this.httpClient.get<Blob>(`${apiUrl}my-cloud/files/${fileId}/thumbnail`,{responseType:'blob' as 'json'});
}
public getImagePreview(fileId:string){
  var apiUrl = environment.apiUrl;
  return this.httpClient.get<Blob>(`${apiUrl}my-cloud/files/${fileId}/preview`,{responseType:'blob' as 'json'});
}
getFileDetails(fileId:string){
  var apiUrl = environment.apiUrl;
  return this.httpClient.get<DataResponseModel<FileItemDetailsResponse>>(`${apiUrl}my-cloud/files/${fileId}/details`);
}
getFolderDetails(folderId:string){
  var apiUrl = environment.apiUrl;
  return this.httpClient.get<DataResponseModel<FolderItemDetailsResponse>>(`${apiUrl}my-cloud/folders/${folderId}/details`);
}
moveFiles(request:MoveFilesRequest){
  var apiUrl = environment.apiUrl;
  return this.httpClient.post<ResponseModel>(`${apiUrl}my-cloud/files/management/move`,request);
}
copyFiles(request:CopyFilesRequest){
  var apiUrl = environment.apiUrl;
  return this.httpClient.post<ResponseModel>(`${apiUrl}my-cloud/files/management/copy`,request);
}
moveFolder(request:MoveFolderRequest){
  var apiUrl = environment.apiUrl;
  return this.httpClient.post<ResponseModel>(`${apiUrl}my-cloud/folders/${request.sourceFolderId}/move`,request);
}
searchFilesystem(q:string){
  var apiUrl = environment.apiUrl;
  return this.httpClient.get<DataResponseModel<FilesystemSearchResult>>(`${apiUrl}my-cloud/filesystem/search?q=${q}`);
}

}
