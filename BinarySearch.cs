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
    
    // Expected time complexity = O(log m * n)
    // Expected space complexity = O(1)
    public bool SearchMatrix(int[][] matrix, int target)
    {
        // Actual time complexity = O(log m * n) -> Binary search on rows, then binary search on columns after the target row has been identified
        // Actual space complexity = O(1) -> All initialized fields are O(1)
        int top = 0;
        int bottom = matrix.Length - 1;
        int lastColumnIndex = matrix[0].Length - 1;

        int left = 0;
        int right = matrix[0].Length - 1;
        int rowContainingPossibleTargetValue = -1;

        // Binary search for row containing possible target value
        while (top <= bottom)
        {
            int row = (top + bottom) / 2;
            int firstRowValue = matrix[row][0];
            int lastRowValue = matrix[row][lastColumnIndex];

            if (firstRowValue == target || lastRowValue == target)
            {
                return true;
            }
            if (firstRowValue <= target && lastRowValue >= target)
            {
                rowContainingPossibleTargetValue = row;
                break;
            }
            if (firstRowValue > target)
            {
                bottom = row - 1;
                continue;
            }
            if (firstRowValue < target)
            {
                top = row + 1;
            }
        }

        // Prevents binary search on column if no possible row with target value is found
        if (rowContainingPossibleTargetValue == -1) return false;

        // Once the possible row is found, binary search on the columns of that row to find the target value
        while (left <= right)
        {
            int column = (left + right) / 2;
            int columnValue = matrix[rowContainingPossibleTargetValue][column];

            if (columnValue == target)
            {
                return true;
            }
            if (columnValue > target)
            {
                right = column - 1;
                continue;
            }

            if (columnValue < target)
            {
                left = column + 1;
            }
        }

        return false;
    }
    
    // Expected time complexity = O(log n)
    // Expected space complexity = O(1)
    public int FindMin(int[] nums)
    {
        // Attempt 1 
        // Actual time complexity = O(log n) -> Binary search
        // Actual space complexity = O(1) -> No extra space used
        if (nums.Length == 1 || nums[0] < nums[nums.Length - 1]) return nums[0];
        if (nums.Length == 2)
        {
            if (nums[0] > nums[1]) return nums[1];
            return nums[0];
        }

        int min = 0;
        int left = 0;
        int right = nums.Length - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (nums[mid] > nums[mid + 1])
            {
                min = nums[mid + 1];
                break;
            }
            if (nums[mid] < nums[mid - 1])
            {
                min = nums[mid];
                break;
            }

            if (nums[mid] < nums[left])
            {
                right = mid - 1;
                continue;
            }
            if (nums[mid] > nums[left])
            {
                left = mid + 1;
            }
        }

        return min;
    }
}