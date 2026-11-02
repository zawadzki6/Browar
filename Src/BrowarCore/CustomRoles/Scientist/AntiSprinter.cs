using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class AntiSprinter : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#FFFF7C>ANTYUCIEKINIER</color>";
    public override string Description { get; set; } = "zapobiegaj ucieczkom - <color=yellow>spowolnij innych</color>" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Antyuciekinier";
    public override uint Id { get; set; } = 304;
    public override RoleTypeId Role { get; set; } = RoleTypeId.Scientist;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override bool SpawnCooldown { get; set; } = true;
    public override uint RechargeTime { get; set; } = 90;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        byte count = 0;
        foreach (Features.Player player in Features.Player.List) {
            if (ev.Player.LeadingTeam != player.LeadingTeam)
            if (UnityEngine.Vector3.Distance(ev.Player.Position, player.Position) <= 6) {
                player.EnableEffect(EffectType.Slowness, 30, 6);
                player.ShowHint($"\n\n\n\n\n\n<size=60%>spowolniony przez {ev.Player.Nickname}!</size>");
                count++;
            }
        }
        ev.Player.ShowHint($"\n\n\n\n\n\n<size=60%>spowolniono {count} graczy</size>");

	Mark(ev.Player);
	Cooldown(ev.Player);
    }
}
