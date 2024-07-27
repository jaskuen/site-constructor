import {Component, Input} from '@angular/core';
import {CheckboxComponent} from "../../../components/checkbox/checkbox.component";
import {SelectComponent} from "../../../components/select/select.component";
import {SelectOption} from "../../../../types";
import {NgForOf} from "@angular/common";
import {TextInputComponent} from "../../../components/text-input/text-input.component";
import {ImageLoaderComponent} from "../../../components/image-loader/image-loader.component";

@Component({
  selector: 'app-content',
  standalone: true,
  imports: [
    CheckboxComponent,
    SelectComponent,
    NgForOf,
    TextInputComponent,
    ImageLoaderComponent
  ],
  templateUrl: './content.component.html',
  styleUrl: './content.component.scss'
})
export class ContentComponent {
  @Input() mainLanguage: string = "Русский";
  selectLanguageOptions: SelectOption[] = [{
    text: "Русский",
  }, {
    text: "Английский",
  }, {
    text: "Немецкий",
  }, {
    text: "Итальянский",
  }, ]
  getMainLanguageId(): number {
    return this.selectLanguageOptions.findIndex(option => option.text === this.mainLanguage);
  }
}
