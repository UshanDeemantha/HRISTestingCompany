function viewOrHideDetail(element) {
    element.classList.toggle("view-more");
    element.classList.toggle("view-less");
    element.firstElementChild.classList.toggle("fa-arrow-right");
    element.firstElementChild.classList.toggle("fa-arrow-left");

    element.parentElement.parentElement.classList.toggle("employee-container-large");

    toggleContainer();
}
function toggleContainer() {
    var largeElements = document.getElementsByClassName("employee-container-large")
    var listContainerElement = document.getElementsByClassName('employee-list-container')[0];

    if (largeElements.length == 1) {
        listContainerElement.classList.add("flex-container");
        listContainerElement.classList.remove("grid-container");
    }
    if (largeElements.length == 0) {
        setTimeout(() => {
        listContainerElement.classList.remove("flex-container");
        listContainerElement.classList.add("grid-container");
        }, 1000)
    }
}
setTimeout(() => {
    document.getElementsByClassName("rdpPagerButton")[0].classList.add('btn', 'btn-sm', 'btn-go');
    let paginatorElement = document.getElementsByClassName("employee-list-paginator")[0];
    paginatorElement.children[2].classList.add('justify-content-end', 'paginator-arrow', 'paginator-arrow-next','text-muted');
    paginatorElement.children[0].classList.add('paginator-arrow','paginator-arrow-prev','text-muted');
}, 0);
