using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class TimeShifer : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#FFFF7C>ZEGARMISTRZ</color>";
    public override string Description { get; set; } = "Potrafi zawrócić się w czasie" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Zegarmistrz";
    public override uint Id { get; set; } = 303;
    public override RoleTypeId Role { get; set; } = RoleTypeId.Scientist;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };
    public override List<string> Inventory { get; set; } = new() {
	ItemType.KeycardScientist.ToString(),
	ItemType.Medkit.ToString()
    };
    public override uint RechargeTime { get; set; } = 75;
    public override bool SpawnCooldown { get; set; } = false;
    public override string Emote { get; set; } = "👓";

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        if (Features.Warhead.DetonationTimer < 7 && Features.Warhead.DetonationTimer > 0) {
            ev.Player.ShowHint(Etc.AbilityUnavailable);
            return;
        }

	Countdown(ev.Player, 5);
	Mark(ev.Player);

	Vector3 pos = ev.Player.Position;
	ev.Player.EnableEffect(EffectType.Fade, 40, 5);
	Timing.CallDelayed(5, () => ev.Player.Teleport(pos));


	Cooldown(ev.Player);
    }
}
