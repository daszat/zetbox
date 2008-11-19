namespace Kistl.Client.Forms.View
{
    partial class WorkspaceView
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
            this._moduleList = new System.Windows.Forms.ListView();
            this._objectClassList = new System.Windows.Forms.ListView();
            this._instancesList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this._viewPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this._viewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _moduleList
            // 
            this._moduleList.Location = new System.Drawing.Point(12, 12);
            this._moduleList.Name = "_moduleList";
            this._moduleList.Size = new System.Drawing.Size(230, 182);
            this._moduleList.TabIndex = 0;
            this._moduleList.UseCompatibleStateImageBehavior = false;
            this._moduleList.View = System.Windows.Forms.View.List;
            this._moduleList.SelectedIndexChanged += new System.EventHandler(this._moduleList_SelectedIndexChanged);
            // 
            // _objectClassList
            // 
            this._objectClassList.Location = new System.Drawing.Point(12, 200);
            this._objectClassList.Name = "_objectClassList";
            this._objectClassList.Size = new System.Drawing.Size(230, 127);
            this._objectClassList.TabIndex = 1;
            this._objectClassList.UseCompatibleStateImageBehavior = false;
            this._objectClassList.View = System.Windows.Forms.View.List;
            this._objectClassList.SelectedIndexChanged += new System.EventHandler(this._objectClassList_SelectedIndexChanged);
            // 
            // _instancesList
            // 
            this._instancesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._instancesList.Location = new System.Drawing.Point(13, 334);
            this._instancesList.Name = "_instancesList";
            this._instancesList.Size = new System.Drawing.Size(229, 139);
            this._instancesList.TabIndex = 2;
            this._instancesList.UseCompatibleStateImageBehavior = false;
            this._instancesList.View = System.Windows.Forms.View.List;
            this._instancesList.SelectedIndexChanged += new System.EventHandler(this._instancesList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // _viewPanel
            // 
            this._viewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._viewPanel.Controls.Add(this.label2);
            this._viewPanel.Location = new System.Drawing.Point(248, 28);
            this._viewPanel.Name = "_viewPanel";
            this._viewPanel.Size = new System.Drawing.Size(392, 445);
            this._viewPanel.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // WorkspaceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 485);
            this.Controls.Add(this._viewPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._instancesList);
            this.Controls.Add(this._objectClassList);
            this.Controls.Add(this._moduleList);
            this.MinimumSize = new System.Drawing.Size(668, 521);
            this.Name = "WorkspaceView";
            this.Text = "WorkspaceView";
            this._viewPanel.ResumeLayout(false);
            this._viewPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView _moduleList;
        private System.Windows.Forms.ListView _objectClassList;
        private System.Windows.Forms.ListView _instancesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel _viewPanel;
        private System.Windows.Forms.Label label2;
    }
}