using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features.Doors;
using Exiled.API.Interfaces;
using Room = Exiled.API.Features.Room;
using Browar.API.World;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.SCP268)]
public class TpHat : CustomItem {
    public override string Name { get; set; } = "<color=#4f809f>CZAPKA TELEPORTUJĄCA</color>";
    public override string Description { get; set; } = "<color=yellow>teleportuje uzytkownika</color> w losowe miejsce. zostaje upuszczona w miejscu użycia.";
    public override uint Id { get; set; } = 51;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; } 

    protected void UsingItem(UsingItemCompletedEventArgs ev) {
	if (!Check(ev.Player.CurrentItem))
	    return;

	ev.IsAllowed = false;
	ev.Item.CreatePickup(ev.Player.Position);
	ev.Item.Destroy();

	bool valid = false;
	Door door = null!;
	while (!valid) {
	    door = Door.Random();
	    if (!door.IsElevator && door.IsDamageable && !door.IsGate && door.AllowsScp106 && !door.IsLocked)
		valid = true;
	}
	(door as IDamageableDoor)!.Break();
	ev.Player.Teleport(door);

	ev.Player.EnableEffect(EffectType.Blurred, 1, 3);
	ev.Player.EnableEffect(EffectType.Exhausted, 1, 10);
    }

    void Populate() =>
	Spawn(Positioning.GetRoomOffset(Room.Get(RoomType.Hcz079), new UnityEngine.Vector3(-4.58f, -3.23f, 12.46f), 180));

    protected override void SubscribeEvents() {
	Handlers.Player.UsingItemCompleted += UsingItem;
	Handlers.Map.Generated += Populate;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Handlers.Player.UsingItemCompleted -= UsingItem;
	Handlers.Map.Generated -= Populate;
        base.UnsubscribeEvents();
    }
}
