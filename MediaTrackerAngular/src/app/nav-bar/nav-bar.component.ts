import { Component, Input } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../services/user.service';
import { User } from '../model/user';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterLink, MatMenuModule,MatIcon,MatToolbarModule,MatButton, CommonModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {

  userid: any;
  userName: any;
  firstname: any;
  email: any;
@Input() user!: User;


  constructor( private router: Router,
    private userService: UserService
   ){}


  ngOnInit(): void{
    this.getUserEmail();
    this.getUser(this.email);
  }


  getUserEmail(): void {
    const headerValue = localStorage.getItem('headerValue');
    if(headerValue){
      const base64Credentials = headerValue.replace('Basic ', '');
      const decodedString = atob(base64Credentials);
      const [extractedEmail] = decodedString.split(':');
      this.email = extractedEmail;
      console.log(this.email);
    } else {
      console.log("failed due to missing authentication!");
    }
  }

  getUser(email: string){
    this.userService.getUserByEmail(email).subscribe((user) => {
      if(user.id !== undefined){
        this.firstname = user.FirstName;
        this.userid = user.id;
      }
    })
  }
  
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('headerValue');
  }
  
  logout(){
    const confirmLoguout = confirm('Are you sure you want to logout?');
    if (!confirmLoguout){
      return;
    }
    // Clear the 'headerValue' from the localStorage
    localStorage.removeItem('headerValue');

    // navigate to the login or home page after successfull clearing of localStorage
    this.router.navigate(['/login']);
  }

  goToProfile(userid: number): void {
    console.log(this.userid);

    if(this.userid){
      this.router.navigate(['/user'])
    }
  }

  

}
