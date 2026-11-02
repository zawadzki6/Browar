using Exiled.Events.EventArgs.Player;

namespace Browar.API.CustomRoles;

public abstract class CustomRole : Exiled.CustomRoles.API.Features.CustomRole {
    public abstract uint RechargeTime { get; set; }
    public abstract bool SpawnCooldown { get; set; }

    protected virtual string CooldownText { get; set; } = $"{Strings.StyleBegin}<color=red>🚫</color>\numiejętność rozładowana{Strings.StyleEnd}";
    protected virtual string RechargeText { get; set; } = $"{Strings.StyleBegin}<color=green>✅</color>\numiejętność naładowana{Strings.StyleEnd}";
    public virtual string Emote { get; set; } = "🏃";

    protected HashSet<string> Cooling = new();
    protected void Remove(ChangingRoleEventArgs ev) => Cooling.Remove(ev.Player.UserId);
    protected void Clear() => Cooling.Clear();
    protected void Mark(Features.Player player) => Cooling.Add(player.UserId);
    protected bool Marked(Features.Player player) => Cooling.Contains(player.UserId);
    protected bool Marked(Features.Player player, bool ShowMessage = false) {
	if (ShowMessage) {
	    if (Cooling.Contains(player.UserId)) {
		player.ShowHint(CooldownText);
		return true;
	    }
	    else return false;
	}
	return Cooling.Contains(player.UserId);
    }

    protected abstract void Ability(TogglingNoClipEventArgs ev);

    protected void Cooldown(Features.Player player) {
	Cooling.Add(player.UserId);
	Timing.CallDelayed(RechargeTime, () => {
	    if (!Check(player) || !Marked(player))
		return;
	    Cooling.Remove(player.UserId);
	    player.ShowHint(RechargeText);
	});
    }
    protected override void RoleAdded(Features.Player player) {
	if (SpawnCooldown)
	    Cooldown(player);
        base.RoleAdded(player);
    }
    protected override void SubscribeEvents() {
	Handlers.Player.TogglingNoClip += Ability;
	Handlers.Player.ChangingRole += Remove;
	Handlers.Server.RestartingRound += Clear;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Handlers.Player.TogglingNoClip -= Ability;
	Handlers.Player.ChangingRole -= Remove;
	Handlers.Server.RestartingRound -= Clear;
        base.UnsubscribeEvents();
    }
    protected void Countdown(Features.Player player, uint secs) {
	player.ShowHint($"{Strings.StyleBegin}{Emote} {secs}{Strings.StyleEnd}", 1);
	Timing.CallPeriodically(secs--, 1, () => player.ShowHint($"{Strings.StyleBegin}{Emote} {secs--}{Strings.StyleEnd}", 1));
    }
}

