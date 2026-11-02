using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Browar.API.World;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.Painkillers)]
public class KasteuszInator : CustomItem {
    public override string Name { get; set; } = "<color=#0084c1>KASTEUSZINATOR-3000</color>";
    public override string Description { get; set; } = "zamienia użytkownika we <color=yellow>wrogiego kastiego</color>";
    public override uint Id { get; set; } = 34;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }

    public void OnUsed(ConsumingItemEventArgs ev) {
	if (!Check(ev.Player.CurrentItem))
	    return;

        ev.IsAllowed = false;

	ev.Player.Role.Set(RoleTypeId.Scp0492, RoleSpawnFlags.None);
	ev.Player.SetScale(new Vector3(1, 0.5f, 1), Features.Player.List);

	ev.Player.CustomInfo = "Kasteusz";
    }

    public void Populate() => Spawn(Positioning.GetRoomOffset(Features.Room.Get(Exiled.API.Enums.RoomType.Lcz173), new Vector3(-2.14f, 12.76f, 0.69f), 0));

    protected override void SubscribeEvents() {
	    Handlers.Player.ConsumingItem += OnUsed;
	    Handlers.Map.Generated += Populate;
            base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents() {
	    Handlers.Player.ConsumingItem -= OnUsed;
	    Handlers.Map.Generated += Populate;
            base.SubscribeEvents();
    }
}
