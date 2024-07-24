import {Component, EventEmitter, Input, Output} from '@angular/core';
import {JsonPipe, NgForOf, NgIf} from "@angular/common";
import {SelectOption} from "../../../types";

@Component({
  selector: 'app-select',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    JsonPipe
  ],
  templateUrl: './select.component.html',
  styleUrl: './select.component.scss'
})
export class SelectComponent {
  @Input() value!: string;
  @Input() type!: 'primary' | 'secondary';
  @Input() placeholder!: string;
  @Input() options!: SelectOption[];
  @Input() description!: string;
  @Output() valueChange = new EventEmitter<string>();
  onValueChange = (event: Event): void => {
    this.value = (event.target as HTMLInputElement).value;
    this.valueChange.emit(this.value);
  }
}
