import {Router, RouterOutlet} from '@angular/router';
import {HttpClientModule} from "@angular/common/http";
import {Component} from "@angular/core";
import {CookieService} from "ngx-cookie-service";
import {DataService} from "./blocks/main/api/data.service";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HttpClientModule,
  ],
  providers: [
    DataService,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  constructor(private dataService: DataService, private router: Router, private cookieService: CookieService) {
    const token = this.cookieService.get("tasty-cookies")
    console.log(token)
    if (token == "") {
      this.router.navigate(["/login"])
    } else {
      this.router.navigate(["/"])
    }
  }
}
