namespace Day12_HillClimbingAlgorithm;

public class MinHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public MinHeap()
    {
        heap = new List<T>();
    }

    public int Count => heap.Count;

    public void Add(T item)
    {
        heap.Add(item);
        HeapifyUp(heap.Count - 1);
    }

    public T ExtractMin()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        T min = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);

        return min;
    }

    private void HeapifyUp(int index)
    {
        int parentIndex = (index - 1) / 2;

        while (index > 0 && heap[index].CompareTo(heap[parentIndex]) < 0)
        {
            Swap(index, parentIndex);
            index = parentIndex;
            parentIndex = (index - 1) / 2;
        }
    }

    private void HeapifyDown(int index)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;
        int smallestIndex = index;

        if (leftChildIndex < heap.Count && heap[leftChildIndex].CompareTo(heap[smallestIndex]) < 0)
            smallestIndex = leftChildIndex;

        if (rightChildIndex < heap.Count && heap[rightChildIndex].CompareTo(heap[smallestIndex]) < 0)
            smallestIndex = rightChildIndex;

        if (smallestIndex != index)
        {
            Swap(index, smallestIndex);
            HeapifyDown(smallestIndex);
        }
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}

