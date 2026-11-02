using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class CombatMedic: BrowarCustomRole {
    public override string Name { get; set; } = "<color=#0096FF>MEDYK BOJOWY</color>";
    public override string Description { get; set; } = "operator specjalizujący się w leczeniu sojuszników" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Medyk bojowy";
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
        ItemType.Medkit.ToString(),
        "<color=#ffda55>MORFINA</color>",
        "<color=#f67eff>BROŃ MEDYCZNA</color>"
    };
    public override bool SpawnCooldown { get; set; } = false;
    public override uint RechargeTime { get; set; } = 20;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        if (ev.Player.IsCuffed) {
            ev.Player.ShowHint(Etc.AbilityUnavailable);
            return;
        }
        if (ev.Player.TryGetRaycastedPlayer(8, default, out Features.Player? player) && player.LeadingTeam == LeadingTeam.FacilityForces) {
            player.Health = player.MaxHealth + 25;
            player.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n<size=60%>uleczono przez {ev.Player.Nickname}");
            ev.Player.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n<size=60%>uleczono {player.Nickname}");
        }
        else {
            ev.Player.ShowHint("\n\n\n\n\n\n\n\n\n\n\n<size=60%>spójrz na sojusznika aby użyć umiejętności</size>");
            return;
        }
	Mark(ev.Player);
	Cooldown(ev.Player);
    }
}
