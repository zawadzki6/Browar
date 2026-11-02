using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Crits : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#0096FF>KRYTOBÓJCA</color>";
    public override string Description { get; set; } = "zadaje <color=yellow>krytyczne obrażenia</color> w zamian za także ich otrzymywanie" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Krytobójca";
    public override uint Id { get; set; } = 601;
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
    public override string Emote { get; set; } = "💥";

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        Countdown(ev.Player, 8);
        ev.Player.EnableEffect(EffectType.Burned, 1, 8);

	Mark(ev.Player);
	Cooldown(ev.Player);
    }
    protected void Attacking(HurtingEventArgs ev) {
        if (!Check(ev.Attacker))
            return;

        if (ev.Attacker.GetEffect(EffectType.Burned).Intensity > 0)
            ev.Amount += 1.25f;
    }
    protected override void SubscribeEvents() {
        Handlers.Player.Hurting += Attacking;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
        Handlers.Player.Hurting -= Attacking;
        base.UnsubscribeEvents();
    }
}
