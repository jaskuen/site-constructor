import {Component, Input, OnInit} from '@angular/core';
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
  DownloadSiteRequest, GetSavedUserSiteDataRequest, Image,
  SiteConstructorData
} from "../../../types";
import {ColorSchemes} from "../../../colorSchemes";
import {English, German, Italian, Russian} from "../../../languages";
import {HttpClientModule} from "@angular/common/http";
import {map} from "rxjs";
import {PopoverComponent} from "../../components/popover/popover.component";
import {popup} from "../../components/popup";

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
  @Input() designPageData: DesignPageData = {
    colorSchemeName: (ColorSchemes[0].text) as ColorSchemeName,
    ...this.currentColorScheme,
    headersFont: 'Franklin Gothic Medium',
    mainTextFont: 'Franklin Gothic Medium',
    logoSrc: [],
    logoBackgroundColor: '',
    removeLogoBackground: false,
    faviconSrc: [],
  };
  @Input() contentPageData: ContentPageData = {
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
  @Input() isPopoverOpened = false
  @Input() siteDownloadUrl: string = "";
  @Input() siteLoading: boolean = false;

  ngOnInit() {
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
          let data = response.data
          console.log(data)
          if (data) {
            this.designPageData.colorSchemeName = data.colorSchemeName;
            this.designPageData.backgroundColors = data.backgroundColors;
            this.designPageData.textColors = data.textColors;
            this.designPageData.headersFont = data.headersFont;
            this.designPageData.mainTextFont = data.mainTextFont;
            this.designPageData.logoBackgroundColor = data.logoBackgroundColor;
            this.designPageData.removeLogoBackground = data.removeLogoBackground;
            this.designPageData.logoSrc = data.images ? data.images.filter(img => img.type === "logo") : [];
            this.designPageData.faviconSrc = data.images ? data.images.filter(img => img.type === "favicon") : [];
            this.contentPageData.header = data.header;
            this.contentPageData.description = data.description;
            this.contentPageData.vkLink = data.vkLink;
            this.contentPageData.telegramLink = data.telegramLink;
            this.contentPageData.youtubeLink = data.youtubeLink;
            this.contentPageData.photosSrc = data.images ? data.images.filter(img => img.type === "main") : [];
          }

        },
        error: (error) => {
          console.log("Error loading data", error)
        }
        }
      )
  }

  handleClick = () => {
    if (this.contentPageData.header.trim() == "") {
      popup("Введите заголовок сайта")
    } else {
      this.isPopoverOpened = true;
    }
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
                popup(error.error.error.reason)
              }
            })
        },
        error: (error) => {
          console.log("Error posting data", error)
          popup(error.error.error.reason)
        }
      })
  }
}
