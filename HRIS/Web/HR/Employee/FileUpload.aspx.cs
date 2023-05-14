using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using HRM.Common.BLL;
using System.Data;
using System.IO;

public partial class HR_Employee_FileUpload : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "FileUpload";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Employee objEmployee = new Employee();

    protected void Page_Load(object sender, EventArgs e)
    {
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

        //EmployeeSearch1.EmployeecodeContolId = tbSearchSupplier;
        //EmployeeSearch1.EmployeeDataList = DataList1;
        //EmployeeSearch1.EmployeeIdContol = hfEmloyeeId;


        if (!Page.IsPostBack)
        {

           // DataList1.Visible = false;

        }
        else
        {
          //  DataList1.Visible = true;
        }
    }

    private bool fileUploaded;
    protected void ASPxFileManager1_FileUploading(object source, FileManagerFileUploadEventArgs e)
    {

        if (hfEmloyeeId.Value != null && hfEmloyeeId.Value != string.Empty)
        {
            string empCode = objEmployee.GetEmployeesCodeByEmployeeID(Convert.ToInt64(hfEmloyeeId.Value));
            int empId = Convert.ToInt32(hfEmloyeeId.Value);

            if (e.File.Folder.Name == "Original")
            {
                e.FileName = empCode + "_" + e.FileName.ToString();
                string filename = e.FileName;
                string ParentFolder = e.File.Folder.Parent.Name;

                objEmployee.AddFileUploadOriginal(Convert.ToInt64(empId), ParentFolder, filename);

            }
            if (e.File.Folder.Name == "Copy")
            {
                e.FileName = empCode + "_" + e.FileName.ToString();
                string filename = e.FileName;
                string ParentFolder = e.File.Folder.Parent.Name;

                objEmployee.AddFileUploadCopy(Convert.ToInt64(empId), ParentFolder, filename);
            }
            if (e.File.Folder.Name == " ")
            {
                e.FileName = empCode + "_" + e.FileName.ToString();
                string filename = e.FileName;
                string ParentFolder = e.File.Folder.Parent.Name;

                objEmployee.AddFileUploadNormal(Convert.ToInt64(empId), ParentFolder, filename);
            }
            else
            {

            }

        }




        fileUploaded = true;
    }
    protected void ASPxFileManager1_Unload(object sender, EventArgs e)
    {
        if (fileUploaded)
        {
            string a = "a";

        }
    }
    protected void ASPxFileManager1_ItemDeleting(object source, FileManagerItemDeleteEventArgs e)
    {
        string filename = e.Item.Name;
        string foldername = e.Item.RelativeName;
        string parent = foldername.Split('\\')[0];
        objEmployee.DeleteUploadedFile(filename, parent);

    }

    protected void tbSearchSupplier_TextChanged(object sender, EventArgs e)
    {
        //if (tbSearchSupplier.Text == string.Empty)
        //{
        //    hfEmloyeeId.Value = "-99";
        //    DataList1.DataBind();

        //}
        //else
        //{
        //    try
        //    {
        //        if (hfEmloyeeId.Value != null && hfEmloyeeId.Value != string.Empty)
        //        {


        //        }
        //    }
        //    catch
        //    { }
        //}
    }

    protected void btnSearchSupplier_Click(object sender, ImageClickEventArgs e)
    {
        //tbSearchSupplier.Text = string.Empty;
        //hfEmloyeeId.Value = "-99";
    }
    protected void ASPxFileManager1_DataBinding(object sender, EventArgs e)
    {
        hfEmloyeeId.Value = "-99";
    }


    protected void ASPxFileManager1_FolderCreating(object source, FileManagerFolderCreateEventArgs e)
    {

        if (e.ParentFolder.Name == "Documents")
        {
            string name = e.Name.ToString();
            objEmployee.AssignFolderToEmployee(name);
        }




    }
}