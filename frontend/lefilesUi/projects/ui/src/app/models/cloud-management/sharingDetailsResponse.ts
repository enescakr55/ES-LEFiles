export interface SharingDetailsResponse {
    name:string,
    access:number,
    end?:string,
    users?:SharingDetailsUsersResponse[]
}
export interface SharingDetailsUsersResponse {
    id:string;
    userName:string;
}