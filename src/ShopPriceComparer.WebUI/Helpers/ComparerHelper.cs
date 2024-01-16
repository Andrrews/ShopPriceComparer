using ShopPriceComparer.WebUI.Models;namespace ShopPriceComparer.WebUI.Helpers{
    /// <summary>
    /// A helper class that provides methods for comparing products from two different shops based on their names. 
    /// It uses Jaccard and Jaro-Winkler similarity coefficients to determine the similarity between product names.
    /// </summary>
    /// <returns>
    /// This class does not return any value.
    /// </returns>
    public static class ComparerHelper    {

        /// <summary>
        /// Compares the products from two different shops and returns a list of matched products based on a similarity threshold.
        /// </summary>
        /// <param name="shop1Products">List of products from shop 1.</param>
        /// <param name="shop2Products">List of products from shop 2.</param>
        /// <param name="similarityThreshold">The threshold for product name similarity to consider them as a match.</param>
        /// <returns>
        /// Returns a list of matched products from both shops based on the similarity threshold.
        /// </returns>
        public static List<MatchedProduct> CompareProducts(List<ProductViewModel> shop1Products, List<ProductViewModel> shop2Products, double similarityThreshold)        {
            List<MatchedProduct> matchedProducts = new List<MatchedProduct>();

            Func<string, string> preprocessName = (name) =>
            {
                string[] wordsToRemove = { "smartfon" };
                string[] words = name.ToLower().Split(' ');

                return string.Join(" ", words.Where(w => !wordsToRemove.Contains(w)));
            };


            foreach (var product1 in shop1Products)            {                foreach (var product2 in shop2Products)                {                    double similarity = CalculateJaccardSimilarity(product1.Name, product2.Name);                    if (similarity >= similarityThreshold)                    {                        matchedProducts.Add(new MatchedProduct                        {                            Shop1Product = product1,                            Shop2Product = product2                        });

                        shop2Products.Remove(product2);                        break;                    }                }            }            return matchedProducts;        }


        /// <summary>
        /// Calculates the Jaccard similarity between two strings. The Jaccard similarity is the size of the intersection divided by the size of the union of the sample sets.
        /// </summary>
        /// <param name="s1">The first string to compare.</param>
        /// <param name="s2">The second string to compare.</param>
        /// <returns>
        /// A double value representing the Jaccard similarity between the two input strings. The value ranges from 0 to 1, where 1 means the strings are identical and 0 means they have no common elements.
        /// </returns>
        private static double CalculateJaccardSimilarity(string s1, string s2)        {            var s1Set = new HashSet<string>(s1.Split(' '));            var s2Set = new HashSet<string>(s2.Split(' '));            var intersection = new HashSet<string>(s1Set);            intersection.IntersectWith(s2Set);            var union = new HashSet<string>(s1Set);            union.UnionWith(s2Set);            return (double)intersection.Count / union.Count;        }


        /// <summary>
        /// Calculates the Jaro-Winkler similarity between two strings. The Jaro-Winkler similarity is a measure of similarity between two strings and is a variant of the Jaro distance metric, which is adjusted for common prefixes.
        /// </summary>
        /// <param name="s1">The first string to compare.</param>
        /// <param name="s2">The second string to compare.</param>
        /// <returns>
        /// A double representing the Jaro-Winkler similarity between the two input strings. The returned value is between 0 and 1, where 1 means the strings are identical and 0 means there's no similarity.
        /// </returns>
        private static double CalculateJaroWinklerSimilarity(string s1, string s2)
        {
            double jaroSimilarity = CalculateJaroSimilarity(s1, s2);

            int prefixLength = 0;
            int maxPrefixLength = 4;
            double prefixScale = 0.1;

            while (prefixLength < maxPrefixLength && s1[prefixLength] == s2[prefixLength])
            {
                prefixLength++;
            }

            return jaroSimilarity + (prefixLength * prefixScale * (1 - jaroSimilarity));
        }

        /// <summary>
        /// Calculates the Jaro similarity between two strings. The Jaro similarity is a measure of similarity between two strings and is defined as the mean of the proportion of matched characters to the length of the two strings.
        /// </summary>
        /// <param name="s1">The first string to compare.</param>
        /// <param name="s2">The second string to compare.</param>
        /// <returns>
        /// A double value representing the Jaro similarity between the two input strings. The value is between 0 (no similarity) and 1 (identical strings).
        /// </returns>
        private static double CalculateJaroSimilarity(string s1, string s2)
        {
            int matchDistance = Math.Max(s1.Length, s2.Length) / 2 - 1;
            bool[] s1Matches = new bool[s1.Length];
            bool[] s2Matches = new bool[s2.Length];

            int matches = 0;
            int transpositions = 0;

            for (int i = 0; i < s1.Length; i++)
            {
                int start = Math.Max(0, i - matchDistance);
                int end = Math.Min(i + matchDistance + 1, s2.Length);

                for (int j = start; j < end; j++)
                {
                    if (s2Matches[j] || s1[i] != s2[j]) continue;

                    s1Matches[i] = true;
                    s2Matches[j] = true;
                    matches++;
                    break;
                }
            }

            if (matches == 0) return 0;

            int k = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (!s1Matches[i]) continue;

                while (!s2Matches[k]) k++;

                if (s1[i] != s2[k]) transpositions++;

                k++;
            }

            return ((double)matches / s1.Length + (double)matches / s2.Length + (double)(matches - transpositions / 2) / matches) / 3;
        }
    }}