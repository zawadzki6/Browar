using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Paranoid : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#5B6370>PARANOICZNY OCHRONIARZ</color>";
    public override string Description { get; set; } = "Ucieka tam gdzie pieprz rośnie.\n\n<color=yellow>Naciśnij lewyalt (noclip) aby użyć umiejętności</color>";
    public override string CustomInfo { get; set; } = "Paranoiczny";
    public override uint Id { get; set; } = 203;
    public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
    public override int MaxHealth { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override float SpawnChance { get; set; } = 100;
    public override int MinPlayers { get; set; } = 1;

    public override uint RechargeTime { get; set; } = 75;
    public override bool SpawnCooldown { get; set; } = false;
    public override string Emote { get; set; } = "💨";

    public override List<string> Inventory { get; set; } = new() {
	ItemType.GunFSP9.ToString(),
	ItemType.ArmorLight.ToString(),
	ItemType.Medkit.ToString(),
	ItemType.Radio.ToString(),
	ItemType.GrenadeFlash.ToString(),
	ItemType.KeycardGuard.ToString()
    };
    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new() { {AmmoType.Nato9, 60} };

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

	ev.Player.EnableEffect(EffectType.Invigorated, 10, true);
	ev.Player.EnableEffect(EffectType.MovementBoost, 10, 2);

	if (ev.Player.GetEffect(EffectType.Scp207).Intensity != 0 || ev.Player.GetEffect(EffectType.AntiScp207).Intensity != 0) {
	    ev.Player.EnableEffect(EffectType.Vitality, 1, 21, true);
	    Timing.CallDelayed(20, () => ev.Player.DisableEffect(EffectType.Poisoned));
	}
	if (ev.Player.GetEffect(EffectType.Scp1853).Intensity == 0) {
	    ev.Player.EnableEffect(EffectType.Scp1853, 1, 20);
            Countdown(ev.Player, 20);
        } else {
	    ev.Player.EnableEffect(EffectType.MovementBoost, 30, 8);
            Countdown(ev.Player, 8);
        }

        Mark(ev.Player);
        Cooldown(ev.Player);
    }
}
