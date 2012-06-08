namespace Kistl.Client.Forms.View
{
    partial class DataObjectListView
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
            this._label = new System.Windows.Forms.Label();
            this._objectList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // _label
            // 
            this._label.Dock = System.Windows.Forms.DockStyle.Top;
            this._label.Location = new System.Drawing.Point(0, 0);
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(227, 13);
            this._label.TabIndex = 0;
            this._label.Text = "Objekt Liste";
            // 
            // _objectList
            // 
            this._objectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._objectList.Location = new System.Drawing.Point(0, 13);
            this._objectList.Name = "_objectList";
            this._objectList.Size = new System.Drawing.Size(227, 91);
            this._objectList.TabIndex = 1;
            this._objectList.UseCompatibleStateImageBehavior = false;
            // 
            // DataObjectListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._objectList);
            this.Controls.Add(this._label);
            this.Name = "DataObjectListView";
            this.Size = new System.Drawing.Size(227, 104);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _label;
        private System.Windows.Forms.ListView _objectList;
    }
}
