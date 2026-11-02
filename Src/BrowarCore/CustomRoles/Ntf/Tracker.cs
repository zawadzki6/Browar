// tracker
// track info (health, location, etc.) of last damaged player
using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features;

namespace BrowarCore.CustomRoles;

public class Tracker : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#0096FF>TRACKER</color>";
    public override string Description { get; set; } = "zamienia się miejscami z najbliższym losowym wrogiem" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Zamiennik";
    public override uint Id { get; set; } = 605;
    public override RoleTypeId Role { get; set; } = RoleTypeId.NtfPrivate;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };
    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new() {
        {AmmoType.Nato9, 160},
        {AmmoType.Nato556, 40}
    };
    public override List<string> Inventory { get; set; } = new() {
        ItemType.KeycardMTFOperative.ToString(),
        ItemType.GunCrossvec.ToString(),
        ItemType.ArmorCombat.ToString(),
        ItemType.Radio.ToString(),
        ItemType.Medkit.ToString()
    };
    public override bool SpawnCooldown { get; set; } = true;
    public override uint RechargeTime { get; set; } = 90;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;
        
        foreach (Player player in Player.List) {
            Mark(ev.Player);
            Cooldown(ev.Player);
            return;
        }

    }
    protected void Attacking(HurtingEventArgs ev) {
        if (!Check(ev.Attacker))
            return;
    }
}
