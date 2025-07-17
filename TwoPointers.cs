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
    
    // Expected time complexity = O(n)
    // Expected space complexity = O(1)
    public int[] TwoSum(int[] numbers, int target)
    {
        // 1st Attempt
        // Actual time complexity = O(n*n) -> Brute force method that revisits elements to find the total that matches up to the target
        // Actual space complexity = O(1) -> Pointers and fixed output array
        // int p1 = 0; // Start pointer
        // int p2 = p1 + 1; // End pointer;
        // int[] result = new int[2];
        // while (p1 < numbers.Length)
        // {
        //     if (numbers[p1] + numbers[p2] == target)
        //     {
        //         result[0] = p1 + 1;
        //         result[1] = p2 + 1;
        //         return result;
        //     }
        //
        //     if (p2 < numbers.Length - 1) // Ensures max value of p2 is the last index of the input array
        //     {
        //         p2++;
        //         continue;
        //     }
        //     
        //     p1++;
        //     p2 = p1 + 1;
        // }
        //
        // return result;

        // 2nd Attempt
        // Actual time complexity = O(n) -> Each element in the input array is only visited once
        // Actual space complexity = O(1) -> Pointers and fixed size array
        int p1 = 0; // Start pointer
        int p2 = numbers.Length - 1; // End pointer
        int[] result = new int[2];
        while (true)
        {
            int total = numbers[p1] + numbers[p2];
            if (total == target)
            {
                result[0] = p1 + 1;
                result[1] = p2 + 1;
                return result;
            }

            if (total > target) // Largest number is too big, decrement end pointer to find the next largest number;
            {
                p2--;
                continue;
            }
            if (total < target) // Smallest number is too small, increment start pointer to find the next smallest number;
            {
                p1++;
            }
        }
    }
    
    // Expected time complexity = O(n*n)
    // Expected space complexity = O(1)
    public List<List<int>> ThreeSum(int[] nums)
    {
        // Attempt 1
        // Actual time complexity = O(n*n) -> revisiting numbers in the array for each increment of i
        // Actual space complexity = O(1) -> Sort(), a, p1, p2, b, c; output does not count
        List<List<int>> result = new List<List<int>>();
        Array.Sort(nums);
        for (int i = 0; i < nums.Length - 2; i++) // nums.Length - 2 because you will always need at least 2 numbers after the current index to make up 3 digits
        {
            int a = nums[i];
            if (i != 0 && a == nums[i - 1]) continue;

            int p1 = i + 1;
            int p2 = nums.Length - 1;
            while (p1 < p2)
            {
                int b = nums[p1];
                int c = nums[p2];
                
                if (b + c < -a || (p1 != i + 1 && b == nums[p1 - 1]))
                {
                    p1++;
                    continue;
                }
                if (b + c > -a || (p2 != nums.Length - 1 && c == nums[p2 + 1]))
                {
                    p2--;
                    continue;
                }
                if (b + c == -a) // a + b + c = 0, therefore b + c = -a
                {
                    result.Add(new List<int>() {a, b, c});
                    p1++;
                    p2--;
                }
            }
        }

        return result;
    }
    
    // Expected time complexity = O(n)
    // Expected space complexity = O(1)
    public int MaxArea(int[] heights)
    {
        // Attempt 1
        // Actual time complexity = O(n) -> Each element visited only once
        // Actual space complexity = O(1)
        int x1 = 0;
        int x2 = heights.Length - 1;

        int maxArea = 0;
        while (x1 < x2)
        {
            int[] height = new int[] { heights[x1], heights[x2] };
            int area = (x2 - x1) * height.Min();

            if (area > maxArea) maxArea = area;

            if (heights[x1] > heights[x2]) x2--;
            else x1++;
        }

        return maxArea;
    }
}