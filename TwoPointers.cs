namespace Neetcode_Practice;

public class TwoPointers
{
    // Expected time complexity = O(n)
    // Expected space complexity = O(1)
    public bool IsPalindrome(string s)
    {
        // 1st Attempt
        // Actual time complexity = O(n) -> All characters in input string only visited once
        // Actual space complexity = O(1) -> Using char fields take up constant space
        int p1 = 0; // Start pointer
        int p2 = s.Length - 1; // End pointer
        while (p1 < p2)
        {
            char startChar = char.ToLower(s[p1]);
            char endChar = char.ToLower(s[p2]);
            
            int startAsciiValue = startChar - '0';
            int endAsciiValue = endChar - '0';
            
            // 0 - 9, 49 - 74 (encompasses ASCII values of 0 = 0 to 9 = 9, a = 49 to z = 74; A - Z not required since the characters have been lowercased)
            // Used to skip over any characters that are not considered alphanumeric
            if (startAsciiValue < 0 || startAsciiValue > 9 && startAsciiValue < 49 || startAsciiValue > 74)
            {
                p1++;
                continue;
            }
            if (endAsciiValue < 0 || endAsciiValue > 9 && endAsciiValue < 49 || endAsciiValue > 74)
            {
                p2--;
                continue;
            }

            if (startChar != endChar) return false;
            p1++;
            p2--;
        }

        return true;
    }
}