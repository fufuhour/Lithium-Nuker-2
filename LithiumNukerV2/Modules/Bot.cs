// Sys shit
using System;
using System.Net;

// Custom


// Nuget
using Newtonsoft.Json;

namespace LithiumNukerV2
{
    public class Bot
    {
        public bool TestToken(string token)
        {
            var req = WebRequest.Create("https://discord.com/api/v9/users/@me");
            req.Headers.Add("Authorization", $"Bot {token}");

            try
            {
                req.GetResponse();
                return true;
            }
            catch { return false; }
        }

        public bool IsInGuild(string token, long guildId)
        {
            var req = WebRequest.Create($"https://discord.com/api/v9/guilds/{guildId}");
            req.Headers.Add("Authorization", $"Bot {token}");

            try
            {
                req.GetResponse();
                return true;
            }
            catch { return false; }
        }
    }
}
