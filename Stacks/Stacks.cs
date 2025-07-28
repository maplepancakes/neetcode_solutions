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
    
    // Expected time complexity = O(n log n)
    // Expected space complexity = O(n)
    public int CarFleet(int target, int[] position, int[] speed)
    {
        // Attempt 1
        // Expected time complexity = O(n log n) -> Sorting of list
        // Expected space complexity = O(n) -> cars, fleets
        List<int[]> cars = new List<int[]>();
        for (int i = 0; i < position.Length; i++)
        {
            cars.Add(new int[] {position[i], speed[i]});
        }
        
        // O (n log n)
        // Sorted in ascending order so the position is representative of where the cars are in a straight line
        cars.Sort((a, b) => a[0].CompareTo(b[0])); 

        Stack<double> fleets = new Stack<double>();
        for (int i = cars.Count - 1; i >= 0; i--)
        {
            double time = (target - cars[i][0]) * 1.0d / cars[i][1]; // time = (target - position) / speed

            // If a car behind is faster than the car in front, the car behind will eventually catch up to the car in front and match its speed.
            // Therefore, the time taken to travel to the target destination for the car behind will always be the same as the car in front.
            // Hence, there is no need to add the time for the car behind.
            if (fleets.Count > 0 && time <= fleets.Peek())
            {
                continue;
            }
            fleets.Push(time);
        }

        return fleets.Count;
    }
}