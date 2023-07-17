using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Permissions.Extensions;
using MEC;

namespace SCP_Kick.com.github.tendrilll.scp_kick {
    public class EventHandler {
        private readonly bool _debugMode;
        private readonly Plugin<Config> _main;
        public EventHandler(Plugin<Config> plugin) {
            _main = plugin;
            _debugMode = plugin.Config.Debug;
            if (_debugMode) {
                Log.Info("Loading EventHandler");
            }
        }
        
        public void OnJoin(VerifiedEventArgs args) {
            Timing.CallDelayed(0.5f, ()=>{
                if (_debugMode) {
                    Log.Info(args.Player.Id + args.Player.DisplayNickname);
                    Log.Info("Checking for permission " + _main.Config.Permission + ": " + args.Player.CheckPermission(_main.Config.Permission));
                    Log.Info("Checking if NW staff: " + args.Player.IsNorthwoodStaff);
                }
                //This cryptic statement translates as: you can play if you have the perm OR you're NW staff and the bypass is on.
                if (!args.Player.CheckPermission(_main.Config.Permission) || (args.Player.IsNorthwoodStaff && !_main.Config.NWBypass)) {
                    args.Player.Kick(_main.Config.KickMessage);
                }
            });
        } 
    }
}
