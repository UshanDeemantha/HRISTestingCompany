function pageLoad(event) {
    doWizardFuctions();
}
function doWizardFuctions() {
    addStyleToSelectedItem();//wizard style
}
function addStyleToSelectedItem() {
    var element = document.getElementsByClassName("selected-item");
    if (element.length!=0) {
        element[0].parentElement.classList.add("selected-tr");
    }
}