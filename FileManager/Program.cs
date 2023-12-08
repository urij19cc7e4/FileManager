using System;
using System.Windows.Forms;

namespace FileManager
{
	public static class Program
	{
		public static MainWindow mainWindow;

		public static string[] buffer;

		public static bool delete;

		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			mainWindow = new MainWindow();
			Application.Run(mainWindow);
		}
	}
}