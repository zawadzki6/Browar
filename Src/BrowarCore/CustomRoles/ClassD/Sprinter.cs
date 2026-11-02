using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Sprinter : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#FF8E00>UCIEKINIER</color>";
    public override string Description { get; set; } = "bardzo szybka klasa d" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Uciekinier";
    public override uint Id { get; set; } = 105;
    public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override bool SpawnCooldown { get; set; } = false;
    public override uint RechargeTime { get; set; } = 75;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;
	
	ev.Player.EnableEffect(EffectType.MovementBoost, 150, 5);
	ev.Player.ResetStamina();
	ev.Player.IsUsingStamina = false;

	Timing.CallDelayed(5, () => {
	    ev.Player.EnableEffect(EffectType.Exhausted, 1, 15);
	    ev.Player.IsUsingStamina = true;
	});

	Countdown(ev.Player, 5);

	Mark(ev.Player);
	Cooldown(ev.Player);
    }
    protected void Debuff(InteractingDoorEventArgs ev) {
	if (ev.Player.GetEffect(EffectType.MovementBoost).Intensity == 150)
	    ev.IsAllowed = false;
    }
    protected override void SubscribeEvents() {
	Handlers.Player.InteractingDoor += Debuff;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Handlers.Player.InteractingDoor -= Debuff;
        base.UnsubscribeEvents();
    }
}
