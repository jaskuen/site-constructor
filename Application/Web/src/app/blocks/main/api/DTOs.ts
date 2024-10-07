import {SiteConstructorData, UserSiteData} from "../../../../types";

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
  userSiteData: UserSiteData,
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
