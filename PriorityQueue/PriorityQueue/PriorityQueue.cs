using System;
using System.Collections.Generic;
using System.Diagnostics;

class Heap<ValueType>
{
	class HeapNode
	{
		public ValueType Value;
		public int Priority;

		public HeapNode(ValueType value, int priority)
		{
			Value = value;
			Priority = priority;
		}

		public override string ToString ()
		{
			return Priority + ":" + Value.ToString();
		}

		public static void Swap(HeapNode e1, HeapNode e2)
		{
			HeapNode temp = new HeapNode(e1.Value, e1.Priority);
			e1.Value = e2.Value;
			e1.Priority = e2.Priority;
			e2.Value = temp.Value;
			e2.Priority = temp.Priority;
		}
	}

	private IList<HeapNode> _heap;

	public int Size 
	{
		get 
		{
			if(_heap != null)
				return _heap.Count;
			return 0;
		}
	}
	public Heap()
	{
		_heap = new List<HeapNode>();
	}

	public void Add(ValueType value, int priority)
	{
		HeapNode newElement = new HeapNode(value, priority);
		_heap.Add(newElement);
		BubbleUp(_heap, _heap.Count - 1);
	}

	void BubbleUp(IList<HeapNode> heap, int index)
	{
		int level =  (int) Math.Log(index + 1, 2);

		int parentIndex = (index - 1) /2;
		//if i am on the min level
		if(level % 2 == 0)
		{
			//if I have a parent, if my priority is higher than my parent's (which is a max level ) swap
			if(index > 0 && heap[index].Priority > heap[parentIndex].Priority)
			{
				HeapNode.Swap(heap[index], heap[parentIndex]);
				BubbleUpMax(heap, parentIndex);
			}
			else
			{
				BubbleUpMin(heap, index);
			}
		}
		else// if i am on the max level
		{
			//if I have a parent, if my priority is lower than my parent's (which is a min level) swap
			if(index > 0 && heap[index].Priority < heap[parentIndex].Priority)
			{
				HeapNode.Swap(heap[index], heap[parentIndex]);
				BubbleUpMin(heap, parentIndex);
			}
			else
			{
				BubbleUpMax(heap, index);
			}
		}
	}

	void BubbleUpMax(IList<HeapNode> heap, int index)
	{
		//if i have a grand parent
		if(index > 2)
		{
			int parentIndex = (index - 1) / 2;
			int grandParentIndex = (parentIndex - 1)/2;
			if(heap[index].Priority > heap[grandParentIndex].Priority)
			{
				HeapNode.Swap(heap[index], heap[grandParentIndex]);
				BubbleUpMax(heap, grandParentIndex);
			}
		}
	}


	void BubbleUpMin(IList<HeapNode> heap, int index)
	{
		//if i have a grand parent
		if(index > 2)
		{
			int parentIndex = (index - 1) / 2;
			int grandParentIndex = (parentIndex - 1)/2;
			if(heap[index].Priority < heap[grandParentIndex].Priority)
			{
				HeapNode.Swap(heap[index], heap[grandParentIndex]);
				BubbleUpMin(heap, grandParentIndex);
			}
		}
	}

	int GetIndexOfLargestPriorityElement(IList<HeapNode> heap, int index, int left, int right, int grandChildLeftLeft, int grandChildLeftRight, int grandChildRightLeft, int grandChildRightRight)
	{
		int maxIndex =  index;
		int maxPriority = heap[maxIndex].Priority;

		if(left < heap.Count && heap[left].Priority > maxPriority)
		{
			maxIndex = left;
			maxPriority = heap[maxIndex].Priority;
		}

		if(right < heap.Count && heap[right].Priority > maxPriority)
		{
			maxIndex = right;
			maxPriority = heap[maxIndex].Priority;
		}

		if(grandChildLeftLeft < heap.Count && heap[grandChildLeftLeft].Priority > maxPriority)
		{
			maxIndex = grandChildLeftLeft;
			maxPriority = heap[maxIndex].Priority;
		}

		if(grandChildLeftRight < heap.Count && heap[grandChildLeftRight].Priority > maxPriority)
		{
			maxIndex = grandChildLeftRight;
			maxPriority = heap[maxIndex].Priority;
		}

		if(grandChildRightLeft < heap.Count && heap[grandChildRightLeft].Priority > maxPriority)
		{
			maxIndex = grandChildRightLeft;
			maxPriority = heap[maxIndex].Priority;
		}

		if(grandChildRightRight < heap.Count && heap[grandChildRightRight].Priority > maxPriority)
		{
			maxIndex = grandChildRightRight;
			maxPriority = heap[maxIndex].Priority;
		}

		return maxIndex;
	}


	public ValueType DeleteMin()
	{
		if(_heap.Count == 0)
			return default(ValueType);
		
		//swap the last element to the top
		HeapNode.Swap(_heap[0], _heap[_heap.Count - 1]);
		ValueType v = _heap[_heap.Count -1].Value;
		_heap.RemoveAt(_heap.Count - 1);
		if(_heap.Count > 1)
			TrickleDownMin(_heap, 0);
		return v;
	}

