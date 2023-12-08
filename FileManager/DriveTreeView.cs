using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileManager
{
	public partial class DriveTreeView : TreeView
	{
		private delegate void Delegate();

		private readonly Thread driveScanThread;

		private void ExecuteOrInvokeAsync(Control control, Delegate @delegate)
		{
			if (control.InvokeRequired)
				control.BeginInvoke(@delegate);
			else
				@delegate.Invoke();
		}

		private void ExecuteOrInvokeSync(Control control, Delegate @delegate)
		{
			if (control.InvokeRequired)
				control.Invoke(@delegate);
			else
				@delegate.Invoke();
		}

		private void Clean()
		{
			driveScanThread.Abort();
		}

		private void DriveScanProc()
		{
			string[] drivesOld = new string[0];

			while (true)
			{
				TreeNode rootNode = new TreeNode("Drives");
				rootNode.Expand();

				DriveInfo[] drives = DriveInfo.GetDrives();
				string[] drivesNew = new string[drives.Length];

				for (int i = 0; i < drives.Length; ++i)
					drivesNew[i] = drives[i].Name;

				if (!new HashSet<string>(drivesNew).SetEquals(drivesOld))
				{
					ExecuteOrInvokeSync(this, new Delegate(() => { Nodes.Clear(); BeginUpdate(); }));

					foreach (DriveInfo drive in drives)
					{
						ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Open in new tab") { Tag = drive.RootDirectory.FullName };
						toolStripMenuItem.Click += ToolStripMenuItem_Click;

						ContextMenuStrip menuStrip = new ContextMenuStrip();
						menuStrip.Items.Add(toolStripMenuItem);

						rootNode.Nodes.Add(new TreeNode($"{drive.VolumeLabel} ({drive.Name})") { ContextMenuStrip = menuStrip, Tag = drive.RootDirectory.FullName });
					}

					ExecuteOrInvokeSync(this, new Delegate(() => { Nodes.Add(rootNode); EndUpdate(); }));
				}

				drivesOld = drivesNew;
				Thread.Sleep(100);
			}
		}

		public DriveTreeView()
		{
			InitializeComponent();

			driveScanThread = new Thread(DriveScanProc);
			driveScanThread.Start();
		}

		private void ToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			Program.mainWindow.GetTabCtrl().CreateTab((string)((ToolStripMenuItem)sender).Tag);
		}

		private void DriveTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode selectedNode = HitTest(e.X, e.Y).Node;

			if ((string)selectedNode.Tag != null)
				if (Program.mainWindow.GetTabCtrl().GetSelectedTab() == null)
					Program.mainWindow.GetTabCtrl().CreateTab((string)selectedNode.Tag);
				else
					Program.mainWindow.GetTabCtrl().GetSelectedTab().Open((string)selectedNode.Tag);
		}

		~DriveTreeView()
		{
			Clean();
		}
	}
}