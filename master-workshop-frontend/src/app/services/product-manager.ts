import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Producto } from '../app';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProductManager {
  private http = inject(HttpClient)
  private apiUrl = `${environment.apiUrl}/Products`

  getAllProducts(): Observable<Producto[]> {
    return this.http.get<Producto[]>(this.apiUrl)
  }

  getProductById(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${this.apiUrl}/${id}`)
  }

  addNewProduct(producto: Producto): Observable<boolean> {
    return this.http.post<boolean>(this.apiUrl, producto)
  }

  updateProduct(producto: Producto): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/update`, producto)
  }

  deleteProduct(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/delete/${id}`)
  }
}
