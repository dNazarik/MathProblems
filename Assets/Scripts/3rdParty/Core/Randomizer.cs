using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3rdParty.Core
{
    public static class Randomizer
    {
        private const float RandomMid = 0.5f;
        
        public static int[] GetNumbers(int length, int min = 0, int max = int.MaxValue, bool sequence = false)
        {
            var numbers = new int[length];
            var range = new List<int>(max - min);

            for (var i = min; i < max; i++)
                range.Add(i);

            if (sequence)
            {
                var startId = Random.Range(0, range.Count);

                if (startId + length > range.Count)
                    startId = range.Count - length;

                for (var i = 0; i < numbers.Length; i++)
                    numbers[i] = range[startId + i];

                return numbers;
            }

            range.Shuffle();

            for (var i = 0; i < numbers.Length; i++)
                numbers[i] = range[i];

            return numbers;
        }

        public static float GetNumberInRange(float from, float to) => Random.Range(from, to);
        public static int GetNumberInRange(int from, int to) => Random.Range(from, to);

        public static int GetNumberInRangeExcluding(int from, int to, IEnumerable<int> excluding)
        {
            var range = Enumerable.Range(from, to).Where(i => !excluding.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, to - excluding.Count());
            return range.ElementAt(index);
        }

        public static Color GetRandomColor(bool isAlphaRandom)
            => new Color(Random.value, Random.value, Random.value, isAlphaRandom ? Random.value : 1.0f);

        public static bool IsRandomHappen() => Random.value > RandomMid;
    }
}