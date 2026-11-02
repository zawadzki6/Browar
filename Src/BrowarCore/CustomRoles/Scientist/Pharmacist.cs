using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;

namespace BrowarCore.CustomRoles;

public class Pharmacist : CustomRole {
    public override string Name { get; set; } = "<color=#FFFF7C>APTEKARZ</color>";
    public override string Description { get; set; } = "Farmaceuta placówki. <color=yellow>Brak limitu</color> przedmiotów medycznych.";
    public override string CustomInfo { get; set; } = "Aptekarz";
    public override uint Id { get; set; } = 302;
    public override RoleTypeId Role { get; set; } = RoleTypeId.Scientist;
    public override int MaxHealth { get; set; } = 100;

    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    public override List<string> Inventory { get; set; } = new() {
	ItemType.KeycardScientist.ToString(),
	ItemType.Adrenaline.ToString(),
	ItemType.Medkit.ToString(),
	ItemType.Painkillers.ToString(),
    };
    protected override void RoleAdded(Features.Player player) {
	player.SetCategoryLimit(ItemCategory.Medical, 8);
        base.RoleAdded(player);
    }
    protected override void RoleRemoved(Features.Player player) {
	player.ResetCategoryLimit(ItemCategory.Medical);
        base.RoleRemoved(player);
    }
}
