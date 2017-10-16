namespace codebusters
{
    partial class frmcommissionreport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.TransactionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.codebustersDataSet1 = new codebusters.codebustersDataSet1();
            this.TransactionTableAdapter = new codebusters.codebustersDataSet1TableAdapters.TransactionTableAdapter();
            this.CustomerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CustomerTableAdapter = new codebusters.codebustersDataSet1TableAdapters.CustomerTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codebustersDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Transaction";
            reportDataSource1.Value = this.TransactionBindingSource;
            reportDataSource2.Name = "Customer";
            reportDataSource2.Value = this.CustomerBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "codebusters.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1452, 604);
            this.reportViewer1.TabIndex = 0;
            // 
            // TransactionBindingSource
            // 
            this.TransactionBindingSource.DataMember = "Transaction";
            this.TransactionBindingSource.DataSource = this.codebustersDataSet1;
            // 
            // codebustersDataSet1
            // 
            this.codebustersDataSet1.DataSetName = "codebustersDataSet1";
            this.codebustersDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TransactionTableAdapter
            // 
            this.TransactionTableAdapter.ClearBeforeFill = true;
            // 
            // CustomerBindingSource
            // 
            this.CustomerBindingSource.DataMember = "Customer";
            this.CustomerBindingSource.DataSource = this.codebustersDataSet1;
            // 
            // CustomerTableAdapter
            // 
            this.CustomerTableAdapter.ClearBeforeFill = true;
            // 
            // frmcommissionreport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 604);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmcommissionreport";
            this.Text = "frmcommissionreport";
            this.Load += new System.EventHandler(this.frmcommissionreport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TransactionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codebustersDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource TransactionBindingSource;
        private codebustersDataSet1 codebustersDataSet1;
        private codebustersDataSet1TableAdapters.TransactionTableAdapter TransactionTableAdapter;
        private System.Windows.Forms.BindingSource CustomerBindingSource;
        private codebustersDataSet1TableAdapters.CustomerTableAdapter CustomerTableAdapter;
    }
}