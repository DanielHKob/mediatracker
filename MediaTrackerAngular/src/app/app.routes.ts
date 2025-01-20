import { Routes } from '@angular/router';
import { MediaItemsComponent } from './media-items/media-items.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
    {path: 'login', component: LoginComponent}, // Login route FIRST
    {path: 'media-list', component: MediaItemsComponent, canActivate: [AuthGuard]},
    {path: 'user/id:', component: UserComponent, canActivate: [AuthGuard]},
    { path: 'create-user', component: CreateUserComponent }, // Ensure this is correct

    // Redirect to login if no matching route
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }
 
    
];
