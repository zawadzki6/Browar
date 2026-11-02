using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using Browar.API.World;

namespace BrowarCore.CustomRoles;

public class ZoneManager : CustomRole {
    public override string Name { get; set; } = "<color=#5B6370>ZARZĄDCA STREFY</color>";
    public override string Description { get; set; } = "Zarządca strefy. Pomóż ochroniarzom.";
    public override string CustomInfo { get; set; } = "Zarzadca Strefy";
    public override uint Id { get; set; } = 20;
    public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
    public override int MaxHealth { get; set; } = 100;

    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };
    public override float SpawnChance { get; set; } = 0;

    public override List<string> Inventory { get; set; } = new() {
	ItemType.KeycardZoneManager.ToString(),
	ItemType.Radio.ToString(),
	ItemType.GunCOM18.ToString(),
	ItemType.Painkillers.ToString()
    };
    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new() {
	{AmmoType.Nato9, 40}
    };
    protected override void RoleAdded(Features.Player player) {
        base.RoleAdded(player);
        Vector3 position = Positioning.GetRoomOffset(Features.Room.Get(RoomType.HczEzCheckpointA), new Vector3(-2.4f, 1, -7), 0);
	player.Teleport(position);
    }
}
