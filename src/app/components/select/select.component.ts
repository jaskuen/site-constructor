import {Component, Input} from '@angular/core';
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
  @Input() type!: 'select' | 'color';
  @Input() placeholder!: string;
  @Input() options!: SelectOption[];
}
