
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.HR.BLL;
using Telerik.Web.UI;

public partial class Skills_SkillGrade : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "HR";
    private const string PageName = "SkillGrade";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Skills objSkill = new Skills();
    DataTable dtSkillGrade = new DataTable();

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
                    btnSave.Visible = false;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    ViewState["IsModify"] = false;
                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnSave.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                        ViewState["IsModify"] = true;
                    }
                    else
                    {
                        btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
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
            ViewState["dtSkillGrade"] = dtSkillGrade;
        }
    }

    #region Methods
    
    private void InitializeControls()
    {
        hfSkillGradeId.Value = string.Empty;
        txtSkillGradeCode.Text = string.Empty;
        txtSkillGradeName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfSkillGradeId.Value = dtTable.Rows[0]["SkillGradeID"].ToString();
            txtSkillGradeCode.Text = dtTable.Rows[0]["SkillGradeCode"].ToString();
            txtSkillGradeName.Text = dtTable.Rows[0]["SkillGradeName"].ToString();          
        }
    }

    #endregion
    
    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objSkill.AddSkillGrade(txtSkillGradeCode.Text, txtSkillGradeName.Text);

            if (!objSkill.IsError)
            {
                btnSave.Enabled = false;
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objSkill.ErrorMsg;
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }
        RadGrid1.DataBind();
        InitializeControls();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objSkill.UpdateSkillGrade(Convert.ToInt32(hfSkillGradeId.Value), txtSkillGradeCode.Text, txtSkillGradeName.Text);
            
            if (!objSkill.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = objSkill.ErrorMsg;
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objSkill.DeleteSkillGrade(Convert.ToInt32(hfSkillGradeId.Value));

        if (objSkill.IsError)
        {
            lblResult.Text = objSkill.ErrorMsg;
        }
        else
        {
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Deleted.";
        }

        RadGrid1.DataBind();
        InitializeControls();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();       
    }    

    #endregion

    #region Grid View

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {            
            dtSkillGrade = objSkill.GetSkillGrade(Convert.ToInt32(e.CommandArgument));

            BindData(dtSkillGrade);
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    #endregion  
    protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

        if (e.CommandName == "Select")
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
                DataTable dtEvent = objSkill.GetSkillGrade(Convert.ToInt32(dataItem.GetDataKeyValue("SkillGradeID")));
                BindData(dtEvent);
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch
            {
                if (RadGrid1.SelectedItems.Count <= 0)
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Text = "Select item for modify!";
                }
            }
        }
    }
    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        #region Grid Permitions

        GridItem cmdItem = RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
        LinkButton lbtnEdit = cmdItem.FindControl("btnEditSelected") as LinkButton;
        lbtnEdit.Visible = false;
        if (Convert.ToBoolean(ViewState["IsModify"]) == true)
        {
            lbtnEdit.Visible = true;
        }
        else
        {
            lbtnEdit.Visible = false;
        }
        #endregion
    }
}
