using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace BrowarCore.Patches;

public class ScpSwap {
    // patch for ScpSwap
    public static void IncompatibleScps(ChangingRoleEventArgs ev) {
	bool invalid = false;
	bool isScp = (ev.Player.LeadingTeam == LeadingTeam.Anomalies);
	int c = 0;
	if (ev.NewRole == RoleTypeId.Scp079) {
	    foreach (Features.Player player in Features.Player.List) {
		if (invalid)
		    break;
		if (player.Role == RoleTypeId.Scp096)
		    invalid = true;
		if (player.LeadingTeam == Exiled.API.Enums.LeadingTeam.Anomalies)
		    c++;
	    }
	} else if (ev.NewRole == RoleTypeId.Scp096) {
	    foreach (Features.Player player in Features.Player.List) {
		if (invalid)
		    break;
		if (player.Role == RoleTypeId.Scp079)
		    invalid = true;
	    }
	}

	if ((c == 0 || isScp) && ev.NewRole == RoleTypeId.Scp079)
	    invalid = true;

	if (invalid) {
	    ev.IsAllowed = false;
	    if (c <= 1)
		ev.Player.ShowHint("<size=60%><color=yellow>SCP-079 nie mogą występować sami!</color></size>", 6);
	    else if (c >= 6)
		ev.IsAllowed = true;
	    else
		ev.Player.ShowHint("<size=60%><color=yellow>SCP-079 z SCP-096 nie mogą występować razem!</color></size>", 6);
	}
    }
}

