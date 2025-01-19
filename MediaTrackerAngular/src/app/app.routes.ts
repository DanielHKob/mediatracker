import { Routes } from '@angular/router';
import { MediaItemsComponent } from './media-items/media-items.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { CreateUserComponent } from './create-user/create-user.component';

export const routes: Routes = [
    {path: 'media-list', component: MediaItemsComponent},
    {path: 'user/id:', component: UserComponent},
    { path: 'create-user', component: CreateUserComponent},
    {path: 'login', component: LoginComponent}, // Login route FIRST

    // Redirect to login if no matching route
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }
 
    
];
