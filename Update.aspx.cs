using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BloodDonars
{
    public partial class Update : System.Web.UI.Page
    {
        void getdata()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            string q = "proc_DispayDonar";
           SqlCommand cmd = new SqlCommand(q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", Session["uname"].ToString());
            cmd.Parameters.AddWithValue("@b", Session["pwd"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "donar");
            GridView1.DataSource=ds;
            GridView1.DataBind();
          


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                getdata();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow rows = GridView1.Rows[e.RowIndex];
            TextBox t1 = (TextBox)rows.FindControl("TextBox1");
            TextBox t2 = (TextBox)rows.FindControl("TextBox2");
            TextBox t3 = (TextBox)rows.FindControl("TextBox3");
            TextBox t4 = (TextBox)rows.FindControl("TextBox4");
            TextBox t5 = (TextBox)rows.FindControl("TextBox5");
            TextBox t6 = (TextBox)rows.FindControl("TextBox6");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            con.Open();
            string q = "porc_Update1Don";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", t1.Text);
            cmd.Parameters.AddWithValue("@b", t2.Text);
            cmd.Parameters.AddWithValue("@c", t3.Text);
            cmd.Parameters.AddWithValue("@d", t4.Text);
            cmd.Parameters.AddWithValue("@e", t5.Text);
            cmd.Parameters.AddWithValue("@f", t6.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            getdata();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            getdata();

        }

        protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
        {
             
        }
    }
}