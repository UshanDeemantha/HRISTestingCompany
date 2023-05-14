using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRM.HR.BLL;
using DevExpress.Web.Rendering;
using System.IO;
using HRM.Common.BLL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Drawing;
using AjaxControlToolkit;

public partial class HR_FileUpload_Upload : System.Web.UI.Page
{
    #region Security
    private const string ApplicationName = "HR";
    private const string PageName = "FileUpload";




    Child SaveFileDetails = new Child();
    Child AddFileDetails = new Child();
    Child DeleteFileDetails = new Child();
    MksSecurity objSecurity = new MksSecurity();
    #endregion

   
    protected void Page_Load(object sender, EventArgs e)
    {



       // DataList1.Visible = false;

        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
            {
                Response.Redirect("~/HR/NoPermissions.aspx");
                return;
            }
        }

        //EmployeeSearch1.EmployeecodeContolId = tbSearchSupplier1;
        //EmployeeSearch1.EmployeeDataList = DataList1;
        //EmployeeSearch1.EmployeeIdContol = hfEmloyeeId;

        if (radFiletype.Value != null)
        {
            wizardExpand();
        }
        else
        {
            wizardMinimize();
        }


        if (!Page.IsPostBack)
        {
            
            #region Form Permition
            if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
            {
                Response.Redirect("~/Payroll/NoPermissions.aspx");
                return;
            }
            else
            {
                btnUpload.Visible = false;
                RbtnUpdate.Visible = false;
                RbtnDelete.Visible = false;
                ViewState["IsModify"] = false;
                if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                {
                    btnUpload.Visible = true;
                    RbtnUpdate.Visible = true;
                    RbtnDelete.Visible = true;
                    ViewState["IsModify"] = true;
                }
                else
                {
                    btnUpload.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                    RbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                    RbtnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
                    if (RbtnDelete.Visible == true || RbtnUpdate.Visible == true)
                    {
                        ViewState["IsModify"] = true;
                    }
                }

            }
            #endregion

            ButtonEnableDisable(true);
            DataList1.Visible = false;
            ViewAttendancePopup.ShowOnPageLoad = false;
           
           
        }
        else
        {
            DataList1.Visible = true;
            formContainer.Attributes.CssStyle.Add("height", "415px");
        }

    }
    public void rt_chnaged(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        Rating rating = (Rating)sender;
        DataListItem item = (DataListItem)rating.NamingContainer;
        TextBox lbl = (TextBox)item.FindControl("lblEmployeeCode");
        var indexs = lbl.Text;
    }
    protected void wizardExpand()
    {
        formContainer.Attributes.CssStyle.Add("height", "415px");
    }
    protected void wizardMinimize()
    {
        if (radFiletype.Value == null && txtDescription.Text == "")
        {
            formContainer.Attributes.CssStyle.Add("height", "54px");
        }
        else
        {
            formContainer.Attributes.CssStyle.Add("height", "412px");
        }

    }
    protected void radcbJobCategory_TextChanged(object sender, EventArgs e)
    {
        DataList1.Visible = false;
        formContainer.Attributes.CssStyle.Add("height", "412px");
    }
    protected void ddlEmployee_DataBound(object sender, EventArgs e)
    {

    }
    protected void radcboShiftCode_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
    {

    }
    protected void radcboRosterGroup_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
    {

    }
    protected void radcboRosterGroup_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }
    protected void lkSelect_Click(object sender, EventArgs e)
    {

        //confirmpopup.ShowOnPageLoad = true;

        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
        var hfId = gridView.GetRowValues(index, "Id").ToString();
        var FileTypeId = gridView.GetRowValues(index, "FileTypeId").ToString();
        var Description = gridView.GetRowValues(index, "DesCription").ToString();
        var EmployeeId = gridView.GetRowValues(index, "EmployeeId").ToString();
        var FileCategoryCode = gridView.GetRowValues(index, "FileCategoryCode").ToString();
        var FileName = gridView.GetRowValues(index, "FileName").ToString();

        //hfFilePath.Value = Server.MapPath(ConfigurationManager.AppSettings["FileUploadMainFolder"]) + FileCategoryCode + "/" + EmployeeId + "/" + FileName;
        //hfFileId.Value = hfId.ToString();
        //hfEmloyeeId.Value = EmployeeId;
        radFiletype.Value = FileTypeId;
        txtDescription.Text = Description;
        DataList1.DataBind();
        ButtonEnableDisable(false);
        formContainer.Attributes.CssStyle.Add("height", "415px");
    }




    
    protected void lkView_Click(object sender, EventArgs e)
    {

        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
        var FileName = gridView.GetRowValues(index, "FileName").ToString();
        var EmployeeId = gridView.GetRowValues(index, "EmployeeId").ToString();
        var FileCategoryCode = gridView.GetRowValues(index, "FileCategoryCode").ToString();
        var path = Server.MapPath(ConfigurationManager.AppSettings["FileUploadMainFolder"] + EmployeeId + "/" + FileCategoryCode + "/" + FileName);
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('" + ConfigurationManager.AppSettings["FileViewPath"] + FileCategoryCode + "/" + EmployeeId + "/" + FileName + "');", true);
        //ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('/web/EmployeeFiles/4832.pdf');", true);
        //ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('D:/Ishara/File.pdf');", true);
        DataList1.Visible = false;

        //LinkButton lnkView = sender as LinkButton;
        // GridViewRow row = lnkView.NamingContainer as GridViewRow;
        // string urlName = Request.Url.AbsoluteUri;
        // // Removing the Page Name
        // urlName = urlName.Remove(urlName.LastIndexOf("/web/Data/"));
        // //Adding FolderName and FileName in the URL
        // string url = string.Format("{0}/Images/{1}", urlName, row.Cells[1].Text);
        // string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
        // this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);


        //ClientScript.RegisterStartupScript(this.Page.GetType(), "",
        //"window.open('~/Data','Graph','height=400,width=500');", true);
        //-----------------------------------------------------------------------//




        //------------------------------------------------------------------------------------------------


        //var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        //var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;
        //var FileName = gridView.GetRowValues(index, "FileName").ToString();
        //string FilePath = Server.MapPath("~/Data/" + FileName + "");
        //WebClient User = new WebClient();
        //Byte[] buffer = User.DownloadData(FilePath);


        //{
        //    Response.Clear();
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-length", buffer.Length.ToString());
        //    Response.BinaryWrite(buffer);
        //}

        //{
        //    Response.ContentType = "application/pdf";

        //    Response.AddHeader("Content-Length", FileBuffer.Length.ToString());

        //    Response.AddHeader("Content-Disposition", "inline; filename=" + FileName + "");
        //    Response.BinaryWrite(FileBuffer);

        //    Response.Write("<script>");
        //    Response.Write("window.open('https://www.youtube.com/','_blank')");
        //    Response.Write("</script>");


        formContainer.Attributes.CssStyle.Add("height", "412px");

    }

    //[System.Web.Services.WebMethod]
    //public File DownloadDoc(string id)
    //{
    //    string jsonStringList = "";
    //    try
    //    {
            
    //        //jsonStringList = new JavaScriptSerializer().Serialize(PendingTasks);
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    return File("");
    //}
