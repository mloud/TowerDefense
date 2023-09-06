using System.Collections.Generic;
using System.Linq;

namespace Base.Core
{
    public static class RandomUtils
    {
        /// <summary>
        /// Returns a random index based on weighted probabilities.
        /// </summary>
        /// <param name="weights">A list of weights representing probabilities of each index.</param>
        /// <returns>The randomly selected index.</returns>
        /// <remarks>
        /// Given a list of weights, this method generates a random value and accumulates
        /// the weights until the random value falls within a specific range.
        /// The index corresponding to the range is returned, simulating a weighted random selection.
        /// </remarks>
        public static int GetRandomIndexUsingWeight(List<float> weights)
        {
            float weightSum = weights.Sum();
            float random = UnityEngine.Random.Range(0, weightSum);
            float currentWeight = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                currentWeight += weights[i];
                if (random < currentWeight)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}