import {Component, EventEmitter, Input, Output} from '@angular/core';
import {JsonPipe, NgForOf, NgIf} from "@angular/common";
import {SelectOption} from "../../../types";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-select',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    JsonPipe,
    FormsModule
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
    let newOption = this.options.find(option => option.text === (event.target as HTMLSelectElement).value)!;
    this.value = newOption.text;
    this.valueChange.emit(this.value);
  }
}
