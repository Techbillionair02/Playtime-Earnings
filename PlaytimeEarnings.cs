using Oxide.Core.Plugins;
using Oxide.Core;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("Playtime Earnings", "GaryG", "1.0.0")]
    public class PlaytimeEarnings : RustPlugin
    {
        [PluginReference] private Plugin Economics;

        private const double PayRatePerHour = 20.0;
        private const float PayIntervalMinutes = 1f;

        private Dictionary<ulong, double> pending = new Dictionary<ulong, double>();

        private void OnServerInitialized()
        {
            if (Economics == null)
            {
                PrintWarning("Economics plugin NOT FOUND — playtime earnings disabled.");
            }

            timer.Every(PayIntervalMinutes * 60, PayPlayers);
        }

        private void PayPlayers()
        {
            if (Economics == null) return;

            double payPerInterval = PayRatePerHour / 60.0; // $0.3333 per minute

            foreach (var player in BasePlayer.activePlayerList)
            {
                if (player == null || !player.userID.IsSteamId()) continue;

                ulong id = player.userID;

                if (!pending.ContainsKey(id))
                    pending[id] = 0;

                pending[id] += payPerInterval;

                // Pay only whole cents to avoid decimal overflow
                if (pending[id] >= 0.01)
                {
                    double amountToPay = pending[id];
                    pending[id] = 0;

                    Economics.Call("Deposit", id, amountToPay);
                }
            }
        }

        private void OnPlayerDisconnected(BasePlayer player, string reason)
        {
            if (player == null) return;

            ulong id = player.userID;

            if (pending.ContainsKey(id))
            {
                pending.Remove(id);
            }
        }
    }
}