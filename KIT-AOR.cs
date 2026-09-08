using System.Reflection;
using SPTarkov.Server.Core.DI;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Spt.Mod;
using KitkaExfilAssortment.Traders;
using Path = System.IO.Path;
using Range = SemanticVersioning.Range;

namespace KitkaExfilAssortment.Main;

// 2. Mod Metadata
public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.kitka.kitkaassortmentofrandomness";
    public override string Name { get; init; } = "Kitka Assortment Of Randomness";
    public override string Author { get; init; } = "Kitka"; 
    
    // Mod Version + SPT Version Needed Under | Need " IsBundleMod " on True
    public override SemanticVersioning.Version Version { get; init; } = new("0.0.1");
    public override Range SptVersion { get; init; } = new("~4.0.13");
    public override string License { get; init; } = "MIT";
    //Need "IsBundleMod" on True
    public override bool? IsBundleMod { get; init; } = true;
    // Put Dependencies Under
    public override Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~2.0.24") }
    };
    // Put Contributors + Incompatibilities Under
    public override string? Url { get; init; }
    public override List<string>? Contributors { get; init; }
    public override List<string>? Incompatibilities { get; init; }
}
// 2. Inject WTT and Load the Mod
[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class KitkaAssortmentOfRandomness(
    WTTServerCommonLib.WTTServerCommonLib wttCommon
) : IOnLoad
{
    public async Task OnLoad()
    {

        // Get your current assembly
        var assembly = Assembly.GetExecutingAssembly();
        
        // trader id gen idk i need to edit ts later
        


        // Use WTT-CommonLib services
        await wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly);
        await wttCommon.CustomLocaleService.CreateCustomLocales(assembly);
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
        
        await Task.CompletedTask;
        
    }
}