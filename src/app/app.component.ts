import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from "./blocks/header/header.component";
import {TitleComponent} from "./blocks/title/title.component";
import {TabsComponent} from "./components/tabs/tabs.component";
import {SiteCreatorComponent} from "./blocks/site-creator/site-creator.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, TitleComponent, TabsComponent, SiteCreatorComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'MyFirstAngularProject';
}
