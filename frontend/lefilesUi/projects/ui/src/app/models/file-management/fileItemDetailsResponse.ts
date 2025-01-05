export interface FileItemDetailsResponse {
    fileId:string,
    fileName:string,
    extension:string,
    icon:string,
    preview:boolean,
    thumbnail:boolean,
    shared:boolean,
    createdAt:Date,
    fileSize:number
}