using AssistClickY.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.Data
{
    public class AssistClickYContext : DbContext
    {
        public DbSet<Hotkey> Hotkeys { get; set; }

        public string DbPath { get; }

        public AssistClickYContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "AssistClickY.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public List<Hotkey> GetAllHotkeys()
        {
            return Hotkeys.ToList();
        }
    }
}
