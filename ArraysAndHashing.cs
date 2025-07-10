using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.XPath;

namespace Neetcode_Practice;

public class ArraysAndHashing
{
    public bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length) return false;

        Dictionary<char, int> occurrences1 = new Dictionary<char, int>();
        Dictionary<char, int> occurrences2 = new Dictionary<char, int>();

        foreach (var character in s)
        {
            if (!occurrences1.TryAdd(character, 1))
            {
                occurrences1[character]++;
            }
        }

        foreach (var character in t)
        {
            if (!occurrences2.TryAdd(character, 1))
            {
                occurrences2[character]++;
            }
        }

        foreach (var occurrence in occurrences1)
        {
            if (!occurrences2.TryGetValue(occurrence.Key, out int value) ||
                occurrences2[occurrence.Key] != occurrence.Value) return false;
        }

        return true;
    }

    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> indexOfNums = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            int difference = target - nums[i];
            if (indexOfNums.TryGetValue(difference, out int indexOfFirstNumber))
            {
                return new int[] { indexOfFirstNumber, i };
            }

            indexOfNums.Add(nums[i], i);
        }

        return Array.Empty<int>();
    }

    public List<List<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        foreach (string str in strs)
        {
            int[] indexOfAlphabets = new int[26];
            foreach (char character in str)
            {
                indexOfAlphabets[character - 'a']++;
            }

            string key = string.Join(",", indexOfAlphabets);

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new List<string>());
            }

            dictionary[key].Add(str);
        }

        return dictionary.Values.ToList();
    }

    public int[] TopKFrequent(int[] nums, int k)
    {
        Dictionary<int, int> occurrences = new Dictionary<int, int>();
        foreach (int num in nums)
        {
            if (!occurrences.ContainsKey(num))
            {
                occurrences.Add(num, 1);
                continue;
            }

            occurrences[num]++;
        }

        List<List<int>> buckets = new List<List<int>>();
        for (int i = 0; i < nums.Length; i++)
        {
            buckets.Add(new List<int>());
        }

        foreach (var occurrence in occurrences)
        {
            buckets[occurrence.Value - 1].Add(occurrence.Key);
        }

        List<int> result = new List<int>();
        for (int i = buckets.Count - 1; i >= 0; i--)
        {
            if (result.Count == k) break;
            foreach (int number in buckets[i])
            {
                result.Add(number);
            }
        }

        return result.ToArray();
    }

    public string Encode(IList<string> strs)
    {
        StringBuilder encoded = new StringBuilder();
        // string encodedString = "";
        foreach (string str in strs)
        {
            int strLength = str.Length;
            encoded.Append(strLength).Append("#").Append(str);
            // encodedString += strLength + "#" + str;
        }

        return encoded.ToString();
        // return encodedString;
    }

    public List<string> Decode(string s)
    {
        List<string> result = new List<string>();

        int indexOfCharacterLength = 0;
        int indexOfDelimiter = 0;
        while (true)
        {
            indexOfDelimiter = s.IndexOf("#", indexOfCharacterLength);
            if (indexOfDelimiter == -1) break;

            string stringContainingCharacterLength =
            s.Substring(indexOfCharacterLength, indexOfDelimiter - indexOfCharacterLength);
            // for (int i = startingIndexToIterate; i < indexOfDelimiter; i++)
            // {
            //     stringContainingCharacterLength += s[i];
            // }

            int characterLength = int.Parse(stringContainingCharacterLength);
            string decodedString = s.Substring(indexOfCharacterLength + stringContainingCharacterLength.Length + 1,
            characterLength);
            // for (int i = indexOfDelimiter + 1; i < indexOfCharacterLength + stringContainingCharacterLength.Length + 1 + characterLength; i++)
            // {
            //     decodedString += s[i];
            // }
            result.Add(decodedString);
            indexOfCharacterLength += stringContainingCharacterLength.Length + 1 + characterLength;
        }

        return result;
    }

    // Expected time complexity = O(n)
    // Expected space complexity = O(n)
    public int[] ProductExceptSelf(int[] nums)
    {
        /*
        1st attempt: -
        Actual time complexity = O(n*n) -> while loop, for loop
        Actual space complexity = O(n)

        int[] result = new int[nums.Length];
        int indexToNotIterateOn = 0;

        while (indexToNotIterateOn < nums.Length)
        {
            int productOfNums = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i == indexToNotIterateOn) continue;
                productOfNums *= nums[i];
            }

            result[indexToNotIterateOn] = productOfNums;
            indexToNotIterateOn++;
        }

        return result;
        */

        // Attempt 2: -
        // Actual time complexity = O(n)
        // Actual space complexity = O(n)
        // TODO: Can attempt 3 optimize space complexity to O(1) by just iterating through the output instead of having extra arrays for prefixes and suffixes?
        int[] output = new int[nums.Length];
        int[] productOfPrefixes = new int[nums.Length];
        int[] productOfSuffixes = new int[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            if (i == 0)
            {
                productOfPrefixes[i] = nums[i];
                continue;
            }

            productOfPrefixes[i] = nums[i] * productOfPrefixes[i - 1];
        }

        for (int i = nums.Length - 1; i >= 0; i--)
        {
            if (i == nums.Length - 1)
            {
                productOfSuffixes[i] = nums[i];
                continue;
            }

            productOfSuffixes[i] = nums[i] * productOfSuffixes[i + 1];
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (i == 0)
            {
                output[i] = productOfSuffixes[i + 1];
                continue;
            }

            if (i == nums.Length - 1)
            {
                output[i] = productOfPrefixes[i - 1];
                continue;
            }

            output[i] = productOfPrefixes[i - 1] * productOfSuffixes[i + 1];
        }

        return output;
    }

    // Expected time complexity = O(n*n)
    // Expected space complexity = O(n*n)
    public bool IsValidSudoku(char[][] board)
    {
        // Attempt 1
        // Actual time complexity = O(n*n) -> iterating through 9 x 9 grid
        // Actual space complexity = O(n*n) -> 9 x 9 space for data structures
        Dictionary<int, HashSet<char>> rows = new Dictionary<int, HashSet<char>>();
        Dictionary<int, HashSet<char>> columns = new Dictionary<int, HashSet<char>>();
        Dictionary<(int, int), HashSet<char>> grids = new Dictionary<(int, int), HashSet<char>>() // Key = (row of grid, column of grid)
        {
            {(0, 0), new HashSet<char>()},
            {(0, 1), new HashSet<char>()},
            {(0, 2), new HashSet<char>()},
            {(1, 0), new HashSet<char>()},
            {(1, 1), new HashSet<char>()},
            {(1, 2), new HashSet<char>()},
            {(2, 0), new HashSet<char>()},
            {(2, 1), new HashSet<char>()},
            {(2, 2), new HashSet<char>()}
        };
        for (int i = 0; i < 9; i++)
        {
            rows[i] = new HashSet<char>();
            columns[i] = new HashSet<char>();
        }

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                char boardElement = board[row][col];
                if (boardElement == '.') continue;
                
                if (rows[row].Contains(boardElement)) return false;
                rows[row].Add(boardElement);

                if (columns[col].Contains(boardElement)) return false;
                columns[col].Add(boardElement);

                int gridRowCoordinate = row / 3; // divided by 3 because there are 3 grids in a row
                int gridColumnCoordinate = col / 3; // divided by 3 because there are 3 grids in a column

                if (grids[(gridRowCoordinate, gridColumnCoordinate)].Contains(boardElement)) return false;
                grids[(gridRowCoordinate, gridColumnCoordinate)].Add(boardElement);
            }
        }
        
        return true;
    }
    
    // Expected time complexity = O(n)
    // Expected space complexity = O(n)
    public int LongestConsecutive(int[] nums)
    {
        // Attempt 1: 
        // Actual time complexity = worst case O(n log n) -> if values are not evenly distributed between buckets, best case O(n) -> if values are evenly distributed between buckets
        // Actual space complexity = O(n)
        // int numberOfInputElements = nums.Length;
        // List<List<int>> buckets = new List<List<int>>();
        // for (int i = 0; i < numberOfInputElements; i++) // number of buckets = number of input elements, best case scenario all elements of input[i] gets sorted into bucket of input[i]
        // {
        //     buckets.Add(new List<int>());
        // }
        //
        // int smallestNum = nums.Min();
        // int largestNum = nums.Max();
        // for (int i = 0; i < numberOfInputElements; i++)
        // {
        //     // bucketIndex = (num - min) * k / (max - min + 1); for scaling values down between buckets[0] and buckets[k - 1] for even distribution
        //     int bucketIndex = (nums[i] - smallestNum) * buckets.Count / (largestNum - smallestNum + 1);
        //     buckets[bucketIndex].Add(nums[i]);
        // }
        //
        // for (int i = 0; i < buckets.Count; i++)
        // {
        //     buckets[i].Sort(); // Smaller number of operations required compared to doing nums.Sort(), hence better time complexity
        // }
        //
        // List<int> sorted = new List<int>();
        // for (int i = 0; i < buckets.Count; i++)
        // {
        //     sorted.AddRange(buckets[i]);
        // }
        //
        // int longestConsecutiveLength = 1;
        // int currentConsecutiveLength = 1;
        // for (int i = 0; i < sorted.Count; i++)
        // {
        //     if (i == 0 || sorted[i] == sorted[i - 1]) continue;
        //     if (sorted[i] - 1 == sorted[i - 1])
        //     {
        //         currentConsecutiveLength++;
        //     }
        //     if (longestConsecutiveLength < currentConsecutiveLength)
        //     {
        //         longestConsecutiveLength = currentConsecutiveLength;
        //         continue;
        //     }
        //     currentConsecutiveLength = 1;
        // }
        //
        // return longestConsecutiveLength;
    }
}