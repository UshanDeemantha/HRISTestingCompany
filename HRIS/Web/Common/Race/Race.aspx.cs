
using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Drawing;

public partial class Race_Race : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "EmployeeRace";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Race objRaces = new Race();
    DataTable dtRaces = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
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
                    Response.Redirect("~/Common/NoPermissions.aspx");
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

        lblResult.Text = string.Empty;

        if (!IsPostBack)
        {
            InitializeControls();
            ViewState["dtRaces"] = dtRaces;
        }
    }

    #region Methods
    
    private void BindData(DataTable dtTable)
    {
        InitializeControls();
        if (dtTable.Rows.Count > 0)
        {
            hfRaceId.Value = dtTable.Rows[0]["RaceID"].ToString();
            txtRaceName.Text = dtTable.Rows[0]["RaceName"].ToString();
        }
    }

    private void InitializeControls()
    {
        hfRaceId.Value = string.Empty;
        txtRaceName.Text = string.Empty;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
    }

    #endregion

    #region Buttons

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objRaces.AddRace(txtRaceName.Text);

            if (!objRaces.IsError)
            {
                btnSave.Enabled = false;
                lblResult.Text = "Successfully Saved.";
            }
            else
            {
                lblResult.Text = objRaces.ErrorMsg;
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }

        radgvDetails.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objRaces.UpdateRace(Convert.ToInt32(hfRaceId.Value), txtRaceName.Text);

            if (!objRaces.IsError)
            {
                lblResult.Text = "Successfully Updated.";
            }
            else
            {
                lblResult.Text = objRaces.ErrorMsg;
            }
        }
        catch
        {
            lblResult.Text = "Unable to Save";
        }

        radgvDetails.DataBind();
        InitializeControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objRaces.DeleteRace(Convert.ToInt32(hfRaceId.Value));

        if (objRaces.IsError)
        {
            lblResult.Text = objRaces.ErrorMsg;
        }
        else
        {
            lblResult.Text = "Successfully Deleted.";
        }

        radgvDetails.DataBind();
        InitializeControls();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeControls();
    }

    #endregion

    #region Grid View
    
    protected void radgvDetails_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                GridDataItem dataItem = (GridDataItem)radgvDetails.SelectedItems[0];
                DataTable dtRaces = objRaces.GetRace(Convert.ToInt32(dataItem.GetDataKeyValue("RaceID")));
                BindData(dtRaces);

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
        catch
        {
            if (radgvDetails.SelectedItems.Count <= 0)
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Select item for modify!";
            }
        }
    }

    protected void radgvDetails_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        #region Grid Permitions

        GridItem cmdItem = radgvDetails.MasterTableView.GetItems(GridItemType.CommandItem)[0];
        LinkButton lbtnEdit = cmdItem.FindControl("radgrdbtnEditSelected") as LinkButton;
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

    #endregion
}
