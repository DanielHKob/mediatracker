import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User} from '../model/user';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserService {

   baseUrl: string = "http://localhost:5057/api";
 
   constructor(private http: HttpClient) {}
  
    // Helper method to create headers
    private getAuthHeaders(): HttpHeaders {
      const token = "Abc123!!!"; // Retrieve the token from localStorage or another storage
      return new HttpHeaders({
        'Authorization': `Bearer ${token}` // Replace 'Bearer' with your specific auth scheme if different
      });
    }

   getUsers(Id: number): Observable<User[]> {
    const headers = this.getAuthHeaders();
     return this.http.get<User[]>(this.baseUrl + "/User", {headers});
   }
 
   getUser(id: number): Observable<User> {
    const headers = this.getAuthHeaders();
     return this.http.get<User>(`${this.baseUrl}/User/${id}`, {headers});
   }
 
   createUser(mediaitems: Partial<User>): Observable<User> {
    const headers = this.getAuthHeaders();
     return this.http.post<User>(`${this.baseUrl}/User`, mediaitems, {headers});
   }
 
   updateUser(id: number, mediaitem: User): Observable<any>{
    const headers = this.getAuthHeaders(); 
    return this.http.put(`${this.baseUrl}/User/${id}`, mediaitem, {headers});
   }
 
   deleteUser(id: number): Observable<any> {
    const headers = this.getAuthHeaders(); 
    return this.http.delete(`${this.baseUrl}/User/${id}`, {headers});
   }
   
   getUserByEmail(email: string): Observable<User> {
    const headers = this.getAuthHeaders();
    return this.http.get<User>(`${this.baseUrl}/User/login/${email}`, {headers});
  }
  getLatestUserId(): Observable<number> {
    const headers = this.getAuthHeaders();
    return this.http.get<number>(this.baseUrl +"/User/latest-id", {headers});
  }

}
