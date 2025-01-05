
export interface FileAndFoldersResponse {
    parents:ParentFolderEntryResponse[];
    entries:FileSystemEntryItemResponse[];
}
export interface ParentFolderEntryResponse {
    id:string,
    name:string
}
export interface FileSystemEntryItemResponse {
    id:string,
    type:number,
    name:string,
    shared:boolean,
    extension?:string,
    createdAt:Date,
    thumbnailExists?:boolean;
    allowPreview?:boolean;
    icon?:string;
}