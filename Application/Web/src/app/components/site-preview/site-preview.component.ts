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
export class SitePreviewComponent implements OnInit {
  @Input() colors!: ColorScheme;
  @Input() logoSrc!: Image[];
  @Input() imagesSrc!: Image[];
  @Input() removeLogoBackground!: boolean;
  @Input() logoBackgroundColor!: string;
  @Input() mainTextFont!: string;
  @Input() headersFont!: string;

  current: number = 0;

  changePhoto(delta: number) {
    console.log('click')
    if (this.current + delta >= 0 && this.current + delta < this.imagesSrc.length) {
      this.current += delta
    }
  }

  ngOnInit() {
    if (this.imagesSrc.length > 1) {
      document.getElementById('arrowLeft')!.addEventListener('click', () => this.changePhoto(-1))
      document.getElementById('arrowRight')!.addEventListener('click', () => this.changePhoto(1))
    } else {
      document.getElementById('arrowLeft')!.style.display = 'none'
      document.getElementById('arrowRight')!.style.display = 'none'
    }
  }

}
