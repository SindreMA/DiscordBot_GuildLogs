using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Audio;

namespace DiscordBot
{
    class MyBot
    {
        DiscordClient _client;
        public Channel vchannel;
        public MyBot()
        {
            _client = new DiscordClient(x =>
            {

                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            _client.UsingCommands(x =>
            {
                x.PrefixChar = '.';
                x.AllowMentionPrefix = true;
            });
     
            _client.UsingAudio(x => // Opens an AudioConfigBuilder so we can configure our AudioService
            {
                x.Mode = AudioMode.Outgoing; // Tells the AudioService that we will only be sending audio
            });
           


            
            var commands = _client.GetService<CommandService>();

            commands.CreateCommand("sum")
           .Do(async (e) =>
           {
               vchannel = e.User.VoiceChannel;
               var _vClient = _client.GetService<AudioService>().Join(vchannel);
               await e.User.VoiceChannel.JoinAudio();
           });
            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Hi!");
                });
            commands.CreateCommand("log")
           .Do(async (e) =>
           {
               string log = "https://www.warcraftlogs.com/guilds/122793/";
               await e.Channel.SendMessage("Here is our warcraftlogs.com page : " + log);
           });

            _client.ExecuteAndWait(async () =>
            {
                await _client.Connect("############################", TokenType.Bot);
            });


        }
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
