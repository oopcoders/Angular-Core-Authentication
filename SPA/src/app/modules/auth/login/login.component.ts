import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../resources/auth.service';
import { ProgressbarService } from 'src/app/shared/services/progressbar.service';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private progressService: ProgressbarService,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {}

  onSubmit(f: NgForm) {
    this.alertService.info('Check login information');
    this.progressService.startLoading();
    this.authService.isLoggedIn = true;

    setTimeout(() => {
      this.progressService.setSuccess();
      this.progressService.completeLoading();
      this.alertService.warning('Welcome User');
    }, 3000);
  }
}
