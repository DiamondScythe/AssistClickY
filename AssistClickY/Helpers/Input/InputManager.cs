using AssistClickY.Data;
using AssistClickY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.Helpers.Input
{
    public class InputManager
    {
        public static List<Hotkey> GetCustomHotkeys(AssistClickYContext dbContext)
        {
            var list = new List<Hotkey>();
            list = dbContext.GetAllHotkeys();
            return list;
        }
    }
}
