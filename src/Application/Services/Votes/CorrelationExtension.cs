using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Votes
{
    public static class CorrelationExtension
    {
        /*
            Pearson Correlation Coefficient:

            r = sum[(x - avgX) * (y - avgY)] / 
            (sqrt(sum[(x - avgX)^2]) * sqrt(sum[(y - avgY)^2]))
        */

        public static double CalcCorrelationCoefficient(this List<int> seq1, List<int> seq2) 
        {
            if (!seq1.Any() || !seq2.Any())
                throw new ArgumentException("Sequence is empty");

            if (seq1.Count != seq2.Count)
                throw new ArgumentException("Sizes of sequences are not equal");

            if (seq1.All(i => i == 0) || seq2.All(i => i == 0))
                throw new ArgumentException("Sequence contains only 0");

            var avg1 = seq1.Average();  
            var avg2 = seq2.Average();

            var numerator = 0.0;

            for (int i = 0; i < seq1.Count; i++)
            {
                numerator += (seq1[i] - avg1) * (seq2[i] - avg2);
            }

            var sum1 = 0.0;
            var sum2 = 0.0;

            for (int i = 0; i < seq1.Count; i++)
            {
                sum1 += Math.Pow(seq1[i] - avg1, 2);
                sum2 += Math.Pow(seq2[i] - avg2, 2);
            }

            var denominator = Math.Sqrt(sum1) * Math.Sqrt(sum2);

            var res = numerator / denominator;
            return res;
        }

    }
}
