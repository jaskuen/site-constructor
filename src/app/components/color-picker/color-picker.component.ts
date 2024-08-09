import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgOptimizedImage} from "@angular/common";
import {ColorPickerModule} from "ngx-color-picker";

@Component({
  selector: 'app-color-picker',
  standalone: true,
  imports: [
    NgOptimizedImage,
    ColorPickerModule,
  ],
  templateUrl: './color-picker.component.html',
  styleUrl: './color-picker.component.scss'
})
export class ColorPickerComponent {
  @Input() color!: string;
  @Input() text!: string;
  @Output() colorChange = new EventEmitter<string>();
  onColorChange(newColor: string) {
    this.color = newColor;
    this.colorChange.emit(this.color);
  }
}
