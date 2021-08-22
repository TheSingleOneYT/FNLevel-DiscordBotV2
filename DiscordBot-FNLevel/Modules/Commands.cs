using Discord.Commands;
using Fortnite_API;
using Fortnite_API.Objects.V1;
using System.Threading.Tasks;

namespace DiscordBot_FNLevel.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        //Begin commands
        [Command("lvl")]
        public async Task GetLevelAsync([Remainder] string s)
        {
            var api = new FortniteApiClient();

            var account = Context.Message.ToString().Remove(0, 5);

            var level = await api.V1.Stats.GetBrV2Async(x =>
            {
                x.Name = account;
                x.ImagePlatform = BrStatsV2V1ImagePlatform.All;
                x.AccountType = BrStatsV2V1AccountType.Epic;
            });

            if (level.IsSuccess)
            {
                await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + ", account '**" + account + "**' is battle pass level **" + level.Data.BattlePass.Level.ToString() + "**.");
            }
            else
            {
                await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + ", could not find account **" + account + "**. Please check your spelling and make sure this is an **EPIC ACCOUNT**.");
            }
        }

        [Command("lvl-byID")]
        public async Task GetIDLevelAsync([Remainder] string s)
        {
            var api = new FortniteApiClient();

            var account = Context.Message.ToString().Remove(0, 10);

            var level = await api.V1.Stats.GetBrV2Async(x =>
            {
                x.AccountId = account;
                x.ImagePlatform = BrStatsV2V1ImagePlatform.All;
                x.AccountType = BrStatsV2V1AccountType.Epic;
            });

            if (level.IsSuccess)
            {
                await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + ", account '**" + account + "**' is battle pass level **" + level.Data.BattlePass.Level.ToString() + "**. (Epic name = **" + level.Data.Account.Name.ToString() + "**).");
            }
            else
            {
                await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + ", could not find account **" + account + "**. Please check your spelling and make sure this is an **EPIC ACCOUNT**.");
            }
        }
    }
}
