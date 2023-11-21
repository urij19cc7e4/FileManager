using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FileManager
{
	public partial class TabControlClosable : TabControl
	{
		public TabControlClosable()
		{
			InitializeComponent();
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (e.Bounds != RectangleF.Empty)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				for (int nIndex = 0; nIndex < TabCount; ++nIndex)
				{
					RectangleF tabTextArea;
					if (nIndex != SelectedIndex)
					{
						tabTextArea = GetTabRect(nIndex);
						GraphicsPath _Path = new GraphicsPath();
						_Path.AddRectangle(tabTextArea);
						using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
						{
							ColorBlend _ColorBlend = new ColorBlend(3)
							{
								Colors = new Color[]
								{
									SystemColors.ControlLightLight,
									Color.FromArgb(255, SystemColors.ControlLight),
									SystemColors.ControlDark,
									SystemColors.ControlLightLight
								},
								Positions = new float[] { 0f, .4f, 0.5f, 1f }
							};
							_Brush.InterpolationColors = _ColorBlend;
							e.Graphics.FillPath(_Brush, _Path);
							using (Pen pen = new Pen(SystemColors.ActiveBorder))
								e.Graphics.DrawPath(pen, _Path);
							_ColorBlend.Colors = new Color[] { SystemColors.ActiveBorder, SystemColors.ActiveBorder, SystemColors.ActiveBorder, SystemColors.ActiveBorder };
							_ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
							_Brush.InterpolationColors = _ColorBlend;
							e.Graphics.FillRectangle(_Brush, tabTextArea.X + tabTextArea.Width - 22, 4, tabTextArea.Height - 3, tabTextArea.Height - 5);
							e.Graphics.DrawRectangle(Pens.White, tabTextArea.X + tabTextArea.Width - 20, 6, tabTextArea.Height - 8, tabTextArea.Height - 9);
							using (Pen pen = new Pen(Color.White, 2))
							{
								e.Graphics.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 16, 9, tabTextArea.X + tabTextArea.Width - 7, 17);
								e.Graphics.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 16, 17, tabTextArea.X + tabTextArea.Width - 7, 9);
							}
						}
						_Path.Dispose();
					}
					else
					{
						tabTextArea = GetTabRect(nIndex);
						GraphicsPath _Path = new GraphicsPath();
						_Path.AddRectangle(tabTextArea);
						using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
						{
							ColorBlend _ColorBlend = new ColorBlend(3)
							{
								Colors = new Color[]
								{
									SystemColors.ControlLightLight,
									Color.FromArgb(255,SystemColors.Control),
									SystemColors.ControlLight,
									SystemColors.Control
								},
								Positions = new float[] { 0f, .4f, 0.5f, 1f }
							};
							_Brush.InterpolationColors = _ColorBlend;
							e.Graphics.FillPath(_Brush, _Path);
							using (Pen pen = new Pen(SystemColors.ActiveBorder))
								e.Graphics.DrawPath(pen, _Path);
							_ColorBlend.Colors = new Color[]
							{
								Color.FromArgb(255,231,164,152),
								Color.FromArgb(255,231,164,152),
								Color.FromArgb(255,197,98,79),
								Color.FromArgb(255,197,98,79)
							};
							_Brush.InterpolationColors = _ColorBlend;
							e.Graphics.FillRectangle(_Brush, tabTextArea.X + tabTextArea.Width - 22, 4, tabTextArea.Height - 3, tabTextArea.Height - 5);
							e.Graphics.DrawRectangle(Pens.White, tabTextArea.X + tabTextArea.Width - 20, 6, tabTextArea.Height - 8, tabTextArea.Height - 9);
							using (Pen pen = new Pen(Color.White, 2))
							{
								e.Graphics.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 16, 9, tabTextArea.X + tabTextArea.Width - 7, 17);
								e.Graphics.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 16, 17, tabTextArea.X + tabTextArea.Width - 7, 9);
							}
						}
						_Path.Dispose();
					}
					string str = TabPages[nIndex].Text;
					StringFormat stringFormat = new StringFormat
					{
						Alignment = StringAlignment.Center
					};
					e.Graphics.DrawString(str, Font, new SolidBrush(TabPages[nIndex].ForeColor), tabTextArea, stringFormat);
				}
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			Graphics g = CreateGraphics();
			g.SmoothingMode = SmoothingMode.AntiAlias;
			for (int nIndex = 0; nIndex < TabCount; ++nIndex)
			{
				RectangleF tabTextArea;
				if (nIndex != SelectedIndex)
				{
					tabTextArea = GetTabRect(nIndex);
					GraphicsPath _Path = new GraphicsPath();
					_Path.AddRectangle(tabTextArea);
					using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
					{
						ColorBlend _ColorBlend = new ColorBlend(3)
						{
							Colors = new Color[]
							{
								SystemColors.ActiveBorder,
								SystemColors.ActiveBorder,
								SystemColors.ActiveBorder,
								SystemColors.ActiveBorder
							},
							Positions = new float[] { 0f, .4f, 0.5f, 1f }
						};
						_Brush.InterpolationColors = _ColorBlend;
						g.FillRectangle(_Brush, tabTextArea.X + tabTextArea.Width - 22, 4, tabTextArea.Height - 2, tabTextArea.Height - 5);
						g.DrawRectangle(Pens.White, tabTextArea.X + tabTextArea.Width - 20, 6, tabTextArea.Height - 8, tabTextArea.Height - 9);
						using (Pen pen = new Pen(Color.White, 2))
						{
							g.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 16, 9, tabTextArea.X + tabTextArea.Width - 7, 17);
							g.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 16, 17, tabTextArea.X + tabTextArea.Width - 7, 9);
						}
					}
					_Path.Dispose();
				}
				else
				{
					tabTextArea = GetTabRect(nIndex);
					GraphicsPath _Path = new GraphicsPath();
					_Path.AddRectangle(tabTextArea);
					using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
					{
						ColorBlend _ColorBlend = new ColorBlend(3)
						{
							Positions = new float[] { 0f, .4f, 0.5f, 1f },
							Colors = new Color[]
							{
								Color.FromArgb(255,231,164,152),
								Color.FromArgb(255,231,164,152),
								Color.FromArgb(255,197,98,79),
								Color.FromArgb(255,197,98,79)
							}
						};
						_Brush.InterpolationColors = _ColorBlend;
						g.FillRectangle(_Brush, tabTextArea.X + tabTextArea.Width - 22, 4, tabTextArea.Height - 3, tabTextArea.Height - 5);
						g.DrawRectangle(Pens.White, tabTextArea.X + tabTextArea.Width - 20, 6, tabTextArea.Height - 8, tabTextArea.Height - 9);
						using (Pen pen = new Pen(Color.White, 2))
						{
							g.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 16, 9, tabTextArea.X + tabTextArea.Width - 7, 17);
							g.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 16, 17, tabTextArea.X + tabTextArea.Width - 7, 9);
						}
					}
					_Path.Dispose();
				}
			}
			g.Dispose();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (!DesignMode)
			{
				Graphics g = CreateGraphics();
				g.SmoothingMode = SmoothingMode.AntiAlias;
				for (int nIndex = 0; nIndex < TabCount; ++nIndex)
				{
					RectangleF tabTextArea = GetTabRect(nIndex);
					tabTextArea = new RectangleF(tabTextArea.X + tabTextArea.Width - 22, 4, tabTextArea.Height - 3, tabTextArea.Height - 5);
					Point pt = new Point(e.X, e.Y);
					if (tabTextArea.Contains(pt))
					{
						using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
						{
							ColorBlend _ColorBlend = new ColorBlend(3)
							{
								Colors = new Color[]
								{
									Color.FromArgb(255, 252, 193, 183),
									Color.FromArgb(255, 252, 193, 183),
									Color.FromArgb(255, 210, 35, 2),
									Color.FromArgb(255, 210, 35, 2)
								},
								Positions = new float[] { 0f, .4f, 0.5f, 1f }
							};
							_Brush.InterpolationColors = _ColorBlend;
							g.FillRectangle(_Brush, tabTextArea);
							g.DrawRectangle(Pens.White, tabTextArea.X + 2, 6, tabTextArea.Height - 3, tabTextArea.Height - 4);
							using (Pen pen = new Pen(Color.White, 2))
							{
								g.DrawLine(pen, tabTextArea.X + 6, 9, tabTextArea.X + 15, 17);
								g.DrawLine(pen, tabTextArea.X + 6, 17, tabTextArea.X + 15, 9);
							}
						}
					}
					else
					{
						if (nIndex != SelectedIndex)
						{
							using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
							{
								ColorBlend _ColorBlend = new ColorBlend(3)
								{
									Colors = new Color[]
									{
										SystemColors.ActiveBorder,
										SystemColors.ActiveBorder,
										SystemColors.ActiveBorder,
										SystemColors.ActiveBorder
									},
									Positions = new float[] { 0f, .4f, 0.5f, 1f }
								};
								_Brush.InterpolationColors = _ColorBlend;
								g.FillRectangle(_Brush, tabTextArea);
								g.DrawRectangle(Pens.White, tabTextArea.X + 2, 6, tabTextArea.Height - 3, tabTextArea.Height - 4);
								using (Pen pen = new Pen(Color.White, 2))
								{
									g.DrawLine(pen, tabTextArea.X + 6, 9, tabTextArea.X + 15, 17);
									g.DrawLine(pen, tabTextArea.X + 6, 17, tabTextArea.X + 15, 9);
								}
							}
						}
					}
				}
				g.Dispose();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!DesignMode)
			{
				RectangleF tabTextArea = GetTabRect(SelectedIndex);
				tabTextArea = new RectangleF(tabTextArea.X + tabTextArea.Width - 22, 4, tabTextArea.Height - 3, tabTextArea.Height - 5);
				Point pt = new Point(e.X, e.Y);
				if (tabTextArea.Contains(pt))
					OnClose?.Invoke(this, new CloseEventArgs(SelectedIndex));
			}
		}
	}

	public class CloseEventArgs : EventArgs
	{
		private int nTabIndex = -1;

		public CloseEventArgs(int nTabIndex)
		{
			this.nTabIndex = nTabIndex;
		}

		public int TabIndex
		{
			get
			{
				return nTabIndex;
			}
			set
			{
				nTabIndex = value;
			}
		}
	}
}