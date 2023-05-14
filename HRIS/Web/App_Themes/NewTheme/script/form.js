function toggleProfileForm(height) {
    
    var element = document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoployeeMembershipControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeCertificationControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeQulificationControl1_formContainer") ||
        document.getElementById(`ContentPlaceHolder1_formContainer`) || 
        document.getElementById("ContentPlaceHolder1_BancBranchControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeFluencyrControl1_formContainer") || 
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSpouseControl1_formContainer") || 
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_formContainer") || 
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSkillsControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoloyeeSportsControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_NewEmployeeAddContol1_UpdatePanel1") ||
        document.getElementById("MainContent_BancBranchControl1_formContainer");

    var profileButtonElement = document.getElementById("profileButton");

    if (profileButtonElement.classList.contains("normal"))
        profileButtonElement.classList.remove("normal");

    if (element.style.cssText == "height: 54px;") {
        showForm(height);
    } else {
        hideForm();
    }
}
function hideForm() {
    var formElement = document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoployeeMembershipControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeCertificationControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeQulificationControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_BancBranchControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeFluencyrControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSpouseControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_formContainer") || 
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSkillsControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoloyeeSportsControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_NewEmployeeAddContol1_UpdatePanel1") ||

        document.getElementById("MainContent_BancBranchControl1_formContainer");

    formElement.style.height = "54px";
    toggleButtonIcon(false);
}
function showForm(height) {
    var formElement = document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoployeeMembershipControl1_formContainer") ||
        document.getElementById ("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeCertificationControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeQulificationControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_BancBranchControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeFluencyrControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSpouseControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSkillsControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoloyeeSportsControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_NewEmployeeAddContol1_UpdatePanel1") ||

        document.getElementById("MainContent_BancBranchControl1_formContainer");

    formElement.style.height = `${height}px`;
    toggleButtonIcon(true);
}
function toggleButtonIcon(isShowForm) {  
    var buttonElement = document.getElementById("profileButton");
    if (!!buttonElement) {
        if (isShowForm) {
            buttonElement.classList.add("minus");
            buttonElement.classList.remove("plus");
        } else {
            buttonElement.classList.remove("minus");
            buttonElement.classList.add("plus");
        }
    } 
} 
function pageLoad(event) {
    
    
    var responseMessage = document.getElementById("ContentPlaceHolder1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_BancBranchControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeQulificationControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeCertificationControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoployeeMembershipControl1_lblResult") ||
        document.getElementById(" ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeFluencyrControl1_lblResult") ||
        document.getElementById(" ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSpouseControl1_lblResult") ||
        document.getElementById(" ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_lblResult") ||
        document.getElementById(" ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSkillsControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoloyeeSportsControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_NewEmployeeAddContol1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeAditionalInfoContol1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeContactDetailsContol1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeFluencyrControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSkillsControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSpouseControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_lblResult") ||

        document.getElementById("MainContent_BancBranchControl1_lblResult"); 

    
    
    var formElement = document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoployeeMembershipControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeCertificationControl1_formContainer") ||
        document.getElementById ("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeQulificationControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_BancBranchControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeFluencyrControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSpouseControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_formContainer") ||    
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSkillsControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoloyeeSportsControl1_formContainer") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_NewEmployeeAddContol1_UpdatePanel1") ||

        document.getElementById("MainContent_BancBranchControl1_formContainer");

    if (!!formElement && formElement.style.height == "54px") {
        toggleButtonIcon(false);
    } else {
        toggleButtonIcon(true);
    }

    addCssClass();
    if (!!responseMessage) {
        addToastrStyle(responseMessage.innerHTML);

    }
}

