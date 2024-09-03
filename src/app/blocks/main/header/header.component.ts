import { Component } from '@angular/core';
import {ButtonComponent} from "../../../components/button/button.component";
import {AuthService} from "../../auth/api/auth.service";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [ButtonComponent],
  providers: [AuthService],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(private authService: AuthService) {
  }
  handleClick = () => {
    this.authService.logout();
    window.location.reload();
  }
}
