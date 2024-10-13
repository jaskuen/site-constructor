import {PopupType} from "../../types";

export const popup = (text: string, popupType: PopupType) => {
  const popupElement = document.createElement("p");
  popupElement.className = `block popup popup-${popupType}`;
  popupElement.textContent = text;
  document.getElementsByTagName("body")[0].appendChild(popupElement);
  setTimeout(() => {
    popupElement.remove()
  }, 2000)
}
