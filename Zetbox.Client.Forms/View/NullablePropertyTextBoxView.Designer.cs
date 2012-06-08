namespace Kistl.Client.Forms.View
{
    partial class NullablePropertyTextBoxView
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
            this.components = new System.ComponentModel.Container();
            this._label = new System.Windows.Forms.Label();
            this._valueBox = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // _label
            // 
            this._label.AutoSize = true;
            this._label.Dock = System.Windows.Forms.DockStyle.Left;
            this._label.Location = new System.Drawing.Point(0, 0);
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(29, 13);
            this._label.TabIndex = 0;
            this._label.Text = "label";
            // 
            // _valueBox
            // 
            this._valueBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._valueBox.Location = new System.Drawing.Point(29, 0);
            this._valueBox.Name = "_valueBox";
            this._valueBox.Size = new System.Drawing.Size(284, 20);
            this._valueBox.TabIndex = 1;
            this._valueBox.TextChanged += new System.EventHandler(this._valueBox_TextChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // NullablePropertyTextBoxView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._valueBox);
            this.Controls.Add(this._label);
            this.Name = "NullablePropertyTextBoxView";
            this.Size = new System.Drawing.Size(313, 20);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _label;
        private System.Windows.Forms.TextBox _valueBox;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
