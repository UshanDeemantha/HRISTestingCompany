﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommenUserControls_HRM_News : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RadRotator1.DataBind();
    }
}