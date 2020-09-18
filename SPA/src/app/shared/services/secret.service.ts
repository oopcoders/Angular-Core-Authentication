import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SecretService {
  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) {}

  getValues(): Observable<string[]> {
    return this.http.get<string[]>(
      this.baseUrl + 'value',
      this.getHttpOptions()
    );
  }

  getHttpOptions() {
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token'),
      }),
    };

    return httpOptions;
  }
}
