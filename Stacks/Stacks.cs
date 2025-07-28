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
    
    // Expected time complexity = O(n)
    // Expected space complexity = O(n)
    public int EvalRPN(string[] tokens)
    {
        // Attempt 1
        // Actual time complexity = O(n) -> elements in input array only visited once, other methods like Pop(), Push() are all O(1)
        // Actual space complexity = O(n) -> stack can store n operands
        Stack<int> operands = new Stack<int>();
        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] != "+" && tokens[i] != "-" && tokens[i] != "*" && tokens[i] != "/")
            {
                operands.Push(int.Parse(tokens[i]));
                continue;
            }
            
            int rightOperand = operands.Pop();
            int leftOperand = operands.Pop();
            
            switch (tokens[i])
            {
                case "+":
                    operands.Push(leftOperand + rightOperand);
                    break;
                case "-":
                    operands.Push(leftOperand - rightOperand);
                    break;
                case "*":
                    operands.Push(leftOperand * rightOperand);
                    break;
                case "/":
                    operands.Push(leftOperand / rightOperand);
                    break;
            }
        }

        return operands.Pop();
    }
    
    // Expected time complexity = O(n)
    // Expected space complexity = O(n)
    public int[] DailyTemperatures(int[] temperatures)
    {
        // Attempt 1
        // Actual time complexity = O(n) -> elements in input array only visited once, other methods like Pop(), Push() are all O(1)
        // Actual space complexity = O(n) -> stack can store n key-value pairs
        int[] result = new int[temperatures.Length];
        Stack<KeyValuePair<int, int>> stack = new Stack<KeyValuePair<int, int>>();

        for (int i = temperatures.Length - 1; i >= 0; i--)
        {
            while (stack.Count != 0 && temperatures[i] >= stack.Peek().Key)
            {
                stack.Pop();
            }

            if (stack.Count == 0)
            {
                result[i] = 0;
            }
            else
            {
                result[i] = stack.Peek().Value - i;   
            }
            
            stack.Push(new KeyValuePair<int, int>(temperatures[i], i));
        }

        return result;
    }
}