import {Component, Input} from '@angular/core';
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
  DownloadSiteRequest,
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
export class MainComponent {
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
      userId: localStorage.getItem("userId")!,
      ...this.contentPageData,
      ...this.designPageData,
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
