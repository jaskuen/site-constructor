import {Component, Input, OnInit, signal} from '@angular/core';
import {ButtonComponent} from "../../../components/button/button.component";
import {Router, RouterLink} from "@angular/router";
import {TextInputComponent} from "../../../components/text-input/text-input.component";
import {ApiResponse, AuthData} from "../../../../types";
import {catchError, debounceTime, map, of, switchMap} from "rxjs";
import {HttpClientModule} from "@angular/common/http";
import {popup} from "../popup";
import {AuthService} from "../api/auth.service";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatError, MatFormField, MatLabel, MatSuffix} from "@angular/material/form-field";
import {MatIcon} from "@angular/material/icon";
import {MatIconButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ButtonComponent,
    RouterLink,
    TextInputComponent,
    HttpClientModule,
    FormsModule,
    MatFormField,
    MatIcon,
    MatIconButton,
    MatInput,
    MatLabel,
    MatSuffix,
    MatError,
    ReactiveFormsModule,
    CommonModule,
  ],
  providers: [AuthService],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  @Input() username: string = ""
  @Input() password: string = ""
  @Input() repeatPassword: string = ""

  disableButton: boolean = false;
  constructor(private authService: AuthService, private  router: Router) {}
  public authForm = new FormGroup({
    username: new FormControl(
      this.username, [
        Validators.required,
      ]),
    password: new FormControl(
      this.password, [
        Validators.required, Validators.minLength(6),
      ]),
    repeatPassword: new FormControl(
      this.repeatPassword, [
        Validators.required, Validators.minLength(6),
      ]
    )
  });
  hide = signal(true);
  hideRepeated = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
  clickRepeatedEvent(event: MouseEvent) {
    this.hideRepeated.set(!this.hideRepeated());
    event.stopPropagation();
  }

  ngOnInit() {
    this.authForm.controls.username.valueChanges
      .pipe(
        debounceTime(100),
        switchMap(value => this.authService.checkLogin({login: value!})),
        catchError(() => of(false)) // Обработка ошибок
      )
      .subscribe(response => {
      if (typeof(response) === "object") {
        if (response.data.exists) {
          this.disableButton = true
          this.authForm.controls.username.setErrors({ loginTaken: true });
        } else {
          this.disableButton = false
        }
      }
    });
  }

  handleRegisterButtonClick = async () => {
    if (this.authForm.controls.password.value !== this.authForm.controls.repeatPassword.value) {
      popup("Пароли не совпадают!")
      return
    }
    const data: AuthData = {
      login: this.authForm.controls.username.value!,
      password: this.authForm.controls.password.value!,
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
