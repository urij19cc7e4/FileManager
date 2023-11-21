using System.Windows.Forms;

namespace FileManager
{
	public partial class TabControlClosable : TabControl
	{
		private System.ComponentModel.Container components = null;
		public delegate void OnHeaderCloseDelegate(object sender, CloseEventArgs e);
		public event OnHeaderCloseDelegate OnClose;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
		}

		#endregion
	}
}