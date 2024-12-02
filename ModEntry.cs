﻿using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.GameData.Locations;
using StardewValley.TerrainFeatures;
using xTile.Tiles;

namespace WishingWell
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        private bool isInWish = false;
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            Vector2 position = e.Cursor.Tile;
            if (Game1.currentLocation.isWaterTile((int)position.X, (int)position.Y) && !isInWish)
            {
                isInWish = true;
                List<QuestionAnswer> ltAnswers = new List<QuestionAnswer>()
                {
                    //The answers will be displayed in the order they are given here
                    new QuestionAnswer("Grant wish", GrantWish),
                    new QuestionAnswer("No", RemoveWishState)
                };
                QuestionDialogue dialogue = new QuestionDialogue("Hello! You have invoked the spirit of the water.\nDo you want to make a wish?", ltAnswers, 1000);
            }
        }

        private void RemoveWishState()
        {
            isInWish = false;
        }

        private void GrantWish()
        {
            if (Game1.player.Money > 0)
            {
                Game1.multipleDialogues(new String[] { "Thank you for the coin", "You are now luckier." });
                Game1.player.luckLevel.Value++;
                Game1.player.health += 20;
                Game1.player.stamina += 20;
                Game1.player.exhausted.Value = false;
                Game1.player.Money--;
                Game1.dialogueUp = false;
                RemoveWishState();
            }
            else if (Game1.player.Money == 0)
            {
                Game1.multipleDialogues(new String[] { "You don't have a single cent on you.", "I will try to help you." });
                Game1.player.health = Game1.player.maxHealth;
                Game1.player.stamina = Game1.player.maxStamina.Value;
                Game1.player.exhausted.Value = false;
                Game1.dialogueUp = false;
                RemoveWishState();
            }
        }
    }
}