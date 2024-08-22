import {Router, RouterOutlet} from '@angular/router';
import {DataService} from "../services/data.service";
import {HttpClientModule} from "@angular/common/http";
import {Component} from "@angular/core";


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
  constructor(private dataService: DataService, private router: Router) {
    const token = localStorage.getItem("token")
    if (!token) {
      this.router.navigate(["/login"])
    } else {
      this.router.navigate(["/"])
    }
  }
}
