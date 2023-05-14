function logout() {
    sessionStorage.clear();
}
function setInitialSession(index, firstHeader, secondHeader) {
    sessionStorage.setItem("indexes", JSON.stringify(index));
    sessionStorage.setItem("firstHeader", firstHeader);
    sessionStorage.setItem("secondHeader", secondHeader);
    sessionStorage.setItem("isShowSubMenu", "false")
}