﻿// System
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Net;
using System.Diagnostics;

// Nuget
using Newtonsoft.Json;

// Custom
using Veylib.ICLI;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LithiumNukerV2
{
    internal class Picker
    {
        public static Core core = Core.GetInstance();

        // Components
        private static Channels channels;
        private static Webhooks webhooks;
        private static Users users;
        private static Roles roles;
        private static Bot bot = new Bot();

        private static void opts()
        {
            core.Clear();

            core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, "Choose an option");
            var option = new SelectionMenu("Webhook spam", "Create/remove channels", "Create/remove roles", "Ban members").Activate();

            switch (option)
            {
                case "Webhook spam":
                    whSpam();
                    break;
                case "Create/remove channels":
                    core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, "Channel mode?");
                    switch (new SelectionMenu("Create", "Remove").Activate())
                    {
                        case "Create":
                            createChans();
                            break;
                        case "Remove":
                            new Thread(() => { channels.Nuke(); }).Start();
                            break;
                    }
                    break;
                case "Create/remove roles":
                    core.WriteLine("Role mode?");
                    switch (new SelectionMenu("Create", "Remove").Activate())
                    {
                        case "Create":
                            createRoles();
                            break;
                        case "Remove":
                            new Thread(() => { roles.Nuke(); }).Start();
                            break;
                    }
                    break;
                case "Ban members":
                    banAll();
                    break;
            }

            /*
            // Create versions table
            var vertable = new AsciiTable(new AsciiTable.Properties { Colors = new AsciiTable.ColorProperties { RainbowDividers = true } });
            vertable.AddColumn($"Version - {LithiumShared.GetVersion()}");
            vertable.AddColumn($"Core version - {LithiumShared.GetVersion(typeof(Bot).Assembly)}");

            // Create options table
            var optable = new AsciiTable(new AsciiTable.Properties { Colors = new AsciiTable.ColorProperties { RainbowDividers = true } });
            optable.AddColumn("1 - Webhook spam channels");
            optable.AddColumn("2 - Create channels");
            optable.AddColumn("3 - Delete channels");
            optable.AddRow("4 - Create roles", "5 - Delete roles", "6 - Ban members");

            // Clear console
            core.Clear();

            // Print tables
            vertable.WriteTable();
            optable.WriteTable();

            // Get the choice
            var choice = core.ReadLine("Choice : ");

            // Parse the input as an int
            if (!int.TryParse(choice, out int ch))
            {
                core.WriteLine("Invalid choice");
                core.Delay(2500);
                opts();
                return;
            }

            // Actually check input
            switch (ch)
            {
                case 1:
                    whSpam();
                    break;
                case 2:
                    createChans();
                    break;
                case 3:
                    new Thread(() => { channels.Nuke(); }).Start();
                    break;
                case 4:
                    createRoles();
                    break;
                case 5:
                    new Thread(() => { roles.Nuke(); }).Start();
                    break;
                case 6:
                    banAll();
                    break;
                default:
                    core.WriteLine("Invalid choice");
                    core.Delay(1000);
                    opts();
                    break;
            }

            Debug.WriteLine($"Picker option {ch} finished");
            */
            //core.Delay(2500);
        }

        private static void addToken(string token)
        {
            Settings.Token = token;
        }

        public static void Choose()
        {
            EnterToken:

            // Clear console
            core.Clear();

            // Regex format for bot tokens
            var regex = new Regex(@"[\w-_]{24}.[\w-_]{6}.[\w-_]{27}");

            // Find any stored tokens
            bool skipEnter = false;
            if (File.Exists("config.json"))
            {
                try
                {
                    dynamic conf = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));

                    var menu = new SelectionMenu(Settings.Style.SelectionMenuStyle);

                    foreach (var t in conf.tokens)
                    {
                        var inf = Users.GetUserInfo(t.ToString(), "@me", true);

                        menu.AddOption($"{inf.id} - {inf.username}");
                    }

                    menu.AddOption("Add a new token");

                    core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, "Select a token to use");
                    menu.AddOption("Add a new token");

                    var opt = menu.Activate();

                    if (opt == "Add a new token")
<<<<<<< HEAD
=======
                    {
                        skipEnter = true;
                        goto TInput;
                    }

                    string tokenId = Convert.ToBase64String(Encoding.ASCII.GetBytes(opt.Split(' ')[0]));
                  
                    // I FUCKING HATE THIS CODE
                    // I literally spent about a half hour on looking for more efficient methods and more simple methods
                    // C# wanted to be a cunt though.
                    foreach (string t in conf.tokens)
