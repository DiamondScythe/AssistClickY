using AssistClickY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.Helpers.Misc
{
    public static class ListTools
    {
        public static List<ClipboardItem> PreprocessList(List<ClipboardItem> list, int newListItemNumber)
        {
            List<ClipboardItem> processedList = new List<ClipboardItem>();
            
            //reverse the list to show newest items first
            processedList = list.AsEnumerable().Reverse().ToList();

            //truncate the list to only get the X newest items
            processedList = processedList.Take(newListItemNumber).ToList();

            return processedList;
        }
    }
}
