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
    public partial class userLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            con.Open();
            string q = "porc_LoginDonar";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", TextBox1.Text);
            cmd.Parameters.AddWithValue("@b", TextBox2.Text);
            object p = cmd.ExecuteScalar();
            {
                if ((int)p != 0)
                {
                    Session["uname"] = TextBox1.Text;
                    Session["pwd"] = TextBox2.Text;
                    Server.Transfer("UWelcome.aspx");

                }
                else
                {
                    Response.Write("Invalid Login");
                }
            }

        }
    }
}