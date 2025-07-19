import { Component } from '@angular/core';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  constructor(private msalService: MsalService) {
    this.msalService.instance.handleRedirectPromise().then(result => {
      if (result?.account) {
        this.msalService.instance.setActiveAccount(result.account);
      }
    });

  }

  
  login() {
    this.msalService.loginRedirect();
  }

  logout() {
    this.msalService.logoutRedirect();
  }
}
