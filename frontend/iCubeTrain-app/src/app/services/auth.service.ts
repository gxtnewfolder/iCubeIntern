import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AuthResponse } from '../interfaces/auth-response';
import { LoginRequest } from '../interfaces/login-request';
import { map, Observable } from 'rxjs';
import { jwtDecode }  from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl:string = environment.apiUrl;
  private tokenkey = 'token';

  constructor(private http:HttpClient) { }

  login(data:LoginRequest):Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/account/login`, data).pipe(
      map((response) => {
        if (response.token) {
          localStorage.setItem(this.tokenkey, response.token);
        }
        return response;
      })
    );
  }

  isLogedIn():boolean {
    return !!localStorage.getItem(this.tokenkey);
  }

  getToken = ():string | null => localStorage.getItem(this.tokenkey) || '';

  isTokenExpired() {
    const token = this.getToken();
    if (!token) return true;
    const decoded: any = jwtDecode(token);
    const isTokenExpired = Date.now() >= decoded['exp']! * 1000;
    if (isTokenExpired) {
      this.logout();
    }
    return isTokenExpired;
  }

  getUserDetails() {
    const token = this.getToken();
    if (!token) return null;
    const decoded: any = jwtDecode(token);
    const userDetails = {
      email: decoded.email,
      username: decoded.unique_name,
    };
    return userDetails;
  }

  logout() {
    localStorage.removeItem(this.tokenkey);
  }
}
