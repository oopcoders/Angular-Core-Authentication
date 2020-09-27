import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IResponse } from 'src/app/modules/auth/resources/IResponse';
import { SecretService } from 'src/app/shared/services/secret.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent implements OnInit {
  secrets$: Observable<IResponse>;
  constructor(private secretService: SecretService) {}

  ngOnInit(): void {
    this.getAdminDeveloperSecrets();
  }

  getAdminDeveloperSecrets() {
    this.secrets$ = this.secretService.adminDeveloperSecrets();
  }
}
