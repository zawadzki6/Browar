using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Addict : CustomRole {
    public override string Name { get; set; } = "<color=#FF8E00>NAŁOGOWIEC</color>";
    public override string Description { get; set; } = "Klasa D pasywnie <color=yellow>odporna na minusy SCP-207</color>.";
    public override string CustomInfo { get; set; } = "Nałogowiec";
    public override uint Id { get; set; } = 101;
    public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 0;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public void AddictCola(HurtingEventArgs ev) {
	if (!Check(ev.Player))
	    return;

	if (ev.DamageHandler.Type == DamageType.Scp207)
	    ev.IsAllowed = false;
    }

    protected override void SubscribeEvents() {
        base.SubscribeEvents();
	Handlers.Player.Hurting += AddictCola;
    }
    protected override void UnsubscribeEvents() {
        base.SubscribeEvents();
	Handlers.Player.Hurting -= AddictCola;
    }
}
