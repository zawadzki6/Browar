using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Depleter : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#008F1C>WYCZERPYWACZ</color>";
    public override string Description { get; set; } = "wyczerpuje wrogów - <color=yellow>zabiera stamine</color>";
    public override string CustomInfo { get; set; } = "Wyładowywacz";
    public override uint Id { get; set; } = 503;
    public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosRifleman;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 0;
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
    public override uint RechargeTime { get; set; } = 60;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        byte count = 0;
        foreach (Features.Player player in Features.Player.List) {
            if (ev.Player.LeadingTeam != player.LeadingTeam)
            if (UnityEngine.Vector3.Distance(ev.Player.Position, player.Position) <= 14) {
                player.Stamina = 0;
                player.EnableEffect(EffectType.Exhausted, 1, 10, true);
                count++;
            }
        }
        ev.Player.ShowHint($"\n\n\n\n\n\n<size=60%>wyczerpano {count} graczy</size>");

	Mark(ev.Player);
	Cooldown(ev.Player);
    }
}
