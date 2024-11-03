export interface ResponseModel {
    success:boolean,
    message:string,

}
export interface DataResponseModel<T> {
    success:boolean,
    message:string,
    data?:T
}