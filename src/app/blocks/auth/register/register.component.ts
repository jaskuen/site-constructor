import {Component, Input} from '@angular/core';
import {ButtonComponent} from "../../../components/button/button.component";
import {Router, RouterLink} from "@angular/router";
import {TextInputComponent} from "../../../components/text-input/text-input.component";
import {AuthData} from "../../../../types";
import {map} from "rxjs";
import {HttpClientModule} from "@angular/common/http";
import {popup} from "../popup";
import {AuthService} from "../api/auth.service";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
      ButtonComponent,
      RouterLink,
      TextInputComponent,
      HttpClientModule,
  ],
  providers: [AuthService],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  @Input() login: string = ""
  @Input() password: string = ""
  @Input() repeatPassword: string = ""

  constructor(private authService: AuthService, private  router: Router) {}
  handleRegisterButtonClick = async () => {
    if (this.password !== this.repeatPassword) {
      popup("Пароли не совпадают!")
      return
    }
    const data: AuthData = {
      login: this.login,
      password: this.password,
    }
    console.log(data)
    this.authService.register(data)
      .pipe(map(response => {
          return response;
        }),
      )
      .subscribe({
        next: (response) => {
          popup("Вы успешно зарегистрировались!")
          setTimeout(() => {
            this.router.navigate(["/login"])
          }, 1000)
        },
        error: (error) => {
          popup("Этот логин уже занят!")
          // проверку на наличие логина в реальном времени (сменить input на form)
          console.log("Error logging in", error)
        }
      })
  }

}
