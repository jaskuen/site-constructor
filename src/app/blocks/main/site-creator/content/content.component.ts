import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CheckboxComponent} from "../../../../components/checkbox/checkbox.component";
import {SelectComponent} from "../../../../components/select/select.component";
import {ContentPageData, LanguageType, SelectLanguageType, SelectOption} from "../../../../../types";
import {NgForOf} from "@angular/common";
import {TextInputComponent} from "../../../../components/text-input/text-input.component";
import {ImageLoaderComponent} from "../../../../components/image-loader/image-loader.component";
import {Languages} from "../../../../../languages";

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
  @Input() mainLanguage: string = Languages[0].text
  @Input() pageData!: ContentPageData
  @Output() pageDataChange: EventEmitter<ContentPageData> = new EventEmitter();
  onChange() {
    this.pageDataChange.emit(this.pageData)
  }
  getMainLanguageId(): number {
    return Languages.findIndex(option => option.text === this.pageData.mainLanguage.name);
  }

  protected readonly Languages = Languages.map(language => {
    return {
      ...language,
      selected: false,
    }
  })
}
