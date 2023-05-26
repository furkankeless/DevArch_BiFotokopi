import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Storage } from '../models/storage';
import { environment } from 'environments/environment';
import { StorageDto } from '../models/storageDto';


@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor(private httpClient: HttpClient) { }


  getStorageDtoList(): Observable<StorageDto[]> {

    return this.httpClient.get<StorageDto[]>('https://localhost:5001/api/Storages/getdtos')
  }

  getStorageById(id: number): Observable<Storage> {
    return this.httpClient.get<Storage>('https://localhost:5001/api/Storages/getbyid'+id)
  }

  addStorage(storage: Storage): Observable<any> {

    return this.httpClient.post('https://localhost:5001/api/Storages', storage, { responseType: 'text' });
  }

  updateStorage(storage: Storage): Observable<any> {
    return this.httpClient.put('https://localhost:5001/api/Storages', storage, { responseType: 'text' });

  }

  deleteStorage(id: number) {
    return this.httpClient.request('delete', 'https://localhost:5001/api/Storages', { body: { id: id } });
  }
  getDtoLookup():Observable<StorageDto>{
    return this.httpClient.get<StorageDto>('https://localhost:5001/api/Storages/getdtos')
  }


}