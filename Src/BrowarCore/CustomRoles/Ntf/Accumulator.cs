using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Accumulator : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#0096FF>KUMULATOR</color>";
    public override string Description { get; set; } = "<color=yellow>kumuluje obrażenia</color> do późniejszego wydania" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Kumulator";
    public override uint Id { get; set; } = 602;
    public override RoleTypeId Role { get; set; } = RoleTypeId.NtfSergeant;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };
    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new() {
        {AmmoType.Nato556, 120},
        {AmmoType.Nato9, 40}
    };
    public override List<string> Inventory { get; set; } = new() {
        ItemType.KeycardMTFOperative.ToString(),
        ItemType.GunE11SR.ToString(),
        ItemType.ArmorCombat.ToString(),
        ItemType.Radio.ToString(),
        ItemType.GrenadeHE.ToString(),
        ItemType.Medkit.ToString()
    };
    public override bool SpawnCooldown { get; set; } = false;
    public override uint RechargeTime { get; set; } = 75;
    public override string Emote { get; set; } = "🔫";

    protected Dictionary<string, int> cumulated = new();
    protected void clear() => cumulated.Clear();
    protected void del(ChangingRoleEventArgs ev) => cumulated.Remove(ev.Player.UserId);

    protected Dictionary<string, int> cumulating = new();
    protected void clearsquared() => cumulating.Clear();

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        if (cumulated.ContainsKey(ev.Player.UserId)) {
            ev.Player.ShowHint(Etc.AbilityUnavailable);
            return;
        }

	Mark(ev.Player);
        cumulating.Add(ev.Player.UserId, 0);
        Countdown(ev.Player, 5);

        Timing.CallDelayed(5, () => {
            ev.Player.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n<size=60%>zebrano {cumulating[ev.Player.UserId]} dmg</size>");
            cumulated.Add(ev.Player.UserId, cumulating[ev.Player.UserId]);
            cumulating.Remove(ev.Player.UserId);

	    Cooldown(ev.Player);
        });
    }
    protected void Attacking(HurtingEventArgs ev) {
        if (!Check(ev.Attacker))
            return;

        int dmg = 0;
        if (cumulated.TryGetValue(ev.Attacker.UserId, out dmg)) {
            ev.Amount += dmg;
            cumulated.Remove(ev.Attacker.UserId);
        }
        if (cumulating.ContainsKey(ev.Attacker.UserId)) {
            ev.IsAllowed = false;
            cumulating[ev.Attacker.UserId] += (int)(ev.Amount * 0.25f);
            ev.Attacker.ShowHitMarker();
        }
    }
    protected override void SubscribeEvents() {
        Handlers.Player.Hurting += Attacking;
        Handlers.Player.ChangingRole += del;
        Handlers.Server.RestartingRound += clear;
        Handlers.Server.RestartingRound += clearsquared;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
        Handlers.Player.Hurting -= Attacking;
        Handlers.Player.ChangingRole -= del;
        Handlers.Server.RestartingRound -= clear;
        Handlers.Server.RestartingRound -= clearsquared;
        base.SubscribeEvents();
    }
}
