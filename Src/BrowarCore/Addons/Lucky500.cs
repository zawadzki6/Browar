using Exiled.Events.EventArgs.Player;

namespace BrowarCore.Addons;

public static class Lucky500 {
    public static void UsedItem(ConsumingItemEventArgs ev) {
	if (ev.Item.Type != ItemType.SCP500)
	    return;

	if (UnityEngine.Random.Range(0, 4) != 0)
	    return;

	switch (UnityEngine.Random.Range(0, 5)) {
	    case 0:
		ev.Player.EnableEffect(EffectType.Invigorated, 3, 6);
		ev.Player.ShowHint("<size=60%>\n\n\n\n\nSCP-500 dało ci efekt <color=green>invigorated</color> na 6 sekund!</size>");
		break;
	    case 1:
		ev.Player.EnableEffect(EffectType.Vitality, 10, 1);
		ev.Player.ShowHint("<size=60%>\n\n\n\n\nSCP-500 dało ci efekt <color=green>vitality</color> na 10 sekund!</size>");
		break;
	    case 2:
		ev.Player.EnableEffect(EffectType.DamageReduction, 20, 20);
		ev.Player.ShowHint("<size=60%>\n\n\n\n\nSCP-500 dało ci efekt <color=green>damage reduction (10%)</color> na 20 sekund!</size>");
		break;
	    case 3:
		ev.Player.EnableEffect(EffectType.SilentWalk, 20, 255);
		ev.Player.ShowHint("<size=60%>\n\n\n\n\nSCP-500 dało ci efekt <color=green>silent walk</color> na 20 sekund!</size>");
		break;
	    case 4:
		ev.Player.EnableEffect(EffectType.MovementBoost, 10, 20);
		ev.Player.ShowHint("<size=60%>\n\n\n\n\nSCP-500 dało ci efekt <color=green>movement boost(10%)</color> na 20 sekund!</size>");
		break;
	}
    }
}
