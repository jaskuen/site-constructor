import {Component, EventEmitter, Input, Output} from '@angular/core';
import {SelectComponent} from "../../../components/select/select.component";
import {ColorScheme, ColorSchemeName, DesignPageData, SelectOption} from "../../../../types";
import {ButtonComponent} from "../../../components/button/button.component";
import {ColorPickerComponent} from "../../../components/color-picker/color-picker.component";
import { Colors } from '../../../../colors';
import {AlertComponent} from "../../../components/alert/alert.component";
import {ImageLoaderComponent} from "../../../components/image-loader/image-loader.component";
import {CheckboxComponent} from "../../../components/checkbox/checkbox.component";
import {ColorPickerModule} from "ngx-color-picker";
import {ColorSchemes} from "../../../../colorSchemes";

@Component({
  selector: 'app-design',
  standalone: true,
  imports: [
    SelectComponent,
    ButtonComponent,
    ColorPickerComponent,
    AlertComponent,
    ImageLoaderComponent,
    CheckboxComponent,
    ColorPickerModule
  ],
  templateUrl: './design.component.html',
  styleUrls: ['./design.component.scss', '../../../../colors.scss']
})
export class DesignComponent {
  @Input() pageData!: DesignPageData;
  @Output() pageDataChange: EventEmitter<DesignPageData> = new EventEmitter()

  selectFontOptions: SelectOption[] = [{
    text: 'Franklin Gothic Demi',
  }, {
    text: 'Open Sans',
  }, {
    text: 'Roboto',
  }, {
    text: 'Ariel',
  }]

  onDataChange() {
    this.pageDataChange.emit(this.pageData)
  }
  protected readonly Colors = Colors;
  protected readonly ColorSchemes = ColorSchemes;
}
