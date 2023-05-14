using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Common_ChangeCompany_DesignationStructureControl : System.Web.UI.UserControl
{
    DataTable dt;
    Designation objDesignation = new Designation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CompanyId"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        {
            PopulateRootLevel();

            ViewState["IsCreated"] = null;
        }
    }

    #region Tree View Methods

    private void PopulateTreeView()
    {
        if (tvDesignationStuctureUserControl.Nodes.Count > 0)
        {
            tvDesignationStuctureUserControl.Nodes.Clear();
        }

        PopulateRootLevel();
    }

    private void PopulateRootLevel()
    {
        //if (Session["IsOk"] == null)
        //{
        //    if (tvDesignationStuctureUserControl.Nodes.Count > 0)
        //    {
        //        tvDesignationStuctureUserControl.Nodes.Clear();
        //    }
        //}

        if (Session["StartingRecreatingDesig"] != null)
        {
            if (Convert.ToBoolean(Session["StartingRecreatingDesig"]) == true)
            {
                tvDesignationStuctureUserControl.Nodes.Clear();
                dt = objDesignation.GetDesignationStructure(0, Convert.ToInt32(Session["NewCompanyId"]));
                PopulateNodes(dt, tvDesignationStuctureUserControl.Nodes);
                Session["StartingRecreatingDesig"] = false;
            }
        }
        
        
    }

    private void PopulateNodes(DataTable Table, RadTreeNodeCollection Nodes)
    {
        foreach (DataRow row in Table.Rows)
        {
            RadTreeNode NewNode = new RadTreeNode();
            NewNode.Text = row["DesignationName"].ToString();
            NewNode.Value = row["DesignationStuctureID"].ToString();
            Nodes.Add(NewNode);
        }
    }

    private void PopulateSubLevel(int ParentId, RadTreeNode ParentNode)
    {
        if (Session["StartingRecreatingDesig"] != null)
        {
            if (Convert.ToBoolean(Session["StartingRecreatingDesig"]) == true)
            {
                dt = objDesignation.GetDesignationStructure(ParentId, Convert.ToInt32(Session["NewCompanyId"]));
                PopulateNodes(dt, ParentNode.Nodes);
            }
        }
    }

    #endregion

    

    public void TreeViewReLoad()
    {
        if (tvDesignationStuctureUserControl.Nodes.Count > 0)
        {
            tvDesignationStuctureUserControl.Nodes.Clear();
        }

        PopulateRootLevel();
        tvDesignationStuctureUserControl.DataBind();
    }

    protected void tvDesignationStuctureUserControl_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        hfDesignationId.Value = tvDesignationStuctureUserControl.SelectedNode.Value;
        hfDesignationName.Value = tvDesignationStuctureUserControl.SelectedNode.Text;

        try
        {
            TextBox txtParentStructureType = (TextBox)Parent.FindControl("txtRootLevel");
            txtParentStructureType.Text = tvDesignationStuctureUserControl.SelectedNode.Text;

            HiddenField hfParentId = (HiddenField)Parent.FindControl("hfParentId");
            hfParentId.Value = tvDesignationStuctureUserControl.SelectedNode.Value;
        }
        catch
        {
            try
            {
                Label lblDesignation = (Label)Parent.FindControl("lblDesignation");
                lblDesignation.Text = tvDesignationStuctureUserControl.SelectedNode.Text;

                HiddenField hfDesignationStucture = (HiddenField)Parent.FindControl("hfDesignationStucture");
                hfDesignationStucture.Value = tvDesignationStuctureUserControl.SelectedNode.Value;
            }
            catch { }
        }
    }

    protected void tvDesignationStuctureUserControl_NodeCreated(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (ViewState["IsCreated"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsCreated"].ToString()) == true)
            {
                return;
            }
        }

        PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
        tvDesignationStuctureUserControl.ExpandAllNodes();
    }

    public void reLoad()
    {
        ViewState["IsCreated"] = false;
        PopulateTreeView();
        ViewState["IsCreated"] = true;
        //tvDesignationStuctureUserControl.DataBind();
    }
}