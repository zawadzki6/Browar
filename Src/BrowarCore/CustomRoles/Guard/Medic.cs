using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;

namespace BrowarCore.CustomRoles;

public class Medic : CustomRole {
    public override string Name { get; set; } = "<color=#5B6370>MEDYK</color>";
    public override string Description { get; set; } = "Ochroniarz wyposarzony w <color=yellow>sprzęt medyczny</color>";
    public override string CustomInfo { get; set; } = "Medyk";
    public override uint Id { get; set; } = 202;
    public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
    public override int MaxHealth { get; set; } = 100;

    public override List<string> Inventory { get; set; } = new() {
	ItemType.ArmorLight.ToString(),
	ItemType.Medkit.ToString(),
	ItemType.Adrenaline.ToString(),
	ItemType.KeycardGuard.ToString(),
	ItemType.Radio.ToString(),
	ItemType.GrenadeFlash.ToString(),
	"<color=#f67eff>BROŃ MEDYCZNA</color>",
        ItemType.GunFSP9.ToString()
    };
    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new() { {AmmoType.Nato9, 60} };

    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

}
