import { Component, OnInit } from '@angular/core';
import { AuthService } from '../resources/auth.service';

@Component({
  selector: 'app-auth-buttons',
  templateUrl: './auth-buttons.component.html',
  styleUrls: ['./auth-buttons.component.scss'],
})
export class AuthButtonsComponent implements OnInit {
  constructor(public authService: AuthService) {}

  ngOnInit(): void {}

  logout() {
    console.log('working');
    this.authService.logout();
  }
}
