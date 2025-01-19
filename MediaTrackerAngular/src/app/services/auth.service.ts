import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl: string = "http://localhost:5057/api";

  constructor(private http: HttpClient) {}

  // Instead of "username", we now call it "identifier"
  authenticate(identifier: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/login/authenticate`, {
      identifier: identifier,
      password: password
    });
  }
}