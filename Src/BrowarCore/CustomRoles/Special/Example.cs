using Exiled.API.Features.Spawn;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;
using Browar.API.World;

namespace BrowarCore.CustomRoles;

public class Example : BrowarCustomRole {
    public override string Name { get; set; } = "<color=#F700FD>MICHAEL MORBIUS</color>";
    public override string Description { get; set; } = "przykładowa rola" + Etc.AbilityNotice;
    public override string CustomInfo { get; set; } = "morbius";
    public override uint Id { get; set; } = 1;
    public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
    public override int MaxHealth { get; set; } = 100;
    public override float SpawnChance { get; set; } = 0;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };
    public override List<string> Inventory { get; set; } = new() {
	ItemType.KeycardZoneManager.ToString()
    };

    public override bool SpawnCooldown { get; set; } = false;
    public override uint RechargeTime { get; set; } = 20;

    protected override void Ability(TogglingNoClipEventArgs ev) {
	if (!Check(ev.Player) || Marked(ev.Player, true))
	    return;

	Mark(ev.Player);
	

	ev.Player.EnableEffect(EffectType.Invigorated, 1, 7);
	ev.Player.EnableEffect(EffectType.Lightweight, 100, 8);
	Timing.CallDelayed(8, () => ev.Player.EnableEffect(EffectType.Exhausted, 1, 15));

	Countdown(ev.Player, 8);
	Cooldown(ev.Player);
    }

    protected override void RoleAdded(Features.Player player) {
	player.Teleport(Positioning.GetRoomOffset(Features.Room.Get(RoomType.HczEzCheckpointA), new Vector3(-2.4f, 1, -7), 0));
        base.RoleAdded(player);
    }
}
