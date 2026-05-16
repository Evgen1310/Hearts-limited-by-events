using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using static StardewValley.Menus.SocialPage;

namespace EventLimiter
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        private static ModEntry _instance;

        /// <summary>Список поддерживаемых имён.</summary>
        private string[] Names = { "Shane"};

        /// <summary>имя персонажа, [сердца, доступно ли]</summary>
        private Dictionary<string, Dictionary<int, bool>> DictNames = new Dictionary<string, Dictionary<int, bool>>();

        private static Dictionary<int, string> EventsShane = new Dictionary<int, string>()
        {
            { 2, "611944"},
            { 4, "3910674"},
            { 6, "2118991"},
            { 7, "3910974" },
            { 8, "3900074"},
            { 10, "9581348"},
            { 14, "-1"}
        };

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            _instance = this;

            helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
            var harmony = new Harmony(ModManifest.UniqueID);
            harmony.PatchAll();
        }

        private void GameLoop_SaveLoaded(object? sender, SaveLoadedEventArgs e)
        {
            UpdateDicts();
        }

        [HarmonyPatch(typeof(Game1), nameof(Game1.eventFinished))]
        public class Game1_EventFinished_Patch
        {
            public static void Postfix()
            {
                _instance?.UpdateDicts();
            }
        }

        private void UpdateDicts()
        {
            DictNames.Clear();
            foreach (var character in Names)
            {
                DictNames[character] = GetBoolDictsForCharacter(character);
            }
        }

        public static Dictionary<int, bool> GetBoolDictsForCharacter(string name)
        {
            var result = new Dictionary<int, bool>();
            var eventsDict = GetEventsDictForCharacter(name);
            foreach (var pair in eventsDict)
            {
                result[pair.Key] = Game1.player.eventsSeen.Contains(pair.Value);
            }
            return result;
        }

        public static Dictionary<int, string> GetEventsDictForCharacter(string name)
        {
            switch (name)
            {
                case "Shane":
                    return EventsShane;
            }
            return new Dictionary<int, string>();
        }

        [HarmonyPatch(typeof(Utility), nameof(Utility.GetMaximumHeartsForCharacter))]
        public class Utility_GetMaximumHeartsForCharacter_Patch
        {
            public static bool Prefix(Character character, ref int __result)
            {
                if (!_instance.Names.Contains(character.Name)) //abort if unknown character
                {
                    return true;
                }

                var hearts = _instance?.GetMaxHeartsForCharacter((string)character.Name);
                if (hearts is int beda)
                {
                    __result = beda;
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public int GetMaxHeartsForCharacter(string name)
        {
            var dict = GetDictForCharacter(name);
            var result = dict.FirstOrDefault(pair => pair.Value == false);
            return result.Key;
        }

        private Dictionary<int, bool> GetDictForCharacter(string name)
        {
            return DictNames[name];
        }

        [HarmonyPatch(typeof(ProfileMenu), "draw")]
        public class ProfileMenu_GrayCapHearts_Patch
        {
            public static void Postfix(ProfileMenu __instance, SpriteBatch b, SocialPage.SocialEntry ___Current, Vector2 ____heartDisplayPosition)
            {
                if (__instance.Current.Character is not NPC npc)
                {
                    return;
                }
                int maxHearts = Utility.GetMaximumHeartsForCharacter(npc);
                if (maxHearts >= 10) return; //think about spouces

                int drawn_hearts = Math.Max(10, Utility.GetMaximumHeartsForCharacter(npc));
                float heart_draw_start_x = ____heartDisplayPosition.X - (float)(Math.Min(10, drawn_hearts) * 32 / 2);
                float heart_draw_offset_y = ((drawn_hearts > 10) ? (-16f) : 0f);

                for (int h = maxHearts; h < 10; h++)
                {
                    Vector2 pos = new Vector2(heart_draw_start_x + (float)(h * 32), ____heartDisplayPosition.Y + heart_draw_offset_y);
                    b.Draw(Game1.mouseCursors, pos, new Rectangle(211, 428, 7, 6), Color.Black * 0.2f, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.88f);
                }
            }
        }

        [HarmonyPatch(typeof(SocialPage), "drawNPCSlot")]
        public class SocialPage_GrayCapHearts_Patch
        {
            public static void Postfix(SocialPage __instance, SpriteBatch b, int i, List<ClickableTextureComponent> ___sprites)
            {
                string name = __instance.GetSocialEntry(i).InternalName;
                int maxHearts = Utility.GetMaximumHeartsForCharacter(Game1.getCharacterFromName(name, true, false));

                if (maxHearts >= 10) return;

                for (int h = maxHearts; h < 10; h++)
                {
                    Vector2 pos = new Vector2(__instance.xPositionOnScreen + 320 - 4 + h * 32, ___sprites[i].bounds.Y + 64 - 28);
                    b.Draw(Game1.mouseCursors, pos, new Rectangle(211, 428, 7, 6), Color.Black * 0.2f, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.88f);
                }
            }
        }

        [HarmonyPatch(typeof(SocialPage), nameof(SocialPage.drawNPCSlotHeart))]
        public class SocialPage_DrawNPCSlotHeart_Patch
        {
            public static void Prefix(SpriteBatch b, int npcIndex, SocialEntry entry, int hearts, bool isDating, bool isCurrentSpouse)
            {
                isDating = true;
                //bool isLockedHeart = true;
                //int heartX = ((hearts < entry.HeartLevel || isLockedHeart) ? 211 : 218);
                //Color heartTint = ((hearts < 10 && isLockedHeart) ? (Color.Black * 0.35f) : Color.White);
                //if (hearts < 10)
                //{
                //    b.Draw(Game1.mouseCursors, new Vector2(__instance.xPositionOnScreen + 320 - 4 + hearts * 32, ___sprites[npcIndex].bounds.Y + 64 - 28), new Rectangle(heartX, 428, 7, 6), heartTint, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.88f);
                //}
                //else
                //{
                //    b.Draw(Game1.mouseCursors, new Vector2(__instance.xPositionOnScreen + 320 - 4 + (hearts - 10) * 32, ___sprites[npcIndex].bounds.Y + 64), new Rectangle(heartX, 428, 7, 6), heartTint, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.88f);
                //}
                //return false;
            }
        }
    }
}