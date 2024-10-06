import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {SiteCreatorComponent} from "./site-creator/site-creator.component";
import {TitleComponent} from "./title/title.component";
import {HeaderComponent} from "./header/header.component";
import {FooterComponent} from "./footer/footer.component";
import {DataService} from "./api/data.service";
import {
  ColorScheme,
  ColorSchemeName,
  ContentPageData,
  DesignPageData,
  DownloadSiteRequest, GetSavedUserSiteDataRequest, HostSiteRequest, Image,
  SiteConstructorData
} from "../../../types";
import {ColorSchemes} from "../../../colorSchemes";
import {English, German, Italian, Russian} from "../../../languages";
import {HttpClientModule} from "@angular/common/http";
import {map} from "rxjs";
import {PopoverComponent} from "../../components/popover/popover.component";
import {popup} from "../../components/popup";
import {Colors} from "../../../colors";

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
  ],
  providers: [
    DataService,
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit {
  constructor(private dataService: DataService) {}
  currentColorScheme: ColorScheme = ColorSchemes[0].colorScheme!;
  designPageData!: DesignPageData;
  contentPageData!: ContentPageData;
  @Input() isPopoverOpened = false
  @Input() siteDownloadUrl: string = "";
  @Input() siteLoading: boolean = false;
  @Input() toDownload!: boolean;

  ngOnInit() {
    let userId = Number(localStorage.getItem("userId")!);
    let getSavedUserDataRequest: GetSavedUserSiteDataRequest = {
      userId: userId,
    }
    this.designPageData = {
      colorSchemeName: (ColorSchemes[0].text) as ColorSchemeName,
      ...this.currentColorScheme,
      headersFont: 'Franklin Gothic Medium',
      mainTextFont: 'Franklin Gothic Medium',
      logoSrc: [],
      logoBackgroundColor: '',
      removeLogoBackground: false,
      faviconSrc: [],
    };

    this.contentPageData = {
      languages: [{
        ...Russian,
        selected: false,
      }, {
        ...English,
        selected: false,
      }, {
        ...German,
        selected: false,
      }, {
        ...Italian,
        selected: false,
      },],
        mainLanguage: Russian,
        header: "",
        description: "",
        vkLink: "",
        telegramLink: "",
        youtubeLink: "",
        photosSrc: [],
    }
    this.dataService.getSavedUserData(getSavedUserDataRequest)
      .pipe(map(response => {
        return response
      }),
      )
      .subscribe({
        next: (response) => {
          let data = response.data.siteData
          let logo = data.images ? data.images.filter(img => img.type === "logo") : []
          let favicon = data.images ? data.images.filter(img => img.type === "favicon") : []
          let images = data.images ? data.images.filter(img => img.type === "main") : []
          console.log(data)
          if (data) {
            this.designPageData.colorSchemeName = data.colorSchemeName ? data.colorSchemeName : "Оранжевая";
            this.designPageData.backgroundColors = data.backgroundColors ? data.backgroundColors : ColorSchemes[0].colorScheme!.backgroundColors;
            this.designPageData.textColors = data.textColors ? data.textColors : ColorSchemes[0].colorScheme!.textColors;
            this.designPageData.headersFont = data.headersFont ? data.headersFont : "Open Sans";
            this.designPageData.mainTextFont = data.mainTextFont ? data.mainTextFont : "Open Sans";
            this.designPageData.logoBackgroundColor = data.logoBackgroundColor ? data.logoBackgroundColor : "";
            this.designPageData.removeLogoBackground = data.removeLogoBackground ? data.removeLogoBackground : false;
            this.designPageData.logoSrc = logo.length > 0 ? [{
              type: "logo",
              imageFileBase64String: logo[0].imageFileBase64String
            }] : [];
            this.designPageData.faviconSrc = favicon.length > 0 ? [{
              type: "logo",
              imageFileBase64String: favicon[0].imageFileBase64String
            }] : [];
            this.contentPageData.header = data.header ? data.header : "";
            this.contentPageData.description = data.description ? data.description : "";
            this.contentPageData.vkLink = data.vkLink ? data.vkLink : "";
            this.contentPageData.telegramLink = data.telegramLink ? data.telegramLink : "";
            this.contentPageData.youtubeLink = data.youtubeLink ? data.youtubeLink : "";
            this.contentPageData.photosSrc = images.length > 0 ? images.map(img => {
              return {
                type: "main",
                imageFileBase64String: img.imageFileBase64String,
              }
            }) : [];
          }
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
                this.siteLoading = false;
                return response;
            }),
            )
            .subscribe({
              next: (response) => {
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
