import {Component, Input} from '@angular/core';
import {TextInputComponent} from "../../components/text-input/text-input.component";
import {ButtonComponent} from "../../components/button/button.component";
import {Router, RouterLink} from "@angular/router";
import {AuthData} from "../../../types";
import {AuthService} from "../../../services/auth.service";
import {map} from "rxjs";
import {HttpClientModule} from "@angular/common/http";
import {popup} from "../../../popup";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    TextInputComponent,
    ButtonComponent,
    RouterLink,
    HttpClientModule,
  ],
  providers: [AuthService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  @Input() login: string = "";
  @Input() password: string = "";
  constructor(private router: Router, private authService: AuthService, ) {}
  handleLoginButtonClick = async () => {
    const data: AuthData = {
      login: this.login,
      password: this.password,
    }
    console.log(data)
    this.authService.login(data)
      .pipe(map(response => {
          return response;
        }),
      )
      .subscribe({
        next: (response) => {
          setTimeout(() => {
            popup("Вы успешно вошли!")
            console.log(response.result);
            const jwtToken = response.result.token
            localStorage.setItem("token", jwtToken);
            const userId = response.result.user.id
            localStorage.setItem("userId", userId);
            // window.location.reload()
          }, 1000)
        },
        error: (error) => {
          popup("Ошибка входа!")
          console.log("Error logging in", error)
        }
      })
  }
}
