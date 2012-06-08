// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
namespace Zetbox.Client.Forms.View
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
            this._propertyPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._propertyPanel.SuspendLayout();
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
            // _propertyPanel
            // 
            this._propertyPanel.ColumnCount = 1;
            this._propertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._propertyPanel.Controls.Add(this.panel1, 0, 0);
            this._propertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyPanel.Location = new System.Drawing.Point(0, 13);
            this._propertyPanel.Name = "_propertyPanel";
            this._propertyPanel.RowCount = 1;
            this._propertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._propertyPanel.Size = new System.Drawing.Size(203, 213);
            this._propertyPanel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 207);
            this.panel1.TabIndex = 0;
            // 
            // DataObjectFullView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._propertyPanel);
            this.Controls.Add(this._objectName);
            this.Name = "DataObjectFullView";
            this.Size = new System.Drawing.Size(203, 226);
            this._propertyPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _objectName;
        private System.Windows.Forms.TableLayoutPanel _propertyPanel;
        private System.Windows.Forms.Panel panel1;

    }
}
