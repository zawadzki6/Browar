using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Room = Exiled.API.Features.Room;
using Browar.API.World;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.Painkillers)]
public class SrakInator : CustomItem {
    public override string Name { get; set; } = "<color=#934a00>SRAKINATOR-1250</color>";
    public override string Description { get; set; } = "wymusza <color=yellow>defekacje</color> użytkownika???";
    public override uint Id { get; set; } = 35;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; } 

    public void OnUsed(ConsumingItemEventArgs ev) {
	if (!Check(ev.Player.CurrentItem))
	    return;

        ev.IsAllowed = false;
	ev.Player.PlaceTantrum();
    }

    protected void Populate() {
	Spawn(Positioning.GetRoomOffset(Room.Get(RoomType.LczToilets), new Vector3(-4.66f, 0.5f, -0.78f), 90));
    }
    protected override void SubscribeEvents() {
	Handlers.Player.ConsumingItem += OnUsed;
	Handlers.Map.Generated += Populate;
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents() {
	Handlers.Player.ConsumingItem -= OnUsed;
	Handlers.Map.Generated -= Populate;
        base.SubscribeEvents();
    }
}
