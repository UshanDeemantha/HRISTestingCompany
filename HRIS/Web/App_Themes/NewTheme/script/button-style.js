

    function pageLoad() {


    var selectall = document.getElementById("ctl00_ContentPlaceHolder1_radbtnSelectAll_input");
    var percom = document.getElementById("ctl00_ContentPlaceHolder1_ASPxButton1_input");
    var searchcom = document.getElementById("ctl00_ContentPlaceHolder1_btnAspx_input");
    var PayDltbtn = document.getElementById("ContentPlaceHolder1_radbtnDelete");
    var PayUplod = document.getElementById("ContentPlaceHolder1_radbtnUplod");
    var PayAddbtn = document.getElementById("ContentPlaceHolder1_radbtnAdd");
    var timeAddbtn = document.getElementById("ctl00_ContentPlaceHolder1_radbtnAdd_input");
    var tmiecancel2 = document.getElementById("ContentPlaceHolder1_radbtnCancel");
    var timecrddelete = document.getElementById("ContentPlaceHolder1_btnDelete");
    var timeReset = document.getElementById("ContentPlaceHolder1_radbtnReset");
    var timeclear = document.getElementById("ContentPlaceHolder1_radbtnClear");
    var timesave2 = document.getElementById("ContentPlaceHolder1_radbtnSave");
    var comupdate = document.getElementById("ContentPlaceHolder1_btnUpdate");
    var timecancel = document.getElementById("ctl00_ContentPlaceHolder1_radbtnCancel_input"); //ctl00_ContentPlaceHolder1_btnCancel_input
    var timedelete = document.getElementById("ctl00_ContentPlaceHolder1_radbtnDelete_input");
    var timeupdate = document.getElementById("ctl00_ContentPlaceHolder1_radbtnUpdate_input");
    var timesave = document.getElementById("ctl00_ContentPlaceHolder1_radbtnSave_input");
    var select2 = document.getElementById("ContentPlaceHolder1_btnDesignationStucture");
    var cemployee = document.getElementById("ContentPlaceHolder1_btnSave");
    var cancel2 = document.getElementById("ContentPlaceHolder1_btnCancel");
    var selectbtn = document.getElementById("ContentPlaceHolder1_btnOrganizationSelect");
    var clear1 = document.getElementById("ContentPlaceHolder1_btnClear");
    var Resetbtn = document.getElementById("ContentPlaceHolder1_btnReset");
    var view1 = document.getElementById("ContentPlaceHolder1_btnView");
    var addBtn = document.getElementById("ContentPlaceHolder1_Button1");
    var chooseElement = document.getElementById("ctl00_ContentPlaceHolder1_radUploadEmpExcelfile0");
    var cancelElement2 = document.getElementById("ContentPlaceHolder1_ASPxPopupControl1_btnPopupCancel");
    var saveElement2 = document.getElementById("ContentPlaceHolder1_ASPxPopupControl1_btnPopupAdd");
    var finishElement3 = document.getElementById("ctl00_ContentPlaceHolder1_EmployeeAditionalInfoWizard_RadButton3_input");
    var finishElement2 = document.getElementById("ctl00_ContentPlaceHolder1_EmployeeAditionalInfoWizard_RadButton2_input");
    var finishElement = document.getElementById("ctl00_ContentPlaceHolder1_EmployeeAditionalInfoWizard_RadButton1_input");
    var selectallElement = document.getElementById("ctl00_ContentPlaceHolder1_radbtnSelectAlll_input");
    var clearElement = document.getElementById("ctl00_ContentPlaceHolder1_radbtnClearAll_input");
    var deleteElement = document.getElementById("ctl00_ContentPlaceHolder1_btnDelete_input");
    var updateElement = document.getElementById("ctl00_ContentPlaceHolder1_btnUpdate_input");
    var cancelElement = document.getElementById("ctl00_ContentPlaceHolder1_btnCancel_input");
    var saveElement = document.getElementById("ctl00_ContentPlaceHolder1_btnSave_input");
    var bankNameElement = document.getElementById("ctl00_ContentPlaceHolder1_radcboBankName_Input");

    if (!!selectall) {
        selectall.classList.add("btn-outline-warning", "btn", "rounded-pill", "btn-width");
    }
    if (!!percom) {
        percom.classList.add("btn-outline-info", "btn", "rounded-pill", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!searchcom) {
        searchcom.classList.add("btn-outline-warning", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!PayDltbtn) {
        PayDltbtn.classList.add("btn-outline-danger", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!PayUplod) {
        PayUplod.classList.add("btn-outline-success", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!PayAddbtn) {
        PayAddbtn.classList.add("btn-outline-primary", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!timeAddbtn) {
        timeAddbtn.classList.add("btn-outline-warning", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!tmiecancel2) {
        tmiecancel2.classList.add("btn-outline-dark", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!timecrddelete) {
        timecrddelete.classList.add("btn-outline-danger", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!timeReset) {
        timeReset.classList.add("btn-outline-secondary", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!timeclear) {
        timeclear.classList.add("btn-outline-dark", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!timesave2) {
        timesave2.classList.add("btn-outline-primary", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!comupdate) {
        comupdate.classList.add("btn-outline-success", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!timecancel) {
        timecancel.classList.add("btn-outline-dark", "btn", "rounded-pill", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!timedelete) {
        timedelete.classList.add("btn-outline-danger", "btn", "rounded-pill", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!timeupdate) {
        timeupdate.classList.add("btn-outline-success", "btn", "rounded-pill", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!timesave) {
        timesave.classList.add("btn-outline-primary", "btn", "rounded-pill", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!select2) {
        select2.classList.add("btn-outline-light", "btn", "rounded-pill", "btn-width");
    }
    if (!!cemployee) {
        cemployee.classList.add("btn-outline-danger", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!cancel2) {
        cancel2.classList.add("btn-outline-secondary", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!selectbtn) {
        selectbtn.classList.add("btn-outline-light", "btn", "rounded-pill", "btn-width");
    }
    if (!!clear1) {
        clear1.classList.add("btn-outline-dark", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!Resetbtn) {
        Resetbtn.classList.add("btn-outline-secondary", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!view1) {
        view1.classList.add("btn-outline-warning", "btn", "rounded-pill", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!addBtn) {
        addBtn.classList.add("btn-outline-warning", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!chooseElement) {
        chooseElement.classList.add("btn-outline-light", "btn", "rounded-pill", "btn-width");
    }
    if (!!cancelElement2) {
        cancelElement2.classList.add("btn-outline-secondary", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!saveElement2) {
        saveElement2.classList.add("btn-outline-primary", "btn", "rounded-pill", "btn-width");
    }
    if (!!finishElement3) {
        finishElement3.classList.add("btn-outline-success", "btn", "rounded-pill", "btn-width");
    }
    if (!!finishElement2) {
        finishElement2.classList.add("btn-outline-success", "btn", "rounded-pill", "btn-width");
    }
    if (!!finishElement) {
        finishElement.classList.add("btn-outline-success", "btn", "rounded-pill", "btn-width");
    }
    if (!!selectallElement) {
        selectallElement.classList.add("btn-outline-warning", "btn", "rounded-pill", "btn-width", "btn-fixed-width");
    }
    if (!!clearElement) {
        clearElement.classList.add("btn-outline-dark", "btn", "rounded-pill", "btn-width", "btn-width", "btn-sm");
    }
    if (!!deleteElement) {
        deleteElement.classList.add("btn-outline-danger", "btn", "rounded-pill", "btn-width", "btn-fixed-width");
    }
    if (!!updateElement) {
        updateElement.classList.add("btn-outline-success", "btn", "rounded-pill", "btn-width", "btn-width", "btn-fixed-width");
    }
    if (!!cancelElement) {
        cancelElement.classList.add("btn-outline-secondary", "btn", "rounded-pill", "btn-width", "btn-fixed-width");
    }
    if (!!saveElement) {
        saveElement.classList.add("btn-primary", "btn", "rounded-pill", "btn-width", "btn-fixed-width");
    }
    if (!!bankNameElement) {
        bankNameElement.classList.add("form-control", "form-control-lg");
    }

    console.log("hre");
}
