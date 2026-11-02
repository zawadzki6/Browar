using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Ghost : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#FF8E00>DUCH</color>";
    public override string Description { get; set; } = "klasa d która potrafi stać się duchem.\n\n<color=yellow>naciśnij lewyalt (noclip) aby użyć umiejętności</color>";
    public override string CustomInfo { get; set; } = "Duch";
    public override uint Id { get; set; } = 103;
    public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override uint RechargeTime { get; set; } = 180;
    public override bool SpawnCooldown { get; set; } = true;
    public override string Emote { get; set; } = "👻";

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

	ev.Player.EnableEffect(EffectType.FogControl, 7, 15);
	ev.Player.EnableEffect(EffectType.Ghostly, 1, 15);
	ev.Player.EnableEffect(EffectType.Fade, 120, 15);
	ev.Player.EnableEffect(EffectType.Slowness, 10, 15);
	ev.Player.EnableEffect(EffectType.SilentWalk, 9, 15);

        Timing.CallPeriodically(14, 1, () => ev.Player.DropHeldItem());

	Countdown(ev.Player, 15);
	Mark(ev.Player);
	Cooldown(ev.Player);
    }
}
