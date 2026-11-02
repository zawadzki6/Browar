using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;

namespace BrowarCore.Addons;

public class SpawnProtect {
    public static void OnSpawned(SpawnedEventArgs ev) {
        Timing.CallDelayed(Timing.WaitForOneFrame, () => {
            if (ev.Player.IsSpawnProtected)
                ev.Player.ShowHint($"<voffset=300><size=60%><color=#e3af35>[ ochrona spawnu aktywna ]</color></size></voffset>");
            });
    }
    public static void RoundEnd(RoundEndedEventArgs ev) {
        foreach (Features.Player player in Features.Player.List)
            player.IsSpawnProtected = false;
    }
    public static void WaveSpawned(RespawnedTeamEventArgs ev) {
        foreach (Features.Player player in ev.Players)
            player.IsSpawnProtected = false;
    }
}
