using System;
using System.Globalization;

namespace FileManager
{
	public static class FileSize
	{
		private static readonly long k = 1024;

		private static readonly long m = k * k;

		private static readonly long g = m * k;

		private static readonly long t = g * k;

		private static readonly long p = t * k;

		private static readonly long e = p * k;

		public static long Parse(string fileSize)
		{
			if (fileSize == "")
				return 0;

			long result = long.Parse(fileSize.Substring(0, fileSize.Length - 2),
				NumberStyles.AllowThousands, new NumberFormatInfo() { NumberGroupSeparator = " " });

			switch (fileSize[fileSize.Length - 2])
			{
				case ' ': return result;
				case 'K': return result * k;
				case 'M': return result * m;
				case 'G': return result * g;
				case 'T': return result * t;
				case 'P': return result * p;
				case 'E': return result * e;
				default: throw new Exception($"FileSize.Parse({fileSize})");
			}
		}

		public static string ToString(long fileSize)
		{
			switch (fileSize)
			{
				case long i when i >= 0 && i < 10000:
					return i.ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " }) + " B";

				case long i when i >= 10000 && i < k * 10000:
					return (i / k).ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " }) + " KB";

				case long i when i >= k * 10000 && i < m * 10000:
					return (i / m).ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " }) + " MB";

				case long i when i >= m * 10000 && i < g * 10000:
					return (i / g).ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " }) + " GB";

				case long i when i >= g * 10000 && i < t * 10000:
					return (i / t).ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " }) + " TB";

				case long i when i >= t * 10000 && i < p * 10000:
					return (i / p).ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " }) + " PB";

				case long i when i >= p * 10000:
					return (i / e).ToString("N0", new NumberFormatInfo() { NumberGroupSeparator = " " }) + " EB";

				default: throw new Exception($"FileSize.ToString({fileSize})");
			}
		}
	}
}