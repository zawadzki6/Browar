using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.CustomItems.API.Features;

namespace BrowarCore;
public class ItemsPatcher {
    private static readonly HashSet<string> ItemTracker = new();

    public static bool Check(Player player) => ItemTracker.Contains(player.UserId);
    public static void Mark(Player player) => ItemTracker.Add(player.UserId); 
    public static void UnMark(Player player) => ItemTracker.Remove(player.UserId);
    public static void Reset() => ItemTracker.Clear();

    public void OnRoundRestart() => Reset();
    public void OnDying(DyingEventArgs ev) => UnMark(ev.Player);

    public static void Patcher(ChangingItemEventArgs ev) {
	if (Check(ev.Player))
	    ev.IsAllowed = false;
    }
    public static void Patcher(DroppingItemEventArgs ev) {
	if (Check(ev.Player))
	    ev.IsAllowed = false;
    }
    public static void Patcher(CancellingItemUseEventArgs ev) {
	if (CustomItem.Registered.Any(c => c.Check(ev.Item)))
	    ev.IsAllowed = false;
    }
    public static void Patcher(UsingItemEventArgs ev) {
	if (CustomItem.Registered.Any(c => c.Check(ev.Item)))
	    Mark(ev.Player);
    }
    public static void Patcher(UsedItemEventArgs ev) => UnMark(ev.Player);
    public static void Patcher(DiedEventArgs ev) => UnMark(ev.Player);
    public static void Patcher(ChangingRoleEventArgs ev) => UnMark(ev.Player);
    public static void Patcher(JoinedEventArgs ev) => UnMark(ev.Player);
    public static void Patcher(LeftEventArgs ev) => UnMark(ev.Player);
}
