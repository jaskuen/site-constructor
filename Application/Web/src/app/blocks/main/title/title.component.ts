import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ButtonComponent} from "../../../components/button/button.component";

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
  @Input() download!: boolean;
  @Output() downloadChange = new EventEmitter();
  @Input() onClick() {};
  handleClick(download: boolean) {
    this.onClick()
    this.download = download
    this.downloadChange.emit(this.download);
  }
}
