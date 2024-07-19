import {Component, Input} from '@angular/core';
import {TabsComponent} from "../../components/tabs/tabs.component";

@Component({
  selector: 'app-site-creator',
  standalone: true,
  imports: [
    TabsComponent
  ],
  templateUrl: './site-creator.component.html',
  styleUrl: './site-creator.component.scss'
})
export class SiteCreatorComponent {
  currentPageNumber: number = 0;
  setCurrentPageNumber(tab: number) {
    this.currentPageNumber = tab;
  }
}
