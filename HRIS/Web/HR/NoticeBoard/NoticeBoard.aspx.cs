
using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;
using System.Drawing;
using DevExpress.Web.Rendering;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Diagnostics;

public partial class NoticeBoard_NoticeBoard: System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "NoticeBoard";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    NoticeBoard objNoticeBoard = new NoticeBoard();
    DataTable dtNoticeBoard = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;
        lblResult.ForeColor = Color.Red;
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (IsPostBack == false)
            {
                if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
                {
                    Response.Redirect("~/HR/NoPermissions.aspx");
                    return;
                }
                else
                {
                    btnSaves.Visible = false;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    ViewState["IsModify"] = false;
                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnSaves.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        btnSaves.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        btnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
                        if (btnDelete.Visible == true || btnUpdate.Visible == true)
                        {
                            ViewState["IsModify"] = true;
                        }
                    }
                }
            }
        }

        if (!IsPostBack)
        {
            InitializeControls();
            ViewState["dtNoticeBoard"] = dtNoticeBoard;
        }
    }

    #region Methods
    
    private bool IsValidate()
    {
        if (Convert.ToDateTime(raddpFromDate.SelectedDate) >= Convert.ToDateTime(raddpToDate.SelectedDate))
        {
            lblResult.Text = "From Date Can not be higher than or equelant to To Date!";
            return false;
        }

        if (Convert.ToDateTime(raddpToDate.SelectedDate) < DateTime.Today)
        {
            lblResult.Text = "To Date  Can not lower than Today!";
            return false;
        }
        
        return true;
    }


    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfNoticeBoardId.Value = dtTable.Rows[0]["NoticeBoardID"].ToString();
            txtNoticeBoardCode.Text = dtTable.Rows[0]["NoticeBoardCode"].ToString();
            txtNoticeBoardName.Text = dtTable.Rows[0]["NoticeBoardTitle"].ToString();
            txtDescription.Text = dtTable.Rows[0]["NoticeBoardDescription"].ToString();
            raddpFromDate.SelectedDate = Convert.ToDateTime(dtTable.Rows[0]["NoticeBoardFromDate"]);
            raddpToDate.SelectedDate = Convert.ToDateTime(dtTable.Rows[0]["NoticeBoardToDate"]);
            cbActive.Checked = Convert.ToBoolean(dtTable.Rows[0]["Active"].ToString());
            cbActive.Enabled = true;
        }
    }

    private void InitializeControls()
    {
        hfNoticeBoardId.Value = string.Empty;
        txtNoticeBoardCode.Text = string.Empty;
        txtNoticeBoardName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        raddpFromDate.SelectedDate = null;
        raddpToDate.SelectedDate = null;
        btnSaves.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        cbActive.Checked = true;
        cbActive.Enabled = false;
    }

    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValidate())
            {
                return;
            }

            objNoticeBoard.AddNoticeBoard(txtNoticeBoardCode.Text, txtNoticeBoardName.Text, txtDescription.Text, Convert.ToDateTime(raddpFromDate.SelectedDate), Convert.ToDateTime(raddpToDate.SelectedDate));
            
            if (!objNoticeBoard.IsError)
            {
                btnSaves.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
                formContainer.Attributes.CssStyle.Add("height", "365px");
            }
            else
            {
                formContainer.Attributes.CssStyle.Add("height", "365px");
                lblResult.Text = objNoticeBoard.ErrorMsg;
            }
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "365px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "365px");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValidate())
            {
                return;
            }
            
            objNoticeBoard.UpdateNoticeBoard(Convert.ToInt32(hfNoticeBoardId.Value), txtNoticeBoardCode.Text, txtNoticeBoardName.Text, txtDescription.Text, Convert.ToDateTime(raddpFromDate.SelectedDate), Convert.ToDateTime(raddpToDate.SelectedDate), cbActive.Checked);
            
            if (!objNoticeBoard.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
                formContainer.Attributes.CssStyle.Add("height", "365px");
            }
            else
            {
                lblResult.Text = objNoticeBoard.ErrorMsg;
                formContainer.Attributes.CssStyle.Add("height", "365px");
            }
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "365px");
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "365px");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objNoticeBoard.DeleteNoticeBoard(Convert.ToInt32(hfNoticeBoardId.Value));

        if (objNoticeBoard.IsError)
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = objNoticeBoard.ErrorMsg;
            formContainer.Attributes.CssStyle.Add("height", "365px");
        }
        else
        {
            formContainer.Attributes.CssStyle.Add("height", "365px");
            lblResult.Text = "Successfully Deleted.";
        }

        RadGrid1.DataBind();
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "365px");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
        formContainer.Attributes.CssStyle.Add("height", "365px");
    }

    #endregion
        
    #region Grid View
    
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            dtNoticeBoard = objNoticeBoard.GetNoticeBoard(Convert.ToInt32(e.CommandArgument));
            BindData(dtNoticeBoard);

            btnSaves.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion   
    protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    try
        //    {
        //        GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        //        DataTable dtEvent = objNoticeBoard.GetNoticeBoard(Convert.ToInt32(dataItem.GetDataKeyValue("NoticeBoardID")));
        //        BindData(dtEvent);
        //        btnSave.Enabled = false;
        //        btnUpdate.Enabled = true;
        //        btnDelete.Enabled = true;
        //    }
        //    catch
        //    {
        //        if (RadGrid1.SelectedItems.Count <= 0)
        //        {
        //            lblResult.ForeColor = Color.Red;
        //            lblResult.Text = "Select item for modify!";
        //        }
        //    }
        //}
    }
    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        #region Grid Permitions

        //GridItem cmdItem = RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
        //LinkButton lbtnEdit = cmdItem.FindControl("btnEditSelected") as LinkButton;
        //lbtnEdit.Visible = false;
        //if (Convert.ToBoolean(ViewState["IsModify"]) == true)
        //{
        //    lbtnEdit.Visible = true;
        //}
        //else
        //{
        //    lbtnEdit.Visible = false;
        //}
        #endregion
    }

    protected void lkSelect_Click(object sender, EventArgs e)
    {
        var gridView = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).Grid;
        var index = ((GridViewTableDataRow)(((LinkButton)sender).Parent.Parent.Parent.Parent)).VisibleIndex;


        hfNoticeBoardId.Value = gridView.GetRowValues(index, "NoticeBoardID").ToString();
        txtNoticeBoardCode.Text = gridView.GetRowValues(index, "NoticeBoardCode").ToString();
        txtNoticeBoardName.Text = gridView.GetRowValues(index, "NoticeBoardTitle").ToString();
        txtDescription.Text = gridView.GetRowValues(index, "NoticeBoardDescription").ToString();
         raddpFromDate.SelectedDate = Convert.ToDateTime(gridView.GetRowValues(index, "NoticeBoardFromDate"));
        //var NoticeBoardCreateDate = gridView.GetRowValues(index, "AccountNumber").ToString();
        // DateTime txtFromDat = gridView.GetRowValues(index, "NoticeBoardFromDate").();
        raddpToDate.SelectedDate = Convert.ToDateTime(gridView.GetRowValues(index, "NoticeBoardToDate"));
        cbActive.Text = gridView.GetRowValues(index, "Active").ToString();
        btnUpdate.Enabled = true;
        btnSaves.Enabled = false;
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
        formContainer.Attributes.CssStyle.Add("height", "365px");
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String downloadExcelFromGrid()
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize("ok");
    }
    protected void RadButton1_Click(object sender, EventArgs e)
    {
     
            RadGrid1.DataBind();
            GridExporter.WriteXlsToResponse();
            GridExporter.Styles.Default.Font.Name = "Century Gothic";
            GridExporter.Styles.Default.Font.Size = 10;
                formContainer.Attributes.CssStyle.Add("height", "365px");


    }
}