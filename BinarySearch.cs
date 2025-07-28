namespace Neetcode_Practice;

public class BinarySearch
{
    // Expected time complexity = O(log n)
    // Expected space complexity = O(1)
    public int Search(int[] nums, int target)
    {
        // Attempt 1
        // Actual time complexity = O(log n) -> binary search halves the time taken for each loop since the input array is halved
        // Actual space complexity = O(1) 
        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int value = nums[mid];

            if (value == target)
            {
                return mid;
            }
            
            if (value > target)
            {
                right = mid - 1;
                continue;
            }

            if (value < target)
            {
                left = mid + 1;
            }
        }

        return -1; // No index found with target value
    }
}