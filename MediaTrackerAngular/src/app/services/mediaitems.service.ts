import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MediaItems } from '../model/media-items';

@Injectable({
  providedIn: 'root'
})
export class MediaitemsService {
  baseUrl: string = "http://localhost:5057/api";

  constructor(private http: HttpClient) {}
 
  getMedias(Id: number): Observable<MediaItems[]> {
    return this.http.get<MediaItems[]>(this.baseUrl + "/Media");
  }

  getMedia(id: number): Observable<MediaItems> {
    return this.http.get<MediaItems>(`${this.baseUrl}/Media/${id}`);
  }

  createMedia(mediaitems: Partial<MediaItems>): Observable<MediaItems> {
    return this.http.post<MediaItems>(`${this.baseUrl}/Media`, mediaitems);
  }

  updateMedia(id: number, mediaitem: MediaItems): Observable<any>{
    return this.http.put(`${this.baseUrl}/Media/${id}`, mediaitem);
  }

  deleteMedia(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Media/${id}`);
  }

}
