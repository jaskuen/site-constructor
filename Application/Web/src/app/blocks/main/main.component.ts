import {
  AfterViewChecked,
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  DoCheck,
  EventEmitter,
  Input,
  OnInit,
  Output
} from '@angular/core';
import {SiteCreatorComponent} from "./site-creator/site-creator.component";
import {TitleComponent} from "./title/title.component";
import {HeaderComponent} from "./header/header.component";
import {FooterComponent} from "./footer/footer.component";
import {DataService} from "./api/data.service";
import {
  ColorScheme,
  ColorSchemeName,
  ContentPageData,
  DesignPageData, Image,
  SiteConstructorData
} from "../../../types";
import {ColorSchemes} from "../../../colorSchemes";
import {English, German, Italian, Russian} from "../../../languages";
import {HttpClientModule} from "@angular/common/http";
import {map} from "rxjs";
import {PopoverComponent} from "../../components/popover/popover.component";
import {popup} from "../../components/popup";
import {Colors} from "../../../colors";
import {DownloadSiteRequest, GetSavedUserSiteDataRequest, HostSiteRequest} from "./api/DTOs";
import {parseLoadedData} from "./parseLoadedData";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    SiteCreatorComponent,
    TitleComponent,
    HeaderComponent,
    FooterComponent,
    HttpClientModule,
    PopoverComponent,
    NgIf,
  ],
  providers: [
    DataService,
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements AfterViewInit {
  constructor(private dataService: DataService, private cdr: ChangeDetectorRef) {}
  currentColorScheme: ColorScheme = ColorSchemes[0].colorScheme!;
  @Input() designPageData!: DesignPageData;
  @Input() contentPageData!: ContentPageData;
  @Input() isPopoverOpened = false
  @Input() siteDownloadUrl: string = "";
  @Input() siteLoading: boolean = false;
  @Input() toDownload!: boolean;

  ngAfterViewInit() {
    let userId = Number(localStorage.getItem("userId")!);
    let getSavedUserDataRequest: GetSavedUserSiteDataRequest = {
      userId: userId,
    }
    this.dataService.getSavedUserData(getSavedUserDataRequest)
      .pipe(map(response => {
        return response
      }),
      )
      .subscribe({
        next: (response) => {
          const parsedData = parseLoadedData(response.data.siteData)
          this.designPageData = {...parsedData.designPageData};
          this.contentPageData = {...parsedData.contentPageData};
          this.cdr.detectChanges()
          popup("Данные загружены", "success")
        },
        error: (error) => {
          console.log("Error loading data", error)
          popup(error.error.error.reason, "error")
        }
        }
      )
  }

  handleClick = () => {
    if (this.contentPageData && this.contentPageData.header && this.contentPageData.header.trim() == "") {
      popup("Введите заголовок сайта", "none")
    } else {
      this.isPopoverOpened = true;
    }
  }
  hostSite = async (hostSiteData: HostSiteRequest) => {
    this.siteLoading = true;
    const data: SiteConstructorData = {
      userId: Number(localStorage.getItem("userId")!),
      contentPageData: this.contentPageData,
      designPageData: this.designPageData,
    }
    this.dataService.postData({siteData: data})
      .pipe(map(response => {
          return response;
        }),
      )
      .subscribe({
        next: (response) => {
          this.dataService.hostSite(hostSiteData)
            .pipe(map(response => {
                return response;
              }),
            )
            .subscribe({
              next: (response) => {
                this.siteLoading = false;
                popup("Сайт был успешно собран!", "success")
                console.log(response)
              },
              error: (error) => {
                console.log("Ошибка сборки сайта: ", error)
                popup(error.error.error.reason, "error")
              }
            })
        },
        error: (error) => {
          console.log("Error posting data", error)
          popup(error.error.error.reason, "error")
        }
      })
  }
  generateSite = async (downloadSiteRequest: DownloadSiteRequest) => {
    this.siteLoading = true;
    const data: SiteConstructorData = {
      userId: Number(localStorage.getItem("userId")!),
      contentPageData: this.contentPageData,
      designPageData: this.designPageData,
    }
    this.dataService.postData({siteData: data})
      .pipe(map(response => {
          return response;
        }),
      )
      .subscribe({
        next: (response) => {
          this.dataService.downloadSite(downloadSiteRequest)
            .pipe(map(response => {
                return response;
            }),
            )
            .subscribe({
              next: (response) => {
                this.siteLoading = false;
                this.siteDownloadUrl = window.URL.createObjectURL(response);
              },
              error: (error) => {
                console.log("Error downloading site", error)
                popup(error.error.error.reason, "error")
              }
            })
        },
        error: (error) => {
          console.log("Error posting data", error)
          popup(error.error.error.reason, "error")
        }
      })
  }
}
