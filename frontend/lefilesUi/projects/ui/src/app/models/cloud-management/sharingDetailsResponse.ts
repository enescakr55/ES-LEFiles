export interface SharingDetailsResponse {
    name:string,
    access:number,
    end?:string,
    accessKey:string,
    users?:SharingDetailsUsersResponse[]
}
export interface SharingDetailsUsersResponse {
    id:string;
    userName:string;
}