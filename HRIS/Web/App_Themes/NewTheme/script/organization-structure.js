function addOrgStruStyle() {
    let elements = document.getElementsByClassName("rtUL");
    for (let i = 2; i < elements.length; i++) {
        elements[i].previousSibling.classList.add("tree__item");
        elements[i].classList.add("tree__nested-items");
        let liElements = elements[i].children;
        for (let k = 0; k < liElements.length; k++) {
            liElements[k].classList.add('tree__item','tree__item--nested');
        }
    }
}

function setOrganizationStructure(event) {
    console.log(event);
    console.log("Done");
    setTimeout(() => {
        console.log(document.getElementById("org-stucture"));
        let orgStructureContainerElement = document.getElementById("org-stucture");
        let ulContainerElement = orgStructureContainerElement.childNodes[1].childNodes[1].childNodes[1];

        console.log(ulContainerElement);


    }, 2000);
}