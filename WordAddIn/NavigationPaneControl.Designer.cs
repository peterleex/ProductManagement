namespace WordAddIn
{
    partial class NavigationPaneControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcModify10Fields = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlQuery = new System.Windows.Forms.Panel();
            this.txtQuestionCode_SystemCode = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dgvQuestion = new System.Windows.Forms.DataGridView();
            this.ColumnQuestionCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQuestionSystemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOperation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblInfo = new System.Windows.Forms.Label();
            this.tcModify10Fields.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestion)).BeginInit();
            this.SuspendLayout();
            // 
            // tcModify10Fields
            // 
            this.tcModify10Fields.Controls.Add(this.tabPage1);
            this.tcModify10Fields.Controls.Add(this.tabPage2);
            this.tcModify10Fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcModify10Fields.Location = new System.Drawing.Point(0, 0);
            this.tcModify10Fields.Name = "tcModify10Fields";
            this.tcModify10Fields.SelectedIndex = 0;
            this.tcModify10Fields.Size = new System.Drawing.Size(554, 811);
            this.tcModify10Fields.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblInfo);
            this.tabPage1.Controls.Add(this.pnlQuery);
            this.tabPage1.Controls.Add(this.dgvQuestion);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(546, 782);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "修改題目";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlQuery
            // 
            this.pnlQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlQuery.Controls.Add(this.txtQuestionCode_SystemCode);
            this.pnlQuery.Controls.Add(this.btnQuery);
            this.pnlQuery.Location = new System.Drawing.Point(6, 4);
            this.pnlQuery.Name = "pnlQuery";
            this.pnlQuery.Size = new System.Drawing.Size(534, 44);
            this.pnlQuery.TabIndex = 4;
            // 
            // txtQuestionCode_SystemCode
            // 
            this.txtQuestionCode_SystemCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuestionCode_SystemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQuestionCode_SystemCode.Location = new System.Drawing.Point(3, 7);
            this.txtQuestionCode_SystemCode.Name = "txtQuestionCode_SystemCode";
            this.txtQuestionCode_SystemCode.Size = new System.Drawing.Size(440, 25);
            this.txtQuestionCode_SystemCode.TabIndex = 1;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnQuery.Location = new System.Drawing.Point(449, 6);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(82, 29);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // dgvQuestion
            // 
            this.dgvQuestion.AllowUserToAddRows = false;
            this.dgvQuestion.AllowUserToDeleteRows = false;
            this.dgvQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQuestion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuestion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnQuestionCode,
            this.ColumnQuestionSystemCode,
            this.ColumnOperation});
            this.dgvQuestion.Location = new System.Drawing.Point(3, 54);
            this.dgvQuestion.Name = "dgvQuestion";
            this.dgvQuestion.ReadOnly = true;
            this.dgvQuestion.RowHeadersVisible = false;
            this.dgvQuestion.RowHeadersWidth = 51;
            this.dgvQuestion.RowTemplate.Height = 27;
            this.dgvQuestion.Size = new System.Drawing.Size(540, 691);
            this.dgvQuestion.TabIndex = 0;
            // 
            // ColumnQuestionCode
            // 
            this.ColumnQuestionCode.HeaderText = "題目編碼";
            this.ColumnQuestionCode.MinimumWidth = 6;
            this.ColumnQuestionCode.Name = "ColumnQuestionCode";
            this.ColumnQuestionCode.ReadOnly = true;
            this.ColumnQuestionCode.Width = 125;
            // 
            // ColumnQuestionSystemCode
            // 
            this.ColumnQuestionSystemCode.HeaderText = "系統編碼";
            this.ColumnQuestionSystemCode.MinimumWidth = 6;
            this.ColumnQuestionSystemCode.Name = "ColumnQuestionSystemCode";
            this.ColumnQuestionSystemCode.ReadOnly = true;
            this.ColumnQuestionSystemCode.Width = 125;
            // 
            // ColumnOperation
            // 
            this.ColumnOperation.HeaderText = "操作";
            this.ColumnOperation.MinimumWidth = 6;
            this.ColumnOperation.Name = "ColumnOperation";
            this.ColumnOperation.ReadOnly = true;
            this.ColumnOperation.Width = 125;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(450, 599);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "檢查錯誤";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Location = new System.Drawing.Point(3, 764);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(41, 15);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "label1";
            // 
            // NavigationPaneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcModify10Fields);
            this.Name = "NavigationPaneControl";
            this.Size = new System.Drawing.Size(554, 811);
            this.tcModify10Fields.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnlQuery.ResumeLayout(false);
            this.pnlQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcModify10Fields;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtQuestionCode_SystemCode;
        private System.Windows.Forms.DataGridView dgvQuestion;
        private System.Windows.Forms.Panel pnlQuery;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuestionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuestionSystemCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnOperation;
        public System.Windows.Forms.Label lblInfo;
    }
}
