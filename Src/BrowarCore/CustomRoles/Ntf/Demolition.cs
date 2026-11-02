/* demolition expert
 * fabricate nades
 * no explosive damage */
using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.CustomRoles;

public class Demoliton : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#0096FF>ROZBIURKOWIEC</color>";
    public override string Description { get; set; } = "operator specjalizujący się w fabrykacji materiałów wybuchowych. <color=yellow>brak obrażeń</color> od wybuchów" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "Rozbiurkowiec";
    public override uint Id { get; set; } = 603;
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
    public override uint RechargeTime { get; set; } = 45;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

        if (ev.Player.IsInventoryFull || ev.Player.IsCuffed) {
            ev.Player.ShowHint(Etc.AbilityUnavailable);
            return;
        }
        switch (UnityEngine.Random.Range(1, 4)) {
            case 1:
                ev.Player.AddItem(ItemType.GrenadeFlash);
                break;
            default:
                ev.Player.AddItem(ItemType.GrenadeHE);
                break;
        }
	Mark(ev.Player);
	Cooldown(ev.Player);
    }
    protected void Hurting(HurtingEventArgs ev) {
        if (!Check(ev.Player))
            return;

        if (ev.DamageHandler.Type == DamageType.Explosion)
            ev.IsAllowed = false;

    }
    protected override void SubscribeEvents() {
        Handlers.Player.Hurting += Hurting;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
        Handlers.Player.Hurting -= Hurting;
        base.UnsubscribeEvents();
    }
}
