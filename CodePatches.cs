using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace EventLimiter
{
    class CodePatches {

        public static void Game1_EventFinished_Postfix()
        {
            ModEntry.Instance.UpdateDicts();
        }

        public static bool Utility_GetMaximumHeartsForCharacter_Prefix(Character character, ref int __result)
        {
            var hearts = ModEntry.Instance.GetMaxHeartsForCharacter((string)character.Name);
            if (hearts is int result)
            {
                __result = result;
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void ProfileMenu_Draw_Postfix(ProfileMenu __instance, SpriteBatch b, SocialPage.SocialEntry ___Current, Vector2 ____heartDisplayPosition)
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

        public static void SocialPage_DrawNPCSlot_Postfix(SocialPage __instance, SpriteBatch b, int i, List<ClickableTextureComponent> ___sprites)
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
}
