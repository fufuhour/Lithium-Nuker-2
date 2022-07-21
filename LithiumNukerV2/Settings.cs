using System.Drawing;
using Veylib.ICLI;

// All of the settings can be changed from here

namespace LithiumNukerV2
{
    public class Settings
    {
        public static class Style
        {
            public static readonly Color Accent = Color.FromArgb(146, 13, 255);
            public static readonly SelectionMenu.Settings SelectionMenuStyle = new SelectionMenu.Settings
            {
                Style = new SelectionMenu.Style
                {
                    PreOptionColor = Accent,
                    SelectionHighlightColor = Accent,
                    SelectedColor = Accent,
                    SelectedFormatTags = Core.Formatting.Bold,
                    SelectionFormatTags = Core.Formatting.Italic,
                    NeutralColor = Color.White
                }
            };
            public static readonly ProgressBar.Settings ProgressBarStyle = new ProgressBar.Settings
            {
                Style = new ProgressBar.Style
                {
                    EdgeColor = Accent,
                    FillingChar = '#'
                }
            };
        }

        public static bool Debug = false;
        
        public static readonly string Logo = @"
            ▒▒▒          ░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒      ▒░▒▒▒      ▒▒▒                                 ▒░░░▒
          ▒▒██▒         ▒▒▒▒▒░▒███████████████████░▒▒▒▒▒▒▒████▒     ▒▒▒█▒       ▒▒▒     ▒▒     ▒ ▒      ▒████▒
        ▒█████▒        ▒████▒   ▒▒▒▒▒█████▒▒       ▒▒░▒▒▒▒████▒    ▒████▒    ░▒▒░░▒   ▒░█▒   ▒░▒░▒      ▒████▒
       ▒█████▒        ▒▒████▒      ░▒████▒▒      ▒░███▒▒▒█████▒   ▒▒████▒   ▒████▒   ▒██▒▒  ▒▒███▒▒    ▒████▒▒
      ▒▒████▒         ▒████▒▒      ▒████░▒      ▒░████▒▒▒████▒    ▒████▒▒   ▒███▒  ▒▒███▒   ▒█████▒ ▒▒█████▒▒
     ░▒████▒          ▒████▒      ▒▒████▒   ▒▒▒░█████▒▒▒████▒▒    ▒████▒  ▒▒███▒▒  ▒████▒   ▒█████▒▒██████▒▒
     ▒░████▒         ░▒███▒       ▒███░▒   ▒▒▒░████████████▒▒▒▒  ▒▒███▒   ▒████▒  ▒█████▒  ▒░████████████▒▒
     ▒████▒          ▒▒███▒      ▒░███▒▒      ▒████▒▒▒▒████▒▒▒   ▒▒██░▒   ▒███▒  ▒▒████▒   ▒███▒▒███▒▒███▒
  ▒▒▒█████▒▒░░░▒░█░▒▒▒███▒       ▒▒██▒▒      ░▒███▒  ▒███▒▒      ▒███▒    ▒███▒ ▒▒████▒   ▒▒███▒▒▒█▒░▒██▒▒
 ░ ░▒█████████████░▒▒▒██▒▒       ▒███▒       ▒███▒▒  ▒███▒       ▒██▒▒    ▒░█████████▒▒   ▒██▒▒▒░▒▒▒▒▒█▒▒
   ▒▒▒░░▒▒▒▒▒▒       ▒░▒▒        ▒░██▒       ▒██░▒   ▒██▒░       ▒░▒░       ▒░█████░▒     ▒█▒       ▒░▒▒
                    ▒░▒          ▒▒█▒        ▒█▒▒   ▒░▒▒        ▒▒▒                                 ▒▒▒
                                 ▒░▒▒       ▒▒▒     ▒▒░
                                 ▒▒▒";
        public static string Token;
        public static long? GuildId = null;
        public static int Threads = 40;
        public static int ConnectionLimit = 25; // 10 concurrent connections
        public static readonly string WebhookName = ".gg/lithium runs cord";
        public static readonly string AvatarUrl = "https://camo.githubusercontent.com/450b75468a748fbd8e4b3116c378cc9cfdcadd8b0b0e676821a6f873fcb85f53/68747470733a2f2f7665726c6f782e63632f7261772f535865475144";
    }
}
