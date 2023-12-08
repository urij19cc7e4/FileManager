using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Caching.Memory;

namespace FileManager
{
	public static class GetFileInfoCached
	{
		private static readonly MemoryCache fileTypeDescCache = new MemoryCache(new MemoryCacheOptions());

		private static readonly MemoryCache iconIndexCache = new MemoryCache(new MemoryCacheOptions());

		public static string GetFileTypeDesc(string filePath)
		{
			if (fileTypeDescCache.TryGetValue(Path.GetExtension(filePath), out string fileTypeDesc))
				return fileTypeDesc;
			else
			{
				WinAPI.SHFILEINFO shfi = new WinAPI.SHFILEINFO();
				WinAPI.SHGetFileInfo(filePath, WinAPI.FILE_ATTRIBUTE_NORMAL, ref shfi, (uint)Marshal.SizeOf(shfi), WinAPI.SHGFI_TYPENAME | WinAPI.SHGFI_USEFILEATTRIBUTES);
				fileTypeDescCache.Set(Path.GetExtension(filePath), shfi.szTypeName);
				return shfi.szTypeName;
			}
		}

		public static int GetIconIndex(string path, bool large)
		{
			if (iconIndexCache.TryGetValue((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory ? "~" : Path.GetExtension(path), out int iconIndex))
				return iconIndex;
			else
			{
				WinAPI.SHFILEINFO shfi = new WinAPI.SHFILEINFO();
				WinAPI.SHGetFileInfo(path, 0, ref shfi, (uint)Marshal.SizeOf(shfi), (large ? WinAPI.SHGFI_LARGEICON : WinAPI.SHGFI_SMALLICON) | WinAPI.SHGFI_SYSICONINDEX);
				iconIndexCache.Set((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory ? "~" : Path.GetExtension(path), shfi.iIcon);
				return shfi.iIcon;
			}
		}
	}
}