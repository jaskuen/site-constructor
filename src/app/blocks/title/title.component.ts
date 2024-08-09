import {Component, Input} from '@angular/core';
import {ButtonComponent} from "../../components/button/button.component";

@Component({
  selector: 'app-title',
  standalone: true,
  imports: [
    ButtonComponent
  ],
  templateUrl: './title.component.html',
  styleUrl: './title.component.scss'
})
export class TitleComponent {
  @Input() onClick() {};
}
