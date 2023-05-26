export class StorageDto {
    id?:number; 
    createdUserId?:number; 
    userName?:string;
    productName?:string;
    createdDate?:(Date | any); 
    lastUpdatedUserId?:number; 
    lastUpdatedDate?:(Date | any); 
    status:boolean; 
    isDeleted:boolean; 
    productId?:number; 
    unitsInStock?:number; 
    isReady:boolean; 
}