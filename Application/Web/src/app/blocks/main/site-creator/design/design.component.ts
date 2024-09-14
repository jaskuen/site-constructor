import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {SelectComponent} from "../../../../components/select/select.component";
import {
  ColorScheme,
  ColorSchemeName,
  ContentPageData,
  DesignPageData, FontType, Image,
  SelectOption,
  SiteConstructorData
} from "../../../../../types";
import {ButtonComponent} from "../../../../components/button/button.component";
import {ColorPickerComponent} from "../../../../components/color-picker/color-picker.component";
import { Colors } from '../../../../../colors';
import {AlertComponent} from "../../../../components/alert/alert.component";
import {ImageLoaderComponent} from "../../../../components/image-loader/image-loader.component";
import {CheckboxComponent} from "../../../../components/checkbox/checkbox.component";
import {ColorPickerModule} from "ngx-color-picker";
import {ColorSchemes} from "../../../../../colorSchemes";
import {SitePreviewComponent} from "../../../../components/site-preview/site-preview.component";

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
    ColorPickerModule,
    SitePreviewComponent,
  ],
  templateUrl: './design.component.html',
  styleUrls: ['./design.component.scss', '../../../../../colors.scss']
})

export class DesignComponent implements OnInit{
  @Input() pageData!: DesignPageData;
  @Input() contentPageData!: ContentPageData;
  @Input() sitePreviewData!: ColorScheme;
  @Output() pageDataChange: EventEmitter<DesignPageData> = new EventEmitter()

  selectFontOptions: SelectOption[] = [{
    text: 'Franklin Gothic Medium',
  }, {
    text: 'Open Sans',
  }, {
    text: 'Roboto',
  }, {
    text: 'Arial',
  }]

  ngOnInit() {
    this.sitePreviewData = {
      backgroundColors: this.pageData.backgroundColors,
      textColors: this.pageData.textColors,
    }
    this.pageData.logoBackgroundColor = Colors.LIGHT;
  }

  onDataChange() {
    this.sitePreviewData = {
      ...this.sitePreviewData,
      ...this.pageData,
    }
    this.pageDataChange.emit(this.pageData)
  }
  protected readonly Colors = Colors;
  protected readonly ColorSchemes = ColorSchemes;
}
