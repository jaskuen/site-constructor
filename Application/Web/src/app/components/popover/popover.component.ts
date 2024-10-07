import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {TextInputComponent} from "../text-input/text-input.component";
import {ButtonComponent} from "../button/button.component";
import {NgIf, NgOptimizedImage} from "@angular/common";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {saveAs} from "file-saver";
import {popup} from "../popup";
import {CheckHostNameRequest, DownloadSiteRequest, HostSiteRequest} from "../../blocks/main/api/DTOs";
import {DataService} from "../../blocks/main/api/data.service";
import {catchError, debounceTime, map, Observable, of, switchMap} from "rxjs";

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
export class PopoverComponent implements OnInit {
  @Input() generateSite(downloadSiteRequest: DownloadSiteRequest) {};
  @Input() hostSite(hostSiteRequest: HostSiteRequest) {};
  @Input() fileName!: string;
  @Input() hostSiteName!: string;
  @Input() isOpened: boolean = false;
  @Output() isOpenedChange = new EventEmitter<boolean>();
  @Input() siteDownloadUrl: string = "";
  @Input() siteLoading: boolean = false;
  @Input() download!: boolean;
  isSiteHostNameAvailable: boolean = true;

  constructor(private dataService: DataService) {
  }
  public fileNameForm = new FormGroup({
    fileName: new FormControl(this.fileName, [
      Validators.required, Validators.pattern(/^[а-яА-ЯёЁa-zA-Z0-9_-]+$/),
    ]),
  })

  public siteNameForm = new FormGroup({
    siteName: new FormControl(this.hostSiteName, [
      Validators.required, Validators.pattern(/^[a-zA-Z0-9_-]+$/),
    ])
  })

  onInput(event: any) {
    const input = event.target.value;
    const allowedChars = /^[а-яА-ЯёЁa-zA-Z0-9_-]+$/;
    if (!allowedChars.test(input)) {
      event.target.value = input.replace(/[^а-яА-ЯёЁa-zA-Z0-9_-]/g, '');
    }
    this.fileNameForm.controls.fileName = event.target.value;
  }
  ngOnInit() {
    this.siteNameForm.controls.siteName.valueChanges
      .pipe(
        debounceTime(100),
        switchMap((value) => {
          let nullObs: Observable<any> = new Observable<any>()
          if (value && value !== "") {
            return this.dataService.isHostNameAvailable({siteHostName: value!})
          }
          return nullObs
        }),
        catchError(() => of(false)) // Обработка ошибок
      )
      .subscribe(response => {
        if (typeof(response) === "object") {
          this.isSiteHostNameAvailable = response.data.isAvailable
        }
      });
  }
  closePopover = () => {
    this.isOpened = false;
    this.isOpenedChange.emit(false);
  }
  handleSaveClick = () => {
    if (this.fileNameForm.controls.fileName.value === "") {
      popup("Введите название для архива", "none")
    } else {
      console.log('aaa')
      const downloadSiteRequest: DownloadSiteRequest = {
        userId: localStorage.getItem("userId")!,
        fileName: this.fileNameForm.controls.fileName.value!,
      }
      this.generateSite(downloadSiteRequest);
    }
  }
  handleDownloadClick = () => {
    saveAs(this.siteDownloadUrl, this.fileNameForm.controls.fileName.value!);
    window.URL.revokeObjectURL(this.siteDownloadUrl);
    this.siteDownloadUrl = ""
    this.fileNameForm.controls.fileName.setValue("")
    this.closePopover()
  };
  handleHostClick = () => {
    if (this.siteNameForm.controls.siteName.value === "") {
      popup("Введите название для сайта", "none")
    } else {
      const hostSiteRequest: HostSiteRequest = {
        userId: Number(localStorage.getItem("userId")!),
        name: this.siteNameForm.controls.siteName.value!,
      }
      this.hostSite(hostSiteRequest);
    }
  }
}
