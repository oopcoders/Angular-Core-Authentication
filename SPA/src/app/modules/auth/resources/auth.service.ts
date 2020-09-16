import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';
import { IUser } from './IUser';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl: string = environment.baseUrl;
  isLoggedIn: boolean;

  currentUser: IUser = {
    username: null,
    email: null,
  };
  constructor(private http: HttpClient) {}

  login(model: any): Observable<IUser> {
    return this.http.post(this.baseUrl + 'identity/login', model).pipe(
      map((response: any) => {
        //temporary
        this.isLoggedIn = response.result.succeeded;
        this.currentUser.username = response.username;
        this.currentUser.email = response.email;

        return this.currentUser;
      })
    );
  }

  logout() {
    this.isLoggedIn = false;
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'identity/register', model);
  }

  confirmEmail(model: any) {
    //Video 16
    return of();
    //return this.http.post(this.baseUrl + 'identity/confirmemail', model);
  }
}
