using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;
using Exiled.CustomRoles.API.Features;

namespace BrowarCore.CustomRoles;

public class NadeZombie : CustomRole {
    public override string Name { get; set; } = "<color=#EC2222>ZAMACHOWIEC</color>";
    public override string Description { get; set; } = "na śmierci wypuszcza <color=yellow>wybucha</color>";
    public override string CustomInfo { get; set; } = "Zamachowiec";
    public override uint Id { get; set; } = 801;

    public override RoleTypeId Role { get; set; } = RoleTypeId.Scp0492;
    public override int MaxHealth { get; set; } = 400;
    public override float SpawnChance { get; set; } = 10;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    protected void Died(DyingEventArgs ev) {
        if (!Check(ev.Player))
            return;

        ev.Player.Explode(ProjectileType.FragGrenade, ev.Player);
    }
    protected override void SubscribeEvents() {
        Handlers.Player.Dying += Died;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
        Handlers.Player.Dying -= Died;
        base.UnsubscribeEvents();
    }
}
