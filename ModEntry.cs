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
        private static ModEntry? _instance;

        private static bool _ifImmersiveShane;
        private static bool _ifImmersiveMarnie;
        private static bool _ifImmersiveJas;

        /// <summary>Список поддерживаемых имён.</summary>
        private string[] Names = { "Shane", "Alex", "Sebastian", "Sam", "Harvey"
        , "Elliott", "Abigail", "Leah", "Maru", "Penny", "Haley", "Emily", "Willy"
        , "Vincent", "Wizard", "Gus", "Demetrius", "Jas", "Jodi", "George"
        , "Kent", "Clint", "Krobus", "Caroline", "Leo", "Linus", "Lewis"
        , "Marnie", "Pam", "Pierre", "Robin", "Evelyn"};

        /// <summary>имя персонажа, [сердца, доступно ли]</summary>
        private Dictionary<string, Dictionary<int, bool>> DictNames = new Dictionary<string, Dictionary<int, bool>>();

        private static Dictionary<int, string> EventsShane_Vanilla = new Dictionary<int, string>()
        {
            { 2, "611944"},
            { 4, "3910674"},
            { 6, "2118991"},
            { 7, "3910974" },
            { 8, "3900074"},
            { 10, "9581348"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsShane_Immersive = new Dictionary<int, string>()
        {
            { 2, "611944"},
            { 4, "3910674"},
            { 6, "2118991"},
            { 7, "3910974" },
            { 8, "9581348"},
            { 10, "59443111"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsAlex = new Dictionary<int, string>()
        {
            { 4, "2481135"},
            { 5, "21"},
            { 6, "2119820"},
            { 8, "288847"},
            { 10, "911526"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsSebastian = new Dictionary<int, string>()
        {
            { 2, "2794460"},
            { 4, "384883"},
            { 6, "27"},
            { 8, "29"},
            { 10, "384882"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsSam = new Dictionary<int, string>()
        {
            { 2, "44"},
            { 4, "46"},
            { 6, "45"},
            { 8, "4081148"},
            { 10, "233104"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsHarvey = new Dictionary<int, string>()
        {
            { 2, "56"},
            { 4, "57"},
            { 6, "58"},
            { 8, "571102"},
            { 10, "528052"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsElliott = new Dictionary<int, string>()
        {
            { 2, "39"},
            { 4, "40"},
            { 6, "423502"},
            { 8, "1848481"},
            { 10, "43"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsAbigail = new Dictionary<int, string>()
        {
            { 2, "1"},
            { 4, "2"}, // 2 (дождь, горы, флейта) Нужна? Optional
            { 6, "4"},
            { 8, "3"},
            { 10, "901756"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsLeah = new Dictionary<int, string>()
        {
            { 2, "50"},
            { 4, "51"},
            { 6, "52"},
            { 8, "55"},
            { 10, "54"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsMaru = new Dictionary<int, string>()
        {
            { 2, "6"},
            { 4, "7"},
            { 6, "8"},
            { 8, "9"},
            { 10, "10"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsPenny = new Dictionary<int, string>()
        {
            { 2, "34"}, // (geroge wheelchair)
            { 4, "35"},
            { 6, "36"},
            { 8, "181928"},
            { 10, "38"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsHaley = new Dictionary<int, string>()
        {
            { 2, "11"},
            { 4, "12"},
            { 6, "13"},
            { 8, "14"},
            { 10, "15"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsEmily = new Dictionary<int, string>()
        {
            { 2, "471942"},
            { 4, "463391"},
            { 6, "917409"},
            { 8, "2123243"},
            { 10, "2123343"},
            { 14, "-1"}
        };

        private static Dictionary<int, string> EventsWilly = new Dictionary<int, string>()
        {
            { 6, "711130"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsVincent = new Dictionary<int, string>()
        {
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsWizard = new Dictionary<int, string>()
        {
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsGus = new Dictionary<int, string>()
        {
            { 4, "96"},
            { 5, "980558"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsDemetrius = new Dictionary<int, string>()
        {
            { 6, "25"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsJas_Vanilla = new Dictionary<int, string>()
        {
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsJas_Immersive = new Dictionary<int, string>()
        {
            { 2, "50706112"},
            { 4, "50706113"},
            { 7, "50706114"},
            { 8, "50706115"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsJodi = new Dictionary<int, string>()
        {
            { 4, "94"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsGeorge = new Dictionary<int, string>()
        {
            { 6, "18"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsKent = new Dictionary<int, string>()
        {
            { 6, "100"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsClint = new Dictionary<int, string>()
        {
            { 3, "97"},
            { 6, "101"}, // (not if 8/10 heart emily or spouce)
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsKrobus = new Dictionary<int, string>()
        {
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsCaroline = new Dictionary<int, string>()
        {
            { 2, "719926"},
            { 6, "17"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsLeo = new Dictionary<int, string>()
        {
            { 2, "6497423"},
            { 4, "6497421"},
            { 6, "6497428"},
            { 9, "8959199"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsLinus = new Dictionary<int, string>()
        {
            { 4, "26"},
            { 8, "371652"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsLewis = new Dictionary<int, string>()
        {
            { 6, "639373"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsMarnie_Vanilla = new Dictionary<int, string>()
        {
            { 3, "92"},
            { 6, "639373"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsMarnie_Immersive = new Dictionary<int, string>()
        {
            { 2, "50706102"},
            { 4, "50706104"},
            { 8, "50706108"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsPam = new Dictionary<int, string>()
        {
            { 9, "503180"}, // (BUILD THE HOUSE DAMN)
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsPierre = new Dictionary<int, string>()
        {
            { 6, "16"}, // (not really relevent? about secret stash)
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsRobin = new Dictionary<int, string>()
        {
            { 6, "33"},
            { 10, "-1"},
        };

        private static Dictionary<int, string> EventsEvelyn = new Dictionary<int, string>()
        {
            { 4, "19"},
            { 10, "-1"},
        };

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            _instance = this;
            _ifImmersiveShane = this.Helper.ModRegistry.IsLoaded("tenthousandcats.ImmersiveCShane");
            _ifImmersiveMarnie = this.Helper.ModRegistry.IsLoaded("Lemurkat.MarnieRanchPack.CP");
            _ifImmersiveJas = this.Helper.ModRegistry.IsLoaded("Lemurkat.JasRanchPack.CP");
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
                DictNames.Add(character, GetBoolDictsForCharacter(character));
            }
        }

        public static Dictionary<int, bool> GetBoolDictsForCharacter(string name)
        {
            var result = new Dictionary<int, bool>();
            var eventsDict = GetEventsDictForCharacter(name);
            foreach (var pair in eventsDict)
            {
                result.Add(pair.Key, Game1.player.eventsSeen.Contains(pair.Value));
            }
            return result;
        }

        public static Dictionary<int, string> GetEventsDictForCharacter(string name)
        {
            switch (name)
            {
                case "Alex":
                    return EventsAlex;
                case "Sebastian":
                    return EventsSebastian;
                case "Sam":
                    return EventsSam;
                case "Shane":
                    if (_ifImmersiveShane)
                    {
                        return EventsShane_Immersive;
                    }
                    else
                    {
                        return EventsShane_Vanilla;
                    }
                case "Harvey":
                    return EventsHarvey;
                case "Elliott":
                    return EventsElliott;
                case "Abigail":
                    return EventsAbigail;
                case "Leah":
                    return EventsLeah;
                case "Maru":
                    return EventsMaru;
                case "Penny":
                    return EventsPenny;
                case "Haley":
                    return EventsHaley;
                case "Emily":
                    return EventsEmily;
                case "Willy":
                    return EventsWilly;
                case "Vincent":
                    return EventsVincent;
                case "Wizard":
                    return EventsWizard;
                case "Gus":
                    return EventsGus;
                case "Demetrius":
                    return EventsDemetrius;
                case "Jas":
                    if (_ifImmersiveJas)
                    {
                        return EventsJas_Immersive;
                    }
                    else
                    {
                        return EventsJas_Vanilla;
                    }
                case "Jodi":
                    return EventsJodi;
                case "George":
                    return EventsGeorge;
                case "Kent":
                    return EventsKent;
                case "Clint":
                    return EventsClint;
                case "Krobus":
                    return EventsKrobus;
                case "Caroline":
                    return EventsCaroline;
                case "Leo":
                    return EventsLeo;
                case "Linus":
                    return EventsLinus;
                case "Lewis":
                    return EventsLewis;
                case "Marnie":
                    if (_ifImmersiveMarnie)
                    {
                        return EventsMarnie_Immersive;
                    }
                    else
                    {
                        return EventsMarnie_Vanilla;
                    }
                case "Pam":
                    return EventsPam;
                case "Pierre":
                    return EventsPierre;
                case "Robin":
                    return EventsRobin;
                case "Evelyn":
                    return EventsEvelyn;
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
            if (!DictNames.Keys.Contains(name))
                return null;
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