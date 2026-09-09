using System.Reflection;
using JetBrains.Annotations;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers.Server;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Routers;
using SPTarkov.Server.Core.Utils;
using Path = System.IO.Path;


namespace KitkaExfilAssortment.Traders;

public class TraderLoader
{
    [Injectable(TypePriority = OnLoadOrder.TraderRegistration + 1), UsedImplicitly]
    public class WTTCAG_TraderLoad(
        ImageRouter imageRouter,
        TimeUtil timeUtil,
        TraderHelper traderHelper,
        TraderConfig traderConfig,
        RagfairConfig ragfairConfig,
        ModHelper modHelper
    ) : IOnLoad
    {
        public Task OnLoadAsync(CancellationToken cancellationToken)
{
    var pathToMod = modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
        
    var traderImagePath = Path.Combine(pathToMod, "db/TraderKitka/kitka.jpg");
        
    var traderBase = modHelper.GetJsonDataFromFile<TraderBase>(pathToMod, "db/TraderKitka/base.jsonc");

    imageRouter.AddRoute(traderBase.Avatar!.Replace(".jpg", ""), traderImagePath);
    TraderHelper.SetTraderUpdateTime(traderConfig, traderBase, timeUtil.GetHoursAsSeconds(1),
        timeUtil.GetHoursAsSeconds(2));

    ragfairConfig.Traders.TryAdd(traderBase.Id, true);



    traderHelper.AddTraderWithEmptyAssortToDb(traderBase);

    traderHelper.AddTraderToLocales(traderBase,
        "A strange woman with cat ears and a cat's tail, she mostly sells strange, useless things, or things that nobody wants. Where does she get all these items? Nobody knows, but one thing is certain: nobody wants them except collectors and strange people");
    

    var assort = modHelper.GetJsonDataFromFile<TraderAssort>(pathToMod, "db/TraderKitka/assort.jsonc");

    traderHelper.OverwriteTraderAssort(traderBase.Id, assort);


    return Task.CompletedTask;
}
}
}
