using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;
using System.Text;
using TestBot.Commands;

namespace TestBot
{
    public class Bot
    {
        public DiscordClient client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            //var json = "";
            //using (var fo = File.OpenRead("config.json"))
            //using (var sr = new StreamReader(fo, new UTF8Encoding(false)))
            //    json = await sr.ReadToEndAsync();

            //var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = "MTEwNjYzMjc5NTAyOTI2MjM3Nw.G9DcMW.GBuBIakyDnvOzkxOkutCxPkv2apu9cqpuiz_Sg",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            client = new DiscordClient(config);
            client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var comConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { "!" },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = client.UseCommandsNext(comConfig);

            Commands.RegisterCommands<Commands1>();

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
