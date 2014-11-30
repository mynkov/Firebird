namespace FirebirdHelper
{
    partial class MainForm
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpReplicas = new System.Windows.Forms.TabPage();
            this.tpDocumentsLog = new System.Windows.Forms.TabPage();
            this.gvDocumentsLog = new System.Windows.Forms.DataGridView();
            this.tpSendBlockLog = new System.Windows.Forms.TabPage();
            this.gvSendBlockLog = new System.Windows.Forms.DataGridView();
            this.gvReplicas = new System.Windows.Forms.DataGridView();
            this.tcMain.SuspendLayout();
            this.tpReplicas.SuspendLayout();
            this.tpDocumentsLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentsLog)).BeginInit();
            this.tpSendBlockLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSendBlockLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReplicas)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpReplicas);
            this.tcMain.Controls.Add(this.tpDocumentsLog);
            this.tcMain.Controls.Add(this.tpSendBlockLog);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(784, 562);
            this.tcMain.TabIndex = 0;
            // 
            // tpReplicas
            // 
            this.tpReplicas.Controls.Add(this.gvReplicas);
            this.tpReplicas.Location = new System.Drawing.Point(4, 22);
            this.tpReplicas.Name = "tpReplicas";
            this.tpReplicas.Padding = new System.Windows.Forms.Padding(3);
            this.tpReplicas.Size = new System.Drawing.Size(776, 536);
            this.tpReplicas.TabIndex = 0;
            this.tpReplicas.Text = "Replicas";
            this.tpReplicas.UseVisualStyleBackColor = true;
            // 
            // tpDocumentsLog
            // 
            this.tpDocumentsLog.Controls.Add(this.gvDocumentsLog);
            this.tpDocumentsLog.Location = new System.Drawing.Point(4, 22);
            this.tpDocumentsLog.Name = "tpDocumentsLog";
            this.tpDocumentsLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpDocumentsLog.Size = new System.Drawing.Size(776, 536);
            this.tpDocumentsLog.TabIndex = 1;
            this.tpDocumentsLog.Text = "DocumentsLog";
            this.tpDocumentsLog.UseVisualStyleBackColor = true;
            // 
            // gvDocumentsLog
            // 
            this.gvDocumentsLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvDocumentsLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDocumentsLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvDocumentsLog.Location = new System.Drawing.Point(3, 3);
            this.gvDocumentsLog.Name = "gvDocumentsLog";
            this.gvDocumentsLog.Size = new System.Drawing.Size(770, 530);
            this.gvDocumentsLog.TabIndex = 0;
            // 
            // tpSendBlockLog
            // 
            this.tpSendBlockLog.Controls.Add(this.gvSendBlockLog);
            this.tpSendBlockLog.Location = new System.Drawing.Point(4, 22);
            this.tpSendBlockLog.Name = "tpSendBlockLog";
            this.tpSendBlockLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpSendBlockLog.Size = new System.Drawing.Size(776, 536);
            this.tpSendBlockLog.TabIndex = 2;
            this.tpSendBlockLog.Text = "SendBlockLog";
            this.tpSendBlockLog.UseVisualStyleBackColor = true;
            // 
            // gvSendBlockLog
            // 
            this.gvSendBlockLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSendBlockLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSendBlockLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvSendBlockLog.Location = new System.Drawing.Point(3, 3);
            this.gvSendBlockLog.Name = "gvSendBlockLog";
            this.gvSendBlockLog.Size = new System.Drawing.Size(770, 530);
            this.gvSendBlockLog.TabIndex = 0;
            // 
            // gvReplicas
            // 
            this.gvReplicas.AllowUserToOrderColumns = true;
            this.gvReplicas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvReplicas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvReplicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvReplicas.Location = new System.Drawing.Point(3, 3);
            this.gvReplicas.Name = "gvReplicas";
            this.gvReplicas.Size = new System.Drawing.Size(770, 530);
            this.gvReplicas.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tcMain);
            this.Name = "MainForm";
            this.Text = "FirebirdHelper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcMain.ResumeLayout(false);
            this.tpReplicas.ResumeLayout(false);
            this.tpDocumentsLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentsLog)).EndInit();
            this.tpSendBlockLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSendBlockLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReplicas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpReplicas;
        private System.Windows.Forms.TabPage tpDocumentsLog;
        private System.Windows.Forms.TabPage tpSendBlockLog;
        private System.Windows.Forms.DataGridView gvDocumentsLog;
        private System.Windows.Forms.DataGridView gvSendBlockLog;
        private System.Windows.Forms.DataGridView gvReplicas;
    }
}

