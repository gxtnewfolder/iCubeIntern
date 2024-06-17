import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../interfaces/product';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  apiUrl = "http://localhost:8001";
  http=inject(HttpClient);
  constructor() { }

  getProducts(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.apiUrl + '/api/products');
  }

  getProduct(id: number): Observable<IProduct> {
    return this.http.get<IProduct>(this.apiUrl + '/api/products/' + id);
  }

  addProduct(product: IProduct): Observable<IProduct> {
    return this.http.post<IProduct>(this.apiUrl + '/api/products', product);
  }

  updateProduct(product: IProduct): Observable<IProduct> {
    return this.http.put<IProduct>(this.apiUrl + '/api/products/' + product.id, product);
  }

  deleteProduct(id: number): Observable<IProduct> {
    return this.http.delete<IProduct>(this.apiUrl + '/api/products/' + id);
  }
}
