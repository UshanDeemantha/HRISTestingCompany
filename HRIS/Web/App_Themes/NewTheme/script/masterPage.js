function toggleDropDownMenu() {
  var element = document.getElementById("drop-down");
  var isHideAvailable = element.classList.contains("drop-down-menu-hide");

  if (isHideAvailable) {
    element.classList.remove("drop-down-menu-hide");
    element.classList.add("drop-down-menu-show");
  } else {
    element.classList.add("drop-down-menu-hide");
    element.classList.remove("drop-down-menu-show");
  }
}

function toggleMenu() {
   
  var slideMenuContainerElement = document.getElementById(
    "slide-menu-container"
  );
  var slideMenuElement = document.getElementById("slide-menu");
  var isMenuHide = slideMenuContainerElement.classList.contains(
    "slide-menu-hide"
  );
  var indexes = JSON.parse(sessionStorage.getItem("indexes"));
  var menuItemsElement = document.getElementsByClassName("rpRootGroup");
  var parent = menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[0].firstChild;

  if (isMenuHide) {
    slideMenuContainerElement.classList.add("slide-menu-show");
    slideMenuContainerElement.classList.remove("slide-menu-hide");

    slideMenuElement.classList.add("slide-bar-show");
      slideMenuElement.classList.remove("slide-bar-hide");

      document.getElementById("companyName").style.display = "block";

      //Remove highlighted bar
      parent.style.position = "static";
      parent.childNodes[1].remove();
  } else {
      setTimeout(() => {
          slideMenuContainerElement.classList.add("slide-menu-hide");
          slideMenuContainerElement.classList.remove("slide-menu-show");
      }, 500);

        slideMenuElement.classList.add("slide-bar-hide");
        slideMenuElement.classList.remove("slide-bar-show");

      //Below code part will show the menu icons only when menu close
      menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[1].style.display = "none";
      document.getElementById("companyName").style.display = "none";

      //Add highlighted bar
      setTimeout(() => {
          var barElement = document.createElement("div");
          barElement.classList.add("highlight-bar");

          parent.style.position = "relative";
          parent.insertBefore(barElement, parent.children[1]);
      }, 500);
    }

    sessionStorage.setItem("isShowSubMenu","false");
}

function toggleLogo() {
    var headElement = document.querySelector(".head");
    var image = document.createElement("IMG");

    if (headElement.classList.contains("head-large")) {
        image.setAttribute("src", "/App_Themes/NewTheme/img/menu/small-logo.ico");
        image.setAttribute("width", "45px");

        headElement.firstElementChild.firstElementChild.remove();
        headElement.firstElementChild.append(image);
    } else {
        image.setAttribute("src", "/App_Themes/NewTheme/img/menu/logo.png");
        //image.setAttribute("margin-right,15px")

        headElement.firstElementChild.firstElementChild.remove();
        headElement.firstElementChild.append(image);
    }

    headElement.classList.toggle("head-large");
    headElement.classList.toggle("head-small");
}

function OnClientItemLoad(sender, args) {
    
    var indexes = JSON.parse(sessionStorage.getItem("indexes"))
   
    var menuItemsElement = document.getElementsByClassName("rpRootGroup");
    
    if (indexes && indexes.length==2) {
        var menuItemsElement = document.getElementsByClassName("rpRootGroup");

        menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[1].style.display = "block";
        menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[1].childNodes[1].childNodes[parseInt(indexes[1]) + 1].classList.add("selected-li");
        menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[1].childNodes[1].childNodes[parseInt(indexes[1]) + 1].childNodes[0].classList.add("rpSelected");

        var node = document.createElement("div");
        var selectedLi = document.getElementsByClassName("selected-li");
        selectedLi[0].appendChild(node);
        selectedLi[0].childNodes[1].classList.add("left-arrow");

    }
        //Add arrow in main menu item
    if (sessionStorage.getItem("isShowSubMenu") == "true") {
        menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[0].classList.add("main-menu-item-selected");
    }

    if (indexes && indexes.length == 1) {
        if (sessionStorage.getItem("isShowSubMenu") == "true") {
            menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[1].style.display = "block";
        } else {
            menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[1].style.display = "none";
        }
    }
    setHeaderValue();
}   
function OnClientItemClicked(sender, args) {
    var menuItemsElement = document.getElementsByClassName("rpRootGroup");
    var selectedItem = JSON.parse(document.getElementById("ctl00_RadPanelBar1_ClientState").value);
    var indexes = selectedItem.selectedItems[0].split(":");
   
    if (indexes.join("") == "00") {
       // alert("hi");
        sessionStorage.clear();
       
    }
    else if (indexes == "0,1")
    {
       
        indexes = ["1", "0"];
        sessionStorage.setItem("indexes", JSON.stringify(indexes));
    }
    else if (indexes == "0,2") {
       
        indexes = ["1", "0"];
        sessionStorage.setItem("indexes", JSON.stringify(indexes));
    }
    else if (indexes == "0,3") {
       
        indexes = ["1", "0"];
        sessionStorage.setItem("indexes", JSON.stringify(indexes));
    }
    else if (indexes == "0,4") {
       
        indexes = ["1", "0"];
        sessionStorage.setItem("indexes", JSON.stringify(indexes));
    }
    else if (indexes == "0")
    {
        
        indexes = ["0"];
        
        sessionStorage.setItem("indexes", JSON.stringify(indexes));

        if (indexes.length == 1) {
            if (sessionStorage.getItem("firstHeader") == "General") {
               
                alert(sessionStorage.getItem("isShowSubMenu"));
                if (sessionStorage.getItem("isShowSubMenu") == "true") {
                  
                    sessionStorage.setItem("isShowSubMenu", "false")
                    
                } else {
                    sessionStorage.setItem("isShowSubMenu", "true")
                }
            }
 }

       
    }
    else {
        sessionStorage.setItem("indexes", JSON.stringify(indexes));
    }

    if (indexes.length == 2) {
        setTimeout(() => {
            menuItemsElement[0].childNodes[parseInt(indexes[0]) + 1].childNodes[1].style.display = "none";
        }, 0);
    } 

    saveHeaderValue(indexes,args);
}   

function saveHeaderValue(indexes,args) {
    if (indexes.length == 1) {
        if (sessionStorage.getItem("firstHeader") == args.get_item().get_text()) {
            toggleMenuMainItemClick();
        }
        if (sessionStorage.getItem("isShowSubMenu") == "false" && sessionStorage.getItem("firstHeader") != args.get_item().get_text()) {
            toggleMenuMainItemClick();
        }

        sessionStorage.setItem("firstHeader", args.get_item().get_text());
        sessionStorage.removeItem("secondHeader");
       
    }
    if (indexes.length == 2) {
        sessionStorage.setItem("secondHeader", args.get_item().get_text());
    }
}
function setHeaderValue() {
    if (!!sessionStorage.getItem("firstHeader")) {
        document.getElementById("firstHeader").innerHTML = sessionStorage.getItem("firstHeader");
    }

    if (!!sessionStorage.getItem("secondHeader")) {
        document.getElementById("secondHeader").innerHTML = "> " + sessionStorage.getItem("secondHeader");
    }
}
function toggleMenuMainItemClick() {
    
    if (sessionStorage.getItem("isShowSubMenu") == "true")
    {
        sessionStorage.setItem("isShowSubMenu", "false")
        
    } else {
        sessionStorage.setItem("isShowSubMenu", "true")
    }
   
}