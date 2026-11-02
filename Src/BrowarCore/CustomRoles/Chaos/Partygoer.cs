using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;
using Exiled.CustomItems.API.Features;

namespace BrowarCore.CustomRoles;

public class Partygoer : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#008F1C>IMPREZOWICZ</color>";
    public override string Description { get; set; } = "zrób impreze! fabrykuje <color=yellow>granaty imprezowe</color>" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Imprezowicz";
    public override uint Id { get; set; } = 502;
    public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosRifleman;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };
    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new() { {AmmoType.Nato762, 120} };
    public override List<string> Inventory { get; set; } = new() {
        ItemType.KeycardChaosInsurgency.ToString(),
        ItemType.GunAK.ToString(),
        ItemType.ArmorCombat.ToString(),
        ItemType.Medkit.ToString(),
        ItemType.Painkillers.ToString(),
    };
    public override bool SpawnCooldown { get; set; } = false;
    public override uint RechargeTime { get; set; } = 40;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;
        
        if (ev.Player.IsInventoryFull) {
            ev.Player.ShowHint(Etc.AbilityUnavailable);
            return;
        }
        CustomItem nade = new BrowarCore.Modules.CustomItems.PartyNade();
        CustomItem.TryGive(ev.Player, nade.Name, true);

	Mark(ev.Player);
	Cooldown(ev.Player);
    }
}
