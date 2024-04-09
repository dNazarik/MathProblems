using System.Collections.Generic;
using Core.Utils;

namespace Gameplay.Math
{
    public class MathProblem
    {
        public string ProblemString;
        public int CorrectAnswer;
        public int[] Options;

        public int[] GetShuffledOptions()
        {
            var allOptions = new List<int>(Options.Length + 1);
            allOptions.AddRange(Options);
            allOptions.Add(CorrectAnswer);
            allOptions.Shuffle();

            return allOptions.ToArray();
        }
    }
}