function addCssClass() {
    var updateElementfremplist = document.getElementById("ctl00_ContentPlaceHolder1_EmployeeAditionalInfoWizard_NewEmployeeAddContol1_radbtnUpdate_input");
    var ctest = document.getElementById("ContentPlaceHolder1_cbStopPay_S_D");
    var ctest2 = document.getElementById("ContentPlaceHolder1_CheckBox6_S_D");
    var payhref1 = document.getElementById("ctl00_ContentPlaceHolder1_cbStopPay_ClientState");
    var check = document.getElementById("ContentPlaceHolder1_chkDefault");
    var commoncheck = document.getElementById("ContentPlaceHolder1_cbActive");
    var otcheck = document.getElementById("ContentPlaceHolder1_radchkEntitle");
    var midnight = document.getElementById("ContentPlaceHolder1_btnMidnightCross");
    var leavetypecheck = document.getElementById("ContentPlaceHolder1_cbNopayType");
    var leavetyperadio = document.getElementById("ContentPlaceHolder1_rblEntitlement_0");
    var leavetyperadio = document.getElementById("ContentPlaceHolder1_rblEntitlement_1");
    var leavetyperadio = document.getElementById("ContentPlaceHolder1_rblShiftCategory_0");
    var leavetyperadio = document.getElementById("ContentPlaceHolder1_rblShiftCategory_1");
    var leavetyperadio = document.getElementById("ContentPlaceHolder1_rblShiftCategory_2");
    var leavetyperadio = document.getElementById("ContentPlaceHolder1_cbShift_0");
    var leavetyperadio = document.getElementById("ContentPlaceHolder1_cbShift_1");
    var formulabtn3 = document.getElementById("ctl00_ContentPlaceHolder1_radbtnPayrollField_input");
    var formulabtn2 = document.getElementById("ctl00_ContentPlaceHolder1_radbtnTxnField_input");
    var formulabtn1 = document.getElementById("ctl00_ContentPlaceHolder1_radbtnMasterField_input");
    var btnvalidate = document.getElementById("ctl00_ContentPlaceHolder1_radbtnValidate_input");
    var Cpopupcancel = document.getElementById("ctl00_ContentPlaceHolder1_ASPxPopupControl1_btnPopupCancel_input");
    var Cpopup = document.getElementById("ctl00_ContentPlaceHolder1_ASPxPopupControl1_btnPopupAdd_input");
    var payreset = document.getElementById("ctl00_ContentPlaceHolder1_radbtnClear_input");
    var payview = document.getElementById("ctl00_ContentPlaceHolder1_btnView_input");
    var payResetElement = document.getElementById("ctl00_ContentPlaceHolder1_radbtnReset_input");
    var payApplyElement = document.getElementById("ctl00_ContentPlaceHolder1_btnApplyDefault_input");
    var paySaveallElement = document.getElementById("ctl00_ContentPlaceHolder1_dxSaveAll_input");
    var deletefrBBranch = document.getElementById("ctl00_ContentPlaceHolder1_BancBranchControl1_radbtnDelete_input");
    var updatefrBBranch = document.getElementById("ctl00_ContentPlaceHolder1_BancBranchControl1_radbtnUpdate_input");
    var cancelfrBBranch = document.getElementById("ctl00_ContentPlaceHolder1_BancBranchControl1_radbtnCancel_input");
    var savefrBBranch = document.getElementById("ctl00_ContentPlaceHolder1_BancBranchControl1_radbtnSave_input");
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
    var commonReset = document.getElementById("ctl00_ContentPlaceHolder1_radbtnReset");
    var timeclear = document.getElementById("ContentPlaceHolder1_radbtnClear");
    var timesave2 = document.getElementById("ContentPlaceHolder1_radbtnSave");
    var comupdate = document.getElementById("ContentPlaceHolder1_btnUpdate");
    var timecancel = document.getElementById("ctl00_ContentPlaceHolder1_radbtnCancel_input"); 
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

    if (!!ctest) {
        ctest.classList.add("chevktest");
    }
    if (!!ctest2) {
        ctest2.classList.add("chevktest");
    }
    if (!!check) {
        check.classList.add("daytypecheck");
    }
    if (!!commoncheck) {
        commoncheck.classList.add("commongroupcheck");
    }
    if (!!otcheck) {
        otcheck.classList.add("otcheck");
    }
    if (!!midnight) {
        midnight.classList.add("midnight");
    }
    if (!!leavetypecheck) {
        leavetypecheck.classList.add("leavetypecheck");
    }
    //if (!!leavetypecheck) {
    //    leavetyperadio.classList.add("leavetyperadio");
    //}
    if (!!updateElementfremplist) {
        updateElementfremplist.classList.add("btn-outline-success", "btn", "rounded-pills", "btn-width", "btn-width", "btn-fixed-width");
    }
    if (!!payhref1) {
        payhref1.classList.add("formul-btn");
    }
    if (!!formulabtn3) {
        formulabtn3.classList.add("formul-btn");
    }
    if (!!formulabtn2) {
        formulabtn2.classList.add("formul-btn");
    }
    if (!!formulabtn1) {
        formulabtn1.classList.add("formul-btn");
    }
    if (!!btnvalidate) {
        btnvalidate.classList.add("btn-outline-dark", "btn", "button", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!Cpopupcancel) {
        Cpopupcancel.classList.add("btn-outline-dark", "btn", "rounded-pills", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!Cpopup) {
        Cpopup.classList.add("btn-outline-primary", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!payreset) {
        payreset.classList.add("btn-outline-secondary", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!payview) {
        payview.classList.add("btn-outline-warning", "btn", "rounded-pill", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!payResetElement) {
        payResetElement.classList.add("btn-outline-secondary", "btn", "rounded-pill", "btn-width", "btn-sm");
    }
    if (!!payApplyElement) {
        payApplyElement.classList.add("btn-outline-success", "btn", "rounded-pill", "btn-width2", "btn-sm", "btn-fixed-width" );
    }
    if (!!paySaveallElement) {
        paySaveallElement.classList.add("btn-outline-primary", "btn", "rounded-pill", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!deletefrBBranch) {
        deletefrBBranch.classList.add("btn-outline-danger", "btn", "btn-width", "btn-fixed-width" ,"btn-sm");
    }
    if (!!updatefrBBranch) {
        updatefrBBranch.classList.add("btn-outline-success", "btn", "btn-width", "btn-width", "btn-fixed-width", "btn-sm");
    }
    if (!!cancelfrBBranch) {
        cancelfrBBranch.classList.add("btn-outline-secondary", "btn", "btn-width", "btn-fixed-width", "btn-sm");
    }
    if (!!savefrBBranch) {
        savefrBBranch.classList.add("btn-outline-primary", "btn", "btn-width", "btn-fixed-width", "btn-sm");
    }
    if (!!selectall) {
        selectall.classList.add("btn-outline-warning", "btn", "rounded-pills", "btn-width");
    }
    if (!!percom) {
        percom.classList.add("btn-outline-info", "btn", "rounded-pills", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!searchcom) {
        searchcom.classList.add("btn-outline-warning", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!PayDltbtn) {
        PayDltbtn.classList.add("btn-outline-danger", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!PayUplod) {
        PayUplod.classList.add("btn-outline-success", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!PayAddbtn) {
        PayAddbtn.classList.add("btn-outline-primary", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!timeAddbtn) {
        timeAddbtn.classList.add("btn-outline-warning", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!tmiecancel2) {
        tmiecancel2.classList.add("btn-outline-dark", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!timecrddelete) {
        timecrddelete.classList.add("btn-outline-danger", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!commonReset) {
        commonReset.classList.add("btn-outline-secondary", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!timeReset) {
        timeReset.classList.add("btn-outline-secondary", "btn", "rounded-pills", "btn-width");
    }
    if (!!timeclear) {
        timeclear.classList.add("btn-outline-dark", "btn", "rounded-pills", "btn-width");
    }
    //if (!!timesave2) {
    //    timesave2.classList.add("btn-outline-primary", "btn", "rounded-pills", "btn-width");
    //}
    if (!!comupdate) {
        comupdate.classList.add("btn-outline-success", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!timecancel) {
        timecancel.classList.add("btn-outline-dark", "btn", "rounded-pills", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!timedelete) {
        timedelete.classList.add("btn-outline-danger", "btn", "rounded-pills", "btn-width", "btn-sm", "btn-fixed-width");
    }
    if (!!timeupdate) {
        timeupdate.classList.add("btn-outline-success", "btn", "rounded-pills", "btn-width", "btn-width", "btn-fixed-width");
    }
    if (!!timesave) {
        timesave.classList.add("btn-outline-primary", "btn", "rounded-pills", "btn-width");
    }
    if (!!select2) {
        select2.classList.add("btn-outline-light", "btn", "rounded-pills", "btn-width");
    }
    if (!!cemployee) {
        cemployee.classList.add("btn-outline-danger", "btn", "btn-width", "btn-sm");
    }
    if (!!cancel2) {
        cancel2.classList.add("btn-outline-secondary", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!selectbtn) {
        selectbtn.classList.add("btn-outline-light", "btn", "rounded-pills");
    }
    if (!!clear1) {
        clear1.classList.add("btn-outline-dark", "btn", "rounded-pills", "btn-width");
    }
    if (!!Resetbtn) {
        Resetbtn.classList.add("btn-outline-secondary", "btn", "rounded-pills", "btn-width");
    }
    if (!!view1) {
        view1.classList.add("btn-outline-warning", "btn", "rounded-pills", "btn-width", "btn-sm","btn-fixed-width");
    }
    if (!!addBtn) {
        addBtn.classList.add("btn-outline-warning", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!chooseElement) {
        chooseElement.classList.add("btn-outline-light", "btn", "rounded-pills", "btn-width");
    }
    if (!!cancelElement2) {
        cancelElement2.classList.add("btn-outline-secondary", "btn", "rounded-pills", "btn-width", "btn-sm");
    }
    if (!!saveElement2) {
        saveElement2.classList.add("btn-outline-primary", "btn", "rounded-pills", "btn-width");
    }
    if (!!finishElement3) {
        finishElement3.classList.add("btn-outline-success", "btn", "rounded-pills", "btn-width");
    }
    if (!!finishElement2) {
        finishElement2.classList.add("btn-outline-success", "btn", "rounded-pills", "btn-width");
    }
    if (!!finishElement) {
        finishElement.classList.add("btn-outline-success", "btn", "rounded-pills", "btn-width");
    }
    if (!!selectallElement) {
        selectallElement.classList.add("btn-outline-warning", "btn", "rounded-pills", "btn-width", "btn-fixed-width");
    }
    if (!!clearElement) {
        clearElement.classList.add("btn-outline-dark", "btn", "rounded-pills", "btn-width", "btn-width", "btn-sm");
    }
    if (!!deleteElement) {
        deleteElement.classList.add("btn-outline-danger", "btn", "rounded-pills", "btn-width", "btn-fixed-width");
    }
    if (!!updateElement) {
        updateElement.classList.add("btn-outline-success", "btn", "rounded-pills", "btn-width", "btn-width", "btn-fixed-width");
    }
    if (!!cancelElement) {
        cancelElement.classList.add("btn-outline-secondary", "btn", "rounded-pills", "btn-width", "btn-fixed-width");
    }
    if (!!saveElement) {
        saveElement.classList.add("btn-outline-primary", "btn", "rounded-pills", "btn-width");
    }
    if (!!bankNameElement) {
        bankNameElement.classList.add("form-control", "form-control-lg");
    }
}



function addToastrStyle(message) {
    
    responseLabelElement = document.getElementById("ContentPlaceHolder1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_BancBranchControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeQulificationControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeCertificationControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoployeeMembershipControl1_lblResult") ||
        document.getElementById(" ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeFluencyrControl1_lblResult") ||
        document.getElementById(" ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSpouseControl1_lblResult") ||
        document.getElementById(" ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_lblResult") ||
        document.getElementById(" ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSkillsControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoloyeeSportsControl1_lblResult") ||
        document.getElementById("MainContent_BancBranchControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeAditionalInfoContol1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeContactDetailsContol1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeFluencyrControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSkillsControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeSpouseControl1_lblResult") ||
        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_lblResult") ||

        document.getElementById("ContentPlaceHolder1_EmployeeAditionalInfoWizard_NewEmployeeAddContol1_lblResult") ;
    
    toastrElement = document.getElementById("toastrContainer");

    if (responseLabelElement.style.color == "red") {
        toastrElement.classList.add("toast-error");
    }
    if (responseLabelElement.style.color=="green") {
        toastrElement.classList.add("toast-success");
    }

    if (!!message) {
        toastrElement.style.display = "grid";
        setTimeout(() => {
            document.getElementById("toastrContainer").style.display = "none";
        }, 5000);
    } else {
        toastrElement.style.display = "none";
    }
}
function closeToastr() {
    document.getElementById("toastrContainer").style.display = "none";
}

