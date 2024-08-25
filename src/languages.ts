import {LanguageType, SelectOption} from "./types";

const Russian: LanguageType = {
  code: "ru-RU",
  name: "Русский",
}

const English: LanguageType = {
  code: "en-US",
  name: "Английский",
}

const German: LanguageType = {
  code: "de-DE",
  name: "Немецкий",
}

const Italian: LanguageType = {
  code: "it-IT",
  name: "Итальянский",
}

const Languages: SelectOption[] = [{
  text: Russian.name,
}, {
  text: English.name,
}, {
  text: German.name,
}, {
  text: Italian.name,
}
]

export {
  Russian,
  English,
  German,
  Italian,
  Languages,
}
