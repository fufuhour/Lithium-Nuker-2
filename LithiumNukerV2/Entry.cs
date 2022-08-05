// System
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Drawing;
using System.Net;
using System.IO;
using System.Text;
using Microsoft.Win32;

// Custom
using Veylib.ICLI;
using Veylib.Utilities;

/*
 * Nuking com is shit
 *  - verlox 2.14.2022
 *  
 * idk why but i get bored and just decide to modify the dumbest shit on this every once in a while
 *  - verlox 6.9.2022
 * */

namespace LithiumNukerV2
{
    internal class Entry
    {
        // Setup CLIUI
        public static Core core = Core.GetInstance();

        // Parse entry point args
        private static void parseArgs(string[] args)
        {
            for (var x = 0;x < args.Length;x++)
            {
                bool succ;

                switch (args[x].ToLower())
                {
                    // Put this shit in debug
                    case "--debug":
                        Settings.Debug = true;
                        break;
                    // Set token on start
                    case "--token":
                        x++;
                        Settings.Token = args[x];
                        break;
                    // Set guild id
                    case "--guild":
                        x++;
                        succ = long.TryParse(args[x], out long lid);
                        if (!succ)
                            core.WriteLine(Color.Red, "--guild argument value invalid");
                        Settings.GuildId = lid;
                        break;
                    // Set threads
                    case "--threads":
                        x++;
                        succ = int.TryParse(args[x], out int threads);
                        if (!succ)
                            core.WriteLine(Color.Red, "--threads argument value invalid");
                        Settings.Threads = threads;
                        break;
                    // Set connection limit
                    case "--connection-limit":
                        x++;
                        succ = int.TryParse(args[x], out int connlimit);
                        if (!succ)
                            core.WriteLine(Color.Red, "--connection-limit argument value invalid");
                        Settings.ConnectionLimit = connlimit;
                        break;
                    // Means that there was an unparsed arg that is unknown
                    default:
                        core.WriteLine(Color.Red, $"Invalid argument: {args[x].ToLower()}");
                        break;
                }
            }
        }

        // Entry point
        static void Main(string[] args)
        {
            #region Setting up the UI
            var props = new Core.StartupProperties {
                MOTD = new Core.StartupMOTDProperties
                {
                    Text = "fuck skids | verlox & russian heavy on top",
                    DividerColor = Settings.Style.Accent,
                },
                ColorRotation = 260,
                Logo = new Core.StartupLogoProperties
                {
                    Text = $"{Settings.Logo}\n{Core.Formatting.HorizontalRainbow(Core.Formatting.Center("made by verlox and russian heavy", 111), 180, 2)}",
                    AutoCenter = true,
                },
                //LogoString = $"{Settings.Logo}",
                DebugMode = Settings.Debug,
                //Author = new Core.StartupAuthorProperties {
                //    Url = "verlox.cc & russianheavy.xyz",
                //    Name = "verlox & russian heavy"
                //},
                Title = new Core.StartupConsoleTitleProperties {
                    Text = "Lithium Nuker V2"
                },
                SplashScreen = new Core.StartupSpashScreenProperties
                {
                    AutoGenerate = true,
                    DisplayProgressBar = true,
                    ProgressBarSettings = new ProgressBar.Settings
                    {
                        Style = new ProgressBar.Style
                        {
                            EdgeColor = Settings.Style.Accent
                        }
                    }
                }
            };

            // No.
            #if DEBUG
                Settings.Debug = true;
                props.SplashScreen = null;
            #endif

            core.Start(props);
            #endregion

            // Parse the args
            parseArgs(args);

            // Setup the stupid ass connection limits
            ServicePointManager.DefaultConnectionLimit = Settings.ConnectionLimit;
            ServicePointManager.Expect100Continue = false;

            var updater = new AutoUpdater();
            updater.CurrentSettings.Mode = AutoUpdater.Settings.UpdateMode.Update;
            updater.CurrentSettings.CurrentVersion = core.GetAutoVersion();
            updater.CurrentSettings.LatestVersionUri = new Uri("https://pastebin.com/raw/hpnq3itV");
            updater.CurrentSettings.AutoRestart = false;
            updater.CurrentSettings.Github.Enabled = true;
            updater.CurrentSettings.Github.Username = "verlox";
            updater.CurrentSettings.Github.Repo = "Lithium-Nuker-2";
            updater.CurrentSettings.Github.AssetNameContains = "LithiumNukerV2";
            updater.CurrentSettings.Github.FileNameContains = "LithiumNukerV2";

            AutoUpdater.UpdateAvailable += (_, args2) =>
            {
                Debug.WriteLine(((AutoUpdater.UpdateEventArgs)args2).Message);
            };

            AutoUpdater.UpdateFailed += (_, args2) =>
            {
                Debug.WriteLine(((AutoUpdater.UpdateEventArgs)args2).Exception);
            };

            AutoUpdater.RestartRequired += (_, _2) =>
            {
                Debug.WriteLine("close application to finish update");
            };

            if (!updater.Check())
            {
                // Open options
                Picker.Choose();
            }
        }
    }
}
