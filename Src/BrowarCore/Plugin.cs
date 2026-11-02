using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;

namespace BrowarCore;

public class Plugin : Features.Plugin<Config> {

    public override string Name => "BrowarCore";
    public override string Author => "Zawadzki <contact@zawadzki.cc>";
    public override string Prefix => "browar";
    public override Version Version => new Version(3, 1, 0);

    public static Plugin? Instance;

    public override void OnEnabled() {
        Instance = this;

        if (Config.CustomItems)
            CustomItem.RegisterItems();
        if (Config.CustomRoles)
            CustomRole.RegisterRoles(false);

        if (Config.AddonLucky500)
            Handlers.Player.ConsumingItem += Addons.Lucky500.UsedItem;
        if (Config.AddonSpawnProtect) {
            Handlers.Server.RoundEnded += Addons.SpawnProtect.RoundEnd;
            Handlers.Player.Spawned += Addons.SpawnProtect.OnSpawned;
            Handlers.Server.RespawnedTeam += Addons.SpawnProtect.WaveSpawned;
        }
        if (Config.PatchScpSwap)
            Handlers.Player.ChangingRole += Patches.ScpSwap.IncompatibleScps;
        base.OnEnabled();
    }

    public override void OnDisabled() {
        Instance = null;

        if (Config.CustomItems)
            CustomItem.RegisterItems();
        if (Config.CustomRoles)
            CustomRole.RegisterRoles(false);

        if (Config.AddonLucky500)
            Handlers.Player.ConsumingItem -= Addons.Lucky500.UsedItem;
        if (Config.AddonSpawnProtect) {
            Handlers.Server.RoundEnded -= Addons.SpawnProtect.RoundEnd;
            Handlers.Player.Spawned -= Addons.SpawnProtect.OnSpawned;
            Handlers.Server.RespawnedTeam -= Addons.SpawnProtect.WaveSpawned;
        }
        if (Config.PatchScpSwap)
            Handlers.Player.ChangingRole -= Patches.ScpSwap.IncompatibleScps;
	base.OnDisabled();
    }
}

