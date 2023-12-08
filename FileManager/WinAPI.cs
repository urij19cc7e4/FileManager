using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FileManager
{
	public static class WinAPI
	{
		public const uint FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
		public const uint FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
		public const uint FILE_ATTRIBUTE_DEVICE = 0x00000040;
		public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
		public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;
		public const uint FILE_ATTRIBUTE_HIDDEN = 0x00000002;
		public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
		public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
		public const uint FILE_ATTRIBUTE_OFFLINE = 0x00001000;
		public const uint FILE_ATTRIBUTE_READONLY = 0x00000001;
		public const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
		public const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
		public const uint FILE_ATTRIBUTE_SYSTEM = 0x00000004;
		public const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
		public const uint FILE_ATTRIBUTE_VIRTUAL = 0x00010000;

		public const uint HDM_FIRST = 0x1200;
		public const uint HDM_GETITEM = HDM_FIRST + 11;
		public const uint HDM_SETITEM = HDM_FIRST + 12;

		public const uint LVM_FIRST = 0x1000;
		public const uint LVM_GETHEADER = LVM_FIRST + 31;
		public const uint LVM_GETIMAGELIST = LVM_FIRST + 2;
		public const uint LVM_SETIMAGELIST = LVM_FIRST + 3;

		public const uint LVSIL_GROUPHEADER = 3;
		public const uint LVSIL_NORMAL = 0;
		public const uint LVSIL_SMALL = 1;
		public const uint LVSIL_STATE = 2;

		public const uint SHGFI_ATTRIBUTES = 0x000000800;
		public const uint SHGFI_ATTR_SPECIFIED = 0x000020000;
		public const uint SHGFI_DISPLAYNAME = 0x000000200;
		public const uint SHGFI_EXETYPE = 0x000002000;
		public const uint SHGFI_ICON = 0x000000100;
		public const uint SHGFI_ICONLOCATION = 0x000001000;
		public const uint SHGFI_LARGEICON = 0x000000000;
		public const uint SHGFI_LINKOVERLAY = 0x000008000;
		public const uint SHGFI_OPENICON = 0x000000002;
		public const uint SHGFI_PIDL = 0x000000008;
		public const uint SHGFI_SELECTED = 0x000010000;
		public const uint SHGFI_SHELLICONSIZE = 0x000000004;
		public const uint SHGFI_SMALLICON = 0x000000001;
		public const uint SHGFI_SYSICONINDEX = 0x000004000;
		public const uint SHGFI_TYPENAME = 0x000000400;
		public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;

		[Flags]
		public enum AssocF : uint
		{
			None = 0,
			Init_NoRemapCLSID = 0x1,
			Init_ByExeName = 0x2,
			Open_ByExeName = 0x2,
			Init_DefaultToStar = 0x4,
			Init_DefaultToFolder = 0x8,
			NoUserSettings = 0x10,
			NoTruncate = 0x20,
			Verify = 0x40,
			RemapRunDll = 0x80,
			NoFixUps = 0x100,
			IgnoreBaseClass = 0x200,
			Init_IgnoreUnknown = 0x400,
			Init_FixedProgId = 0x800,
			IsProtocol = 0x1000,
			InitForFile = 0x2000
		}

		public enum AssocStr
		{
			Command = 1,
			Executable,
			FriendlyDocName,
			FriendlyAppName,
			NoOpen,
			ShellNewValue,
			DDECommand,
			DDEIfExec,
			DDEApplication,
			DDETopic,
			InfoTip,
			QuickTip,
			TileInfo,
			ContentType,
			DefaultIcon,
			ShellExtension,
			DropTarget,
			DelegateExecute,
			SupportedUriProtocols,
			ProgID,
			AppID,
			AppPublisher,
			AppIconReference,
			Max
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HDITEM
		{
			public Mask mask;
			public int cxy;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string pszText;
			public IntPtr hbm;
			public int cchTextMax;
			public Format fmt;
			public IntPtr lParam;
			public int iImage;
			public int iOrder;
			public uint type;
			public IntPtr pvFilter;
			public uint state;

			[Flags]
			public enum Mask
			{
				Format = 0x4,
			};

			[Flags]
			public enum Format
			{
				SortDown = 0x200,
				SortUp = 0x400,
			};
		};

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SHFILEINFO
		{
			public IntPtr hIcon;
			public int iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		}

		[DllImport("shlwapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern uint AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra, [Out] StringBuilder pszOut, ref uint pcchOut);

		[DllImport("comctl32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ImageList_Destroy(IntPtr hImageList);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, ref HDITEM lParam);

		[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
	}
}