export const popup = (text: string) => {
  const popupElement = document.createElement("p");
  popupElement.className = "block popup";
  popupElement.textContent = text;
  document.getElementsByTagName("body")[0].appendChild(popupElement);
  setTimeout(() => {
    popupElement.remove()
  }, 2000)
}
