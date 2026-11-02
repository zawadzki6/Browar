using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.Painkillers)]
public class SuperKasteuszInator : CustomItem {
    public override string Name { get; set; } = "<color=#c10020>KASTEUSZINATOR-9000</color>";
    public override string Description { get; set; } = "wamienia użytkownika w <color=yellow>ulepszonego wrogiego kastiego</color>";
    public override uint Id { get; set; } = 36;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }

    public void OnUsed(ConsumingItemEventArgs ev) {
	if (!Check(ev.Player.CurrentItem))
	    return;

        ev.IsAllowed = false;

	ev.Player.Role.Set(RoleTypeId.Scp096, RoleSpawnFlags.None);
	ev.Player.SetScale(new Vector3(1, 0.5f, 1), Features.Player.List);
	ev.Player.MaxHealth = 100;
	ev.Player.MaxHumeShield = 200;

	ev.Player.CustomInfo = "Kasteusz";
    }
    protected void Populate() {
	Spawn(Features.Room.Get(RoomType.Hcz096).WorldPosition(new Vector3(0.3f, 1, 3)));
    }
    protected override void SubscribeEvents() {
	Handlers.Player.ConsumingItem += OnUsed;
	// ehmap.Generated += Populate;
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents() {
	Handlers.Player.ConsumingItem -= OnUsed;
	// ehmap.Generated -= Populate;
        base.SubscribeEvents();
    }
}
