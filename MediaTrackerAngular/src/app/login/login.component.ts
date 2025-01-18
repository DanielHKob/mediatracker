import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { User } from '../model/user';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatFormFieldModule , MatInputModule, MatLabel, CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

userName!: string;
email: string = ""
password!: string;
password_input!: string;
authenticated = false; 
loginForm: any;
@Input() user!: User;
constructor(private router: Router,
  private fb: FormBuilder,
  private userService: UserService
)
 {
this.loginForm = this.fb.group({
  userName: ['', [Validators.required]],
  password: ['', Validators.required]
});
}

  ngOnInit(): void{

  }

  login(){
    if(this.userName.includes('@')){
      console.log(this.userName);
      this.email = this.userName;
      this.getUserByEmail(this.email);
    }
    else 
    {
      alert('Please enter a validated email-adress'); 
    }
  }

  getUserByEmail(email: string){
    console.log("get user by email has been called!");
    this.userService.getUserByEmail(email).subscribe({
      next: (user) => {
        this.password = user.Password;
        console.log("dbpassword:", this.password);
        this.UserPasswordCompare();
      }
    })
  }

  UserPasswordCompare() {
    if(this.password_input == this.password){
      console.log('Basic' + btoa(`${this.email}:${this.password_input}`));
      const basicValue = 'Basic' + btoa(`${this.email}:${this.password_input}`);
      localStorage.setItem('headerValue', basicValue);
      // now navigate to safe space 
      this.router.navigate(['/media-items'])
    }
  }



}
