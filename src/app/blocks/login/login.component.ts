import {Component, Input} from '@angular/core';
import {TextInputComponent} from "../../components/text-input/text-input.component";
import {ButtonComponent} from "../../components/button/button.component";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    TextInputComponent,
    ButtonComponent,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  @Input() login: string = "";
  @Input() password: string = "";
}
