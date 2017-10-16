namespace codebusters
{
    partial class Saleschargetotalform
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AgentsaleschargeDataSet = new codebusters.AgentsaleschargeDataSet();
            this.AgentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AgentTableAdapter = new codebusters.AgentsaleschargeDataSetTableAdapters.AgentTableAdapter();
            this.PropertyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PropertyTableAdapter = new codebusters.AgentsaleschargeDataSetTableAdapters.PropertyTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.AgentsaleschargeDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Agentsaleschargedataset";
            reportDataSource1.Value = this.PropertyBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "codebusters.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1533, 706);
            this.reportViewer1.TabIndex = 0;
            // 
            // AgentsaleschargeDataSet
            // 
            this.AgentsaleschargeDataSet.DataSetName = "AgentsaleschargeDataSet";
            this.AgentsaleschargeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AgentBindingSource
            // 
            this.AgentBindingSource.DataMember = "Agent";
            this.AgentBindingSource.DataSource = this.AgentsaleschargeDataSet;
            // 
            // AgentTableAdapter
            // 
            this.AgentTableAdapter.ClearBeforeFill = true;
            // 
            // PropertyBindingSource
            // 
            this.PropertyBindingSource.DataMember = "Property";
            this.PropertyBindingSource.DataSource = this.AgentsaleschargeDataSet;
            // 
            // PropertyTableAdapter
            // 
            this.PropertyTableAdapter.ClearBeforeFill = true;
            // 
            // Saleschargetotalform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1533, 706);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Saleschargetotalform";
            this.Text = "Saleschargetotalform";
            this.Load += new System.EventHandler(this.Saleschargetotalform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AgentsaleschargeDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource AgentBindingSource;
        private AgentsaleschargeDataSet AgentsaleschargeDataSet;
        private System.Windows.Forms.BindingSource PropertyBindingSource;
        private AgentsaleschargeDataSetTableAdapters.AgentTableAdapter AgentTableAdapter;
        private AgentsaleschargeDataSetTableAdapters.PropertyTableAdapter PropertyTableAdapter;
    }
}