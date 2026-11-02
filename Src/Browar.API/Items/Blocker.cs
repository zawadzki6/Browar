using Exiled.Events.EventArgs.Player;
using Exiled.CustomItems.API.Features;

namespace Browar.API.Items;

public class Blocker {
    private static readonly HashSet<string> ItemTracker = new();

    public static bool Check(Features.Player player) => ItemTracker.Contains(player.UserId);
    public static void Mark(Features.Player player) => ItemTracker.Add(player.UserId); 
    public static void UnMark(Features.Player player) => ItemTracker.Remove(player.UserId);
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
    protected static void Patcher(UsingItemCompletedEventArgs ev) => UnMark(ev.Player);
    protected static void Patcher(DiedEventArgs ev) => UnMark(ev.Player);
    protected static void Patcher(ChangingRoleEventArgs ev) => UnMark(ev.Player);
    protected static void Patcher(JoinedEventArgs ev) => UnMark(ev.Player);
    protected static void Patcher(LeftEventArgs ev) => UnMark(ev.Player);
    
    public static void Subscribe() {
	Handlers.Player.ChangingItem += Patcher;
	Handlers.Player.UsingItem += Patcher;
	Handlers.Player.DroppingItem += Patcher;
	Handlers.Player.UsingItemCompleted += Patcher;
	Handlers.Player.Died += Patcher;
	Handlers.Player.ChangingRole += Patcher;
	Handlers.Player.Joined += Patcher;
	Handlers.Player.Left += Patcher;
	Handlers.Player.CancellingItemUse += Patcher;
    }
    public static void Unsubscribe() {
	Handlers.Player.ChangingItem -= Patcher;
	Handlers.Player.UsingItem -= Patcher;
	Handlers.Player.DroppingItem -= Patcher;
	Handlers.Player.UsingItemCompleted -= Patcher;
	Handlers.Player.Died -= Patcher;
	Handlers.Player.ChangingRole -= Patcher;
	Handlers.Player.Joined -= Patcher;
	Handlers.Player.Left -= Patcher;
	Handlers.Player.CancellingItemUse -= Patcher;
    }
}
