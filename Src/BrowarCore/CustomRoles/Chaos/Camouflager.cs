using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Camouflager : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#008F1C>KAMUFLAŻYSTA</color>";
    public override string Description { get; set; } = "potrafi stać się prawie <color=yellow>niewidocznym</color> na koszt zwinności" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Kamuflażysta";
    public override uint Id { get; set; } = 501;
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
        ItemType.Painkillers.ToString()
    };
    public override bool SpawnCooldown { get; set; } = false;
    public override uint RechargeTime { get; set; } = 60;
    public override string Emote { get; set; } = "👒";

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        ev.Player.EnableEffect(EffectType.Fade, 175, 8);
        ev.Player.EnableEffect(EffectType.SilentWalk, 9, 8);
        ev.Player.EnableEffect(EffectType.Slowness, 90, 8);
        Countdown(ev.Player, 8);

	Mark(ev.Player);
	Cooldown(ev.Player);
    }
}
