import {AfterViewInit, Component, Input, OnInit} from '@angular/core';
import {ColorScheme, Image} from "../../../types";
import {NgIf, NgStyle} from "@angular/common";

@Component({
  selector: 'app-site-preview',
  standalone: true,
  imports: [
    NgStyle,
    NgIf
  ],
  templateUrl: './site-preview.component.html',
  styleUrl: './site-preview.component.scss'
})
export class SitePreviewComponent implements AfterViewInit {
  @Input() colors!: ColorScheme;
  @Input() logoSrc!: Image[];
  @Input() imagesSrc!: Image[];
  @Input() removeLogoBackground!: boolean;
  @Input() logoBackgroundColor!: string;
  @Input() mainTextFont!: string;
  @Input() headersFont!: string;

  current: number = 0;

  changePhoto(delta: number) {
    if (this.current + delta >= 0 && this.current + delta < this.imagesSrc.length) {
      this.current += delta
    }
  }

  ngAfterViewInit() {
    const arrowLeft = document.getElementById('arrowLeft')
    const arrowRight = document.getElementById('arrowRight')
    if (arrowLeft && arrowRight) {
      if (this.imagesSrc && this.imagesSrc.length > 1) {
        arrowLeft.addEventListener('click', () => this.changePhoto(-1))
        arrowLeft.addEventListener('click', () => this.changePhoto(1))
      } else {
        arrowLeft.style.display = 'none'
        arrowRight.style.display = 'none'
      }
    }
  }
}
