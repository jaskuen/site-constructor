import {ColorScheme, ColorSchemeName, ContentPageData, DesignPageData, Image, UserSiteData} from "../../../types";
import {ColorSchemes} from "../../../colorSchemes";
import {English, German, Italian, Russian} from "../../../languages";

type ParsedLoadedData = {
  contentPageData: ContentPageData;
  designPageData: DesignPageData;
}

const parseLoadedData = (data: UserSiteData): ParsedLoadedData => {
  let currentColorScheme: ColorScheme = ColorSchemes[0].colorScheme!;
  let designPageData: DesignPageData = {
    colorSchemeName: (ColorSchemes[0].text) as ColorSchemeName,
    ...currentColorScheme,
    headersFont: 'Franklin Gothic Medium',
    mainTextFont: 'Franklin Gothic Medium',
    logoSrc: [],
    logoBackgroundColor: '',
    removeLogoBackground: false,
    faviconSrc: [],
  };
  let contentPageData: ContentPageData = {
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
  };
  let logo = data.images ? data.images.filter((img: Image) => img.type === "logo") : []
  let favicon = data.images ? data.images.filter((img: Image) => img.type === "favicon") : []
  let images = data.images ? data.images.filter((img: Image) => img.type === "main") : []
  if (data) {
    designPageData.colorSchemeName = data.colorSchemeName ? data.colorSchemeName : "Оранжевая";
    designPageData.backgroundColors = data.backgroundColors ? data.backgroundColors : ColorSchemes[0].colorScheme!.backgroundColors;
    designPageData.textColors = data.textColors ? data.textColors : ColorSchemes[0].colorScheme!.textColors;
    designPageData.headersFont = data.headersFont ? data.headersFont : "Open Sans";
    designPageData.mainTextFont = data.mainTextFont ? data.mainTextFont : "Open Sans";
    designPageData.logoBackgroundColor = data.logoBackgroundColor ? data.logoBackgroundColor : "";
    designPageData.removeLogoBackground = data.removeLogoBackground ? data.removeLogoBackground : false;
    designPageData.logoSrc = logo.length > 0 ? [{
      type: "logo",
      imageFileBase64String: logo[0].imageFileBase64String
    }] : [];
    designPageData.faviconSrc = favicon.length > 0 ? [{
      type: "logo",
      imageFileBase64String: favicon[0].imageFileBase64String
    }] : [];
    contentPageData.header = data.header ? data.header : "";
    contentPageData.description = data.description ? data.description : "";
    contentPageData.vkLink = data.vkLink ? data.vkLink : "";
    contentPageData.telegramLink = data.telegramLink ? data.telegramLink : "";
    contentPageData.youtubeLink = data.youtubeLink ? data.youtubeLink : "";
    contentPageData.photosSrc = images.length > 0 ? images.map((img: Image) => {
      return {
        type: "main",
        imageFileBase64String: img.imageFileBase64String,
      }
    }) : [];
  }
  return {
    designPageData: designPageData,
    contentPageData: contentPageData,
  }
}

export {
  parseLoadedData,
}
