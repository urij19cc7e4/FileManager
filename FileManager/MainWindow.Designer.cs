namespace FileManager
{
	public partial class MainWindow
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
			this.tableMW = new System.Windows.Forms.TableLayoutPanel();
			this.tableVW = new System.Windows.Forms.TableLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tableMW.SuspendLayout();
			this.tableVW.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableMW
			// 
			this.tableMW.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableMW.ColumnCount = 3;
			this.tableMW.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			this.tableMW.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableMW.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 245F));
			this.tableMW.Controls.Add(this.tableVW, 2, 0);
			this.tableMW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableMW.Location = new System.Drawing.Point(0, 0);
			this.tableMW.Margin = new System.Windows.Forms.Padding(0);
			this.tableMW.Name = "tableMW";
			this.tableMW.RowCount = 1;
			this.tableMW.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableMW.Size = new System.Drawing.Size(960, 480);
			this.tableMW.TabIndex = 0;
			// 
			// tableVW
			// 
			this.tableVW.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableVW.ColumnCount = 1;
			this.tableVW.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableVW.Controls.Add(this.button1, 0, 1);
			this.tableVW.Controls.Add(this.textBox1, 0, 0);
			this.tableVW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableVW.Location = new System.Drawing.Point(714, 1);
			this.tableVW.Margin = new System.Windows.Forms.Padding(0);
			this.tableVW.Name = "tableVW";
			this.tableVW.RowCount = 2;
			this.tableVW.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableVW.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableVW.Size = new System.Drawing.Size(245, 478);
			this.tableVW.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button1.Location = new System.Drawing.Point(60, 417);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(125, 40);
			this.button1.TabIndex = 0;
			this.button1.Text = "Start tests";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(4, 4);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(237, 389);
			this.textBox1.TabIndex = 1;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(960, 480);
			this.Controls.Add(this.tableMW);
			this.MinimumSize = new System.Drawing.Size(978, 527);
			this.Name = "MainWindow";
			this.Text = "File Manager";
			this.tableMW.ResumeLayout(false);
			this.tableVW.ResumeLayout(false);
			this.tableVW.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableMW;
		private System.Windows.Forms.TableLayoutPanel tableVW;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
	}
}