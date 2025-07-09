using System.Globalization;
using System.Text;
using System.Xml.XPath;

namespace Neetcode_Practice;

public class Solutions
{
    public bool IsAnagram(string s, string t) {
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
            if (!occurrences2.TryGetValue(occurrence.Key, out int value) || occurrences2[occurrence.Key] != occurrence.Value) return false;
        }

        return true;
    }
    
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> indexOfNums = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            int difference = target -  nums[i];
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
        foreach(int num in nums)
        {
            if(!occurrences.ContainsKey(num))
            {
                occurrences.Add(num, 1);
                continue;
            }
            occurrences[num]++;
        }

        List<List<int>> buckets = new List<List<int>>();
        for(int i = 0; i < nums.Length; i++)
        {
            buckets.Add(new List<int>());
        }

        foreach(var occurrence in occurrences)
        {
            buckets[occurrence.Value - 1].Add(occurrence.Key);
        }

        List<int> result = new List<int>();
        for(int i = buckets.Count - 1; i >= 0; i--)
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

            string stringContainingCharacterLength = s.Substring(indexOfCharacterLength, indexOfDelimiter - indexOfCharacterLength);
            // for (int i = startingIndexToIterate; i < indexOfDelimiter; i++)
            // {
            //     stringContainingCharacterLength += s[i];
            // }

            int characterLength = int.Parse(stringContainingCharacterLength);
            string decodedString = s.Substring(indexOfCharacterLength + stringContainingCharacterLength.Length + 1, characterLength);
            // for (int i = indexOfDelimiter + 1; i < indexOfCharacterLength + stringContainingCharacterLength.Length + 1 + characterLength; i++)
            // {
            //     decodedString += s[i];
            // }
            result.Add(decodedString);
            indexOfCharacterLength += stringContainingCharacterLength.Length + 1 + characterLength;
        }
        
        return result;
    }
}