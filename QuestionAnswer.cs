using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishingWell
{
    //Instead of using this class one could also use a tuple, but that might cause issues with your mod
    //on Linux and macOS
    /// <summary>A possible answer for a question dialogue.</summary>
    internal class QuestionAnswer
    {
        /// <summary>The answer key provided to the game.</summary>
        public string SKey { get; }

        /// <summary>Handles the answer when the player chooses it.</summary>
        public Action? FunctionHandler { get; }

        /// <summary>Construct an instance.</summary>
        /// <param name="sKey">The answer key provided to the game.</param>
        /// <param name="functionHandler">Handles the answer when the player chooses it.</param>
        public QuestionAnswer(string sKey, Action? functionHandler)
        {
            this.SKey = sKey;
            this.FunctionHandler = functionHandler;
        }
    }
}
