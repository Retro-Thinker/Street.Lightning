export interface ResponseDto<T> {
   data: T
   operationStatus: number,
   message: string
}