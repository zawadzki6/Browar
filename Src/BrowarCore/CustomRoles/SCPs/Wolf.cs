using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Wolf : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#EC2222>WILKOŁAK</color>";
    public override string Description { get; set; } = "Nakłada na wrogach <color=red>efekt krwawienia</color>" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Wilkołak";
    public override uint Id { get; set; } = 701;
    public override RoleTypeId Role { get; set; } = RoleTypeId.Scp939;
    public override int MaxHealth { get; set; } = 2500;
    public override float SpawnChance { get; set; } = 10;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override bool SpawnCooldown { get; set; } = true;
    public override uint RechargeTime { get; set; } = 60;
    public override string Emote { get; set; } = "🩸";

    protected HashSet<string> active = new();
    protected void delete(ChangingRoleEventArgs ev) => active.Remove(ev.Player.UserId);
    protected void clear() => active.Clear();

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

	active.Add(ev.Player.UserId);
	Timing.CallDelayed(10, () => active.Remove(ev.Player.UserId));

	Countdown(ev.Player, 10);
	Mark(ev.Player);
	Cooldown(ev.Player);
    }
    protected void Effect(HurtingEventArgs ev) {
	if (ev.Player == null || ev.Attacker == null || ev.DamageHandler == null)
	    return;
	if (!active.Contains(ev.Attacker.UserId))
	    return;

	ev.Player.EnableEffect(EffectType.Bleeding, 1, 8, true);
    }
    protected override void SubscribeEvents() {
	Handlers.Player.Hurting += Effect;
	Handlers.Player.ChangingRole += delete;
	Handlers.Server.RestartingRound += clear;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Handlers.Player.Hurting -= Effect;
	Handlers.Player.ChangingRole -= delete;
	Handlers.Server.RestartingRound -= clear;
        base.UnsubscribeEvents();
    }
}
