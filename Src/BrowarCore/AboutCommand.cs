using CommandSystem;

namespace BrowarCore;

[CommandHandler(typeof(ClientCommandHandler))]
public class AboutCommand : ICommand {
    public string Command { get; } = "browar";
    public string[] Aliases { get; } = {"browar"};
    public string Description { get; } = "Plugin info";

    public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response) {
	response = $"Browar base {Plugin.Instance?.Version} is made by: {Plugin.Instance?.Author}\nHash: {Plugin.Instance?.Assembly.GetHashCode()}";
	return true;
    }
}