	private void TrickleDownMin(IList<HeapNode> heap, int index)
	{
		int left = 2 * index + 1;
		int right = 2 * index + 2;

		int grandChildLeftLeft = 2 * left + 1;
		int grandChildLeftRight = 2 * left + 2;

		int grandChildRightLeft = 2 * right + 1;
		int grandChildRightRight = 2 * right + 2;

		//get index of the smallest element from children and grand children if they exist
		int n = GetIndexOfDesiredPriorityElement(heap, index, left, right, grandChildLeftLeft, grandChildLeftRight, grandChildRightLeft, grandChildRightRight, (x, y)=> x < y);

		if(n == grandChildLeftLeft || n == grandChildLeftRight || n == grandChildRightLeft || n == grandChildRightRight)
		{
			//move our element down
			HeapNode.Swap(heap[n], heap[index]);
			int parentOfN = (n-1)/2;
			if(heap[n].Priority > heap[parentOfN].Priority)
				HeapNode.Swap(heap[n], heap[parentOfN]);
			
			TrickleDownMin(heap, n);
		}
		else if(n == left  || n == right)
		{
			HeapNode.Swap(heap[n], heap[index]);
		}
	}


	int GetIndexOfDesiredPriorityElement(IList<HeapNode> heap, int index, int left, int right, int grandChildLeftLeft, int grandChildLeftRight, int grandChildRightLeft, int grandChildRightRight, Func<int, int, bool> comparator)
	{
		int minIndex = index;
		int minPriority = heap[minIndex].Priority;

		if(left < heap.Count && comparator(heap[left].Priority, minPriority))
		{
			minIndex = left;
			minPriority = heap[minIndex].Priority;
		}

		if(right < heap.Count && comparator(heap[right].Priority, minPriority))
		{
			minIndex = right;
			minPriority = heap[minIndex].Priority;
		}

		if(grandChildLeftLeft < heap.Count && comparator(heap[grandChildLeftLeft].Priority,  minPriority))
		{
			minIndex = grandChildLeftLeft;
			minPriority = heap[minIndex].Priority;
		}

		if(grandChildLeftRight < heap.Count && comparator( heap[grandChildLeftRight].Priority, minPriority))
		{
			minIndex = grandChildLeftRight;
			minPriority = heap[minIndex].Priority;
		}

		if(grandChildRightLeft < heap.Count && comparator(heap[grandChildRightLeft].Priority , minPriority))
		{
			minIndex = grandChildRightLeft;
			minPriority = heap[minIndex].Priority;
		}

		if(grandChildRightRight < heap.Count && comparator(heap[grandChildRightRight].Priority , minPriority))
		{
			minIndex = grandChildRightRight;
			minPriority = heap[minIndex].Priority;
		}

		return minIndex;
	}


	public ValueType DeleteMax()
	{
		if(_heap.Count == 0)
			return default(ValueType);

		int maxIndex = 0;
		if(_heap.Count > 1)
			maxIndex = 1;
		if(_heap.Count > 2 && _heap[2].Priority > _heap[maxIndex].Priority)
			maxIndex = 2;


		//swap the last element to the top
		HeapNode.Swap(_heap[maxIndex], _heap[_heap.Count - 1]);
		ValueType v = _heap[_heap.Count -1].Value;
		_heap.RemoveAt(_heap.Count - 1);

		if(maxIndex < _heap.Count)
			TrickleDownMax(_heap, maxIndex);
		return v;
	}

	private void TrickleDownMax(IList<HeapNode> heap, int index)
	{
		int left = 2 * index + 1;
		int right = 2 * index + 2;

		int grandChildLeftLeft = 2 * left + 1;
		int grandChildLeftRight = 2 * left + 2;

		int grandChildRightLeft = 2 * right + 1;
		int grandChildRightRight = 2 * right + 2;

		//get index of the smallest element from children and grand children if they exist
		int n = GetIndexOfDesiredPriorityElement(heap, index, left, right, grandChildLeftLeft, grandChildLeftRight, grandChildRightLeft, grandChildRightRight, (x, y) => x > y);

		if(n == grandChildLeftLeft || n == grandChildLeftRight || n == grandChildRightLeft || n == grandChildRightRight)
		{
			//move our element down
			HeapNode.Swap(heap[n], heap[index]);
			int parentOfN = (n - 1)/2;
			if(heap[n].Priority < heap[parentOfN].Priority)
				HeapNode.Swap(heap[n], heap[parentOfN]);
			
			TrickleDownMax(heap, n);
		}
		else if(n == left  || n == right)
		{
			HeapNode.Swap(heap[n], heap[index]);
		}
	}

	public void Dump()
	{
		Console.Write("Heap = [");
		foreach(HeapNode e in _heap)
		{
			Console.Write(" " + e.ToString());
		}
		Console.WriteLine(" ]");
	}
}

public class PriorityQueue<ValueType>
{
	Heap<ValueType> _heap = new Heap<ValueType>();
	public void Enqueue (ValueType value, int priority)
	{
		_heap.Add(value, priority);
	}

	public ValueType DequeueMin()
	{
		return _heap.DeleteMin();
	}

	public ValueType DequeueMax()
	{
		return _heap.DeleteMax();
	}

	public int GetSize()
	{
		return _heap.Size;
	}
	public void Print()
	{
		_heap.Dump();
	}
}

