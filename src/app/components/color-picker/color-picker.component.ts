import {Component, Input} from '@angular/core';
import {NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-color-picker',
  standalone: true,
  imports: [
    NgOptimizedImage
  ],
  templateUrl: './color-picker.component.html',
  styleUrl: './color-picker.component.scss'
})
export class ColorPickerComponent {
  @Input() bgColor!: string;
  @Input() text!: string;
}
