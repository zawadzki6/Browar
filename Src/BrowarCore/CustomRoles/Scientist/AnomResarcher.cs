using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;

namespace BrowarCore.Modules.CustomRoles;

public class AnomResarcher : CustomRole {
    public override string Name { get; set; } = "<color=#FFFF7C>ANOMALNY BADACZ</color>";
    public override string Description { get; set; } = "Po eksperymantach zyskał <color=yellow>odporność na negatywne efekty</color>.";
    public override string CustomInfo { get; set; } = "Anomalny Badacz";
    public override uint Id { get; set; } = 301;
    public override RoleTypeId Role { get; set; } = RoleTypeId.Scientist;
    public override int MaxHealth { get; set; } = 100;

    public override float SpawnChance { get; set; } = 100;
    public override SpawnProperties SpawnProperties { get; set; } = new() { Limit = 1 };

    protected override void RoleAdded(Features.Player player) {
	player.EnableEffect(Exiled.API.Enums.EffectType.Vitality);
        base.RoleAdded(player);
    }
    protected override void RoleRemoved(Features.Player player) {
        base.RoleRemoved(player);
    }
}
