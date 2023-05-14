using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;
using System.Drawing;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using System.Collections.Generic;

public partial class Designation_DesignationStructure : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "DesignationStructure";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    Designation objDesignation = new Designation();
    DataTable dt;
    DataTable dtTable = new DataTable();
    CompanyProfile Getcompany = new CompanyProfile();
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

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        btnSave.Visible = true;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        btnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                        btnDelete.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.DeleteOnly);
                    }
                }
            }
        }

        lblResult.ForeColor = Color.Red;
        lblResult.Text = string.Empty;        
        EnabledDisabled(true);
        if (!IsPostBack)
        {            
            InitializeControls();

            if (Session["CompanyId"].ToString() != null && Session["CompanyId"].ToString() != "")
            {
                //   Session["CompanyId"].ToString();



                GEtDataIncompany(Convert.ToInt32(Session["CompanyId"]));
               // Companyfrdrops(Convert.ToInt32(Session["CompanyId"]));
            }
            else
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }

            PopulateTreeView();
            ViewState["IsCreated"] = true;
        }
    }

  

    #region Methods

    private void InitializeControls()
    {        
        txtRootLevel.Text = string.Empty;
        
        radcboDesignations.SelectedIndex = -1;
        ddlTreeIndex.SelectedIndex = 0;

        BindDesignation();
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    private void BindDesignation()
    {
        radcboDesignations.DataSource = objDesignation.GetDesignation(0);
        radcboDesignations.ValueField = "DesignationID";
        radcboDesignations.TextField = "DesignationName";
        radcboDesignations.DataBind();
    }

    private void BindData(DataTable dtTable)
    {
        InitializeControls();

        if (dtTable.Rows.Count > 0)
        {
            hfParentId.Value = dtTable.Rows[0]["DesignationStructureID"].ToString();
            radcboDesignations.Value = dtTable.Rows[0]["DesignationID"].ToString();
            ddlTreeIndex.Value = dtTable.Rows[0]["ParentDesignationID"].ToString();
        }
    }

    private void EnabledDisabled(bool Enabled)
    {
        btnSave.Enabled = Enabled;
        btnUpdate.Enabled = !Enabled;
        btnDelete.Enabled = !Enabled;
    }

    private bool IsValidate()
    {
        if (radcboDesignations.SelectedIndex <= 0)
        {
            lblResult.Text = "Select Designation!";
            return false;
        }

        return true;
    }

    #endregion
     
    #region Buttons
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {
            objDesignation.AddDesignationStructure(Convert.ToInt32(hfParentId.Value), Convert.ToInt32(ddlTreeIndex.Value), Convert.ToInt32(radcboDesignations.Value), Convert.ToInt32(radcboCompany.Value));
            if (!objDesignation.IsError)
            {
                InitializeControls();
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Saved...";
                PopulateTreeView();
               // DesignationStructureControl.reLoad();
            }
            else
            {
                if (objDesignation.ErrorNo == 2601)
                {
                    lblResult.Text = "Cannot duplicate Designation Stucture";
                }
                else
                {
                    lblResult.Text = objDesignation.ErrorMsg;
                }
                //formContainer.Attributes.CssStyle.Add("height", "400px");
            }
            InitializeControls();
        }
        catch(Exception ex)
        {
            //formContainer.Attributes.CssStyle.Add("height", "400px");
            lblResult.Text = "Unable to Save";
        }        
    }

    private void UpdateDesignationStructure()
    {
        objDesignation.UpdateDesignationStucture(Convert.ToInt32(tvDesignation.SelectedNode.Value), Convert.ToInt32(hfParentId.Value), Convert.ToInt32(ddlTreeIndex.Value), Convert.ToInt32(radcboDesignations.Value));
        if (!objDesignation.IsError)
        {
            InitializeControls();
            lblResult.ForeColor = Color.Green;
            lblResult.Text = "Successfully Updated...";
            PopulateTreeView();
            //DesignationStructureControl.reLoad();
        }
        else
        {
            if (objDesignation.ErrorNo == 2601)
            {
                lblResult.Text = "Cannot duplicate Organization Stucture";
            }
            else
            {
                lblResult.Text = objDesignation.ErrorMsg;
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {
            UpdateDesignationStructure();
        }
        catch
        {
           // formContainer.Attributes.CssStyle.Add("height", "400px");
            lblResult.Text = "Unable to Save!";
        }
        InitializeControls();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objDesignation.DeleteDesignationStucture(Convert.ToInt32(tvDesignation.SelectedNode.Value));
            if (!objDesignation.IsError)
            {
                InitializeControls();
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Record Successfully Deleted...";
                PopulateTreeView();
               // DesignationStructureControl.reLoad();
            }
            else
            {
                lblResult.Text = objDesignation.ErrorMsg;
            }
           // formContainer.Attributes.CssStyle.Add("height", "400px");
        }
        catch
        {
            //formContainer.Attributes.CssStyle.Add("height", "400px");
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        InitializeControls();
        EnabledDisabled(true);
    }
    #endregion

    #region Data Grid
    
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            DataTable dtEmployeeCareerPath = objDesignation.GetDesignationStructure(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(radcboCompany.Value));
            BindData(dtEmployeeCareerPath);
            EnabledDisabled(false);
            btnDelete.Enabled = false;
        }
    } 

    #endregion
    
    #region Tree View
    private void PopulateSubLevel(int ParentId, RadTreeNode ParentNode)
    {
        dt = objDesignation.GetDesignationStructure(ParentId, Convert.ToInt32(radcboCompany.Value));
        PopulateNodes(dt, ParentNode.Nodes);
    }

    private void PopulateTreeView()
    {
        if (tvDesignation.Nodes.Count > 0)
        {
            tvDesignation.Nodes.Clear();

            ViewState["IsCreated"] = false;
        }

        PopulateRootLevel();
        tvDesignation.DataBind();

        ViewState["IsCreated"] = true;
        if (tvDesignation.GetAllNodes().Count == 0)
        {
            txtRootLevel.Enabled = false;
            hfParentId.Value = "0";
        }
        else
        {
            txtRootLevel.Enabled = true;
            hfParentId.Value = "0";
        }
    }

    private void PopulateRootLevel()
    {
        //dt = objDesignation.GetDesignationStructure(0, Convert.ToInt32(radcboCompany.Value));
        //PopulateNodes(dt, tvDesignation.Nodes);
    }

    private void PopulateNodes(DataTable Table, RadTreeNodeCollection Nodes)
    {
        foreach (DataRow row in Table.Rows)
        {
            RadTreeNode NewNode = new RadTreeNode();
            NewNode.Text = row["DesignationName"].ToString();
            NewNode.Value = row["DesignationStuctureID"].ToString();
            Nodes.Add(NewNode);

            //NewNode.PopulateOnDemand = (Convert.ToInt32(row["ChildNodeCount"].ToString()) > 0);
        }
    }

    //protected void tvDesignation_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    //{
    //    PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
    //}

    //protected void tvDesignation_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    RadComboBoxItem selectedItem = radcboDesignations.Items.FindItemByText(tvDesignation.SelectedNode.Text);
    //    if (selectedItem != null)
    //    {
    //        radcboDesignations.SelectedValue = selectedItem.Value;
    //    }
         
    //    if (tvDesignation.SelectedNode.Parent != null)
    //    {
    //        txtRootLevel.Text = tvDesignation.SelectedNode.Parent.Text;
    //        hfParentId.Value = tvDesignation.SelectedNode.Parent.Value;
    //        EnabledDisabled(false);
    //    }
    //    else
    //    {
    //        txtRootLevel.Text = tvDesignation.SelectedNode.Text;
    //        hfParentId.Value = tvDesignation.SelectedNode.Value;
    //        lblResult.Text = "Can not Edit First Node!";
    //    }

    //    ddlTreeIndex.SelectedValue = tvDesignation.SelectedNode.Target;    
    //}

    #endregion  

    protected void radcboDesignations_DataBound(object sender, EventArgs e)
    {
        radcboDesignations.Items.Insert(0, new DevExpress.Web.ListEditItem("<< Select >>", "0"));
        
    }

    protected void tvDesignation_NodeClick(object sender, RadTreeNodeEventArgs e)
    {
        DevExpress.Web.ListEditItem selectedItem = radcboDesignations.Items.FindByText(tvDesignation.SelectedNode.Text);
        if (selectedItem != null)
        {
            radcboDesignations.Value = selectedItem.Value;
        }

        if (tvDesignation.SelectedNode.ParentNode != null)
        {
            txtRootLevel.Text = tvDesignation.SelectedNode.ParentNode.Text;
            hfParentId.Value = tvDesignation.SelectedNode.ParentNode.Value;

            EnabledDisabled(false);
        }
        else
        {
            txtRootLevel.Text = tvDesignation.SelectedNode.Text;
            hfParentId.Value = tvDesignation.SelectedNode.Value;

            lblResult.Text = "Cannot Edit First Node!";
        }

        ddlTreeIndex.Value = tvDesignation.SelectedNode.Target;
    }

    protected void tvDesignation_NodeCreated(object sender, RadTreeNodeEventArgs e)
    {
        if (ViewState["IsCreated"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsCreated"].ToString()) == true)
            {
                return;
            }
        }

        PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);

        e.Node.CollapseParentNodes();
        if (e.Node.HasControls())
        {
            e.Node.ExpandMode = TreeNodeExpandMode.ServerSide;

            e.Node.ExpandChildNodes();
            e.Node.ExpandMode = TreeNodeExpandMode.ClientSide;
        }
    }

    protected void tvDesignation_DataBound(object sender, EventArgs e)
    {
        txtRootLevel.Enabled = false;
    }

    [WebMethod]
    public static List<object> GetChartData()
    {
        string query = "select DesignationStuctureID As EmployeeId,DesignationCode as Name,'A' as Designation,ParentID as ReportingManager,9383738 as Mobile from DesignationStucture" +
        " inner join Designation on DesignationStucture.DesignationID=Designation.DesignationID where CompanyId=1";
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<object> chartData = new List<object>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        chartData.Add(new object[]
            {
                    sdr["EmployeeId"], sdr["Name"], sdr["Designation"] , sdr["ReportingManager"],sdr["Mobile"]
            });
                    }
                }
                con.Close();
                return chartData;
            }
        }
    }
    public void GEtDataIncompany(int CompanyId)
    {

        dtTable = Getcompany.GetCompanyProfile(CompanyId);

        radcboCompany.Text = dtTable.Rows[0]["CompanyName"].ToString();


    }


}