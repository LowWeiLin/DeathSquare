/// Minimal generic min heap and priority queue implementation in C#, in less than 100 lines of code

using System;
using System.Collections;
using System.Collections.Generic;

class MinHeap<T> where T : IComparable<T>
{
	private List<T> array = new List<T> ();
	
	public void Add(T element)
	{
		array.Add(element);
		int c = array.Count - 1;
		int parent = (c - 1) >> 1;
		while (c > 0 && array[c].CompareTo(array[parent]) < 0)
		{
			T tmp = array[c];
			array[c] = array[parent];
			array[parent] = tmp;
			c = parent;
			parent = (c - 1) >> 1;
		}
	}
	
	public T RemoveMin()
	{
		if (Count == 0) {
			return default(T);
		}
		T ret = array[0];
		array[0] = array[array.Count - 1];
		array.RemoveAt(array.Count - 1);
		
		int c = 0;
		while (c < array.Count)
		{
			int min = c;
			if (2 * c + 1 < array.Count && array[2 * c + 1].CompareTo(array[min]) == -1)
				min = 2 * c + 1;
			if (2 * c + 2 < array.Count && array[2 * c + 2].CompareTo(array[min]) == -1)
				min = 2 * c + 2;
			
			if (min == c)
				break;
			else
			{
				T tmp = array[c];
				array[c] = array[min];
				array[min] = tmp;
				c = min;
			}
		}
		
		return ret;
	}
	
	public T Peek()
	{
		if (Count == 0) {
			return default(T);
		}
		return array[0];
	}
	
	public int Count
	{
		get
		{
			return array.Count;
		}
	}

	public void Clear() {
		array.Clear ();
	}

	public List<T> GetList() {
		return array;
	}
}

class PriorityQueue<T> : IEnumerable
{
	internal class PriorityQueueNode : IComparable<PriorityQueueNode>
	{
		public float Priority;
		public T O;
		public int CompareTo(PriorityQueueNode other)
		{
			return Priority.CompareTo(other.Priority);
		}
	}
	
	private MinHeap<PriorityQueueNode> minHeap = new MinHeap<PriorityQueueNode>();
	
	public void Add(float priority, T element)
	{
		minHeap.Add(new PriorityQueueNode() { Priority = priority, O = element });
	}
	
	public T RemoveMin()
	{
		if (Count == 0) {
			return default(T);
		}
		return minHeap.RemoveMin().O;
	}
	
	public T Peek()
	{
		return minHeap.Peek().O;
	}
	
	public int Count
	{
		get
		{
			return minHeap.Count;
		}
	}

	public void Clear() {
		minHeap.Clear ();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		List<T> list = new List<T> ();
		foreach (PriorityQueueNode n in minHeap.GetList()) {
			list.Add(n.O);
		}
		return (IEnumerator) list.GetEnumerator();
	}
}