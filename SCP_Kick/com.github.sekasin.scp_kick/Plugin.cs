using System;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;

namespace SCP_Kick.com.github.sekasin.scp_kick {
    public class SCP_Kick : Plugin<Config> {
        public override string Name => "SCP_Kick";
        public override string Author => "TenDRILLL";
        public override Version Version => new Version(1, 1, 0);
        public EventHandler EventHandler;
        
        public override void OnEnabled() {
            Log.Info("SCP_Kick loading...");
            if (!Config.IsEnabled) {
                Log.Warn("SCP_Kick disabled from config, unloading...");
                OnDisabled();
                return;                
            }
            if (Config.Permission.Length == 0) {
                Log.Warn("SCP_Kick permission missing, unloading...");
                OnDisabled();
                return;    
            }
            EventHandler = new EventHandler(this);
            Player.Verified += EventHandler.OnJoin;
            Log.Info("SCP_Kick loaded.");
        }

        public override void OnDisabled()
        {
            Player.Verified -= EventHandler.OnJoin;
            Log.Info("SCP_Kick unloaded.");
        }
    }
}
