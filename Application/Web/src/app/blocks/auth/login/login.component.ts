import {Component, Input, signal} from '@angular/core';
import {TextInputComponent} from "../../../components/text-input/text-input.component";
import {ButtonComponent} from "../../../components/button/button.component";
import {Router, RouterLink} from "@angular/router";
import {AuthData} from "../../../../types";
import {map} from "rxjs";
import {HttpClientModule} from "@angular/common/http";
import {popup} from "../popup";
import {AuthService} from "../api/auth.service";
import {MatFormField, MatFormFieldModule} from "@angular/material/form-field";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatInput} from "@angular/material/input";
import {MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {BrowserModule} from "@angular/platform-browser";
import {CommonModule} from "@angular/common";
import {stringify} from "uuid";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    TextInputComponent,
    ButtonComponent,
    RouterLink,
    HttpClientModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInput,
    MatIconButton,
    MatIcon,
    CommonModule,
  ],
  providers: [AuthService, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  @Input() username: string = "";
  @Input() password: string = "";
  constructor(private router: Router, private authService: AuthService, ) {}
  public authForm = new FormGroup({
    username: new FormControl(
      this.username, [
        Validators.required,
      ]),
    password: new FormControl(
      this.password, [
        Validators.required, Validators.minLength(6),
        ])
  });
  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
  handleLoginButtonClick = async () => {
    const data: AuthData = {
      login: this.authForm.controls.username.value!,
      password: this.authForm.controls.password.value!,
    }
    console.log(data)
    this.authService.login(data)
      .pipe(map(response => {
          return response;
        }),
      )
      .subscribe({
        next: (response) => {
          popup("Вы успешно вошли!")
          console.log(response)
          localStorage.setItem("userId", response.result.userId.toString())
          setTimeout(() => {
            window.location.reload()
          }, 1000)
        },
        error: (error) => {
          popup("Ошибка входа!")
          console.log("Error logging in", error)
        }
      })
  }
}