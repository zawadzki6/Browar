using Exiled.API.Features.Components;
using Exiled.API.Features.Items;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features.Pickups.Projectiles;
using Enums = Exiled.API.Enums;

namespace BrowarCore.Modules.CustomItems;

[CustomItem(ItemType.GunRevolver)]
public class GrenadeLauncher : CustomWeapon {
    public override string Name { get; set; } = "<color=#bcff9b>GRANATNIK</color>";
    public override string Description { get; set; } = "<color=yellow>Strzela granatami</color> które wybuchają przy zderzeniu. Odnawia się po 60s";
    public override uint Id { get; set; } = 11;
    public override float Weight { get; set; } = default;
    public override SpawnProperties? SpawnProperties { get; set; }
    public override float Damage { get; set; } = 0f;
 
    protected override void OnShooting(ShootingEventArgs ev) {
	if (!Check(ev.Player.CurrentItem)) return;
	ev.IsAllowed = false;
	if (Tracker.HasUsed(ev.Player)) {
	    ev.Firearm.MagazineAmmo = 0;
	    return;
	}

	Projectile projectile = ev.Player.ThrowGrenade(Enums.ProjectileType.FragGrenade).Projectile;
	projectile.GameObject.AddComponent<CollisionHandler>().Init(ev.Player.GameObject, projectile.Base);

	Tracker.Mark(ev.Player);
	Timing.CallDelayed(60, () => { Tracker.UnMark(ev.Player); ev.Player.ShowHint("\n\n[ Granatnik jest gotowy ]"); ev.Firearm.MagazineAmmo = 8;});
	
    }
    
    public void OnReload(ReloadingWeaponEventArgs ev) {
	if (!Check(ev.Player.CurrentItem)) return;
	ev.IsAllowed = false;
    }
    
    public void OnUnload(UnloadingWeaponEventArgs ev) {
	if (!Check(ev.Player.CurrentItem)) return;
	ev.IsAllowed = false;
    }

    public void OnDrop(DroppingItemEventArgs ev) {
	if (!Check(ev.Item)) return;
	if (ev.Item is Firearm Firearm) Firearm.MagazineAmmo = 8;
    }

    protected override void SubscribeEvents()
        {
	    Handlers.Player.Shooting += OnShooting;
	    Handlers.Player.ReloadingWeapon += OnReload;
	    Handlers.Player.UnloadingWeapon += OnUnload;
	    Handlers.Server.RestartingRound += OnRoundRestart;
	    Handlers.Player.DroppingItem += OnDrop;
            base.SubscribeEvents();
        }
    protected override void UnsubscribeEvents()
        {
	    Handlers.Player.Shooting -= OnShooting;
	    Handlers.Player.ReloadingWeapon -= OnReload;
	    Handlers.Player.UnloadingWeapon -= OnUnload;
	    Handlers.Server.RestartingRound -= OnRoundRestart;
	    Handlers.Player.DroppingItem -= OnDrop;
            base.UnsubscribeEvents();
        }


    protected class Tracker {
        private static readonly HashSet<string> used = new();
    
        public static bool HasUsed(Features.Player player) => used.Contains(player.UserId);
        public static void Mark(Features.Player player) => used.Add(player.UserId); 
	public static void UnMark(Features.Player player) => used.Remove(player.UserId);
        public static void Reset() => used.Clear();
    }

    void OnRoundRestart() => Tracker.Reset();
}
