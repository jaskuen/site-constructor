import {Component, Input, OnInit} from '@angular/core';
import {ColorScheme, Image} from "../../../types";
import {NgStyle} from "@angular/common";

@Component({
  selector: 'app-site-preview',
  standalone: true,
  imports: [
    NgStyle
  ],
  templateUrl: './site-preview.component.html',
  styleUrl: './site-preview.component.scss'
})
export class SitePreviewComponent {
  @Input() colors!: ColorScheme;
  @Input() logoSrc!: Image[];
  @Input() removeLogoBackground!: boolean;
  @Input() logoBackgroundColor!: string;
}
