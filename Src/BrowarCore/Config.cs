using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BrowarCore;

public class Config : IConfig {
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; }

    [Description("Primary features")]
    public bool CustomItems { get; set; } = true;
    public bool CustomRoles { get; set; } = false;
    public uint CustomItemsOffset { get; set; } = 0;

    [Description("Addons")]
    public bool AddonLucky500 { get; set; } = true;
    public bool AddonSpawnProtect { get; set; } = true;

    [Description("ScpSwap patch for impossible SCPs")]
    public bool PatchScpSwap { get; set; } = true;
}
