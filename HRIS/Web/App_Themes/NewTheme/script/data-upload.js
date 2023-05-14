
setTimeout(() => {
let fileUploadElment = document.getElementsByClassName("ruFileWrap")[0];

    fileUploadElment.classList.add('hide-file-input','d-flex','flex-column');

    fileUploadElment.parentElement.classList.add('d-flex', 'justify-content-center');
    fileUploadElment.style.width = "min-content";
    fileUploadElment.style.opacity = "1";

    let imageElment = document.createElement("img");
    imageElment.setAttribute("src", "/App_Themes/NewTheme/img/upload.png");
    imageElment.classList.add("upload-img");
    fileUploadElment.prepend(imageElment)

    let fakeInputElement = document.getElementsByClassName("ruFakeInput")[0];
    fakeInputElement.classList.add("form-control");

    let uploadButtonElement = document.getElementsByClassName("rbDecorated")[0];
    uploadButtonElement.classList.add('btn', 'btn-light');
    uploadButtonElement.style.opacity = "1";

    fileUploadElment.lastChild.style.display = "none";
}, 1000);
