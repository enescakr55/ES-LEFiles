export interface FileProgressItem {
    progressId:string,
    fileId:string,
    fileName:string,
    type:string, //download | upload
    text:string,
    progress:number,
    status:string,
    lastUpdate:Date

}