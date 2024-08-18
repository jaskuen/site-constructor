import { RouterOutlet } from '@angular/router';
import {DataService} from "./data.service";
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

}
