import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from '../models/order';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClient: HttpClient) { }


  getOrderList(): Observable<Order[]> {

    return this.httpClient.get<Order[]>('https://localhost:5001/api/Orders/getall')
  }

  getOrderById(id: number): Observable<Order> {
    return this.httpClient.get<Order>(environment.getApiUrl + '/orders/getbyid?id='+id)
  }

  addOrder(order: Order): Observable<any> {

    return this.httpClient.post('https://localhost:5001/api/Orders', order, { responseType: 'text' });
  }

  updateOrder(order: Order): Observable<any> {
    return this.httpClient.put('https://localhost:5001/api/Orders', order, { responseType: 'text' });

  }

  deleteOrder(id: number) {
    return this.httpClient.request('delete','https://localhost:5001/api/Orders', { body: { id: id } });
  }


}