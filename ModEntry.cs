using EventLimiter;
using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

internal sealed class ModEntry : Mod
{
    static ModEntry _instance;
    internal static ModEntry Instance => _instance;

    private CharacterEventsProvider _eventsProvider;

    /// <summary>character name, [heart number, available?]</summary>
    private Dictionary<string, Dictionary<int, bool>> _dictNames = new();

    public override void Entry(IModHelper helper)
    {
        _instance = this;

        var modsUsed = new List<string>();
        modsUsed.AddRange(new[]
        {
            "FlashShifter.StardewValleyExpandedCP",
            "tenthousandcats.ImmersiveCShane",
            "Lemurkat.MarnieRanchPack.CP",
            "Lemurkat.JasRanchPack.CP"
        }.Where(modId => helper.ModRegistry.IsLoaded(modId)));

        _eventsProvider = new CharacterEventsProvider(this.Helper, this.Monitor, modsUsed);

        helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;

        var harmony = new Harmony(ModManifest.UniqueID);
        harmony.Patch(
            original: AccessTools.Method(typeof(Game1), nameof(Game1.eventFinished)),
            postfix: new HarmonyMethod(typeof(CodePatches), nameof(CodePatches.Game1_EventFinished_Postfix))
        );
        harmony.Patch(
            original: AccessTools.Method(typeof(Utility), nameof(Utility.GetMaximumHeartsForCharacter)),
            prefix: new HarmonyMethod(typeof(CodePatches), nameof(CodePatches.Utility_GetMaximumHeartsForCharacter_Prefix))
        );
        harmony.Patch(
            original: AccessTools.Method(
                typeof(ProfileMenu),
                nameof(ProfileMenu.draw),
                new[] { typeof(SpriteBatch) }
            ),
            postfix: new HarmonyMethod(typeof(CodePatches), nameof(CodePatches.ProfileMenu_Draw_Postfix))
        );
        harmony.Patch(
            original: AccessTools.Method(typeof(SocialPage), nameof(SocialPage.drawNPCSlot)),
            postfix: new HarmonyMethod(typeof(CodePatches), nameof(CodePatches.SocialPage_DrawNPCSlot_Postfix))
        );

        //var data = this.Helper.Data.ReadJsonFile<Dictionary<string, Dictionary<int, string>>>("assets/data.json");
        //string path = Path.Combine(this.Helper.DirectoryPath, @"Characters\Shane_default.txt");
        //string jsonString = File.ReadAllText(path);

        //var nestedDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonString)
        //         ?? new Dictionary<string, Dictionary<string, string>>();

        ////// Доступ к элементам
        //string prodHost = nestedDict["Production"]["Host"]; // "://example.com"
        //Monitor.Log($"Production Host: {prodHost}", LogLevel.Debug);
    }

    private void GameLoop_SaveLoaded(object? sender, SaveLoadedEventArgs e)
    {
        UpdateDicts();
    }

    public void UpdateDicts()
    {
        if (!_eventsProvider.Settings.Contains("joja") &&  Utility.HasAnyPlayerSeenEvent("191393"))
            _eventsProvider.Settings.Add("joja");
        if (!_eventsProvider.Settings.Contains("no_emily") && Game1.player.eventsSeen.Contains("2123243"))
            _eventsProvider.Settings.Add("no_emily"); //just for you, Clint. feeling special?

        _dictNames.Clear();
    }

    private Dictionary<int, bool>? GetBoolDictsForCharacter(string name)
    {
        var result = new Dictionary<int, bool>();
        var eventsDict = _eventsProvider.GetEventsDictForCharacter(name);
        var eventsSeen = Game1.player.eventsSeen;

        if (eventsDict == null)
            return null;

        foreach (var pair in eventsDict)
        {
            result[pair.Key] = eventsSeen.Contains(pair.Value);
        }
        return result;
    }

    public int? GetMaxHeartsForCharacter(string name)
    {
        var dict = GetDictForCharacter(name);
        if (dict is null)
            return null;

        var result = dict.FirstOrDefault(pair => pair.Value == false);
        return result.Key;
    }

    private Dictionary<int, bool>? GetDictForCharacter(string name)
    {
        if (!_dictNames.Keys.Contains(name))
        {
            if (!InsertCharacter(name))
                return null;
        }
        return _dictNames[name];
    }

    private bool InsertCharacter(string name)
    {
        var dict = _instance?.GetBoolDictsForCharacter(name);
        if (dict == null)
        {
            return false;
        } else
        {
            _dictNames[name] = dict;
            return true;
        }
    }
}
