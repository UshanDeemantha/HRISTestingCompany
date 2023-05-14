window.addEventListener('load', (event) => {
    var wizardContainer = document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_SideBarContainer_SideBarList");

    let arrowPrev = document.createElement("div");
    let arrowLast = document.createElement("div");

    arrowPrev.innerHTML = "<div id='wizardLeftArrow' class='arrow'><img style='opacity:0.5' src='/../App_Themes/NewTheme/img/table/left2.png'/></div>";
    arrowLast.innerHTML = "<div id='wizardRightArrow' class='arrow'><img style='opacity:0.5' src='/App_Themes/NewTheme/img/table/right-arrow.png'/></div>";

    wizardContainer.classList.add('wizard-table');
    wizardContainer.prepend(arrowPrev);
    wizardContainer.append(arrowLast);
    let bodyElement = wizardContainer.children[1];


    document.getElementById("wizardLeftArrow").addEventListener("click", (event) => {
        console.log(bodyElement);
        bodyElement.scrollTo({
            left: bodyElement.scrollLeft - 200,
            behavior: 'smooth'
        })
    })

    document.getElementById("wizardRightArrow").addEventListener("click", (event) => {
        console.log(bodyElement);
        bodyElement.scrollTo({
            left: bodyElement.scrollLeft + 200,
            behavior: 'smooth'
        })
    })
})

