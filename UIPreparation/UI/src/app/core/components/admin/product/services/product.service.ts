import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})

export class ProductService {

  constructor(private httpClient: HttpClient) { }
  productUrl:'https://localhost:5001/api'

  getProductList(): Observable<Product[]> {

    return this.httpClient.get<Product[]>('https://localhost:5001/api/Products/getall')
  }

  getProductById(id: number): Observable<Product> {
    return this.httpClient.get<Product>(this.productUrl + '/Products/getbyid?id='+id)
  }

  addProduct(product: Product): Observable<any> {

    return this.httpClient.post('https://localhost:5001/api/Products', product, { responseType: 'text' });
  }

  updateProduct(product: Product): Observable<any> {
    return this.httpClient.put(this.productUrl + '/Products/', product, { responseType: 'text' });

  }

  deleteProduct(id: number) {
    return this.httpClient.request('delete', 'https://localhost:5001/api/Products', { body: { id: id } });
  }


}