/* == borked - players are backtracked by something ==
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Handlers;
using UnityEngine;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.GunRevolver)]
public class SwitchGun : CustomWeapon {
    public override string Name { get; set; } = "<color=#a0daf9>Switch Gun</color>";
    public override string Description { get; set; } = "Instantly <color=yellow>switch places</color> with your enemy!";
    public override uint Id { get; set; } = 102;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }
    public override float Damage { get; set; } = 1;

    protected override void OnReloading(ReloadingWeaponEventArgs ev) => ev.IsAllowed = false;
    protected void OnUnloading(UnloadingWeaponEventArgs ev) {
	if (Check(ev.Player.CurrentItem))
		ev.IsAllowed = false;
	}

    protected override void OnHurting(HurtingEventArgs ev) {
	if (ev.Attacker == null || ev.Player == null)
	    return;
	if (!Check(ev.Attacker.CurrentItem))
	    return;

	ev.IsAllowed = false;
	Vector3 player = ev.Player.Position;
	ev.Player.Teleport(ev.Attacker.Position);
	ev.Attacker.Teleport(player);

	// base.OnHurting(ev);
    }

    protected override void SubscribeEvents() {
	Player.UnloadingWeapon += OnUnloading;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Player.UnloadingWeapon -= OnUnloading;
        base.SubscribeEvents();
    }
}
*/
