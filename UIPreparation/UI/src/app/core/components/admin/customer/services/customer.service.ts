import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private httpClient: HttpClient) { }


  getCustomerList(): Observable<Customer[]> {

    return this.httpClient.get<Customer[]>('https://localhost:5001/api/Customers/getall')
  }

  getCustomerById(id: number): Observable<Customer> {
    return this.httpClient.get<Customer>(environment.getApiUrl + '/customers/getbyid?id='+id)
  }

  addCustomer(customer: Customer): Observable<any> {

    return this.httpClient.post('https://localhost:5001/api/Customers', customer, { responseType: 'text' });
  }

  updateCustomer(customer: Customer): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/customers/', customer, { responseType: 'text' });

  }

  deleteCustomer(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/customers/', { body: { id: id } });
  }


}