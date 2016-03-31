using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace GeneticMarket.Common.Helper
{
    // Implements the manual sorting of items by columns.
    public class ListViewItemComparer : IComparer
    {
        public int col;
        public SortOrder order = SortOrder.None;

        public ListViewItemComparer()
        {
        }

        public int Compare(object x, object y)
        {
            bool isDouble = false; // (col == 1);
            bool isNum = (col > 1);

            int result = 0;

            if (isDouble)
            {
                result = (int) (double.Parse(((ListViewItem)x).SubItems[col].Text) - double.Parse(((ListViewItem)y).SubItems[col].Text));
            }
            else if (isNum)
            {
                result = int.Parse(((ListViewItem)x).SubItems[col].Text) - int.Parse(((ListViewItem)y).SubItems[col].Text);
            }
            else
            {
                result = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }

            if (order == SortOrder.Descending)
            {
                return -result;
            }

            return result;
        }
    }

}
