using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Random = UnityEngine.Random;
using Room = Exiled.API.Features.Room;
using Browar.API.World;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.GrenadeFlash)]
public class PartyNade : CustomGrenade {
    public override string Name { get; set; } = "<color=#ff4ba5>GRANAT IMPREZOWY</color>";
    public override string Description { get; set; } = "zmienia oświetlenie pokoju";
    public override uint Id { get; set; } = 52;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }
    public override bool ExplodeOnCollision { get; set; } = false;
    public override float FuseTime { get; set; } = 2.8f;

    protected override void OnExploding(ExplodingGrenadeEventArgs ev) {
	ev.IsAllowed = false;
	ev.Projectile.Room.Color = new Color(Random.Range(0.4f, 4), Random.Range(0.4f, 4), Random.Range(0.4f, 4));
    }

    protected void Populate() {
	Positioning.RoomLocation location1 = new(Room.Get(RoomType.EzPcs), new Vector3(6.92f, 0.99f, 6.03f), 90);
	Positioning.RoomLocation location2 = new(Room.Get(RoomType.LczArmory), new Vector3(4.02f, 0.1f, -0.87f), 0);
	Positioning.RoomLocation location3 = new (Room.Get(RoomType.Hcz049), new Vector3(-5.79f, 89.13f, -3.55f), 0);
	Positioning.RoomLocation location4 = new(Room.Get(RoomType.HczCornerDeep), new Vector3(-1.95f, 1.10f, -1.51f), 0);

	Spawn(Positioning.GetRoomOffset(location1));
	Spawn(Positioning.GetRoomOffset(location2));
	Spawn(Positioning.GetRoomOffset(location3));
	Spawn(Positioning.GetRoomOffset(location4));
    }
    protected override void SubscribeEvents() {
	Handlers.Map.Generated += Populate;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Handlers.Map.Generated -= Populate;
        base.UnsubscribeEvents();
    }
}
