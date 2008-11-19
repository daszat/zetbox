namespace Kistl.Client.Forms.View
{
    partial class DataObjectFullView
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
            this._objectName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _objectName
            // 
            this._objectName.AutoSize = true;
            this._objectName.Dock = System.Windows.Forms.DockStyle.Top;
            this._objectName.Location = new System.Drawing.Point(0, 0);
            this._objectName.Name = "_objectName";
            this._objectName.Size = new System.Drawing.Size(68, 13);
            this._objectName.TabIndex = 1;
            this._objectName.Text = "(  loading...  )";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 213);
            this.panel1.TabIndex = 2;
            // 
            // DataObjectFullView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._objectName);
            this.Name = "DataObjectFullView";
            this.Size = new System.Drawing.Size(203, 226);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _objectName;
        private System.Windows.Forms.Panel panel1;

    }
}
