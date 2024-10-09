import {Component, Input} from '@angular/core';
import {ButtonComponent} from "../../../components/button/button.component";
import {AuthService} from "../../auth/api/auth.service";
import {ContentPageData, DesignPageData, Image, UserSiteData} from "../../../../types";
import {DataService} from "../api/data.service";
import {map} from "rxjs";
import {popup} from "../../../components/popup";
import {SaveUserSiteDataRequest} from "../api/DTOs";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [ButtonComponent],
  providers: [AuthService],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Input() contentPageData!: ContentPageData;
  @Input() designPageData!: DesignPageData;
  @Input() disableSaveButton: boolean = false;
  constructor(private authService: AuthService, private dataService: DataService) {
  }
  username = window.localStorage.getItem("username");
  userId = window.localStorage.getItem("userId")!;
  handleSaveClick = () => {
    let images: Array<Image> = [];
    if (this.designPageData && this.designPageData.logoSrc && this.designPageData.logoSrc.length > 0) {
      images = [...images, this.designPageData.logoSrc[0]]
    }
    if (this.designPageData && this.designPageData.faviconSrc && this.designPageData.faviconSrc.length > 0) {
      images = [...images, this.designPageData.faviconSrc[0]]
    }
    if (this.contentPageData && this.contentPageData.photosSrc && this.contentPageData.photosSrc.length > 0) {
      for (let i = 0; i < this.contentPageData.photosSrc.length; i++) {
        images = [...images, this.contentPageData.photosSrc[i]]
      }
    }

    let siteData: UserSiteData = {
      userId: Number(this.userId),
      colorSchemeName: this.designPageData.colorSchemeName,
      backgroundColors: this.designPageData.backgroundColors,
      textColors: this.designPageData.textColors,
      headersFont: this.designPageData.headersFont,
      mainTextFont: this.designPageData.mainTextFont,
      logoBackgroundColor: this.designPageData.logoBackgroundColor,
      removeLogoBackground: this.designPageData.removeLogoBackground,
      header: this.contentPageData.header,
      description: this.contentPageData.description,
      vkLink: this.contentPageData.vkLink,
      telegramLink: this.contentPageData.telegramLink,
      youtubeLink: this.contentPageData.youtubeLink,
      images: images,
    }
    this.disableSaveButton = true
    this.dataService.saveUserData(siteData)
        .pipe(map(response => {
          return response;
        }),
      )
      .subscribe({
        next: (response) => {
          popup("Данные успешно сохранены", "success")
        },
        error: (error) => {
          popup("Ошибка загрузки данных", "error")
        }
      })
    this.disableSaveButton = false
  }
  handleLogoutClick = () => {
    this.authService.logout();
    window.location.reload();
  }
}
