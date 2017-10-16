using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace codebusters
{
    public partial class Manager : Form
    {
        MySqlConnection connection;
        private string userfname;
        private string userlname;

        private string agentfirstname;
        private string agentlastname;

        private string customerfirstname;
        private string customerlastname;

        private int manid;
        private int targetproperty;

        private int agentidforsale;
        private int propertyprice;
        private int transactionidcheck = 0;

        //============ for commision calculation
        private string flatrate;
        private string additionalrate;
        private string gst;
        //============ for commission calculation

        private int customerid;

        DateTime Transactiondate;
        DateTime Registrationdate;


        XmlTextReader reader = new XmlTextReader("data.xml");

        public Manager(string Managerfirstname, string Managerlastname, int Managerloggedinid)
        {
            InitializeComponent();
            userfname = Managerfirstname;
            userlname = Managerlastname;
            manid = Managerloggedinid;
            loggedinman.Text = userfname + " " + userlname;            
            loggedinastab2.Text = userfname + " " + userlname;
        }
        private void Manager_Load(object sender, EventArgs e)
        {
            connect();

            MANCustomer_owned_properties.View = View.Details;
            MANCustomer_owned_properties.GridLines = true;
            MANCustomer_owned_properties.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            MANCustomer_owned_properties.FullRowSelect = true;

            MANCustomer_owned_properties.Columns.Add("Property ID", 70, HorizontalAlignment.Left);
            MANCustomer_owned_properties.Columns.Add("Property Type", 110, HorizontalAlignment.Left);
            MANCustomer_owned_properties.Columns.Add("Property Area", 90, HorizontalAlignment.Left);
            MANCustomer_owned_properties.Columns.Add("Property Location", 150, HorizontalAlignment.Left);
            MANCustomer_owned_properties.Columns.Add("Property Price", 90, HorizontalAlignment.Left);
            MANCustomer_owned_properties.Columns.Add("Property Registration Date", 150, HorizontalAlignment.Left); 
        }

        private void connect()
        {
            string connectionString = "";

            string[] names = { "SERVER", "database", "uid", "password" };
            string[] values = new string[3];
            for (int i = 0; i < names.Length; i++)
            {
                reader.ReadToFollowing(names[i]);
                reader.Read();
                connectionString += names[i] + "=" + reader.Value + ";";
            }
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }
            if (connection != null)
                connection.Close();
        }

        private void exitform(object sender, FormClosingEventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
        }

        private void ontextchangepropertyid(object sender, EventArgs e)
        {
            Agentfirstlastname.Clear();
            Customerfirstlastname.Clear();
            propertypriceforsale.Clear();

            transactionidpreview.Clear();
            transactiondatepreview.Clear();
            customerregpreview.Clear();
            agentassignedpreview.Clear();
            customerpreview.Clear();


            flatratepreview.Clear();
            additionalpreview.Clear();
            gstpreview.Clear();
            transactionidcheck = 0;


            if (int.TryParse(managersalespropID.Text.ToString().Trim(), out targetproperty))
            {
                string query = "select P.Agent_ID , T.Transaction_ID, T.Trans_date, T.Flatrate, T.Additional, T.GST, C.Customer_ID, C.Cust_fname, C.Cust_lname, A.Agent_fname,A.Agent_lname, P.Prop_price, R.Reg_date ";
                query += "from Property P ";
                query += "inner join Agent A on A.Agent_ID = P.Agent_ID ";
                query += "inner join Registration R on R.Property_ID = P.Property_ID ";
                query += "inner join Customer C on C.Customer_ID = R.Customer_ID ";
                query += "left join Transaction T on T.Property_ID = P.Property_ID ";
                query += "where P.Property_ID = \"" + targetproperty + "\"";
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter mcmd = new MySqlDataAdapter();
                    MySqlDataReader reader;
                    mcmd.SelectCommand = cmd;

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        propertyprice = int.Parse(reader["Prop_price"].ToString().Trim());
                        propertypriceforsale.Text = propertyprice.ToString();
                        agentidforsale = int.Parse(reader["Agent_ID"].ToString().Trim());

                        agentfirstname = reader["Agent_fname"].ToString();
                        agentlastname = reader["Agent_lname"].ToString();

                        customerfirstname = reader["Cust_fname"].ToString();
                        customerlastname = reader["Cust_lname"].ToString();

                        Agentfirstlastname.Text = agentfirstname + " " + agentlastname;
                        Customerfirstlastname.Text = customerfirstname + " " + customerlastname;

                        Registrationdate = (DateTime)reader["Reg_date"];
                        //Commission.Text = calculate_commission(propertyprice).ToString();

                        customerid = int.Parse(reader["Customer_ID"].ToString().Trim());

                        try
                        {
                            transactionidcheck = int.Parse(reader["Transaction_ID"].ToString().Trim());
                            Transactiondate = (DateTime)reader["Trans_date"];
                            MessageBox.Show("This property has already been sold on: " + Transactiondate.ToString("d"));
                            flatrate = reader["Flatrate"].ToString();
                            additionalrate = reader["Additional"].ToString();
                            gst = reader["GST"].ToString();

                            transactionidpreview.Text = transactionidcheck.ToString();
                            transactiondatepreview.Text = Transactiondate.ToString("d");
                            customerpreview.Text = customerfirstname + " " + customerlastname;
                            customerregpreview.Text = Registrationdate.ToString("d");
                            flatratepreview.Text = flatrate;
                            additionalpreview.Text = additionalrate;
                            gstpreview.Text = gst;
                            agentassignedpreview.Text = agentfirstname + " " + agentlastname;
                        }
                        catch
                        {

                        }
                    }
                }
                catch
                {
                    MessageBox.Show("error occured");
                }
                connection.Close();
            }
        }

        private void registersaleproperty_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                if ((managersalespropID.Text.Trim() == "") ||
                   (Agentfirstlastname.Text.Trim() == "") ||
                   (Customerfirstlastname.Text.Trim() == ""))
                {
                    MessageBox.Show("Please Lookup first!");
                    return;
                }

                if (transactionidcheck != 0)
                {
                    MessageBox.Show("property is already sold");
                    MANreset_Click(sender, e);
                    return;
                }

                connection.Open();
                string nameOfProcedure = "add_sale";
                MySqlCommand cmd = new MySqlCommand(nameOfProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("agentid", agentidforsale);
                cmd.Parameters["agentid"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("propertyid", int.Parse(managersalespropID.Text.ToString()));
                cmd.Parameters["propertyid"].Direction = ParameterDirection.Input;

                DateTime thisDay = DateTime.Today;

                cmd.Parameters.AddWithValue("thisday", thisDay);
                cmd.Parameters["thisday"].Direction = ParameterDirection.Input;
                //// calculate the commission first


                Decimal calc_flatrate;
                Decimal calc_additional;
                Decimal calc_gst;
                Decimal calc_subtotal;
                if (propertyprice <= 100000)
                {
                    calc_flatrate = propertyprice * (Decimal)0.07;
                    calc_additional = 0.00M;
                    calc_subtotal = calc_flatrate + calc_additional;
                    calc_gst = calc_subtotal * (Decimal)0.05;
                }
                else
                {
                    calc_flatrate = ((propertyprice + 100000) - propertyprice) * (Decimal)0.07;
                    calc_additional = (propertyprice - 100000) * (Decimal)0.03;
                    calc_subtotal = calc_flatrate + calc_additional;
                    calc_gst = calc_subtotal * (Decimal)0.05;
                }

                //MessageBox.Show("flatrate = " + calc_flatrate.ToString() + " additional = " + calc_additional.ToString() + " gst = " + calc_gst.ToString());



                cmd.Parameters.AddWithValue("flatrate", calc_flatrate);
                cmd.Parameters["flatrate"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("additional", calc_additional);
                cmd.Parameters["additional"].Direction = ParameterDirection.Input;

                //cmd.Parameters.AddWithValue("subtotal", calc_subtotal);
                //cmd.Parameters["subtotal"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("gst", calc_gst);
                cmd.Parameters["gst"].Direction = ParameterDirection.Input;

                MySqlParameter retId = new MySqlParameter("returnId", MySqlDbType.Int16);
                retId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retId);

                cmd.ExecuteNonQuery();

                connection.Close();

                int transactionadded = 0;
                transactionadded = int.Parse(cmd.Parameters["returnId"].Value.ToString());
                if (transactionadded > 0)
                {
                    MessageBox.Show("Transaction added with id: " + transactionadded);

                    try //// fill in the preview section
                    {
                        string query = "select P.Agent_ID , T.Transaction_ID, T.Trans_date, T.Flatrate, T.Additional, T.GST, C.Customer_ID, C.Cust_fname, C.Cust_lname, A.Agent_fname,A.Agent_lname, P.Prop_price, R.Reg_date ";
                        query += "from Property P ";
                        query += "inner join Agent A on A.Agent_ID = P.Agent_ID ";
                        query += "inner join Registration R on R.Property_ID = P.Property_ID ";
                        query += "inner join Customer C on C.Customer_ID = R.Customer_ID ";
                        query += "left join Transaction T on T.Property_ID = P.Property_ID ";
                        query += "where P.Property_ID = \"" + targetproperty + "\"";

                        connection.Open();
                        MySqlCommand previewcmd = new MySqlCommand(query, connection);
                        MySqlDataAdapter mcmd = new MySqlDataAdapter();
                        MySqlDataReader reader;
                        mcmd.SelectCommand = previewcmd;

                        reader = previewcmd.ExecuteReader();

                        if (reader.Read())
                        {
                            transactionidpreview.Text = reader["Transaction_ID"].ToString();
                            transactiondatepreview.Text = ((DateTime)reader["Trans_date"]).ToString("d");
                            customerpreview.Text = reader["Cust_fname"].ToString() + " " + reader["Cust_lname"].ToString();
                            customerregpreview.Text = ((DateTime)reader["Reg_date"]).ToString("d");
                            flatratepreview.Text = reader["Flatrate"].ToString();
                            additionalpreview.Text = reader["Additional"].ToString();
                            gstpreview.Text = reader["GST"].ToString();
                            agentassignedpreview.Text = reader["Agent_fname"].ToString() + " " + reader["Agent_lname"].ToString();
                        }
                        connection.Close();
                    }
                    catch (Exception eex)
                    {
                        MessageBox.Show(eex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("An Error Occurred");
                }
            }
        }

        /*
        private float calculate_commission(int propertyprice)
        {
            float total_commission;
            if(propertyprice <= 100000)
            {
                total_commission = propertyprice * (float)0.07;
                total_commission = total_commission / 2;                
            }
            else
            {
                float fixed_partcommission = (float)0.07 * 100000;
                propertyprice -= 100000;
                float second_partcommission = propertyprice * (float)0.03;
                total_commission = fixed_partcommission + second_partcommission;
                total_commission = total_commission/2;
            }
            return total_commission;
        }*/

        private void MANreset_Click(object sender, EventArgs e)
        {
            transactionidpreview.Clear();
            Agentfirstlastname.Clear();
            Customerfirstlastname.Clear();
            propertypriceforsale.Clear();
            managersalespropID.Clear();

            transactiondatepreview.Clear();
            customerpreview.Clear();
            transactiondatepreview.Clear();
            customerregpreview.Clear();
            agentassignedpreview.Clear();
            flatratepreview.Clear();
            additionalpreview.Clear();
            gstpreview.Clear();

            transactionidcheck = 0;
        }

        private void showreport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(transactiondatepreview.Text))
            {
                frmcommissionreport frmcomm = new frmcommissionreport(targetproperty);
                frmcomm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Report Available");
            }
        }

        private void Searchsalechargelist_Click(object sender, EventArgs e)
        {
            Saleschargegrid.Rows.Clear();

            if (salechargefromdate.Checked == true && salechargetodate.Checked == true)
            {
                try
                {
                    connection.Open();
                    string nameOfProcedure = "search_agent_total";
                    MySqlCommand cmd = new MySqlCommand(nameOfProcedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DateTime Begindate = new DateTime(salechargefromdate.Value.Year, salechargefromdate.Value.Month, salechargefromdate.Value.Day);
                    DateTime Enddate = new DateTime(salechargetodate.Value.Year, salechargetodate.Value.Month, salechargetodate.Value.Day);

                    cmd.Parameters.AddWithValue("begindate", Begindate);
                    cmd.Parameters["begindate"].Direction = ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("enddate", Enddate);
                    cmd.Parameters["enddate"].Direction = ParameterDirection.Input;


                    try
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int countsales = int.Parse(reader["numbertrans"].ToString());
                            int salescharge = countsales * 250;

                            Saleschargegrid.Rows.Add(
                                reader["Agent_fname"].ToString() + " " + reader["Agent_lname"].ToString(),
                                countsales.ToString(), salescharge.ToString()
                            );
                        }
                    }
                    catch (Exception ex1)
                    {
                        MessageBox.Show(ex1.Message);
                    }
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message);
                }
                connection.Close();
            }
            else
            {
                MessageBox.Show("Please choose date range");
            }

        }

        private void generatereport_Click(object sender, EventArgs e)
        {
            if (salechargefromdate.Checked == true && salechargetodate.Checked == true)
            {
                DateTime Begindate = new DateTime(salechargefromdate.Value.Year, salechargefromdate.Value.Month, salechargefromdate.Value.Day);
                DateTime Enddate = new DateTime(salechargetodate.Value.Year, salechargetodate.Value.Month, salechargetodate.Value.Day);

                Saleschargetotalform frmcomm = new Saleschargetotalform(Begindate, Enddate);
                frmcomm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Report Available for that date range, Please search again");
            }
        }

        private void MSearchproperty_Click(object sender, EventArgs e)
        {
            int searchparamcount = 0;

            string query = "select P.* , P.Agent_ID , T.Transaction_ID, T.Trans_date, T.Flatrate, T.Additional, T.GST, C.Customer_ID, C.Cust_fname, C.Cust_lname, C.Cust_address, C.Cust_phone, A.Agent_fname,A.Agent_lname, P.Prop_price, R.Reg_date ";
            query += "from Property P inner join Agent A on A.Agent_ID = P.Agent_ID inner join Registration R on R.Property_ID = P.Property_ID ";
            query += "inner join Customer C on C.Customer_ID = R.Customer_ID ";
            query += "left join Transaction T on T.Property_ID = P.Property_ID where ";

            if (!String.IsNullOrEmpty(Manager_sproperty_ID.Text))
            {
                query += "P.Property_ID =" + "\"" + Manager_sproperty_ID.Text.ToString() + "\"";
                searchparamcount++;
            }

            if (!String.IsNullOrEmpty(Manager_sproperty_custID.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "C.Customer_ID =" + "\"" + Manager_sproperty_custID.Text.ToString() + "\"";
                searchparamcount++;
            }

            if (!String.IsNullOrEmpty(Manager_sproperty_agentID.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "A.Agent_ID =" + "\"" + Manager_sproperty_agentID.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Manager_sproperty_be.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_Br =" + "\"" + Manager_sproperty_be.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Manager_sproperty_ba.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_Ba =" + "\"" + Manager_sproperty_ba.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Manager_sproperty_type.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_type =" + "\"" + Manager_sproperty_type.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Manager_sproperty_year.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_Yr =" + "\"" + Manager_sproperty_year.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Manager_sproperty_livarea.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_area =" + "\"" + Manager_sproperty_livarea.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Manager_sproperty_price.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_price =" + "\"" + Manager_sproperty_price.ToString() + "\"";
                searchparamcount++;
            }
            if (searchparamcount == 0)
            {
                query = "select P.* , P.Agent_ID , T.Transaction_ID, T.Trans_date, T.Flatrate, T.Additional, T.GST, C.Customer_ID, C.Cust_fname, C.Cust_lname, C.Cust_address, C.Cust_phone, A.Agent_fname,A.Agent_lname, P.Prop_price, R.Reg_date ";
                query += "from Property P inner join Agent A on A.Agent_ID = P.Agent_ID inner join Registration R on R.Property_ID = P.Property_ID ";
                query += "inner join Customer C on C.Customer_ID = R.Customer_ID ";
                query += "left join Transaction T on T.Property_ID = P.Property_ID";
            }

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter mcmd = new MySqlDataAdapter();
                MySqlDataReader reader;
                mcmd.SelectCommand = cmd;

                reader = cmd.ExecuteReader();

                Manpropsearchdatagrid.Rows.Clear();

                while (reader.Read())
                {
                    this.Manpropsearchdatagrid.Rows.Add(
                         reader["Property_ID"].ToString(),
                         reader["Prop_type"].ToString(),
                         reader["Prop_area"].ToString(),
                         reader["Prop_Br"].ToString(),
                         reader["Prop_Ba"].ToString(),
                         reader["Prop_Yr"].ToString(),
                         reader["Prop_Loc"].ToString(),
                         reader["Prop_price"].ToString(),
                         reader["Transaction_ID"].ToString(),
                         reader["Customer_ID"].ToString(),
                         reader["Cust_fname"].ToString(),
                         reader["Cust_lname"].ToString(),
                         reader["Agent_ID"].ToString(),
                         reader["Agent_fname"].ToString(),
                         reader["Agent_lname"].ToString()
                         );
                }
            }
            catch(Exception exer)
            {
                MessageBox.Show(exer.Message);
            }
            connection.Close();
        }

        private void MReset_Click(object sender, EventArgs e)
        {
            Manager_sproperty_ID.Clear();
            Manager_sproperty_custID.Clear();
            Manager_sproperty_agentID.Clear();
            Manager_sproperty_be.ResetText();
            Manager_sproperty_ba.ResetText();
            Manager_sproperty_type.ResetText();
            Manager_sproperty_year.Clear();
            Manager_sproperty_livarea.Clear();
            Manager_sproperty_price.Clear();
            Manpropsearchdatagrid.Rows.Clear();
        }

        private void MANSearchCustomer_Click(object sender, EventArgs e)
        {
            int searchparamcount = 0;
            string query = "select * from Customer where ";

            if (!String.IsNullOrEmpty(MANSearch_custID.Text))
            {
                query += "Customer_ID =" + "\"" + MANSearch_custID.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(MANSearch_custFN.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_fname =" + "\"" + MANSearch_custFN.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(MANSearch_custLN.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_lname =" + "\"" + MANSearch_custLN.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(MANSearch_custPhone.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_phone =" + "\"" + MANSearch_custPhone.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(MANSearch_custGovid.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_govid =" + "\"" + MANSearch_custGovid.Text.ToString() + "\"";
                searchparamcount++;
            }
           
            if (searchparamcount == 0)
            {
                query = "select * from Customer";
            } 
    

             try 
            { 
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mcmd = new MySqlDataAdapter();
            MySqlDataReader reader;
            mcmd.SelectCommand = cmd;

            reader = cmd.ExecuteReader();

            MANSearch_Cust_Datagrid.Rows.Clear();

                while (reader.Read())
                {
                    this.MANSearch_Cust_Datagrid.Rows.Add(
                         reader["Customer_ID"].ToString(),
                         reader["Cust_fname"].ToString(),
                         reader["Cust_lname"].ToString(),
                         reader["Cust_phone"].ToString(),
                         reader["Cust_govid"].ToString(),
                         reader["Cust_address"].ToString()
                         );          
                } 
            }
            catch
            {
                MessageBox.Show("Insert less parameters for more results");
            }

            connection.Close();
        }

        private void MANSearchcustdatagridclickcell(object sender, DataGridViewCellEventArgs e)
        {
            MANSearch_custID.Clear();

            try
            {
                if (e.RowIndex >= 0)
                {
                    MANSearch_custID.Text = MANSearch_Cust_Datagrid.Rows[e.RowIndex].Cells["CustsearchID"].Value.ToString();
                    MANSearch_custFN.Text = MANSearch_Cust_Datagrid.Rows[e.RowIndex].Cells["CustsearchFN"].Value.ToString();
                    MANSearch_custLN.Text = MANSearch_Cust_Datagrid.Rows[e.RowIndex].Cells["CustsearchLN"].Value.ToString();
                    MANSearch_custPhone.Text = MANSearch_Cust_Datagrid.Rows[e.RowIndex].Cells["Custsearchphone"].Value.ToString();
                    MANSearch_custGovid.Text = MANSearch_Cust_Datagrid.Rows[e.RowIndex].Cells["Custsearchgovid"].Value.ToString();                   
                }

                string query = "Select P.*, R.Reg_ID , R.Reg_date from Property P ";
                query += "inner join Registration R on R.Property_ID = P.Property_ID ";
                query += "inner join Customer C on C.Customer_ID = R.Customer_ID ";
                query += "where C.Customer_ID = \"" + MANSearch_custID.Text.ToString() + "\"";

                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter mcmd = new MySqlDataAdapter();
                    MySqlDataReader reader;
                    mcmd.SelectCommand = cmd;


                    reader = cmd.ExecuteReader();

                    ListViewItem item;
                    string[] data = new string[6];

                    MANCustomer_owned_properties.Items.Clear();
                    while (reader.Read())
                    {
                        data[0] = reader["Property_ID"].ToString().Trim();
                        data[1] = reader["Prop_type"].ToString().Trim();
                        data[2] = reader["Prop_area"].ToString().Trim();
                        data[3] = reader["Prop_Loc"].ToString().Trim();
                        data[4] = reader["Prop_price"].ToString().Trim();
                        data[5] = ((DateTime)reader["Reg_date"]).ToString("d");

                        item = new ListViewItem(data);
                        MANCustomer_owned_properties.Items.Add(item);
                    }
                }
                catch
                {
                    MessageBox.Show("Insert less parameters for more results");
                }
                connection.Close();
            }
            catch
            {

            }            
        }

        private void MANResetcust_Click(object sender, EventArgs e)
        {
            MANSearch_custID.Clear();
            MANSearch_custFN.Clear();
            MANSearch_custLN.Clear();
            MANSearch_custPhone.Clear();
            MANSearch_custGovid.Clear();
            MANCustomer_owned_properties.Items.Clear();
            MANSearch_Cust_Datagrid.Rows.Clear();
        }        
    }
}








   

