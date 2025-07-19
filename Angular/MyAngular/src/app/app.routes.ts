import { Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { MsalGuard } from '@azure/msal-angular';

export const routes: Routes = [
    { path: '', component: HomeComponent },
 {
    path: 'dashboard',
  //  canActivate: [MsalGuard],
    loadComponent: () => import('./Components/dashboard/dashboard.component').then(m => m.DashboardComponent),
  },
];