>>>>>>> 2458d1f06c482a24add49f137141e992b93e3bcc
                    {
                        addToken(core.ReadLine("Bot Token $ "));
                    } else
                    {
                        string tokenId = Convert.ToBase64String(Encoding.ASCII.GetBytes(opt.Split(' ')[0]));
                  
                        // I FUCKING HATE THIS CODE
                        // I literally spent about a half hour on looking for more efficient methods and more simple methods
                        // C# wanted to be a cunt though.
                        foreach (string t in conf.tokens)
                        {
                            if (t.Contains(tokenId))
                            {
                                Settings.Token = t;
                                break;
                            }
                        }
                    }
                } catch (Exception ex)
                {
                    Debug.Write(ex);
                    core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Failed to import ", Color.Pink, "config.json");
                }
            }

            TInput:

            string token = Settings.Token;

            // If the token is null, prompt for input
            if (token == null)
                token = core.ReadLine("Bot token $ ");

            // See if the token matches the regex pattern
            if (regex.Match(token).Length == 0)
            {
                core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Input does not conform to token format.");
                core.Delay(2500);

                if (skipEnter)
                    goto TInput;
                else
                    goto EnterToken;
            }
            else
            {
                // Test token
                if (!bot.TestToken(token))
                {
                    core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Invalid bot token.");
                    core.Delay(2500);

                    if (skipEnter)
                        goto TInput;
                    else
                        goto EnterToken;
                }
                else
                    Settings.Token = token;
            }

            bool success = Settings.GuildId != null;
            long gid;

            EnterGuildId:

            // Clear console
            core.Clear();

            // Try to parse the input as a long
            if (!success)
                success = long.TryParse(core.ReadLine("Guild ID $ "), out gid);
            else
                gid = (long)Settings.GuildId;
    
            if (!success)
            {
                core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Guild ID couldn't be parsed.");
                core.Delay(2500);
                goto EnterGuildId;
            } else
            {
                // Check if its in the guild, if not, write out
                if (!bot.IsInGuild(Settings.Token, gid))
                {
                    core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Bot is not in guild.");
                    core.Delay(2500);
                    goto EnterGuildId;
                }
                else
                    Settings.GuildId = gid;
            }

            // Setup new instances
            channels = new Channels(Settings.Token, (long)Settings.GuildId, Settings.Threads);
            webhooks = new Webhooks(Settings.Token, (long)Settings.GuildId, Settings.Threads);
            users = new Users(Settings.Token, (long)Settings.GuildId, Settings.Threads);
            roles = new Roles(Settings.Token, (long)Settings.GuildId, Settings.Threads);

            // Finish events will retrigger options
            Channels.Finished += () => { core.Delay(1000); opts(); };
            Webhooks.Finished += () => { core.Delay(1000); opts(); };
            Users.Finished += () => { core.Delay(1000); opts(); };
            Roles.Finished += () => { core.Delay(1000); opts(); };

            // Show options
            opts();
        }

        private static void whSpam()
        {
            // User input
            string content = core.ReadLine("Content : ");
            bool succ = int.TryParse(core.ReadLine("Amount of messages per webhook : "), out int amnt);

            // Check if conversion was successful
            if (!succ)
            {
                core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Failed to parse amount of messages to an int");
                core.Delay(5000);
                opts();
                return;
            }

            string check = core.ReadLine("Reuse existing webhooks (causes long delay) : [y/N] ");

            bool scan = true;
            if (check == "" || check.ToLower() == "n")
                scan = false;

            // Auto fill content
            if (content == "")
                content = "@everyone discord.gg/lith";

            // Spam
            new Thread(() => { webhooks.Spam(Settings.WebhookName, Settings.AvatarUrl, content, amnt, scan); }).Start();
        }

        private static void createChans()
        {
            // User input
            string name = core.ReadLine("Channel name : ");
            string type = core.ReadLine("Type : [text, voice] ");
            bool succ = int.TryParse(core.ReadLine("Amount : "), out int amnt);

            if (!succ)
            {
                core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Failed to parse amount to an int");
                core.Delay(5000);
                opts();
                return;
            }

            // Default to text
            if (type == "")
                type = "text";

            // Validate input
            if (type.ToLower() != "text" && type.ToLower() != "voice")
            {
                core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Invalid channel type");
                core.Delay(5000);
                opts();
                return;
            }

            type = type.Substring(0, 1).ToUpper() + type.Substring(1).ToLower();

            // Autofill name as this if blank
            if (name == "")
                name = "ran by lithium";

            // Spam
            new Thread(() => { channels.Spam(name, (Channels.Type)Enum.Parse(typeof(Channels.Type), type), amnt); }).Start();
        }

        private static void createRoles()
        {
            string name = core.ReadLine("Role name : ");
            bool succ = int.TryParse(core.ReadLine("Amount : "), out int amnt);

            if (!succ)
            {
                core.WriteLine(new Core.MessageProperties { Time = null, Label = null }, Color.Red, "Failed to parse amount to an int");
                return;
            }

            new Thread(() => { roles.Spam(name, amnt, ColorTranslator.FromHtml("#6B00FF")); }).Start();
        }

        private static void banAll()
        {
            // User input
            string inp = core.ReadLine("Ban IDs : [y/N] ");
            bool banIds;

            // Input parsing
            if (inp == "" || inp.ToLower() == "n")
                banIds = false;
            else
                banIds = true;

            // If banning ids, download the ids
            if (banIds && !File.Exists("ids.txt"))
                new WebClient().DownloadFile("https://pastebin.com/raw/3NPBvgK5", "ids.txt");

            new Thread(() => { users.BanAll(banIds); }).Start();
        }
    }
}
