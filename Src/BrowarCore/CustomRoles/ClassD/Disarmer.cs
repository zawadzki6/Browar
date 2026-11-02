using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Disarmer : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#FF8E00>ROZBRAJNIK</color>";
    public override string Description { get; set; } = "Klasa D która potrafi <color=yellow>rozbroić przeciwnika</color> siłą woli" + Etc.AbilityNotice + "\n<color=red>wymagana broń w ręce</color>";
    public override string CustomInfo { get; set; } = "Rozbrajnik";
    public override uint Id { get; set; } = 106;
    public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override uint RechargeTime { get; set; } = 120;
    public override bool SpawnCooldown { get; set; } = false;


    protected override void Ability(TogglingNoClipEventArgs ev) {
        if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        if (ev.Player.CurrentItem == null) {
            ev.Player.ShowHint($"\n\n\n\n\n\n<size=60%>wymagana broń w ręce</size>");
            return;
        } else if (ev.Player.CurrentItem.Category != ItemCategory.Firearm && ev.Player.CurrentItem.Category != ItemCategory.SpecialWeapon) {
            ev.Player.ShowHint($"\n\n\n\n\n\n<size=60%>wymagana broń w ręce</size>");
            return;
        }

        foreach (Features.Player player in Features.Player.List) {
            if (player == ev.Player)
                continue;
            if (player.LeadingTeam == ev.Player.LeadingTeam)
                continue;
            if (Vector3.Distance(ev.Player.Position, player.Position) > 5)
                continue;

            player.Handcuff();
            player.DropHeldItem();
            Timing.CallDelayed(3, () => player.RemoveHandcuffs());
            break;
        }
        ev.Player.Handcuff();
        ev.Player.DropHeldItem();
        Timing.CallDelayed(3, () => ev.Player.RemoveHandcuffs());

	Mark(ev.Player);
	Cooldown(ev.Player);
    }
}
