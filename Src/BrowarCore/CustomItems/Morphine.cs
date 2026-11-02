using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Room = Exiled.API.Features.Room;
using Browar.API.World;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.Adrenaline)]
public class Morphine : CustomItem {
    public override string Name { get; set; } = "<color=#ffda55>MORFINA</color>";
    public override string Description { get; set; } = "silny opiat - obniża obrażenia";
    public override uint Id { get; set; } = 56;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }
 
    public void OnUsed(ConsumingItemEventArgs ev) {
	if (!Check(ev.Player.CurrentItem))
	    return;

	ev.Player.EnableEffect(EffectType.Exhausted, 1, 25);
	ev.Player.EnableEffect(EffectType.SoundtrackMute, 1, 10);
	ev.Player.EnableEffect(EffectType.Deafened, 1, 5);
	ev.Player.EnableEffect(EffectType.DamageReduction, 60, 10);
    }
    protected void Populate() {
	Spawn(Positioning.GetRoomOffset(Room.Get(RoomType.HczArmory), new Vector3(-1.5f, 1, -3.9f), 90));
	Spawn(Positioning.GetRoomOffset(Room.Get(RoomType.EzGateA), new Vector3(-3.36f, 1, 4.03f), 270));
	// Spawn(browar.DynamicRoomOffset(Room.Get(RoomType.LczClassDSpawn), new Vector3(-20.97f, -7.62f, 2.33f), 0)); borked??
    }
    protected override void SubscribeEvents() {
	Handlers.Player.ConsumingItem += OnUsed;
        // Map.Generated += Populate;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
        Handlers.Player.ConsumingItem -= OnUsed;
        // Map.Generated -= Populate;
        base.UnsubscribeEvents();
    }

}
