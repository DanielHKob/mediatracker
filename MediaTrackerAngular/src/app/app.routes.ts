import { Routes } from '@angular/router';
import { MediaItemsComponent } from './media-items/media-items.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
    {path: 'media-list', component: MediaItemsComponent},
    { path: 'login', component: LoginComponent}, // Login route FIRST


    // Redirect to login if no matching route
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' } // Wildcard route last



];
