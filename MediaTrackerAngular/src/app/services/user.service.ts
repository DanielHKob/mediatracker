import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User

 } from '../model/user';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserService {

   baseUrl: string = "http://localhost:5057/api";
 
   constructor(private http: HttpClient) {}
  
   getMedias(Id: number): Observable<User[]> {
     return this.http.get<User[]>(this.baseUrl + "/User");
   }
 
   getMedia(id: number): Observable<User> {
     return this.http.get<User>(`${this.baseUrl}/User/${id}`);
   }
 
   createMedia(mediaitems: Partial<User>): Observable<User> {
     return this.http.post<User>(`${this.baseUrl}/User`, mediaitems);
   }
 
   updateMedia(id: number, mediaitem: User): Observable<any>{
     return this.http.put(`${this.baseUrl}/User/${id}`, mediaitem);
   }
 
   deleteMedia(id: number): Observable<any> {
     return this.http.delete(`${this.baseUrl}/User/${id}`);
   }
 
}
