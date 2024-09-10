type SelectOption = {
  colorScheme?: ColorScheme,
  iconColor?: string,
  text: ColorSchemeName | FontType | string,
  selected?: boolean,
  disabled?: boolean,
}

type FontType = "Franklin Gothic Medium" | "Open Sans" | "Roboto" | "Arial" // enum
type LanguageCode = "ru-RU" | "en-US" | "de-DE" | "it-IT"
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

type Image = {
  id: string,
  userId: string,
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

type SiteConstructorData = DesignPageData & ContentPageData

type AuthData = {
  login: string,
  password: string,
}

export type {
  SelectLanguageType,
  SelectOption,
  LanguageType,
  DesignPageData,
  ContentPageData,
  SiteConstructorData,
  ColorSchemeName,
  ColorScheme,
  AuthData,
  Image,
  FontType,
}
