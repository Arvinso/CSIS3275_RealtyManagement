using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codebusters
{
    public partial class frmcommissionreport : Form
    {
        int propertyid;
        public frmcommissionreport(int property_id)
        {
            propertyid = property_id;
            InitializeComponent();
        }

        private void frmcommissionreport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'codebustersDataSet1.Transaction' table. You can move, or remove it, as needed.
            this.TransactionTableAdapter.Fill(this.codebustersDataSet1.Transaction);
            // TODO: This line of code loads data into the 'codebustersDataSet1.Customer' table. You can move, or remove it, as needed.
            this.CustomerTableAdapter.Fill(this.codebustersDataSet1.Customer);
            // TODO: This line of code loads data into the 'codebustersDataSet1.Transaction' table. You can move, or remove it, as needed.
            this.TransactionTableAdapter.FillBy(this.codebustersDataSet1.Transaction, propertyid);
           
            this.reportViewer1.RefreshReport();
        }
       
    }
}