﻿

    protected void UploadBtn_Click(object sender, EventArgs e)
    {

    }
    protected void radbtnSave_Click(object sender, EventArgs e)
    {

        string filename = Path.GetFileName(FileUpLoad1.PostedFile.FileName);
        string contentType = FileUpLoad1.PostedFile.ContentType;
        using (Stream fs = FileUpLoad1.PostedFile.InputStream)
        {
            formContainer.Attributes.CssStyle.Add("height", "412px");
            //using (BinaryReader br = new BinaryReader(fs))
            //{
            //    byte[] bytes = br.ReadBytes((Int32)fs.Length);
            //    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(constr))
            //    {
            //        string query = "INSERT INTO tblFiles VALUES (@Name, @ContentType, @Data)";
            //        using (SqlCommand cmd = new SqlCommand(query))
            //        {
            //            cmd.Connection = con;
            //            cmd.Parameters.AddWithValue("@Name", filename);
            //            cmd.Parameters.AddWithValue("@ContentType", contentType);
            //            cmd.Parameters.AddWithValue("@Data", bytes);
            //            con.Open();
            //            cmd.ExecuteNonQuery();
            //            con.Close();
            //        }
            //    }
        }




        // string path = Server.MapPath("Images1/");
        //if (FileUpLoad1.HasFile) 
        //{
        //string ext = Path.GetExtension(FileUpLoad1.FileName);
        // if (ext == ".jpg" || ext == ".png") 
        // {

        // FileUpLoad1.SaveAs(path + FileUpLoad1.FileName);
        //   string name = "Images1/" + FileUpLoad1.FileName;
        //   SaveFileDetails.SaveFileDetails(Convert.ToInt32(radcbJobCategory.Value), txtDescription.Text,FileUpLoad1.FileName ,Convert.ToInt64(hfEmloyeeId.Value) );
        // }

        // }
        //string fileName = FileUpLoad1.FileName;



    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        InitialPopupControls();
        ViewAttendancePopup.ShowOnPageLoad = true;
       DataList1.Visible = false;
        formContainer.Attributes.CssStyle.Add("height", "412px");
    }

    protected void BtnAddViewPopup_Click(object sender, EventArgs e)
    {

    }

    protected void BtnAddViewPopup_Click1(object sender, EventArgs e)
    {
        try
        {
            if (BtnAddViewPopup.Text == "Add")
            {
                if (CheckPopupValidations()) {
                    string mainFolderPath = Server.MapPath(ConfigurationManager.AppSettings["FileUploadMainFolder"]) + dxTxtFileTypeCode.Text;
                    if (!Directory.Exists(mainFolderPath))
                    {
                        Directory.CreateDirectory(mainFolderPath);
                    }
                    AddFileDetails.AddFileDetails(dxTxtFileTypeCode.Text, dxTxtFileTypeName.Text, Session["UserName"].ToString());
                    GvAtte.DataBind();
                    radFiletype.DataBind();
                    InitialPopupControls();
                    DataList1.Visible = false;
                    lblMsg.ForeColor = Color.Green;
                    lblMsg.Text = "Submitted Successfully";
                }
                formContainer.Attributes.CssStyle.Add("height", "412px");
            }
            else
            {
                if (dxTxtFileTypeName.Text == "")
                {
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Please Enter File Type Name.";
                }
                else {
                    AddFileDetails.UpdateFileDetails(Convert.ToInt32(hfFileTypeId.Value), dxTxtFileTypeCode.Text, dxTxtFileTypeName.Text, Session["UserName"].ToString());
                    GvAtte.DataBind();
                    InitialPopupControls();
                    DataList1.Visible = false;
                    lblMsg.ForeColor = Color.Green;
                    lblMsg.Text = "Submitted Successfully";
                }
                formContainer.Attributes.CssStyle.Add("height", "412px");

            }

        }

        catch (Exception ex)
        {
            formContainer.Attributes.CssStyle.Add("height", "412px");
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = ex.Message;
        }

    }

    private void InitialPopupControls() {
        dxTxtFileTypeCode.Text = "";
        dxTxtFileTypeName.Text = "";
        dxTxtFileTypeCode.Enabled = true;
        BtnAddViewPopup.Text = "Add";
        lblMsg.Text = "";
    }

    private bool CheckPopupValidations() {

        if (dxTxtFileTypeCode.Text == "") {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Please Enter File Type Code.";
            return false;
        }

        if (dxTxtFileTypeName.Text == "")
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Please Enter File Type Name.";
            return false;
        }

        DataTable dtFileType = AddFileDetails.GetFileTypeCode(dxTxtFileTypeCode.Text);
        if (dtFileType.Rows.Count > 0) {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "File Type Code Already Exists.";
            return false;
        }

        return true;
    }

    protected void lkEditGvAtte_Click(object sender, EventArgs e)
    {

    }
    protected void lkDeleteGvAtte_Click(object sender, EventArgs e)
    {

    }
    protected void BtnCloseViewPopup_Click(object sender, EventArgs e)
    {

        ViewAttendancePopup.ShowOnPageLoad = false;
        DataList1.Visible = false;

    }
    protected void lkSelect_Click1(object sender, EventArgs e)
    {
        try
        {
            var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
            var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;

            var FileCategoryCode = gridView.GetRowValues(index, "FileCategoryCode").ToString(); //Request.QueryString["id"];
            var FileCategoryName = gridView.GetRowValues(index, "FileCategoryName").ToString();
            hfFileTypeId.Value = gridView.GetRowValues(index, "FileTypeId").ToString();

            dxTxtFileTypeCode.Text = FileCategoryCode.ToString();
            dxTxtFileTypeName.Text = FileCategoryName.ToString();
            dxTxtFileTypeCode.Enabled = false;

            BtnAddViewPopup.Text = "Update";
            DataList1.Visible = false;
            formContainer.Attributes.CssStyle.Add("height", "415px");
        }
        catch
        {
        }
    }
    protected void tbSearchSupplier1_TextChanged(object sender, EventArgs e)
    {

    }
    //protected void btnSearchSupplier_Click(object sender, ImageClickEventArgs e)
    //{
    //   // tbSearchSupplier1.Text = string.Empty;
    //    hfEmloyeeId.Value = "-99";
    //}
    protected void ASPxFileManager1_DataBinding(object sender, EventArgs e)
    {
        //hfEmloyeeId.Value = "-99";
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //if (Validate()) {
        //    DataList1.Visible = false;
        //    if (FileUpLoad1.HasFile)
        //    {
        //        string fileextension = System.IO.Path.GetExtension(FileUpLoad1.FileName);
        //        if (fileextension.ToLower() == ".pdf")
        //        {
        //            DataTable dtFileType = AddFileDetails.GetFileTypeCodeById(Convert.ToInt32(radFiletype.Value));
        //            string fileTypeCode = dtFileType.Rows[0]["FileCategoryCode"].ToString();
        //            string fileName = FileUpLoad1.FileName;
        //            string folderPath = Server.MapPath(ConfigurationManager.AppSettings["FileUploadMainFolder"]) + fileTypeCode + "/" + hfEmloyeeId.Value;

        //            if (!Directory.Exists(folderPath))
        //            {
        //                Directory.CreateDirectory(folderPath);
        //            }

        //            FileUpLoad1.PostedFile
        //                .SaveAs(folderPath + "/" + fileName);
        //            SaveFileDetails.SaveFileDetails(Convert.ToInt32(radFiletype.Value), txtDescription.Text, FileUpLoad1.FileName, Convert.ToInt64(hfEmloyeeId.Value), Session["UserName"].ToString());
        //            lblFilUp.ForeColor = Color.Green;
        //            lblFilUp.Text = "File Upload SuccessFully";
        //            Grid.DataBind();
        //            formContainer.Attributes.CssStyle.Add("height", "412px");

        //            //btnSearchSupplier.ToolTip = "";
        //            radFiletype.Value = "";
        //            txtDescription.Text = "";
        //        }
        //        else
        //        {
        //            lblFilUpNo.Text = "Allows Only .Pdf";
        //            formContainer.Attributes.CssStyle.Add("height", "412px");
        //        }
        //        DataList1.Visible = false;
        //        formContainer.Attributes.CssStyle.Add("height", "412px");
        //    }
        //}
    }

    private bool Validate() {

        //if (hfEmloyeeId.Value == "" || hfEmloyeeId.Value == null) {
        //    lblFilUp.ForeColor = Color.Red;
        //    lblFilUp.Text = "Please Select Employee";
        //    return false;
        //}

        if (radFiletype.Value == "" || radFiletype.Value == null)
        {
            lblFilUp.ForeColor = Color.Red;
            lblFilUp.Text = "Please Select File Type";
            return false;
        }

        return true;
    }


    protected void GetDocuments(int ITCode)
    {
        string[] filePaths = Directory.GetFiles((@"D:\Ishara\EarlsCourt HRM\15112020\HRM\Web\Data" + ITCode));
        List<ListItem> files = new List<ListItem>();
        foreach (string filePath in filePaths)
        {
            files.Add(new ListItem(Path.GetFileName(filePath), filePath));
        }
        Grid.DataSource = files;
        Grid.DataBind();
    }



    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "412px");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //DeleteFileDetails.DeleteFileDetails(Convert.ToInt32(hfFileId.Value));
       // string file = hfFilePath.Value;
        //if ((System.IO.File.Exists(file)))
        //{
        //    System.IO.File.Delete(file);
        //}
        confirmpopup.ShowOnPageLoad = false;
        lblFilUp.ForeColor = Color.Green;
        lblFilUp.Text = "File Delete SuccessFully";
        Grid.DataBind();

        // btnSearchSupplier.ToolTip = "";
        radFiletype.Value = "";
        txtDescription.Text = "";
       // hfEmloyeeId.Value = "";
        DataList1.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        confirmpopup.ShowOnPageLoad = false;
        //
    }
    protected void btnPopupClear_Click(object sender, EventArgs e)
    {
        InitialPopupControls();
        DataList1.Visible = false;
    }
    protected void RbtnUpdate_Click(object sender, EventArgs e)
    {
        //try
        //{
           // SaveFileDetails.UpdateFile(Convert.ToInt32(radFiletype.Value), txtDescription.Text, FileUpLoad1.FileName, Convert.ToInt64(hfFileId.Value), Session["UserName"].ToString());
            //if (FileUpLoad1.HasFile) {
               // string file = hfFilePath.Value;
                //if ((System.IO.File.Exists(file)))
                //{
                //    System.IO.File.Delete(file);
                //}

              //  string fileextension = System.IO.Path.GetExtension(FileUpLoad1.FileName);
                //if (fileextension.ToLower() == ".pdf")
                //{
                   // DataTable dtFileType = AddFileDetails.GetFileTypeCodeById(Convert.ToInt32(radFiletype.Value));
                    //string fileTypeCode = dtFileType.Rows[0]["FileCategoryCode"].ToString();
                   // string fileName = FileUpLoad1.FileName;
                   // string folderPath = Server.MapPath(ConfigurationManager.AppSettings["FileUploadMainFolder"]) + fileTypeCode + "/" + hfEmloyeeId.Value;

                   // if (!Directory.Exists(folderPath))
                    //{
                      //  Directory.CreateDirectory(folderPath);
                    //}

                  //  FileUpLoad1.PostedFile
                      //  .SaveAs(folderPath + "/" + fileName);
              //  }
                //else
                //{
                //    lblFilUpNo.Text = "Allows Only .Pdf";
                //}
              //  DataList1.Visible = false;
           // }

           // lblFilUp.ForeColor = Color.Green;
           // lblFilUp.Text = "File Update SuccessFully";
            //Grid.DataBind();

            // btnSearchSupplier.ToolTip = "";
           // radFiletype.Value = "";
           // txtDescription.Text = "";
           // hfEmloyeeId.Value = "";
            //.DataBind();
           // ButtonEnableDisable(true);
       //}
        //catch { 
        //}
    }

    private void InitialControls() {
        radFiletype.Value = null;
        txtDescription.Text = "";
       // hfFileId.Value = "";
        ButtonEnableDisable(true);
        lblFilUp.Text = "";
        formContainer.Attributes.CssStyle.Add("height", "412px");
    }

    private void ButtonEnableDisable(bool Enable)
    {
        btnUpload.Enabled = Enable;
        RbtnUpdate.Enabled = !Enable;
        RbtnDelete.Enabled = !Enable;
    }

    protected void RbtnDelete_Click(object sender, EventArgs e)
    {
        confirmpopup.ShowOnPageLoad = true;
    }
    protected void RbtnCancel_Click(object sender, EventArgs e)
    {
        InitialControls();
       // hfEmloyeeId.Value = "";
        DataList1.DataBind();
    }

    protected void RadButton1_Click(object sender, EventArgs e)
    {
        Grid.DataBind();
        GridExporter.WriteXlsToResponse();
        GridExporter.Styles.Default.Font.Name = "Century Gothic";
        GridExporter.Styles.Default.Font.Size = 10;
    }

    protected void CmbCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void tbSearchSupplier_TextChanged(object sender, EventArgs e)
    {

    }
}
