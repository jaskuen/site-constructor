import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges
} from '@angular/core';
import {TextInputComponent} from "../text-input/text-input.component";
import {ButtonComponent} from "../button/button.component";
import {NgIf, NgOptimizedImage} from "@angular/common";
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
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
    ReactiveFormsModule,
    NgOptimizedImage
  ],
  templateUrl: './popover.component.html',
  styleUrl: './popover.component.scss'
})
export class PopoverComponent implements AfterViewInit {
  @Input() generateSite(downloadSiteRequest: DownloadSiteRequest) {};
  @Input() hostSite(hostSiteRequest: HostSiteRequest) {};
  @Input() isOpened!: boolean;
  @Output() isOpenedChange = new EventEmitter<boolean>();
  @Input() siteDownloadUrl: string = "";
  @Input() siteLoading: boolean = false;
  @Input() download!: boolean;
  isSiteHostNameAvailable: boolean = true;
  constructor(private dataService: DataService, private cdr: ChangeDetectorRef) {
  }
  public fileNameForm = new FormGroup({
    fileName: new FormControl("", [
      Validators.required, Validators.pattern(/^[а-яА-ЯёЁa-zA-Z0-9_-]+$/),
    ])
  });

  public siteNameForm = new FormGroup({
    siteName: new FormControl("", [
      Validators.required, Validators.pattern(/^[a-zA-Z0-9_-]+$/),
    ])
  });
  onInput(event: any, formControlName: string) {
    const input: string = event.target.value;
    const allowedChars = /^[а-яА-ЯёЁa-zA-Z0-9_-]+$/;
    if (!allowedChars.test(input)) {
      event.target.value = input.replace(/[^а-яА-ЯёЁa-zA-Z0-9_-]/g, '');
    }
    formControlName == "siteName"
      ? this.siteNameForm.controls.siteName.setValue(event.target.value)
      : this.fileNameForm.controls.fileName.setValue(event.target.value)
  }

  ngAfterViewInit() {
    this.siteNameForm.controls["siteName"].valueChanges
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
    this.fileNameForm.controls.fileName.setValue("")
    this.siteNameForm.controls.siteName.setValue("")
    this.isOpened = false;
    this.isOpenedChange.emit(false);
  }
  handleSaveClick = () => {
    if (this.fileNameForm.controls["fileName"].value === "") {
      popup("Введите название для архива", "none")
    } else {
      const downloadSiteRequest: DownloadSiteRequest = {
        userId: localStorage.getItem("userId")!,
        fileName: this.fileNameForm.controls["fileName"].value!,
      }
      this.generateSite(downloadSiteRequest);
    }
  }
  handleDownloadClick = () => {
    saveAs(this.siteDownloadUrl, this.fileNameForm.controls["fileName"].value!);
    window.URL.revokeObjectURL(this.siteDownloadUrl);
    this.siteDownloadUrl = ""
    this.fileNameForm.controls["fileName"].setValue("")
    this.closePopover()
  };
  handleHostClick = () => {
    if (this.siteNameForm.controls["siteName"].value === "") {
      popup("Введите название для сайта", "none")
    } else {
      const hostSiteRequest: HostSiteRequest = {
        userId: Number(localStorage.getItem("userId")!),
        name: this.siteNameForm.controls["siteName"].value!,
      }
      this.hostSite(hostSiteRequest);
    }
  }
}
