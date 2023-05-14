using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Services;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Organization_OrganizationStructure : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "OrganizationStructure";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    DataTable dt = new DataTable();
    Organization objOrganization = new Organization();    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (!IsPostBack)
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
            if (Session["CompanyId"] != null)
            {
                radcboCompany.SelectedValue = Session["CompanyId"].ToString();
            }
            else
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }
            PopulateTreeView();
            Clear();
            DataBindToCombo();
        }       
     }

    #region Methods
    private void Initialize()
    {
        try
        {
            //btnOrganizationSelect.Enabled = true;
            objOrganization.OrganizationStuctureTypeID = Convert.ToInt32(radcboOrganizationType.Value);
            objOrganization.ParentId = Convert.ToInt32(hfParentId.Value.Trim());
            objOrganization.OrganizationStructureLevelIndex = Convert.ToInt32(ddlIndex.Value);
            formContainer.Attributes.CssStyle.Add("height", "54px");
        }
        catch
        { 
        }
    }

    private void DataBindToCombo()
    {
        radcboOrganizationType.DataSource = objOrganization.GetOrganizationTypes(Convert.ToInt32(Session["CompanyId"].ToString()), Convert.ToInt32(hfChildLevel.Value), 0);
        radcboOrganizationType.TextField="OrganizationTypeName";
        radcboOrganizationType.ValueField = "OrganizationTypeID";
        radcboOrganizationType.DataBind();
    }

    private int GetOrganizationLevelByStructureId(int StructureId)
    {
        DataTable dtPatentOrganizationIndex = objOrganization.GetOrganizationStructure(StructureId,false);

        if (dtPatentOrganizationIndex.Rows.Count > 0)
        {
            int TypeId = Convert.ToInt32(dtPatentOrganizationIndex.Rows[0]["OrganizationTypeID"].ToString());
            return GetOrganizationLevelIndex(TypeId);
        }
        else
        {
            return 0;
        }        
    }

    private int GetOrganizationLevelIndex(int TypeId)
    {
        DataTable dtOrganizationIndex = objOrganization.GetOrganizationLevelIndexByOrganizationTypeId(TypeId);

        if (dtOrganizationIndex.Rows.Count > 0)
        {
            return Convert.ToInt32(dtOrganizationIndex.Rows[0]["OrganizationalIndex"].ToString());
        }
        else
        {
            return 0;
        }        
    }

    private bool ValidateOrganizationStucture()
    {
        int ChildTypeId = GetOrganizationLevelIndex(Convert.ToInt32(radcboOrganizationType.Value));
        int ParentTypeId = GetOrganizationLevelByStructureId(Convert.ToInt32(hfParentId.Value.Trim()));
         
        if (ChildTypeId <= ParentTypeId)
        {
            lblResult.Text = "Invalid Hierarchy... !";
            return false;
        }
        return true;
    }
    
    private void Clear()
    {
       // hfParentId.Value = "1";
        hfChildLevel.Value = "-99";
        txtRootLevel.Text = string.Empty;       
        radcboOrganizationType.SelectedIndex = -1;
        ddlIndex.SelectedIndex = -1;
        hfParentId.Value = string.Empty;
        formContainer.Attributes.CssStyle.Add("height", "54px");
    }

    private void EnabledDisabled(bool Enabled)
    {
        btnSave.Enabled = Enabled;
        btnUpdate.Enabled = !Enabled;
        btnDelete.Enabled = !Enabled;
    }
   
    #region Tree View Methods

    private void PopulateTreeView()
    {
        if (TreeView1.Nodes.Count > 0)
            TreeView1.Nodes.Clear();

        PopulateRootLevel();
        TreeView1.DataBind();
    }

    private void PopulateRootLevel()
    {
        DataTable dt;
        Organization objOrganizationStructure = new Organization();
        dt = objOrganizationStructure.GetOrganizationStructureRootLevel(Convert.ToInt32(Session["CompanyId"]), 0);
        PopulateNodes(dt, TreeView1.Nodes);
    }

    private void PopulateNodes(DataTable Table, TreeNodeCollection Nodes)
        {
        foreach (DataRow row in Table.Rows)
        {
            TreeNode NewNode = new TreeNode();
            NewNode.Text = row["OrganizationTypeName"].ToString();
            NewNode.Value = row["OrgStructureID"].ToString();
            Nodes.Add(NewNode);
            NewNode.PopulateOnDemand = (Convert.ToInt32(row["ChildNodeCount"].ToString()) > 0);
        }
    }

    private void PopulateSubLevel(int ParentId, TreeNode ParentNode)
    {
        Organization objOrganizationStructure = new Organization();
        dt = objOrganizationStructure.GetOrganizationStructure(ParentId, Convert.ToInt32(Session["CompanyId"]));
        PopulateNodes(dt, ParentNode.ChildNodes);
    }

    #endregion

    #endregion

    #region TreeView

    protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        formContainer.Attributes.CssStyle.Add("height", "375px");
        DevExpress.Web.ListEditItem selectedItem = radcboOrganizationType.Items.FindByText(TreeView1.SelectedNode.Text);

        if (selectedItem != null)
            radcboOrganizationType.Value = selectedItem.Value;

        if (TreeView1.SelectedNode.Parent != null)
        {
            txtRootLevel.Text = TreeView1.SelectedNode.Parent.Text;
            hfParentId.Value = TreeView1.SelectedNode.Parent.Value;
        }
        else
        {
            txtRootLevel.Text = TreeView1.SelectedNode.Text;
            hfParentId.Value = TreeView1.SelectedNode.Value;
        }

        hfChildLevel.Value = objOrganization.GetOrganizationChildLevel(Convert.ToInt32(hfParentId.Value)).ToString();
        DataBindToCombo();

        radcboOrganizationType.Value = objOrganization.GetOrganizationTypeId(Convert.ToInt32(TreeView1.SelectedNode.Value)).ToString();

        ddlIndex.Value = TreeView1.SelectedNode.Target;
        EnabledDisabled(false);
    }
    #endregion

    #region Buttons
    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Initialize();
            if (ValidateOrganizationStucture() == true)
            {
                objOrganization.AddOrganizationalStructure(Convert.ToInt32(radcboCompany.SelectedValue));
                if (!objOrganization.IsError)
                {
                    lblResult.ForeColor = Color.Green;
                    lblResult.Text = "Successfully Saved...";
                    EnabledDisabled(true);
                    PopulateTreeView();
                    OrganizationStucture1.reLoad();
                }
                else
                {
                    if (objOrganization.ErrorNo == 2601)
                    {
                        lblResult.Text = "Cannot duplicate Department Stucture! ";
                    }
                    else
                        lblResult.Text = objOrganization.ErrorMsg;
                }
                formContainer.Attributes.CssStyle.Add("height", "375px");
            }
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "375px");
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Initialize();
            objOrganization.UpdateOrganizationalStucture(Convert.ToInt32(TreeView1.SelectedNode.Value));
            if (!objOrganization.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Updated...";
                Clear();
                EnabledDisabled(true);
                PopulateTreeView();
                OrganizationStucture1.reLoad();
            }
            else
            {
                if (objOrganization.ErrorNo == 2601)
                {
                    lblResult.Text = "Cannot duplicate Department Stucture!";
                }
                else
                    lblResult.Text = objOrganization.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "375px");
        }
          
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "375px");
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void radbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            objOrganization.DeleteOrganizationalStucture(Convert.ToInt32(TreeView1.SelectedNode.Value));
            if (!objOrganization.IsError)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully Deleted...";
                Clear();
                EnabledDisabled(true);
                PopulateTreeView();
                OrganizationStucture1.reLoad();
            }
            else
            {
                lblResult.Text = objOrganization.ErrorMsg;
            }
            formContainer.Attributes.CssStyle.Add("height", "375px");
        }
        catch
        {
            formContainer.Attributes.CssStyle.Add("height", "375px");
            lblResult.Text = "Some Error Occured!";
        }
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        EnabledDisabled(true);
        formContainer.Attributes.CssStyle.Add("height", "54px");
    } 
    #endregion

    #region Drop Down
    protected void radcboOrganizationType_DataBound1(object sender, EventArgs e)
    {
        
        //radcboOrganizationType.Items.Insert(0, new DevExpress.Web.ListEditItem("<< Select >>", "0"));

    }

    //protected void radcboLevel_DataBound(object sender, EventArgs e)
    //{
    //    radcboLevel.Items.Insert(0, new RadComboBoxItem("<< Select >>", "0"));
    //}
    #endregion     

   
 
    protected void txtRootLevel_PreRender(object sender, EventArgs e)
    {
        if (hfParentId.Value == "")
        {
            hfParentId.Value = "0";
            hfChildLevel.Value = objOrganization.GetOrganizationChildLevel(Convert.ToInt32(hfParentId.Value)).ToString();
            DataBindToCombo();
        }
        else
        {
            hfChildLevel.Value = objOrganization.GetOrganizationChildLevel(Convert.ToInt32(hfParentId.Value)).ToString();
            DataBindToCombo();
            formContainer.Attributes.CssStyle.Add("height", "375px");
        }
        

    }
    //protected void txtRootLevel_TextChanged(object sender, EventArgs e)
    //{

    //}
    //protected void txtRootLevel_TextChanged1(object sender, EventArgs e)
    //{

    //}
    //protected void TextBox1_TextChanged(object sender, EventArgs e)
    //{

    //}

    //protected void radcboLevel_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    //{
    //  //  int ChildTypeId = GetOrganizationLevelIndex(Convert.ToInt32(radcboOrganizationType.SelectedValue));
    //    int ParentTypeId = GetOrganizationLevelByStructureId(Convert.ToInt32(hfParentId.Value.Trim()));
    //    hfChildLevel.Value = (ParentTypeId + 1).ToString();
    //    DataBindToCombo();
    //}    

    protected void radcboOrganizationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DataTable dtOrganizationIndex = objOrganization.GetOrganizationLevelIndexByOrganizationTypeId(Convert.ToInt32(radcboOrganizationType.Value));

        if (dtOrganizationIndex.Rows.Count > 0)
        {
            string ChildTypeId = dtOrganizationIndex.Rows[0]["OrganizationalIndex"].ToString();
            ddlIndex.Value = ChildTypeId;
        }
        formContainer.Attributes.CssStyle.Add("height", "375px");

    }
    [WebMethod]
    public static List<object> GetChartData()
    {
        string query = "select top 10 DesignationStuctureID As EmployeeId,DesignationCode as Name,'A' as Designation,ParentID as ReportingManager,9383738 as Mobile from DesignationStucture" +
        " inner join Designation on DesignationStucture.DesignationID=Designation.DesignationID ";
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
}