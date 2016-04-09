using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;


namespace TimelordDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
 
    }

    public sealed class WindowFunctions
    {
        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public bool BringToFront(string title)
        {

            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            //the window is hidden so we restore it
            ShowWindow(handle, (int)ShowWindowEnum.ShowNormal);
            ShowWindow(handle, (int)ShowWindowEnum.Restore);

            // Make Calculator the foreground application
            return (SetForegroundWindow(handle));
        }

        public string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
    };

    public class TimeEntity
    {
        public string Customer { get; set; }
    }

    public class TimeContext: DbContext
    {
        // Define table
        public DbSet<TimeEntity> TimeTable { get; set; }

        // Connect context with database
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var connectionStringBuilder = new SQLiteConnectionStringBuilder { DataSource = "time.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SQLiteConnection(connectionString);

            modelBuilder.Build(connection);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
