using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using static StardewValley.Menus.SocialPage;

namespace EventLimiter
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(ModManifest.UniqueID);
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Utility), nameof(Utility.GetMaximumHeartsForCharacter))]
        public class Utility_GetMaximumHeartsForCharacter_Patch
        {
            public static bool Prefix(Character character, ref int __result)
            {
                switch ((string)character.Name)
                {
                    case "Shane":
                        if (Game1.player.eventsSeen.Contains("611944"))
                        {
                            __result = 5;
                            return false;
                        }
                        break;
                }
                __result = 2;
                return false;
            }
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
                if (maxHearts >= 10) return;

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