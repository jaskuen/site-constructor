import {Component, EventEmitter, Input, Output} from '@angular/core';
import {TextInputComponent} from "../text-input/text-input.component";
import {ButtonComponent} from "../button/button.component";
import {NgIf} from "@angular/common";
import {FormControl, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {DownloadSiteRequest} from "../../../types";
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
    ReactiveFormsModule
  ],
  templateUrl: './popover.component.html',
  styleUrl: './popover.component.scss'
})
export class PopoverComponent {
  @Input() generateSite(downloadSiteRequest: DownloadSiteRequest) {};
  @Input() siteName!: string;
  @Input() isOpened: boolean = false;
  @Output() isOpenedChange = new EventEmitter<boolean>();
  @Input() siteDownloadUrl: string = "";

  fileNameControl = new FormControl(this.siteName, [
    Validators.required, Validators.pattern(/^[a-zA-Z0-9_-]+$/),
  ]);
  onInput(event: any) {
    const input = event.target.value;
    const allowedChars = /^[а-яА-ЯёЁa-zA-Z0-9_-]+$/;
    if (!allowedChars.test(input)) {
      event.target.value = input.replace(/[^a-zA-Z0-9_-]/g, '');
    }
    this.siteName = event.target.value;
  }
  closePopover = () => {
    this.isOpened = false;
    this.isOpenedChange.emit(false);
  }
  handleClick = () => {
    if (this.siteName === "") {
      popup("Введите название для архива")
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
}
