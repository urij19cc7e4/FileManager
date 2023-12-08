using System.Windows.Forms;

namespace FileManager
{
	public partial class TabPageDirectory : TabPage
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			Clean();

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent()
		{
			this.dirGroup = new System.Windows.Forms.ListViewGroup("Directories", System.Windows.Forms.HorizontalAlignment.Left);
			this.fileGroup = new System.Windows.Forms.ListViewGroup("Files", System.Windows.Forms.HorizontalAlignment.Left);
			this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.dateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.typeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.sizeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.backward = new System.Windows.Forms.Button();
			this.forward = new System.Windows.Forms.Button();
			this.upward = new System.Windows.Forms.Button();
			this.pathText = new System.Windows.Forms.TextBox();
			this.searchText = new System.Windows.Forms.TextBox();
			this.actView = new System.Windows.Forms.TableLayoutPanel();
			this.dirView = new System.Windows.Forms.ListView();
			this.tableTB = new System.Windows.Forms.TableLayoutPanel();
			this.dirView.SuspendLayout();
			this.actView.SuspendLayout();
			this.tableTB.SuspendLayout();
			this.SuspendLayout();
			// 
			// dirGroup
			// 
			this.dirGroup.Header = "Directories";
			this.dirGroup.Name = "dirGroup";
			// 
			// fileGroup
			// 
			this.fileGroup.Header = "Files";
			this.fileGroup.Name = "fileGroup";
			// 
			// nameColumn
			// 
			this.nameColumn.Text = "Name";
			this.nameColumn.Width = 240;
			// 
			// dateColumn
			// 
			this.dateColumn.Text = "Date modified";
			this.dateColumn.Width = 120;
			// 
			// typeColumn
			// 
			this.typeColumn.Text = "Type";
			this.typeColumn.Width = 80;
			// 
			// sizeColumn
			// 
			this.sizeColumn.Text = "Size";
			this.sizeColumn.Width = 60;
			// 
			// backward
			// 
			this.backward.Dock = System.Windows.Forms.DockStyle.Fill;
			this.backward.Image = global::FileManager.Properties.Resources.arrow_left;
			this.backward.Location = new System.Drawing.Point(4, 4);
			this.backward.Margin = new System.Windows.Forms.Padding(5);
			this.backward.Name = "backward";
			this.backward.Size = new System.Drawing.Size(32, 32);
			this.backward.TabIndex = 0;
			this.backward.UseVisualStyleBackColor = true;
			this.backward.Click += new System.EventHandler(this.backward_Click);
			// 
			// forward
			// 
			this.forward.Dock = System.Windows.Forms.DockStyle.Fill;
			this.forward.Image = global::FileManager.Properties.Resources.arrow_right;
			this.forward.Location = new System.Drawing.Point(44, 4);
			this.forward.Margin = new System.Windows.Forms.Padding(5);
			this.forward.Name = "forward";
			this.forward.Size = new System.Drawing.Size(32, 32);
			this.forward.TabIndex = 1;
			this.forward.UseVisualStyleBackColor = true;
			this.forward.Click += new System.EventHandler(this.forward_Click);
			// 
			// upward
			// 
			this.upward.Dock = System.Windows.Forms.DockStyle.Fill;
			this.upward.Image = global::FileManager.Properties.Resources.arrow_up;
			this.upward.Location = new System.Drawing.Point(84, 4);
			this.upward.Margin = new System.Windows.Forms.Padding(5);
			this.upward.Name = "upward";
			this.upward.Size = new System.Drawing.Size(32, 32);
			this.upward.TabIndex = 2;
			this.upward.UseVisualStyleBackColor = true;
			this.upward.Click += new System.EventHandler(this.upward_Click);
			// 
			// pathText
			// 
			this.pathText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pathText.Location = new System.Drawing.Point(125, 7);
			this.pathText.Margin = new System.Windows.Forms.Padding(5);
			this.pathText.Name = "pathText";
			this.pathText.Size = new System.Drawing.Size(279, 22);
			this.pathText.TabIndex = 3;
			this.pathText.TextChanged += new System.EventHandler(this.pathText_TextChanged);
			this.pathText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pathText_KeyDown);
			// 
			// searchText
			// 
			this.searchText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.searchText.Location = new System.Drawing.Point(414, 7);
			this.searchText.Margin = new System.Windows.Forms.Padding(5);
			this.searchText.Name = "searchText";
			this.searchText.Size = new System.Drawing.Size(114, 22);
			this.searchText.TabIndex = 4;
			this.searchText.TextChanged += new System.EventHandler(this.searchText_TextChanged);
			this.searchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchText_KeyDown);
			// 
			// actView
			// 
			this.actView.ColumnCount = 5;
			this.actView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.actView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.actView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.actView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.actView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.actView.Controls.Add(this.backward, 0, 0);
			this.actView.Controls.Add(this.forward, 1, 0);
			this.actView.Controls.Add(this.upward, 2, 0);
			this.actView.Controls.Add(this.pathText, 3, 0);
			this.actView.Controls.Add(this.searchText, 4, 0);
			this.actView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.actView.Location = new System.Drawing.Point(0, 0);
			this.actView.Margin = new System.Windows.Forms.Padding(0);
			this.actView.Name = "actView";
			this.actView.RowCount = 1;
			this.actView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.actView.Size = new System.Drawing.Size(533, 36);
			this.actView.TabStop = false;
			// 
			// dirView
			// 
			this.dirView.CheckBoxes = true;
			this.dirView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.nameColumn, this.dateColumn, this.typeColumn, this.sizeColumn });
			this.dirView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dirView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] { this.dirGroup, this.fileGroup });
			this.dirView.HideSelection = false;
			this.dirView.Location = new System.Drawing.Point(0, 36);
			this.dirView.Margin = new System.Windows.Forms.Padding(0);
			this.dirView.Name = "dirView";
			this.dirView.Size = new System.Drawing.Size(533, 399);
			this.dirView.TabStop = false;
			this.dirView.UseCompatibleStateImageBehavior = false;
			this.dirView.View = System.Windows.Forms.View.Details;
			this.dirView.Click += new System.EventHandler(this.dirView_Click);
			this.dirView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.dirView_ColumnClick);
			this.dirView.DoubleClick += new System.EventHandler(this.dirView_DoubleClick);
			this.dirView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dirView_MouseClick);
			this.dirView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dirView_MouseDoubleClick);
			this.dirView.ItemCheck += new ItemCheckEventHandler(this.dirView_ItemCheck);
			this.dirView.ItemChecked += new ItemCheckedEventHandler(this.dirView_ItemChecked);
			this.dirView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.dirView_ItemSelectionChanged);
			// 
			// tableTB
			// 
			this.tableTB.ColumnCount = 1;
			this.tableTB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableTB.Controls.Add(this.dirView, 0, 1);
			this.tableTB.Controls.Add(this.actView, 0, 0);
			this.tableTB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableTB.Location = new System.Drawing.Point(0, 0);
			this.tableTB.Margin = new System.Windows.Forms.Padding(0);
			this.tableTB.Name = "tableTB";
			this.tableTB.RowCount = 2;
			this.tableTB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableTB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableTB.Size = new System.Drawing.Size(538, 435);
			this.tableTB.TabStop = false;
			// 
			// TabPageDirectory
			// 
			this.Controls.Add(tableTB);
			this.dirView.ResumeLayout(false);
			this.actView.ResumeLayout(false);
			this.tableTB.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button backward;
		private System.Windows.Forms.Button forward;
		private System.Windows.Forms.Button upward;
		private System.Windows.Forms.TextBox pathText;
		private System.Windows.Forms.TextBox searchText;
		private System.Windows.Forms.ListViewGroup dirGroup;
		private System.Windows.Forms.ListViewGroup fileGroup;
		private System.Windows.Forms.ColumnHeader nameColumn;
		private System.Windows.Forms.ColumnHeader dateColumn;
		private System.Windows.Forms.ColumnHeader typeColumn;
		private System.Windows.Forms.ColumnHeader sizeColumn;
		private System.Windows.Forms.TableLayoutPanel actView;
		private System.Windows.Forms.ListView dirView;
		private System.Windows.Forms.TableLayoutPanel tableTB;
	}
}