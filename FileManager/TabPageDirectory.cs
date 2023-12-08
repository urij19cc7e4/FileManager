using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	public partial class TabPageDirectory : TabPage
	{
		private delegate void Delegate();

		private readonly TabControlClosable tabControl;

		private readonly Stack<string> nextDirs = new Stack<string>();

		private readonly Stack<string> prevDirs = new Stack<string>();

		private readonly FileSystemWatcher fsWatcher;

		private readonly Thread dirCheckerThread;

		private IntPtr sysLargeIconList;

		private IntPtr sysSmallIconList;

		private void CheckMovementButtons()
		{
			ExecuteOrInvokeAsync(backward, new Delegate(() => { backward.Enabled = prevDirs.Count != 0; }));
			ExecuteOrInvokeAsync(forward, new Delegate(() => { forward.Enabled = nextDirs.Count != 0; }));
			ExecuteOrInvokeAsync(upward, new Delegate(() => { upward.Enabled = new DirectoryInfo(fsWatcher.Path).Parent != null; }));
		}

		private void Clean()
		{
			UnLinkSystemIconList();
			dirCheckerThread.Abort();
			fsWatcher.EnableRaisingEvents = false;
		}

		private void ClearDir()
		{
			dirView.Items.Clear();
		}

		private void DirCheckerProc()
		{
			try
			{
				while (true)
				{
					File.GetAttributes(fsWatcher.Path);
					Thread.Sleep(100);
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception)
			{
				ExecuteOrInvokeAsync(tabControl, new Delegate(() => { tabControl.CloseTab(this); }));
			}
		}

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

		private void FileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			if ((File.GetAttributes(e.FullPath) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				DirectoryInfo dirInfo = new DirectoryInfo(e.FullPath);

				ExecuteOrInvokeAsync(dirView, new Delegate(() =>
				{
					foreach (ListViewItem item in dirView.Items)
						if (string.Compare((string)item.Tag, e.FullPath) == 0)
						{
							item.SubItems[1].Text = dirInfo.LastWriteTime.ToString("g");

							item.SubItems[1].Tag = dirInfo.LastWriteTime;

							break;
						}
				}));
			}
			else
			{
				FileInfo fileInfo = new FileInfo(e.FullPath);

				ExecuteOrInvokeAsync(dirView, new Delegate(() =>
				{
					foreach (ListViewItem item in dirView.Items)
						if (string.Compare((string)item.Tag, e.FullPath) == 0)
						{
							item.SubItems[1].Text = fileInfo.LastWriteTime.ToString("g");
							item.SubItems[3].Text = FileSize.ToString(fileInfo.Length);

							item.SubItems[1].Tag = fileInfo.LastWriteTime;
							item.SubItems[3].Tag = fileInfo.Length;

							break;
						}
				}));
			}

			ExecuteOrInvokeAsync(dirView, new Delegate(() => { dirView.Sort(); }));
		}

		private void FileWatcher_Created(object sender, FileSystemEventArgs e)
		{
			if ((File.GetAttributes(e.FullPath) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				DirectoryInfo dirInfo = new DirectoryInfo(e.FullPath);

				ListViewItem item = new ListViewItem(new[] { dirInfo.Name, dirInfo.LastWriteTime.ToString("g"), "Directory", "" },
					GetFileInfoCached.GetIconIndex(dirInfo.FullName, IsLargeIconView(dirView.View)), dirView.Groups[0])
				{ Tag = dirInfo.FullName };

				item.SubItems[1].Tag = dirInfo.LastWriteTime;
				item.SubItems[3].Tag = (long)0;

				ExecuteOrInvokeAsync(dirView, new Delegate(() => { dirView.Items.Add(item); }));
			}
			else
			{
				FileInfo fileInfo = new FileInfo(e.FullPath);

				ListViewItem item = new ListViewItem(new[] { fileInfo.Name, fileInfo.LastWriteTime.ToString("g"),
						GetFileInfoCached.GetFileTypeDesc(fileInfo.FullName), FileSize.ToString(fileInfo.Length) },
						GetFileInfoCached.GetIconIndex(fileInfo.FullName, IsLargeIconView(dirView.View)), dirView.Groups[1])
				{ Tag = fileInfo.FullName };

				item.SubItems[1].Tag = fileInfo.LastWriteTime;
				item.SubItems[3].Tag = fileInfo.Length;

				ExecuteOrInvokeAsync(dirView, new Delegate(() => { dirView.Items.Add(item); }));
			}

			ExecuteOrInvokeAsync(dirView, new Delegate(() => { dirView.Sort(); }));
		}

		private void FileWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			ExecuteOrInvokeAsync(dirView, new Delegate(() =>
			{
				foreach (ListViewItem item in dirView.Items)
					if (string.Compare((string)item.Tag, e.FullPath) == 0)
					{
						dirView.Items.Remove(item);

						break;
					}
			}));
		}

		private void FileWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			if ((File.GetAttributes(e.FullPath) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				DirectoryInfo dirInfo = new DirectoryInfo(e.FullPath);

				ExecuteOrInvokeAsync(dirView, new Delegate(() =>
				{
					foreach (ListViewItem item in dirView.Items)
						if (string.Compare((string)item.Tag, e.OldFullPath) == 0)
						{
							item.Tag = dirInfo.FullName;

							item.SubItems[0].Text = dirInfo.Name;
							item.SubItems[1].Text = dirInfo.LastWriteTime.ToString("g");

							item.SubItems[1].Tag = dirInfo.LastWriteTime;

							item.ImageIndex = GetFileInfoCached.GetIconIndex(dirInfo.FullName, IsLargeIconView(dirView.View));

							break;
						}
				}));
			}
			else
			{
				FileInfo fileInfo = new FileInfo(e.FullPath);

				ExecuteOrInvokeAsync(dirView, new Delegate(() =>
				{
					foreach (ListViewItem item in dirView.Items)
						if (string.Compare((string)item.Tag, e.OldFullPath) == 0)
						{
							item.Tag = fileInfo.FullName;

							item.SubItems[0].Text = fileInfo.Name;
							item.SubItems[1].Text = fileInfo.LastWriteTime.ToString("g");
							item.SubItems[2].Text = GetFileInfoCached.GetFileTypeDesc(fileInfo.FullName);
							item.SubItems[3].Text = FileSize.ToString(fileInfo.Length);

							item.SubItems[1].Tag = fileInfo.LastWriteTime;
							item.SubItems[3].Tag = fileInfo.Length;

							item.ImageIndex = GetFileInfoCached.GetIconIndex(fileInfo.FullName, IsLargeIconView(dirView.View));

							break;
						}
				}));
			}

			ExecuteOrInvokeAsync(dirView, new Delegate(() => { dirView.Sort(); }));
		}

		public static bool Delete(string path)
		{
			try
			{
				if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
					Directory.Delete(path, true);
				else
					File.Delete(path);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private static bool DirMove(string srcPath, string dstPath, bool delete)
		{
			try
			{
				if (Directory.Exists(dstPath))
					Delete(dstPath);

				if (delete)
					Directory.Move(srcPath, dstPath);
				else
				{
					if (!Directory.Exists(dstPath))
						Directory.CreateDirectory(dstPath);

					string[] dirs = Directory.GetDirectories(srcPath);
					string[] files = Directory.GetFiles(srcPath);

					foreach (string dir in dirs)
						DirMove(dir, Path.Combine(dstPath, new DirectoryInfo(dir).Name), delete);

					foreach (string file in files)
						FileMove(file, Path.Combine(dstPath, new FileInfo(file).Name), delete);
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private static bool FileMove(string srcPath, string dstPath, bool delete)
		{
			try
			{
				if (File.Exists(dstPath))
					Delete(dstPath);

				if (delete)
					File.Move(srcPath, dstPath);
				else
					File.Copy(srcPath, dstPath);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool MovePath(string srcPath, string dstPath, bool delete)
		{
			try
			{
				if ((File.GetAttributes(srcPath) & FileAttributes.Directory) == FileAttributes.Directory)
					return DirMove(srcPath, Path.Combine(dstPath, new DirectoryInfo(srcPath).Name), delete);
				else
					return FileMove(srcPath, Path.Combine(dstPath, new DirectoryInfo(srcPath).Name), delete);
			}
			catch (Exception)
			{
				return false;
			}
		}

		private bool OpenDir(string dirPath)
		{
			try
			{
				DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

				fsWatcher.EnableRaisingEvents = false;
				fsWatcher.Path = dirInfo.FullName;
				fsWatcher.EnableRaisingEvents = true;

				ExecuteOrInvokeAsync(pathText, new Delegate(() => { pathText.Text = dirInfo.FullName; }));

				ExecuteOrInvokeAsync(this, new Delegate(() => { Text = dirInfo.Name; }));

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private bool OpenFile(string filePath)
		{
			try
			{
				if (Path.GetExtension(filePath) == ".exe")
					using (Process process = new Process())
					{
						process.StartInfo.FileName = filePath;
						process.Start();
					}
				else
				{
					string exePath = GetAssocPath(Path.GetExtension(filePath));

					if (File.Exists(exePath))
						using (Process process = new Process())
						{
							process.StartInfo.FileName = exePath;
							process.StartInfo.Arguments = $"\"{filePath}\"";
							process.Start();
						}
					else
					{
						if (MessageBox.Show($"No associated applications to open {filePath}.\nDo you want to choose manually?",
							"FileManager", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
						{
							OpenFileDialog openFileDialog = new OpenFileDialog()
							{
								Filter = "Executable file (*.exe)|*.exe"
							};
							openFileDialog.ShowDialog();

							if (File.Exists(openFileDialog.FileName))
								using (Process process = new Process())
								{
									process.StartInfo.FileName = openFileDialog.FileName;
									process.StartInfo.Arguments = $"\"{filePath}\"";
									process.Start();
								}
						}
					}
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void OpenPath(string path)
		{
			string prev = fsWatcher.Path;

			try
			{
				if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
					if (OpenDir(path))
					{
						nextDirs.Clear();
						prevDirs.Push(prev);

						CheckMovementButtons();

						ClearDir();
						LoadDir();
					}
					else
						MessageBox.Show($"No access to directory {path}", "FileManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
				else if (!OpenFile(path))
					MessageBox.Show($"No access to file {path}", "FileManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception) { }
		}

		private string GetAssocPath(string ext)
		{
			uint length = 0;

			WinAPI.AssocQueryString(WinAPI.AssocF.None, WinAPI.AssocStr.Executable, ext, null, null, ref length);

			StringBuilder stringBuilder = new StringBuilder((int)length);

			if (WinAPI.AssocQueryString(WinAPI.AssocF.None, WinAPI.AssocStr.Executable, ext, null, stringBuilder, ref length) == 0)
				return stringBuilder.ToString();
			else
				return "";
		}

		private bool IsLargeIconView(View view)
		{
			switch (view)
			{
				case View.LargeIcon:
				case View.Tile:
					return true;

				case View.Details:
				case View.SmallIcon:
				case View.List:
					return false;

				default: throw new Exception($"IsLargeIconView({view})");
			}
		}

		private void LinkSystemIconList()
		{
			WinAPI.SHFILEINFO shfi = new WinAPI.SHFILEINFO();

			sysLargeIconList = WinAPI.SHGetFileInfo("", 0, ref shfi, (uint)Marshal.SizeOf(shfi), WinAPI.SHGFI_LARGEICON | WinAPI.SHGFI_SYSICONINDEX);
			sysSmallIconList = WinAPI.SHGetFileInfo("", 0, ref shfi, (uint)Marshal.SizeOf(shfi), WinAPI.SHGFI_SMALLICON | WinAPI.SHGFI_SYSICONINDEX);
		}

		private void LoadDir(string filter = "*", bool recursive = false)
		{
			WinAPI.SendMessage(dirView.Handle, WinAPI.LVM_SETIMAGELIST,
				IsLargeIconView(dirView.View) ? WinAPI.LVSIL_NORMAL : WinAPI.LVSIL_SMALL,
				IsLargeIconView(dirView.View) ? sysLargeIconList : sysSmallIconList);

			SortOrder sortOrder = ((ListViewColumnSorter)dirView.ListViewItemSorter).SortOrder;
			((ListViewColumnSorter)dirView.ListViewItemSorter).SortOrder = SortOrder.None;
			dirView.BeginUpdate();

			dirView.Items.AddRange(LoadDir(fsWatcher.Path, filter, recursive).ToArray());

			dirView.EndUpdate();
			((ListViewColumnSorter)dirView.ListViewItemSorter).SortOrder = sortOrder;

			dirView.Sort();
		}

		private List<ListViewItem> LoadDir(string dirPath, string filter = "*", bool recursive = false)
		{
			try
			{
				string[] dirs = Directory.GetDirectories(dirPath, filter);
				string[] files = Directory.GetFiles(dirPath, filter);

				List<ListViewItem> items = new List<ListViewItem>(dirs.Length + files.Length);
				object sync = new object();

				ParallelLoopResult dirsResult = Parallel.ForEach(dirs, dir =>
				   {
					   DirectoryInfo dirInfo = new DirectoryInfo(dir);

					   ListViewItem item = new ListViewItem(new[] { dirInfo.Name, dirInfo.LastWriteTime.ToString("g"), "Directory", "" },
						   GetFileInfoCached.GetIconIndex(dirInfo.FullName, IsLargeIconView(dirView.View)))
					   { Tag = dirInfo.FullName };

					   item.SubItems[1].Tag = dirInfo.LastWriteTime;
					   item.SubItems[3].Tag = (long)0;

					   lock (sync)
					   {
						   item.Group = dirView.Groups[0];
						   items.Add(item);
					   }
				   });

				ParallelLoopResult filesResult = Parallel.ForEach(files, file =>
				  {
					  FileInfo fileInfo = new FileInfo(file);

					  ListViewItem item = new ListViewItem(new[] { fileInfo.Name, fileInfo.LastWriteTime.ToString("g"),
						GetFileInfoCached.GetFileTypeDesc(fileInfo.FullName), FileSize.ToString(fileInfo.Length) },
						GetFileInfoCached.GetIconIndex(fileInfo.FullName, IsLargeIconView(dirView.View)))
					  { Tag = fileInfo.FullName };

					  item.SubItems[1].Tag = fileInfo.LastWriteTime;
					  item.SubItems[3].Tag = fileInfo.Length;

					  lock (sync)
					  {
						  item.Group = dirView.Groups[1];
						  items.Add(item);
					  }
				  });

				while (!dirsResult.IsCompleted || !filesResult.IsCompleted)
					Thread.Sleep(1);

				if (recursive)
				{
					string[] dirsToObserve = Directory.GetDirectories(dirPath);

					foreach (string dir in dirsToObserve)
						items.AddRange(LoadDir(dirPath, filter, recursive));
				}

				return items;
			}
			catch (Exception) { }

			return new List<ListViewItem>();
		}

		private void SearchFor(string name)
		{
			ClearDir();

			try
			{
				if (name == "")
					LoadDir();
				else
					LoadDir("*" + name + "*", true);
			}
			catch (Exception) { }
		}

		private void SetSortIcon(int sortColumn, SortOrder sortOrder)
		{
			IntPtr columnHdr = WinAPI.SendMessage(dirView.Handle, WinAPI.LVM_GETHEADER, 0, IntPtr.Zero);

			for (int i = 0; i < dirView.Columns.Count; ++i)
			{
				IntPtr columnPtr = new IntPtr(i);
				WinAPI.HDITEM item = new WinAPI.HDITEM { mask = WinAPI.HDITEM.Mask.Format };
				WinAPI.SendMessage(columnHdr, WinAPI.HDM_GETITEM, (uint)columnPtr, ref item);

				if (sortColumn == i)
					switch (sortOrder)
					{
						case SortOrder.Ascending:
							item.fmt &= ~WinAPI.HDITEM.Format.SortDown;
							item.fmt |= WinAPI.HDITEM.Format.SortUp;
							break;

						case SortOrder.Descending:
							item.fmt &= ~WinAPI.HDITEM.Format.SortUp;
							item.fmt |= WinAPI.HDITEM.Format.SortDown;
							break;
					}
				else
					item.fmt &= ~WinAPI.HDITEM.Format.SortDown & ~WinAPI.HDITEM.Format.SortUp;

				WinAPI.SendMessage(columnHdr, WinAPI.HDM_SETITEM, (uint)columnPtr, ref item);
			}
		}

		private void UnLinkSystemIconList()
		{
			WinAPI.ImageList_Destroy(sysLargeIconList);
			WinAPI.ImageList_Destroy(sysSmallIconList);
		}

		public TabPageDirectory(TabControlClosable tabControl, string dirPath)
		{
			InitializeComponent();

			this.tabControl = tabControl;

			ToolStripMenuItem[] menuItems = new ToolStripMenuItem[6];

			menuItems[0] = new ToolStripMenuItem("Open in new tab");
			menuItems[0].Click += OpenNewTab_Click;

			menuItems[1] = new ToolStripMenuItem("Copy");
			menuItems[1].Click += Copy_Click;

			menuItems[2] = new ToolStripMenuItem("Cut");
			menuItems[2].Click += Cut_Click;

			menuItems[3] = new ToolStripMenuItem("Paste");
			menuItems[3].Click += Paste_Click;

			menuItems[4] = new ToolStripMenuItem("Rename");
			menuItems[4].Click += Rename_Click;

			menuItems[5] = new ToolStripMenuItem("Delete");
			menuItems[5].Click += Delete_Click;

			ContextMenuStrip = new ContextMenuStrip();
			ContextMenuStrip.Items.AddRange(menuItems);

			DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

			Text = dirInfo.Name;

			fsWatcher = new FileSystemWatcher(dirInfo.FullName);
			fsWatcher.Changed += FileWatcher_Changed;
			fsWatcher.Created += FileWatcher_Created;
			fsWatcher.Deleted += FileWatcher_Deleted;
			fsWatcher.Renamed += FileWatcher_Renamed;
			fsWatcher.EnableRaisingEvents = true;

			dirCheckerThread = new Thread(DirCheckerProc);
			dirCheckerThread.Start();

			dirView.ListViewItemSorter = new ListViewColumnSorter()
			{
				SortColumn = 0,
				SortOrder = SortOrder.Ascending
			};

			LinkSystemIconList();

			LoadDir();

			CheckMovementButtons();
		}

		~TabPageDirectory()
		{
			Clean();
		}

		private void OpenNewTab_Click(object sender, EventArgs e)
		{
			if (dirView.CheckedItems.Count == 1 &&
				(File.GetAttributes((string)dirView.CheckedItems[0].Tag) & FileAttributes.Directory) == FileAttributes.Directory)
				tabControl.CreateTab((string)dirView.CheckedItems[0].Tag);
		}

		private void Copy_Click(object sender, EventArgs e)
		{
			if (dirView.CheckedItems.Count != 0)
			{
				Program.buffer = new string[dirView.CheckedItems.Count];

				for (int i = 0; i < dirView.CheckedItems.Count; ++i)
					Program.buffer[i] = (string)dirView.CheckedItems[i].Tag;

				Program.delete = false;
			}
		}

		private void Cut_Click(object sender, EventArgs e)
		{
			if (dirView.CheckedItems.Count != 0)
			{
				Program.buffer = new string[dirView.CheckedItems.Count];

				for (int i = 0; i < dirView.CheckedItems.Count; ++i)
					Program.buffer[i] = (string)dirView.CheckedItems[i].Tag;

				Program.delete = true;
			}
		}

		private void Paste_Click(object sender, EventArgs e)
		{
			if (Program.buffer != null)
				if (dirView.CheckedItems.Count == 0)
				{
					foreach (string item in Program.buffer)
						MovePath(item, fsWatcher.Path, Program.delete);

					Program.buffer = null;
				}
				else if (dirView.CheckedItems.Count == 1 &&
					(File.GetAttributes((string)dirView.CheckedItems[0].Tag) & FileAttributes.Directory) == FileAttributes.Directory)
				{
					foreach (string elem in Program.buffer)
						MovePath(elem, (string)dirView.CheckedItems[0].Tag, Program.delete);

					Program.buffer = null;
				}
		}

		private void Rename_Click(object sender, EventArgs e)
		{

		}

		private void Delete_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in dirView.CheckedItems)
				Delete((string)item.Tag);
		}

		private void dirView_ItemCheck(object sender, ItemCheckEventArgs e)
		{

		}

		private void dirView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			e.Item.Selected = e.Item.Checked;
		}

		private void dirView_Click(object sender, EventArgs e)
		{

		}

		private void dirView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (((ListViewColumnSorter)dirView.ListViewItemSorter).SortColumn == e.Column)
				if (((ListViewColumnSorter)dirView.ListViewItemSorter).SortOrder == SortOrder.Ascending)
					((ListViewColumnSorter)dirView.ListViewItemSorter).SortOrder = SortOrder.Descending;
				else
					((ListViewColumnSorter)dirView.ListViewItemSorter).SortOrder = SortOrder.Ascending;
			else
			{
				((ListViewColumnSorter)dirView.ListViewItemSorter).SortColumn = e.Column;
				((ListViewColumnSorter)dirView.ListViewItemSorter).SortOrder = SortOrder.Ascending;
			}

			SetSortIcon(((ListViewColumnSorter)dirView.ListViewItemSorter).SortColumn,
				((ListViewColumnSorter)dirView.ListViewItemSorter).SortOrder);

			dirView.Sort();
		}

		private void dirView_DoubleClick(object sender, EventArgs e)
		{

		}

		private void dirView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			e.Item.Checked = e.Item.Selected;
		}

		private void dirView_MouseClick(object sender, MouseEventArgs e)
		{

		}

		private void dirView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			OpenPath((string)dirView.HitTest(e.X, e.Y).Item.Tag);
		}

		private void backward_Click(object sender, EventArgs e)
		{
			bool opened = false;
			string next = fsWatcher.Path;

			while (prevDirs.Count != 0 && !(opened = OpenDir(prevDirs.Pop()))) ;

			if (opened)
			{
				nextDirs.Push(next);

				CheckMovementButtons();

				ClearDir();
				LoadDir();
			}
		}

		private void forward_Click(object sender, EventArgs e)
		{
			bool opened = false;
			string prev = fsWatcher.Path;

			while (nextDirs.Count != 0 && !(opened = OpenDir(nextDirs.Pop()))) ;

			if (opened)
			{
				prevDirs.Push(prev);

				CheckMovementButtons();

				ClearDir();
				LoadDir();
			}
		}

		private void upward_Click(object sender, EventArgs e)
		{
			bool opened = false;
			DirectoryInfo dirInfo;
			string prev = fsWatcher.Path;

			while ((dirInfo = new DirectoryInfo(fsWatcher.Path).Parent) != null && !(opened = OpenDir(dirInfo.FullName))) ;

			if (opened)
			{
				nextDirs.Clear();
				prevDirs.Push(prev);

				CheckMovementButtons();

				ClearDir();
				LoadDir();
			}
		}

		private void pathText_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				OpenPath(pathText.Text);

				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		private void searchText_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SearchFor(searchText.Text);

				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		private void pathText_TextChanged(object sender, EventArgs e)
		{

		}

		private void searchText_TextChanged(object sender, EventArgs e)
		{

		}

		public void Open(string path)
		{
			OpenPath(path);
		}
	}
}