import {ColorScheme, SelectOption} from "./types";
import {Colors} from "./colors";

const Orange: ColorScheme = {
  backgroundColors: {
    main: Colors.LIGHT,
    additional: Colors.DARK,
    translucent: Colors.DARK_LIGHT_3,
    navigation: Colors.ACCENT12_LIGHT_2,
  },
  textColors: {
    main: Colors.DARK,
    additional: Colors.LIGHT,
    translucent: Colors.LIGHT,
    accent: Colors.LIGHT,
  },
}

const ColorSchemes: SelectOption[] = [{
  colorScheme: Orange,
  iconColor: '$ACCENT12',
  text: "Оранжевая",
}, {
  text: "Пользовательская"
}
]

export {
  ColorSchemes,
}
