using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.GunCOM15)]
public class MedicGun : CustomWeapon {
    public override string Name { get; set; } = "<color=#f67eff>BROŃ MEDYCZNA</color>";
    public override string Description { get; set; } = "<color=yellow>leczy sojuszników</color>. zadaje normalne obrażenia wrogom";
    public override uint Id { get; set; } = 31;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }
 
    public void OnShooting(HurtingEventArgs ev) {
	if (ev.Attacker == null || ev.Player == null) 
	    return;

	if (!Check(ev.Attacker.CurrentItem))
	    return;

	if (ev.Attacker.LeadingTeam == ev.Player.LeadingTeam) {
	    ev.IsAllowed = false;
            ev.Attacker.ShowHitMarker();
	    ev.Player.Heal(ev.DamageHandler.Damage);
	}
    }

    // void Populate() =>
    //      Spawn(browar.DynamicRoomOffset(Room.Get(RoomType.EzIntercom), new UnityEngine.Vector3(4.41f, 4.95f, 2.06f), 90));

    protected override void SubscribeEvents() {
	Handlers.Player.Hurting += OnShooting;
	// Map.Generated += Populate;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	// Map.Generated -= Populate;
	Handlers.Player.Hurting -= OnShooting;
        base.UnsubscribeEvents();
    }

}
