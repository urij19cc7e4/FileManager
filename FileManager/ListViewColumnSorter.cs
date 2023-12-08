using System;
using System.Collections;
using System.Windows.Forms;

namespace FileManager
{
	public class ListViewColumnSorter : IComparer
	{
		private int _sortColumn;

		private SortOrder _sortOrder;

		private CaseInsensitiveComparer _compareObject;

		public int SortColumn
		{
			get => _sortColumn;
			set => _sortColumn = value;
		}

		public SortOrder SortOrder
		{
			get => _sortOrder;
			set => _sortOrder = value;
		}

		public CaseInsensitiveComparer CompareObject
		{
			get => _compareObject;
			set
			{
				if (value == null)
					_compareObject = new CaseInsensitiveComparer();
				else
					_compareObject = value;
			}
		}

		public ListViewColumnSorter()
		{
			_sortColumn = 0;
			_sortOrder = SortOrder.None;
			_compareObject = new CaseInsensitiveComparer();
		}

		private int Compare(ListViewItem lhs, ListViewItem rhs)
		{
			if (_sortOrder == SortOrder.None)
				return 0;

			int ascending = _sortOrder == SortOrder.Ascending ? 1 : _sortOrder == SortOrder.Descending ? -1 : 0;

			switch (_sortColumn)
			{
				case 0: return CompareObject.Compare(lhs.SubItems[_sortColumn].Text, rhs.SubItems[_sortColumn].Text) * ascending;
				case 1: return CompareObject.Compare((DateTime)lhs.SubItems[_sortColumn].Tag, (DateTime)rhs.SubItems[_sortColumn].Tag) * ascending;
				case 2: return CompareObject.Compare(lhs.SubItems[_sortColumn].Text, rhs.SubItems[_sortColumn].Text) * ascending;
				case 3: return CompareObject.Compare((long)lhs.SubItems[_sortColumn].Tag, (long)rhs.SubItems[_sortColumn].Tag) * ascending;
				default: return 0;
			}
		}

		public int Compare(object lhs, object rhs)
		{
			return Compare((ListViewItem)lhs, (ListViewItem)rhs);
		}
	}
}