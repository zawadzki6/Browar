using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Switch : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#0096FF>ZAMIENNIK</color>";
    public override string Description { get; set; } = "zamienia się miejscami z najbliższym losowym wrogiem" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Zamiennik";
    public override uint Id { get; set; } = 604;
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
    public override bool SpawnCooldown { get; set; } = false;
    public override uint RechargeTime { get; set; } = 60;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;
        
        foreach (Features.Player player in Features.Player.List) {
            if (ev.Player == player)
                continue;
            if (Vector3.Distance(ev.Player.Position, player.Position) > 16)
                continue;
            if (ev.Player.LeadingTeam == player.LeadingTeam)
                continue;
            
            Vector3 pos = player.Position;
            player.Teleport(ev.Player.Position);
            ev.Player.Teleport(pos);
            player.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n<size=60%>zamieniono miejscami z {ev.Player.Nickname}</size>");
            ev.Player.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n<size=60%>zamieniono miejscami z {player.Nickname}</size>");

            Mark(ev.Player);
            Cooldown(ev.Player);
            return;
        }
            ev.Player.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n<size=60%>nie znaleziono wroga</size>");
    }
}
