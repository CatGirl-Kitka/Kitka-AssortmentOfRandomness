using JetBrains.Annotations;
using SPTarkov.Server.Core.Models.Spt.Mod;
using Range = SemanticVersioning.Range;
using Version = SemanticVersioning.Version;

namespace KitkaAssortmentOfRandomness;

[UsedImplicitly]
public record ModMetadata : IModMetadata
{
    public string ModGuid { get; init; } = "com.kitka.aassortmentofrandomness";
    public string Name { get; init; } = "Kitka Assortment Of Randomness";
    public string Author { get; init; } = "Kitka";
    public List<string>? Contributors { get; init; } = null;
    public Version Version { get; init; } = new(typeof(ModMetadata).Assembly.GetName().Version!.ToString(3));
    public Range SptVersion { get; init; } = new("~4.1.5");
    public bool HasPrepatcher { get; init; } = false;
    public List<string>? Incompatibilities { get; init; }

    public Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        // TODO: fine-tune dependency versions
         { "com.wtt.commonlib", new Range("^3.0.6") },
        // { "com.wtt.contentbackport", new Range("^2.0.0") },
        // { "com.wtt.armory", new Range("^3.0.0") }
    };

    public string? Url { get; init; } = "";
    public string License { get; init; } = "CC-NC-ND 4.0";
}