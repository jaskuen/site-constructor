import { Component } from '@angular/core';
import {SelectComponent} from "../../../components/select/select.component";
import {SelectOption} from "../../../../types";
import {ButtonComponent} from "../../../components/button/button.component";
import {ColorPickerComponent} from "../../../components/color-picker/color-picker.component";
import { Colors } from '../../../../colors';
import {AlertComponent} from "../../../components/alert/alert.component";
import {ImageLoaderComponent} from "../../../components/image-loader/image-loader.component";
import {CheckboxComponent} from "../../../components/checkbox/checkbox.component";

@Component({
  selector: 'app-design',
  standalone: true,
  imports: [
    SelectComponent,
    ButtonComponent,
    ColorPickerComponent,
    AlertComponent,
    ImageLoaderComponent,
    CheckboxComponent
  ],
  templateUrl: './design.component.html',
  styleUrls: ['./design.component.scss', '../../../../colors.scss']
})
export class DesignComponent {
  selectThemeOptions: SelectOption[] = [{
    iconColor: '$ACCENT12',
    text: 'Оранжевый',
  }, {
    iconColor: '$ACCENT11',
    text: 'Кастомный',
  }
  ];
  selectFontOptions: SelectOption[] = [{
    text: 'Franklin Gothic Demi',
  }, {
    text: 'Open Sans',
  }, {
    text: 'Roboto',
  }, {
    text: 'Ariel',
  },];
  protected readonly Colors = Colors;
}
