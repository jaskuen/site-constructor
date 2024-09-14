import {Component, Input} from '@angular/core';
import {SiteCreatorComponent} from "./site-creator/site-creator.component";
import {TitleComponent} from "./title/title.component";
import {HeaderComponent} from "./header/header.component";
import {FooterComponent} from "./footer/footer.component";
import {DataService} from "./api/data.service";
import {ColorScheme, ColorSchemeName, ContentPageData, DesignPageData, SiteConstructorData} from "../../../types";
import {ColorSchemes} from "../../../colorSchemes";
import {English, German, Italian, Russian} from "../../../languages";
import {HttpClientModule} from "@angular/common/http";
import {map} from "rxjs";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    SiteCreatorComponent,
    TitleComponent,
    HeaderComponent,
    FooterComponent,
    HttpClientModule,
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
  handleClick = async () => {
    const data: SiteConstructorData = {
      userId: localStorage.getItem("userId")!,
      ...this.contentPageData,
      ...this.designPageData,
    }
    this.dataService.postData(data)
      .pipe(map(response => {
          return response;
        }),
      )
      .subscribe({
        next: (response) => {
          console.log("Data successfully posted", response)
          this.dataService.downloadSite(localStorage.getItem("userId")!)
            .pipe(map(response => {
              return response;
            }),
            )
            .subscribe({
              next: (response) => {
                console.log('Download site')
                const url = window.URL.createObjectURL(response)
                const a = document.createElement("a")
                a.href = url
                a.download = ''
                document.body.appendChild(a);
                a.click();
                setTimeout(() => {
                  a.remove()
                  window.URL.revokeObjectURL(url)
                }, 100)
              },
              error: (error) => {
                console.log("Error downloading site", error)
              }
            })
        },
        error: (error) => {
          console.log("Error posting data", error)
        }
      })
  }
}
