using System.Windows.Forms;

namespace FileManager
{
	public partial class DriveTreeView : TreeView
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
			components = new System.ComponentModel.Container();
			this.MouseDoubleClick += DriveTreeView_MouseDoubleClick;
		}

		#endregion
	}
}