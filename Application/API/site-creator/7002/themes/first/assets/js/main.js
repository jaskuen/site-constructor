const languageChanger = {
    opened: false,
    languages: [],
    value: '',
}

const images = {
    main: [],
    logo: "",
    favicon: "",
    current: 0,
}



let slides = document.querySelectorAll('.slide');
let totalSlides = slides.length;

var currentSlide = 0;

document.addEventListener("DOMContentLoaded", async () => {
  const parsedData = JSON.parse(window.data)
    if (parsedData) {
        languageChanger.languages = parsedData.ContentPageData.Languages.filter(element => {
          return element.Selected
        })
        images.main = parsedData.ContentPageData.PhotosSrc
        if (images.main.length == 0) {
            document.getElementsByClassName('wrapper-main')[0].style.background = parsedData.DesignPageData.BackgroundColors.Additional
        }
        if (parsedData.DesignPageData.LogoSrc.length > 0) {
            images.logo = parsedData.DesignPageData.LogoSrc[0].ImageFileBase64String
        }
        if (parsedData.DesignPageData.RemoveLogoBackground || parsedData.DesignPageData.LogoSrc.length == 0) {
          document.getElementsByClassName('site-logo')[0].style.background = "none"
        }
        languageChanger.value = parsedData.ContentPageData.Languages[0].Code
        if (languageChanger.languages.length <= 1) {
          document.getElementById('language-changer').style.display = 'none'
        }
        if (!parsedData.ContentPageData.VkLink) {
          document.getElementById('link-vk').style.display = 'none'
        }
        if (!parsedData.ContentPageData.YoutubeLink) {
          document.getElementById('link-youtube').style.display = 'none'
        }
        if (!parsedData.ContentPageData.TelegramLink) {
          document.getElementById('link-telegram').style.display = 'none'
        }
    }
    document.getElementById('site-logo').src = images.logo
    document.getElementById('language-change-value').textContent = languageChanger.value
    //document.getElementById('language-changer').addEventListener('click', onLanguageClick)
    //setImage("arrow", 0)
    if (images.main.length > 1) {
        slides = document.querySelectorAll('.slide');
        totalSlides = slides.length;

        document.querySelector('.prev').addEventListener('click', function (event) {
            event.preventDefault(); // �������� ����������� ��������� ������
            currentSlide = (currentSlide > 0) ? currentSlide - 1 : 0;
            console.log(totalSlides)
            showSlide(currentSlide);
        });

        document.querySelector('.next').addEventListener('click', function (event) {
            event.preventDefault(); // �������� ����������� ��������� ������
            currentSlide = (currentSlide < totalSlides - 1) ? currentSlide + 1 : totalSlides - 1;
            console.log(currentSlide)
            showSlide(currentSlide);
        });
        document.getElementById('arrowLeft').addEventListener('click', () => {
            document.querySelector('.prev').click()
        })
        document.getElementById('arrowRight').addEventListener('click', () => {
            document.querySelector('.next').click()
        })
    } else {
        document.getElementById('arrowLeft').style.display = 'none'
        document.getElementById('arrowRight').style.display = 'none'
        document.getElementById('image-list').style.display = 'none'
    }
    createImageList()
})

const showSlide = (index) => {
    const slidesContainer = document.querySelector('.main-image');
    slidesContainer.style.transform = `translateX(-${index * 100}%)`;

    images.current = index;

    const inputs = document.querySelectorAll('input[name="imageSelector"]');
    inputs.forEach((input, idx) => {
        input.checked = (idx === images.current);
    });
};

const onLanguageClick = () => {
    const changer = document.getElementById('language-select-wrapper')
    if (!languageChanger.opened) {
        const select = document.createElement('div')
        select.id = 'language-select'
        select.className = 'language-select'
        changer.appendChild(select)
        for (let i = 0; i < languageChanger.languages.length; i++) {
            if (languageChanger.languages[i].Selected) {
                const selectOption = document.createElement('div');
                selectOption.className = 'language-select-option'
                const p = document.createElement('p')
                p.textContent = languageChanger.languages[i].Name
                selectOption.appendChild(p)
                select.appendChild(selectOption)
                if (i !== languageChanger.languages.length - 1) {
                    const divider = document.createElement('div')
                    divider.className = 'divider'
                    select.appendChild(divider)
                }
                selectOption.onclick = function (event) {
                    event.stopPropagation()
                    changeLanguage(i)
                    document.getElementById('language-change-value').textContent = languageChanger.value
                }
            }
        }
    } else {
        const select = document.getElementById('language-select');
        select.remove()
    }
    languageChanger.opened = !languageChanger.opened;
}

const changeLanguage = (languageId) => {
    languageChanger.value = languageChanger.languages[languageId].Code
    languageChanger.opened = false
    const select = document.getElementById('language-select');
    select.remove()
}

const createImageList = () => {
    const imageList = document.querySelector('.image-list');
    for (let i = 0; i < images.main.length; i++) {
        const imageSelectorSpan = document.createElement('span');
        imageSelectorSpan.className = "checkmark";
        const imageSelectorInput = document.createElement('input');
        imageSelectorInput.type = "radio";
        imageSelectorInput.name = "imageSelector";
        imageSelectorInput.id = `image-selector-${i}`;
        if (i === images.current) {
            imageSelectorInput.checked = true;
        }
        const imageSelectorLabel = document.createElement('label');
        imageSelectorLabel.className = "container";
        imageSelectorLabel.appendChild(imageSelectorInput);
        imageSelectorLabel.appendChild(imageSelectorSpan);
        imageList.appendChild(imageSelectorLabel);
        imageSelectorLabel.addEventListener('click', () => showSlide(i));
    }
};
