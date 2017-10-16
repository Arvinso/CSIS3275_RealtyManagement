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
   
    public partial class Saleschargetotalform : Form
    {
        DateTime Begindateforreport;
        DateTime Enddateforreport;
        public Saleschargetotalform(DateTime BeginDate, DateTime EndDate)
        {
            InitializeComponent();

            Begindateforreport = BeginDate;
            Enddateforreport = EndDate;
        }

        private void Saleschargetotalform_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'AgentsaleschargeDataSet.Agent' table. You can move, or remove it, as needed.
            this.AgentTableAdapter.Fill(this.AgentsaleschargeDataSet.Agent);
            // TODO: This line of code loads data into the 'AgentsaleschargeDataSet.Property' table. You can move, or remove it, as needed.
            this.PropertyTableAdapter.Fill(this.AgentsaleschargeDataSet.Property, Begindateforreport, Enddateforreport);

            this.reportViewer1.RefreshReport();
        }
    }
}
