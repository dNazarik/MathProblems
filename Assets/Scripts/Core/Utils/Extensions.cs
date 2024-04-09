using System.Collections.Generic;
using System.Linq;

namespace Core.Utils
{
    public static class Extensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable) =>
            enumerable.RandomElementUsing<T>(new System.Random());

        private static T RandomElementUsing<T>(this IEnumerable<T> enumerable, System.Random rand)
        {
            var iEnumerable = enumerable.ToArray();
            int index = rand.Next(0, iEnumerable.Length);
            return iEnumerable.ElementAt(index);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}