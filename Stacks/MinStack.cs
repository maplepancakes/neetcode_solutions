namespace Neetcode_Practice;

// Expected time complexity = O(1)
// Expected space complexity = O(n)
public class MinStack
{
    // Attempt 1
    // Actual time complexity = O(1) -> all methods use only interact with the top most element of both stacks
    // Actual space complexity = O(n) -> n elements can be stored in stacks
    private Stack<int> _stack = new Stack<int>();
    private Stack<int> _minStack = new Stack<int>();
    
    public MinStack() {
        
    }

    public void Push(int val)
    {
        _stack.Push(val);
        if (_minStack.Count == 0 || val < _minStack.Peek())
        {
            _minStack.Push(val);
            return;
        }
        _minStack.Push(_minStack.Peek());
    }   
    
    public void Pop()
    {
        _stack.Pop();
        _minStack.Pop();
    }
    
    public int Top()
    {
        return _stack.Peek();
    }
    
    public int GetMin()
    {
        return _minStack.Peek();
    }
}