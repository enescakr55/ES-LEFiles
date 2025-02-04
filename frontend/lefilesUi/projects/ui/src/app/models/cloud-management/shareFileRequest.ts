import { SharedItemAccessTypesEnum } from "../enums/sharedItemAccessTypesEnum";

export interface ShareFileRequest {
    fileId:string,
    end?:string,
    access:SharedItemAccessTypesEnum,
    users?:string

}