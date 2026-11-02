using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Fabia : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#FF8E00>FABIAN ZEMLIK</color>";
    public override string Description { get; set; } = "Klasa D która potrafi <color=yellow>zjeść przedmiot</color> w ręku" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Fabią";
    public override uint Id { get; set; } = 10;
    public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 0;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override uint RechargeTime { get; set; } = 3;
    public override bool SpawnCooldown { get; set; } = false;


    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;
	
	if (ev.Player.CurrentItem == null) {
	    ev.Player.ShowHint("\n\n\n\n\n\n\n\n\n\n\n<size=60%>weź do ręki przedmiot żeby użyć umiejętności</size>");
	    return;
	}

	ev.Player.RemoveItem(ev.Player.CurrentItem);
	ev.Player.AddAhp(35, 100, 0);;

	Mark(ev.Player);
	Cooldown(ev.Player);
    }
}
