namespace Neetcode_Practice;

public class Stacks
{
    // Expected time complexity = O(n)
    // Expected space complexity = O(n)
    public bool IsValid(string s)
    {
        // Attempt 1
        // Actual time complexity = O(n) -> characters in input string only visited once
        // Actual space complexity = O(n) -> stack can store n number of open brackets
        Dictionary<char, char> validBrackets = new Dictionary<char, char>()
        {
            { ']', '[' },
            { '}', '{' },
            { ')', '(' }
        };
        
        Stack<char> openBrackets = new Stack<char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (validBrackets.ContainsValue(s[i]))
            {
                openBrackets.Push(s[i]);
                continue;
            }

            if (validBrackets.ContainsKey(s[i]))
            {
                if (openBrackets.Count == 0 || openBrackets.Pop() != validBrackets[s[i]])
                {
                    return false;
                }
            }
        }

        if (openBrackets.Count != 0)
        {
            return false;
        }

        return true;
    }
}