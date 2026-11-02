using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Room = Exiled.API.Features.Room;
using Browar.API.World;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.Adrenaline)]
public class Button : CustomItem {
    public override string Name { get; set; } = "<color=#ff6c23>PRZYCISK SAMOBÓJCZY</color>";
    public override string Description { get; set; } = "<color=yellow>natychmiastowo wybuchnij</color> kliknięciem! kto to wymyślił?";
    public override float Weight { get; set; } = default;
    public override uint Id { get; set; } = 33;
    public override SpawnProperties? SpawnProperties { get; set; }

    public void OnUsingItem(UsingItemEventArgs ev) {
	if (!Check(ev.Player.CurrentItem))
	    return;

	ev.Item.Destroy();
	ev.Player.Explode();
    }
    protected void Populate() {
	Spawn(Room.Get(RoomType.Hcz127).WorldPosition(Positioning.RemapByYaw(new Vector3(-0.77f, 0.8f, -0.92f), 90, Room.Get(RoomType.Hcz127))));
    }
    protected override void SubscribeEvents() {
	Handlers.Player.UsingItem += OnUsingItem;
	Handlers.Map.Generated += Populate;
	base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Handlers.Player.UsingItem -= OnUsingItem;
	Handlers.Map.Generated -= Populate;
	base.SubscribeEvents();
    }
}
