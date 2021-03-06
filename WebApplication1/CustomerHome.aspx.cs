﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class CustomerHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ViewProducts(object sender, EventArgs e)
        {
            string field1 = (string)(Session["field1"]);
            Response.Write(field1);


            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("ShowProductsbyPrice", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            //IF the output is a table, then we can read the records one at a time
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
    

            while (rdr.Read())
            {
                //Get the value of the attribute name in the Company table
                int Prodserialno = rdr.GetInt32(rdr.GetOrdinal("serial_no"));
                string Prodserial = (rdr.GetInt32(rdr.GetOrdinal("serial_no"))).ToString();
                string ProdName = rdr.GetString(rdr.GetOrdinal("product_name"));
                //Get the value of the attribute field in the Company table
                string Prodfinalprice = (rdr.GetSqlDecimal(rdr.GetOrdinal("final_price"))).ToString();
                string Prodprice = (rdr.GetSqlDecimal(rdr.GetOrdinal("price"))).ToString();
                
                string availability = (rdr.GetSqlBoolean(rdr.GetOrdinal("available"))).ToString();
                  if (rdr.GetSqlBoolean(rdr.GetOrdinal("available")))
                  {
                    availability = "available";
                  }
                  else availability = "not available";

                string category = rdr.GetString(rdr.GetOrdinal("category"));

                string color;
                if (!rdr.IsDBNull(rdr.GetOrdinal("color")))
                {
                    color = (rdr.GetString(rdr.GetOrdinal("product_description")));
                }
                else color = "__";
                string descrip;
                if (!rdr.IsDBNull(rdr.GetOrdinal("product_description")))
                {
                    descrip = (rdr.GetString(rdr.GetOrdinal("product_description")));
                }
                else descrip = "__";
                
                string rating;
                if (!rdr.IsDBNull(rdr.GetOrdinal("rate")))
                {
                     rating = (rdr.GetFieldValue<Int16>(rdr.GetOrdinal("rate"))).ToString();
                }
                else rating = "__";

                string ProdVendorName = rdr.GetString(rdr.GetOrdinal("vendor_username"));

                //Create a new label and add it to the HTML form
                Label lbl_serial = new Label();
                lbl_serial.Text = "<div>"+" no.: " + Prodserial +"</div>" ;
                form1.Controls.Add(lbl_serial);

                Label lbl_ProdName = new Label();
                lbl_ProdName.Text = "<div>" + "   name: " + ProdName + "</div>";
                form1.Controls.Add(lbl_ProdName);

                Label lbl_category = new Label();
                lbl_category.Text = "<div>" + "   category: " + category + "</div>" ;
                form1.Controls.Add(lbl_category);

                Label lbl_color = new Label();
                lbl_color.Text = "<div>" + "   color: " + color + "</div>";
                form1.Controls.Add(lbl_color);

                Label lbl_avail = new Label();
                lbl_avail.Text = "<div>" +"    " + availability + "</div>";
                form1.Controls.Add(lbl_avail);

                Label lbl_Prodvendor = new Label();
                lbl_Prodvendor.Text = "<div>"+ "   category: " + ProdVendorName + "</div>";
                form1.Controls.Add(lbl_Prodvendor);

                Label lbl_rate = new Label();
                lbl_rate.Text = "<div>" + "   rating: " + rating + "</div>";
                form1.Controls.Add(lbl_rate);

                Label lbl_price = new Label();
                lbl_price.Text = "<div>" + "   price: " + Prodprice + "</div>";
                form1.Controls.Add(lbl_price);

                Label lbl_finalprice = new Label();
                lbl_finalprice.Text = "<div>" + "   final price: " + Prodfinalprice + "</div>" ;
                form1.Controls.Add(lbl_finalprice);

                Label lbl_descrip = new Label();
                lbl_descrip.Text = "<div>" + "   description: " + descrip + "</div>" + "  <br /> <br />";
                form1.Controls.Add(lbl_descrip);
              

               // Button AddToCart_btn = new Button();
               // AddToCart_btn.Text = "   Add to cart: " ; 
               // form1.Controls.Add(AddToCart_btn);
            
        }
          
        }
        protected void GoCart(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx", true);
        }

        
        
        protected void GoWish(object sender, EventArgs e)
        {
            Response.Redirect("Wishlists.aspx", true);
        }
        protected void GoCredit(object sender, EventArgs e)
        {
            Response.Redirect("AddCreditCard.aspx", true);
        }
        protected void GoTel(object sender, EventArgs e)
        {
            Response.Redirect("AddTelephone.aspx", true);
        }
    }

}
