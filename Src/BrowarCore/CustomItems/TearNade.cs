using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Map = Exiled.Events.Handlers.Map;
using Player = Exiled.API.Features.Player;

[CustomItem(ItemType.GrenadeFlash)]
public class TearNade : CustomGrenade {
    public override string Name { get; set; } = "<color=#bcff9b>GRANAT ŁZAWIĄCY</color>";
    public override string Description { get; set; } = "skuteczny agent chemiczny. szkodzi graczom w pobliżu.";
    public override uint Id { get; set; } = 53;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }
    public override bool ExplodeOnCollision { get; set; } = false;
    public override float FuseTime { get; set; } = 2.8f;

    protected override void OnExploding(ExplodingGrenadeEventArgs ev) {
	if (!Check(ev.Projectile))
	    return;


	ev.IsAllowed = false;

	foreach (Player player in ev.TargetsToAffect) {
	    player.EnableEffect(EffectType.Poisoned, 1, 15);
	    player.EnableEffect(EffectType.FogControl, 4, 15);
	    player.EnableEffect(EffectType.Decontaminating, 1, 2);
	    Timing.CallDelayed(7, () => player.EnableEffect(EffectType.Decontaminating, 1, 2));
	    Timing.CallDelayed(15, () => player.EnableEffect(EffectType.Burned, 1, 5));
	    player.ShowHint("\n\n<size=60%><color=#aaaa00>[ Zostaliście trafieni agentem łzawiącym ]</color></size>", 6);
	}
    }

    protected void Populate() {
	/* browar.RoomLocation location1 = new(Room.Get(RoomType.EzPcs), new Vector3(6.92f, 0.99f, 6.03f), 90);
	browar.RoomLocation location2 = new(Room.Get(RoomType.LczArmory), new Vector3(4.02f, 0.1f, -0.87f), 0);
	browar.RoomLocation location3 = new (Room.Get(RoomType.Hcz049), new Vector3(-5.79f, 89.13f, -3.55f), 0);
	browar.RoomLocation location4 = new(Room.Get(RoomType.HczCornerDeep), new Vector3(-1.95f, 1.10f, -1.51f), 0);

	Spawn(browar.DynamicRoomOffset(location1));
	Spawn(browar.DynamicRoomOffset(location2));
	Spawn(browar.DynamicRoomOffset(location3));
	Spawn(browar.DynamicRoomOffset(location4)); */
    }
    protected override void SubscribeEvents() {
	Map.Generated += Populate;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	Map.Generated -= Populate;
        base.UnsubscribeEvents();
    }
}
