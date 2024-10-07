import {HttpStatusCode} from "@angular/common/http";

type SelectOption = {
  colorScheme?: ColorScheme,
  iconColor?: string,
  text: ColorSchemeName | FontType | string,
  selected?: boolean,
  disabled?: boolean,
}

type FontType = "Franklin Gothic Medium" | "Open Sans" | "Roboto" | "Arial" // enum
type LanguageCode = "ru-RU" | "en-US" | "de-DE" | "it-IT"
type PopupType = "none" | "success" | "error"

type LanguageType = {
  code: LanguageCode,
  name: string,
}
type SelectLanguageType = LanguageType & {
  selected: boolean,
}
type ColorSchemeName = "Оранжевая" | "Пользовательская"
type ColorScheme = {
  backgroundColors: {
    main: string,
    additional: string,
    translucent: string,
    navigation: string,
  },
  textColors: {
    main: string,
    additional: string,
    translucent: string,
    accent: string,
  },
}

type Entity = {
  id: number,
}

type Image = {
  type: string,
  imageFileBase64String: string,
}

type DesignPageData = ColorScheme & {
  colorSchemeName: ColorSchemeName,
  headersFont: FontType,
  mainTextFont: FontType,
  logoSrc: Image[],
  logoBackgroundColor: string,
  removeLogoBackground: boolean,
  faviconSrc: Image[],
}

type ContentPageData = {
  languages: SelectLanguageType[],
  mainLanguage: LanguageType,
  header: string,
  description: string,
  vkLink: string,
  telegramLink: string,
  youtubeLink: string,
  photosSrc: Image[],
}

type UserSiteData = ColorScheme & {
  colorSchemeName: ColorSchemeName,
  headersFont: FontType,
  mainTextFont: FontType,
  logoBackgroundColor: string,
  removeLogoBackground: boolean,
  // languages: SelectLanguageType[],
  // mainLanguage: LanguageType,
  header: string,
  description: string,
  vkLink: string,
  telegramLink: string,
  youtubeLink: string,
  images: Image[],
  userId: number,
}

type SiteConstructorData = {
  designPageData: DesignPageData,
  contentPageData: ContentPageData,
  userId: number,
}

export type {
  SelectLanguageType,
  SelectOption,
  LanguageType,
  PopupType,
  DesignPageData,
  ContentPageData,
  SiteConstructorData,
  ColorSchemeName,
  ColorScheme,
  Image,
  FontType,
  UserSiteData,
}
