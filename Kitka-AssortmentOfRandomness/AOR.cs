using System.Reflection;
using JetBrains.Annotations;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using WTTServerCommonLib.Models;

namespace KitkaAssortmentOfRandomness;

[Injectable(TypePriority = OnLoadOrder.TraderRegistration + 3), UsedImplicitly]
public class KITAOR(WTTServerCommonLib.WTTServerCommonLib wttCommon) : IOnLoad
{
    public async Task OnLoadAsync(CancellationToken cancellationToken)
    {
        var assembly = Assembly.GetExecutingAssembly();

        TraderIds.Add("HOSER", "69eccbae0764116786033c2e");

        await wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly,
           Path.Join("db", "CustomItems"));
        await wttCommon.CustomLocaleService.CreateCustomLocales(assembly,
            Path.Join("db", "CustomLocales"));
        await wttCommon.CustomHideoutRecipeService.CreateHideoutRecipes(assembly,
            Path.Join("db", "CustomHideoutRecipes"));
        await wttCommon.CustomQuestService.CreateCustomQuests(assembly,
            Path.Join("db", "CustomQuests"));
        await wttCommon.CustomQuestZoneService.CreateCustomQuestZones(assembly,
            Path.Join("db", "CustomQuestZones"));
        await wttCommon.CustomStaticSpawnService.CreateCustomStaticSpawns(assembly,
            Path.Join("db", "CustomStaticSpawns"));
        await wttCommon.CustomLootspawnService.CreateCustomLootSpawns(assembly,
            Path.Join("db", "CustomLootspawns"));
        await wttCommon.CustomDialogueService.CreateCustomDialogues(assembly,
            Path.Join("db", "CustomDialogues"));
    }
}