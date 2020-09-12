import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl: string = environment.baseUrl;
  isLoggedIn: boolean;
  constructor(private http: HttpClient) {}

  login() {
    this.isLoggedIn = true;
  }
  logout() {
    this.isLoggedIn = false;
  }

  confirmEmail(model: any) {
    //Video 16
    return of();
    //return this.http.post(this.baseUrl + 'identity/confirmemail', model);
  }
}
