using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileManager
{
	public partial class MainWindow : Form
	{
		private delegate void Delegate();

		private readonly TabControlClosable tabCtrl;

		private readonly DriveTreeView treeView;

		public MainWindow()
		{
			InitializeComponent();

			tabCtrl = new TabControlClosable();
			treeView = new DriveTreeView();

			tabCtrl.SuspendLayout();
			treeView.SuspendLayout();

			tabCtrl.Dock = DockStyle.Fill;
			tabCtrl.ItemSize = new Size(125, 25);
			tabCtrl.Location = new Point(167, 6);
			tabCtrl.Margin = new Padding(5);
			tabCtrl.Name = "tabCtrl";
			tabCtrl.Padding = new Point(0, 0);
			tabCtrl.SelectedIndex = 0;
			tabCtrl.Size = new Size(541, 468);
			tabCtrl.SizeMode = TabSizeMode.Fixed;
			tabCtrl.TabStop = false;

			treeView.Dock = DockStyle.Fill;
			treeView.Location = new Point(6, 6);
			treeView.Margin = new Padding(5);
			treeView.Name = "treeView";
			treeView.Size = new Size(150, 468);
			treeView.TabStop = false;

			tableMW.Controls.Add(tabCtrl, 1, 0);
			tableMW.Controls.Add(treeView, 0, 0);

			components = new Container();

			components.Add(tabCtrl);
			components.Add(treeView);

			tabCtrl.ResumeLayout(false);
			treeView.ResumeLayout(false);
		}

		public TabControlClosable GetTabCtrl()
		{
			return tabCtrl;
		}

		private DirectoryInfo CreateDirTest(string path)
		{
			try
			{
				return Directory.CreateDirectory(path);
			}
			catch (Exception)
			{
				return null;
			}
		}

		private FileInfo CreateFileTest(string path)
		{
			try
			{
				FileStream fileStream = File.Create(path);
				fileStream.Close();

				return new FileInfo(path);
			}
			catch (Exception)
			{
				return null;
			}
		}

		private void Test()
		{
			Directory.CreateDirectory("Dir copied");
			Directory.CreateDirectory("Dir moved");
			Directory.CreateDirectory("File copied");
			Directory.CreateDirectory("File moved");
			CreateFileTest("file");

			Thread.Sleep(500);
			Invoke(new Delegate(() =>
			{
				textBox1.Text += "Directory creation test ";
				if (CreateDirTest("dir") == null)
					textBox1.Text += "failed";
				else
					textBox1.Text += "successful";
			}));

			Thread.Sleep(500);
			Invoke(new Delegate(() =>
			{
				textBox1.Text += Environment.NewLine + "File creation test ";
				if (CreateFileTest("dir/file") == null)
					textBox1.Text += "failed";
				else
					textBox1.Text += "successful";
			}));

			Thread.Sleep(500);
			Invoke(new Delegate(() =>
			{
				textBox1.Text += Environment.NewLine + "Dir copy test ";
				if (TabPageDirectory.MovePath("dir", "Dir copied", false) && Directory.Exists("Dir copied/dir") && File.Exists("Dir copied/dir/file"))
					textBox1.Text += "successful";
				else
					textBox1.Text += "failed";
			}));

			Thread.Sleep(500);
			Invoke(new Delegate(() =>
			{
				textBox1.Text += Environment.NewLine + "File copy test ";
				if (TabPageDirectory.MovePath("dir/file", "File copied", false) && File.Exists("File copied/file"))
					textBox1.Text += "successful";
				else
					textBox1.Text += "failed";
			}));

			Thread.Sleep(500);
			Invoke(new Delegate(() =>
			{
				textBox1.Text += Environment.NewLine + "Dir move test ";
				if (TabPageDirectory.MovePath("dir", "Dir moved", true) && Directory.Exists("Dir moved/dir") && File.Exists("Dir moved/dir/file"))
					textBox1.Text += "successful";
				else
					textBox1.Text += "failed";
			}));

			Thread.Sleep(500);
			Invoke(new Delegate(() =>
			{
				textBox1.Text += Environment.NewLine + "File move test ";
				if (TabPageDirectory.MovePath("file", "File moved", true) && File.Exists("File moved/file"))
					textBox1.Text += "successful";
				else
					textBox1.Text += "failed";
			}));

			Thread.Sleep(500);
			Invoke(new Delegate(() =>
						{
							textBox1.Text += Environment.NewLine + "Dir deletion test ";
							if (TabPageDirectory.Delete("Dir copied") && !Directory.Exists("Dir copied"))
								textBox1.Text += "successful";
							else
								textBox1.Text += "failed";
						}));

			Thread.Sleep(500);
			Invoke(new Delegate(() =>
						{
							textBox1.Text += Environment.NewLine + "File deletion test ";
							if (TabPageDirectory.Delete("File copied/file") && !File.Exists("File copied/file"))
								textBox1.Text += "successful";
							else
								textBox1.Text += "failed";
						}));
		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
			new Thread(Test).Start();
		}
	}
}