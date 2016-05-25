using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds_agent_oriented_simulation.Entities
{
    class Combiner
    {
        private static string text;
        public static void Combine()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 ,6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
            text = "";
            for (int i = 8; i < 21; i++)
            {
                sum_up(numbers, i);
            }
            File.WriteAllText("combinations.txt", text);

        }

        private static void sum_up(List<int> numbers, int target)
        {
            sum_up_recursive(numbers, target, new List<int>());
        }

        private static void sum_up_recursive(List<int> numbers, int target, List<int> partial)
        {
            int s = 0;
            foreach (int x in partial) s += x;

            if (s == target)
                text += string.Join(",", partial.ToArray()) + Environment.NewLine;
                //File.WriteAllText("combinations.txt", string.Join(",", partial.ToArray()));
                //Console.WriteLine(string.Join(",", partial.ToArray()));

            if (s >= target)
                return;

            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> remaining = new List<int>();
                int n = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

                List<int> partial_rec = new List<int>(partial);
                partial_rec.Add(n);
                sum_up_recursive(remaining, target, partial_rec);
            }
        }

    }
}
