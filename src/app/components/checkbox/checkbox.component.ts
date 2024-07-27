import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-checkbox',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './checkbox.component.html',
  styleUrl: './checkbox.component.scss'
})
export class CheckboxComponent {
  @Input() main: boolean = false;
  @Input() placeholder: string = "";
  @Input() value: boolean = false
  @Output() valueChange = new EventEmitter<boolean>();
  onChange($event: Event) {
    this.value = !this.value;
    this.valueChange.emit(this.value);
  }
}
