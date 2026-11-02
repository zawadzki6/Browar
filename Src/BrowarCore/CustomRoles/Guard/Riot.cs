using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;

namespace BrowarCore.Modules.CustomRoles;

public class Riot : CustomRole {
    public override string Name { get; set; } = "<color=#5B6370>ANTYBUNTOWNIK</color>";
    public override string Description { get; set; } = "Ochroniarz wyposarzony w <color=yellow>sprzęt do zwalczania buntów</color>";
    public override string CustomInfo { get; set; } = "Kontrola buntów";
    public override uint Id { get; set; } = 204;
    public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
    public override int MaxHealth { get; set; } = 100;

    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };
    public override float SpawnChance { get; set; } = 0;

    public override List<string> Inventory { get; set; } = new() {
	ItemType.GunFSP9.ToString(),
	ItemType.GunRevolver.ToString(),
	ItemType.ArmorHeavy.ToString(),
	ItemType.KeycardGuard.ToString(),
	ItemType.Medkit.ToString(),
	ItemType.Radio.ToString(),
	"<color=#bcff9b>GRANAT ŁZAWIĄCY</color>",
	"<color=#bcff9b>GRANAT ŁZAWIĄCY</color>"
    };
    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new() {
	{AmmoType.Nato9, 90},
	{AmmoType.Ammo44Cal, 12}
    };
}
