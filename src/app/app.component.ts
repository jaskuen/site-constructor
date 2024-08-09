import {Component, importProvidersFrom, Input, Provider} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from "./blocks/header/header.component";
import {TitleComponent} from "./blocks/title/title.component";
import {TabsComponent} from "./components/tabs/tabs.component";
import {SiteCreatorComponent} from "./blocks/site-creator/site-creator.component";
import {FooterComponent} from "./blocks/footer/footer.component";
import {ColorScheme, ColorSchemeName, ContentPageData, DesignPageData, SiteConstructorData} from "../types";
import {ColorSchemes} from "../colorSchemes";
import {English, German, Italian, Russian} from "../languages";
import {DataService} from "./data.service";
import {HttpClientModule} from "@angular/common/http";
import {catchError, map} from "rxjs";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    TitleComponent,
    TabsComponent,
    SiteCreatorComponent,
    FooterComponent,
    HttpClientModule,
  ],
  providers: [
    DataService,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  constructor(private dataService: DataService) {}
  currentColorScheme: ColorScheme = ColorSchemes[0].colorScheme!;
  @Input() designPageData: DesignPageData = {
    colorSchemeName: (ColorSchemes[0].text) as ColorSchemeName,
    ...this.currentColorScheme,
    headersFont: 'Franklin Gothic Demi',
    mainTextFont: 'Franklin Gothic Demi',
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
      ...this.contentPageData,
      ...this.designPageData,
    }
    this.dataService.postData(data)
      .pipe(map(response => {
          return response;
        }),
        catchError(error => {
          console.error('Ошибка:', error);
          throw error;
        })
      )
      .subscribe({
      next: (response) => {
        console.log("Data successfully posted", response)
      },
      error: (error) => {
        console.log("Error posting data", error)
      }
  })
  }
}
