import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [
    NgIf,
    FormsModule
  ],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss'
})
export class TextInputComponent {
  @Input() link: boolean = false;
  @Input() label!: string;
  @Input() value!: string;
  @Output() valueChanged = new EventEmitter<string>();
  @Input() symbolLimit!: number;
  @Input() info: boolean = false;
  onValueChange($event: Event) {
    console.log('aaa');
    this.value = ($event.target as HTMLInputElement).value;
    this.valueChanged.emit(this.value);
  }

  remaining(): number {
    return this.symbolLimit - this.value.length;
  }
}
