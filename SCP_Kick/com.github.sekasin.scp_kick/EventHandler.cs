using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Permissions.Extensions;
using MEC;

namespace SCP_Kick.com.github.sekasin.scp_kick;
public class EventHandler {
    private readonly bool _debugMode;
    private readonly Plugin<Config> _main;
    private readonly string verificationUrl;
    private WebClient client;
    private List<string> cachedResults = new List<string>();

    public EventHandler(Plugin<Config> plugin) {
        _main = plugin;
        _debugMode = plugin.Config.Debug;
        verificationUrl = plugin.Config.verificationUrl;
        
        if (_debugMode) {
            Log.Info("Loading EventHandler");
        }
    }
    
    public void OnJoin(VerifiedEventArgs args) {
        Timing.CallDelayed(1.5f, ()=>{
            if (_debugMode) {
                Log.Info(args.Player.Id + args.Player.DisplayNickname);
                Log.Info("Checking if NW staff: " + args.Player.IsNorthwoodStaff);
            }
            
            string parsedId = args.Player.UserId.Split('@')[0];
            if (cachedResults.Contains(parsedId))
            {
                Log.Debug("Result cached.");
                return;
            }
            client = new WebClient();
            Stream verificationStream = null;
            try
            {
                verificationStream = client.OpenRead(verificationUrl + parsedId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (verificationStream != null)
            {
                StreamReader sr = new StreamReader(verificationStream);
                string result = sr.ReadToEnd();
                sr.Close();
                Log.Info(result);
                if (result == "true")
                {
                    cachedResults.Add(parsedId);
                    if (cachedResults.Count > 50)
                    {
                        cachedResults.RemoveAt(0);
                    }
                    return;
                }
            }

            if (args.Player.IsNorthwoodStaff && _main.Config.NWBypass) return;
            
            args.Player.Kick(_main.Config.KickMessage);
        });
    } 
}