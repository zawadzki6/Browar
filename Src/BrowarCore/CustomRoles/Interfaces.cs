using Browar.API.CustomRoles;

namespace BrowarCore.CustomRoles;

public abstract class BrowarCustomRole : CustomRole {
    protected override string CooldownText { get; set; } = $"{Etc.StyleBegin}<color=red>🚫</color>\numiejętność rozładowana{Etc.StyleEnd}";
    protected override string RechargeText { get; set; } = $"{Etc.StyleBegin}<color=green>✅</color>\numiejętność naładowana{Etc.StyleEnd}";
}
public static class Etc {
    public static string StyleBegin = "\n\n\n\n\n\n\n\n\n\n\n<size=60%>";
    public static string StyleEnd = "</size>";

    public static string AbilityNotice = "\n\n<color=yellow>naciśnij lewyalt (noclip) aby użyć umiejętności</color>";
    public static string AbilityUnavailable = $"{StyleBegin}<color=#aaaaff>😨</color>\numiejętność niemożliwa w tym czasie{StyleEnd}";
}
