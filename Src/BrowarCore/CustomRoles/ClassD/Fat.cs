using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Fat : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#FF8E00>GRUBY</color>";
    public override string Description { get; set; } = "gruba klasa d która potrafi <color=yellow>zjeść przedmiot</color> w ręku" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Gruby";
    public override uint Id { get; set; } = 107;
    public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 0;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override uint RechargeTime { get; set; } = 45;
    public override bool SpawnCooldown { get; set; } = false;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;
	
	if (ev.Player.CurrentItem == null) {
	    ev.Player.ShowHint("\n\n\n\n\n\n\n\n\n\n\n<size=60%>weź do ręki przedmiot żeby użyć umiejętności</size>");
	    return;
	}
        switch (ev.Player.CurrentItem.Category) {
            case ItemCategory.Firearm:
            case ItemCategory.SpecialWeapon:
            case ItemCategory.SCPItem:
                ev.Player.ShowHint("\n\n\n\n\n\n\n\n\n\n\n<size=60%>nie można zjeść przedmiotu tego typu</size>");
                return;
            case ItemCategory.Grenade:
                if (ev.Player.CurrentItem.Type == ItemType.GrenadeHE)
                    Timing.CallDelayed(3, () => ev.Player.Explode());
                else
                    ev.Player.EnableEffect(EffectType.Burned, 1, 10);

	        ev.Player.RemoveItem(ev.Player.CurrentItem);
                break;
            default:
	        ev.Player.RemoveItem(ev.Player.CurrentItem);
	        ev.Player.AddAhp(20, 100, 0.1f);;
	        Mark(ev.Player);
	        Cooldown(ev.Player);
                break;
        }
    }
    protected override void RoleAdded(Features.Player player) {
        player.SetScale(new UnityEngine.Vector3(1.05f, 0.94f, 1.1f));
        base.RoleAdded(player);
    }
    protected override void RoleRemoved(Features.Player player) {
        player.SetScale(UnityEngine.Vector3.one);
        base.RoleRemoved(player);
    }
}
