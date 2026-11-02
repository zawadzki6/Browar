using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.GunCOM15)]
public class PosGun : CustomWeapon {
    public override string Name { get; set; } = "<color=yellow>Developer Offset Gun</color>";
    public override string Description { get; set; } = "Point and click! Gets the shot's offset\n<color=red>BULLET SPREAD IS INCLUDED!!</color>";
    public override uint Id { get; set; } = 101;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }
    public override float Damage { get; set; } = 0;

    protected override void OnShot(ShotEventArgs ev) {
	ev.CanSpawnImpactEffects = false;
	Vector3 local = ev.Player.CurrentRoom.LocalPosition(Vector3.zero);
	Vector3 world = ev.Player.CurrentRoom.WorldPosition(Vector3.zero);
	Vector3 room = ev.Player.CurrentRoom.Position;
	Vector3 offset = ev.Position - room;
	ev.Player.ShowHint($"<size=75%>{ev.Player.CurrentRoom.Name}\noffset: {offset.x}, {offset.y}, {offset.z}\nlocal: {local.x}, {local.y}, {local.z}\nworld: {world.x}, {world.y}, {world.z}\nyaw: {ev.Player.CurrentRoom.Rotation.eulerAngles.y}</size>", 15);
	ev.Firearm.MagazineAmmo++;
	ev.Player.ShowHitMarker(0.5f);
    }

    protected override void OnReloading(ReloadingWeaponEventArgs ev) => ev.IsAllowed = false;
    protected void OnUnloading(UnloadingWeaponEventArgs ev){
	if (Check(ev.Player.CurrentItem))
	    ev.IsAllowed = false;
    }

    protected override void SubscribeEvents() {
	Handlers.Player.UnloadingWeapon += OnUnloading;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Handlers.Player.UnloadingWeapon -= OnUnloading;
        base.SubscribeEvents();
    }
}
