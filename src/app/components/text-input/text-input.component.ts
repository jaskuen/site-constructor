import {AfterViewInit, Component, EventEmitter, Input, Output} from '@angular/core';
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {Guid} from "guid-typescript";
import {stringify} from "uuid";

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
export class TextInputComponent implements AfterViewInit {
  @Input() link: boolean = false;
  @Input() label!: string;
  @Input() value: string = "";
  @Output() valueChange = new EventEmitter<string>();
  @Input() symbolLimit!: number;
  @Input() info: boolean = false;
  @Input() login: boolean = false;
  @Input() password: boolean = false;
  @Input() noInsertion: boolean = false;
  id: string = Guid.create().toString()
  ngAfterViewInit() {
    if (this.noInsertion) {
      document.getElementById(this.id)!.addEventListener('paste', (event) => {
        event.preventDefault();
      })
    }
  }
  onValueChange(event: string) {
    this.valueChange.emit(event);
  }

  remaining(): number | undefined {
    if (!this.symbolLimit) {
      return undefined;
    }
    return this.symbolLimit - this.value.length;
  }
}
