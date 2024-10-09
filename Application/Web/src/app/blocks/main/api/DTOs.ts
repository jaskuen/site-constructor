import {ColorSchemeName, FontType, Image, SiteConstructorData, UserSiteData} from "../../../../types";

type Error = {
  reason: string,
}

type ApiResponse<T> = {
  isSuccess: boolean,
  error?: Error,
  data: T,
}

type DownloadSiteRequest = {
  userId: string,
  fileName: string,
}

type HostSiteRequest = {
  userId: number,
  name: string,
}

type CheckHostNameRequest = {
  siteHostName: string,
}

type CheckHostNameResult = {
  isAvailable: boolean
}

type GetSiteDataRequest = {
  siteData: SiteConstructorData,
}

type SaveUserSiteDataRequest = {
  colorSchemeName: ColorSchemeName,
  headersFont: FontType,
  mainTextFont: FontType,
  logoBackgroundColor: string,
  removeLogoBackground: boolean,
  header: string,
  description: string,
  vkLink: string,
  telegramLink: string,
  youtubeLink: string,
  images: Image[],
  userId: number,
}

type GetSavedUserSiteDataRequest = {
  userId: number,
}

type GetSavedUserSiteDataResponse = {
  siteData: UserSiteData,
}

export type {
  ApiResponse,
  DownloadSiteRequest,
  HostSiteRequest,
  CheckHostNameRequest,
  CheckHostNameResult,
  GetSiteDataRequest,
  SaveUserSiteDataRequest,
  GetSavedUserSiteDataRequest,
  GetSavedUserSiteDataResponse,
}
