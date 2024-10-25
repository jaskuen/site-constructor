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
    console.log('aaa', delta, this.current);
    if (this.current > 0 && delta == -1 || this.current < this.imagesSrc.length - 1 && delta == 1) {
      this.current += delta
    }
  }

  ngAfterViewInit() {
    const arrowLeft = document.getElementById('arrowLeft')
    const arrowRight = document.getElementById('arrowRight')
    if (arrowLeft && arrowRight) {
      if (this.imagesSrc && this.imagesSrc.length > 1) {
        arrowLeft.addEventListener('click', () => this.changePhoto(-1))
        arrowRight.addEventListener('click', () => this.changePhoto(1))
      } else {
        arrowLeft.style.display = 'none'
        arrowRight.style.display = 'none'
      }
    }
  }
}
