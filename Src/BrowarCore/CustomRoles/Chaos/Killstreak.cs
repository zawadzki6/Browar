using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Killstreaker : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#008F1C>MNOŻNIK</color>";
    public override string Description { get; set; } = "z każdym zabiciem wroga otrzymaj mnożnik obrażen do poźniejszego użycia";
    public override string CustomInfo { get; set; } = "Mnożnik";
    public override uint Id { get; set; } = 504;
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
    public override uint RechargeTime { get; set; } = 45;
    public override string Emote { get; set; } = "🔫";

    protected Dictionary<string, float> cumulated = new();
    protected void clear() => cumulated.Clear();
    protected void del(ChangingRoleEventArgs ev) => cumulated.Remove(ev.Player.UserId);

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        if (!cumulated.ContainsKey(ev.Player.UserId)) {
            ev.Player.ShowHint(Etc.AbilityUnavailable);
            return;
        }
        // this is stupid
        // this effect should have no impact on anything
        // doing this we can check if the ability is active without making another dictionary
        ev.Player.EnableEffect(EffectType.FocusedVision, 1, 15);
        Timing.CallDelayed(15, () => cumulated.Remove(ev.Player.UserId));

        Countdown(ev.Player, 15);
	Mark(ev.Player);
	Cooldown(ev.Player);
    }
    protected void Multiply(DyingEventArgs ev) {
        if (!Check(ev.Attacker) || ev.Attacker == null)
            return;
        if (ev.Attacker.GetEffect(EffectType.FocusedVision).Intensity > 0)
            return;

        if (!cumulated.ContainsKey(ev.Attacker.UserId))
            cumulated.Add(ev.Attacker.UserId, 1);

        cumulated[ev.Attacker.UserId] += 0.1f;

        ev.Attacker.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n<size=60%>nowy mnożnik: {cumulated[ev.Attacker.UserId]}</size>");

    }
    protected void Attacking(HurtingEventArgs ev) {
        if (!Check(ev.Attacker) || ev.Attacker == null)
            return;
        if (ev.Attacker.GetEffect(EffectType.FocusedVision).Intensity > 0)
            ev.Amount *= cumulated[ev.Attacker.UserId];
    }
    protected override void SubscribeEvents() {
        Handlers.Player.Hurting += Attacking;
        Handlers.Player.Dying += Multiply;
        Handlers.Player.ChangingRole += del;
        Handlers.Server.RestartingRound += clear;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
        Handlers.Player.Hurting -= Attacking;
        Handlers.Player.Dying -= Multiply;
        Handlers.Player.ChangingRole -= del;
        Handlers.Server.RestartingRound -= clear;
        base.UnsubscribeEvents();
    }
}
