import {Component, EventEmitter, Input, OnChanges, Output, SimpleChanges} from '@angular/core';
import {TextInputComponent} from "../text-input/text-input.component";
import {ButtonComponent} from "../button/button.component";
import {NgIf, NgOptimizedImage} from "@angular/common";
import {FormControl, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {DownloadSiteRequest, HostSiteRequest} from "../../../types";
import {saveAs} from "file-saver";
import {popup} from "../popup";

@Component({
  selector: 'app-popover',
  standalone: true,
  imports: [
    TextInputComponent,
    ButtonComponent,
    NgIf,
    FormsModule,
    ReactiveFormsModule,
    NgOptimizedImage
  ],
  templateUrl: './popover.component.html',
  styleUrl: './popover.component.scss'
})
export class PopoverComponent {
  @Input() generateSite(downloadSiteRequest: DownloadSiteRequest) {};
  @Input() hostSite(hostSiteRequest: HostSiteRequest) {};
  @Input() siteName!: string;
  @Input() isOpened: boolean = false;
  @Output() isOpenedChange = new EventEmitter<boolean>();
  @Input() siteDownloadUrl: string = "";
  @Input() siteLoading: boolean = false;
  @Input() download!: boolean;

  fileNameControl = new FormControl(this.siteName, [
    Validators.required, Validators.pattern(/^[а-яА-ЯёЁa-zA-Z0-9_-]+$/),
  ]);
  onInput(event: any) {
    const input = event.target.value;
    const allowedChars = /^[а-яА-ЯёЁa-zA-Z0-9_-]+$/;
    if (!allowedChars.test(input)) {
      event.target.value = input.replace(/[^а-яА-ЯёЁa-zA-Z0-9_-]/g, '');
    }
    this.siteName = event.target.value;
  }
  closePopover = () => {
    this.isOpened = false;
    this.isOpenedChange.emit(false);
  }
  handleSaveClick = () => {
    if (this.siteName === "") {
      popup("Введите название для архива", "none")
    } else {
      const downloadSiteRequest: DownloadSiteRequest = {
        userId: localStorage.getItem("userId")!,
        fileName: this.siteName,
      }
      this.generateSite(downloadSiteRequest);
    }
  }
  handleDownloadClick = () => {
    saveAs(this.siteDownloadUrl, this.siteName);
    window.URL.revokeObjectURL(this.siteDownloadUrl);
    this.siteDownloadUrl = ""
    this.siteName = ""
    this.closePopover()
  };
  handleHostClick = () => {

  }
}
