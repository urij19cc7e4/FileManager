﻿using System;
using System.Windows.Forms;

namespace FileManager
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var wnd = new MainWindow();
			Application.Run(wnd);
		}
	}
}