namespace BattleGame
{
    partial class BattleForm
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
            this.listViewUnits = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.DetailsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewUnits
            // 
            this.listViewUnits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DetailsColumn});
            this.listViewUnits.Location = new System.Drawing.Point(12, 12);
            this.listViewUnits.Name = "listViewUnits";
            this.listViewUnits.Size = new System.Drawing.Size(476, 199);
            this.listViewUnits.TabIndex = 0;
            this.listViewUnits.UseCompatibleStateImageBehavior = false;
            this.listViewUnits.View = System.Windows.Forms.View.Details;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(476, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Generate army";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DetailsColumn
            // 
            this.DetailsColumn.Text = "Army details";
            this.DetailsColumn.Width = 370;
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewUnits);
            this.Name = "BattleForm";
            this.Text = "Battle game";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewUnits;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader DetailsColumn;
    }
}

