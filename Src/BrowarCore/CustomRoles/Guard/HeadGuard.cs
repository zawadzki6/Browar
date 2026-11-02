using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class HeadGuard : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#5B6370>KIEROWNIK</color>";
    public override string Description { get; set; } = "umacniaj sojuszników!" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Kierownik";
    public override uint Id { get; set; } = 201;
    public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };
    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new() { {AmmoType.Nato9, 60} };
    public override List<string> Inventory { get; set; } = new() {
	ItemType.ArmorLight.ToString(),
	ItemType.Medkit.ToString(),
	ItemType.Painkillers.ToString(),
	ItemType.KeycardGuard.ToString(),
	ItemType.Radio.ToString(),
	ItemType.GunCrossvec.ToString(),
	ItemType.GrenadeHE.ToString()
    };
    public override bool SpawnCooldown { get; set; } = false;
    public override uint RechargeTime { get; set; } = 45;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

	Mark(ev.Player);
	
	ushort count = 0;
	foreach (Features.Player player in Features.Player.List) {
	    if (player == ev.Player)
		continue;
	    if (player.LeadingTeam != ev.Player.LeadingTeam)
		continue;
	    Log.Debug(Vector3.Distance(ev.Player.Position, player.Position).ToString());
	    if (Vector3.Distance(ev.Player.Position, player.Position) > 12)
		continue;
	    player.EnableEffect(EffectType.DamageReduction, 8, 5);
	    player.AddAhp(25, 25, 0.2f, 0.7f);
	    player.ShowHint($"\n\n\n\n\n\n<size=60%>otrzymano wzmocnienie od {ev.Player.Nickname}!</size>");
	    count++;
	}
	ev.Player.ShowHint($"\n\n\n\n\n\n<size=60%>wzmocniono {count} graczy</size>");

        if (count > 0) {
	    ev.Player.EnableEffect(EffectType.DamageReduction, 8, 5);
	    ev.Player.AddAhp(25, 25, 0.2f, 0.7f);
        }
	
	Cooldown(ev.Player);
    }
}
