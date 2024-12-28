export interface ClientRegistrationTokenItemModel {
    createdAt:Date,
    deviceName:string,
    expiration:Date,
    id:string,
    secret:string,
    token:string
}