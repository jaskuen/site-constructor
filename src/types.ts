type SelectOption = {
  colorScheme?: ColorScheme,
  iconColor?: string,
  text: ColorSchemeName | FontType | string,
  selected?: boolean,
  disabled?: boolean,
}

type FontType = "Franklin Gothic Demi" | "Open Sans" | "Roboto" | "Ariel"
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

type DesignPageData = ColorScheme & {
  colorSchemeName: ColorSchemeName,
  headersFont: FontType,
  mainTextFont: FontType,
  logoSrc: string[],
  logoBackgroundColor: string,
  removeLogoBackground: boolean,
  faviconSrc: string[],
}

type ContentPageData = {
  languages: SelectLanguageType[],
  mainLanguage: LanguageType,
  header: string,
  description: string,
  vkLink: string,
  telegramLink: string,
  youtubeLink: string,
  photosSrc: string[],
}

type SiteConstructorData = DesignPageData & ContentPageData

export type {
  SelectLanguageType,
  SelectOption,
  LanguageType,
  DesignPageData,
  ContentPageData,
  SiteConstructorData,
  ColorSchemeName,
  ColorScheme,
}
