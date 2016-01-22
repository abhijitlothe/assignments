using System;
using System.Collections.Generic;
using System.Diagnostics;

public class ApplicationTest
{
	public static void Main(string[] args)
	{

		RunTests();
	}

	static void RunTests()
	{
		Test1();
		Test2();
		Test3();
		Test4();
		Test5();
		Test6();
		Test7();
		Test8();
		Test9();
	}

	//test a simple enque works
	static void Test1()
	{
		PriorityQueue<string> testQueue = new PriorityQueue<string>();
		testQueue.Enqueue("test1", 1);
		Assert(testQueue.GetSize() == 1, "Size test");	
	}

	//test DequeMax empty queue has no effect
	static void Test2()
	{
		PriorityQueue<string> testQueue = new PriorityQueue<string>();
		Assert(testQueue.GetSize() == 0, "Size test");
		Assert(testQueue.DequeueMax() == null, "DequeueMax empty queue");
	}

	//test DequeMin empty queue has no effect
	static void Test3()
	{
		PriorityQueue<string> testQueue = new PriorityQueue<string>();
		Assert(testQueue.GetSize() == 0, "Size test");
		Assert(testQueue.DequeueMin() == null, "DequeueMin empty queue");
	}

	//test Dequeue Min in order works
	static void Test4()
	{
		PriorityQueue<string> testQueue = new PriorityQueue<string>();

		testQueue.Enqueue("s3", 3);
		testQueue.Enqueue("s2", 2);
		testQueue.Enqueue("s1", 1);

		Assert(testQueue.DequeueMin().Equals("s1"), "min priority preserved");
		Assert(testQueue.DequeueMin().Equals("s2"), "min priority preserved");
		Assert(testQueue.DequeueMin().Equals("s3"), "min priority preserved");

		Assert(testQueue.GetSize() == 0, "queue is cleaned properly");
	}

	//test Dequeue Max in order works
	static void Test5()
	{
		PriorityQueue<string> testQueue = new PriorityQueue<string>();

		testQueue.Enqueue("s3", 3);
		testQueue.Enqueue("s2", 2);
		testQueue.Enqueue("s1", 1);

		Assert(testQueue.DequeueMax().Equals("s3"), "max priority preserved");
		Assert(testQueue.DequeueMax().Equals("s2"), "max priority preserved");
		Assert(testQueue.DequeueMax().Equals("s1"), "max priority preserved");

		Assert(testQueue.GetSize() == 0, "queue is cleaned properly");
	}

	//test Dequeue Min Max alternatively works
	static void Test6()
	{
		PriorityQueue<string> testQueue = new PriorityQueue<string>();

		testQueue.Enqueue("s3", 3);
		testQueue.Enqueue("s2", 2);
		testQueue.Enqueue("s1", 1);
		testQueue.Enqueue("s4", 4);

		Assert(testQueue.DequeueMax().Equals("s4"), "max priority preserved");
		Assert(testQueue.DequeueMin().Equals("s1"), "min priority preserved");
		Assert(testQueue.DequeueMax().Equals("s3"), "max priority preserved");
		Assert(testQueue.DequeueMin().Equals("s2"), "min priority preserved");

		Assert(testQueue.GetSize() == 0, "queue is cleaned properly");
	}

	//test dequeuemin on a Large array
	static void Test7()
	{
		PriorityQueue<int> testQueue = new PriorityQueue<int>();
		List<int> list = new List<int>();

		Random rand = new Random();
		for(int i = 0; i < 10000; ++i)
		{
			int next = rand.Next();
			testQueue.Enqueue(next, next);
			list.Add(next);
		}

		list.Sort();
		for(int i = 0; i < 10000; ++i)
		{
			int desired = list[i];
			int actual = testQueue.DequeueMin();
			Assert(actual == desired, "prirorities are right");
		}

		Assert(testQueue.GetSize() == 0, "queue is cleaned properly");
	}

	//test dequeueMax on a Large array
	static void Test8()
	{
		PriorityQueue<int> testQueue = new PriorityQueue<int>();
		List<int> list = new List<int>();

		Random rand = new Random();
		for(int i = 0; i < 10; ++i)
		{
			int next = rand.Next();
			testQueue.Enqueue(next, next);
			list.Add(next);
		}

		list.Sort((x, y) => y.CompareTo(x));
		for(int i = 0; i < 10; ++i)
		{
			int desired = list[i];
			int actual = testQueue.DequeueMax();
			Assert(actual == desired, "prirorities are right");
		}

		Assert(testQueue.GetSize() == 0, "queue is cleaned properly");
	}

	//test dequeue alternate min and Max on a Large array
	static void Test9()
	{
		PriorityQueue<int> testQueue = new PriorityQueue<int>();
		List<int> list = new List<int>();

		Random rand = new Random();
		for(int i = 0; i < 10000; ++i)
		{
			int next = rand.Next();
			testQueue.Enqueue(next, next);
			list.Add(next);
		}

		list.Sort();
		for(int i = 0, j = 10000-1; i <= j; ++i, --j)
		{
			int desired = list[i];
			int actual = testQueue.DequeueMin();
			Assert(actual == desired, "prirorities are right");
			if(j == i)
				break;
			desired = list[j];
			actual = testQueue.DequeueMax();
			Assert(actual == desired, "prirorities are right");
		}

		Assert(testQueue.GetSize() == 0, "queue is cleaned properly");
	}

	static void Assert(bool condition, string optionalMsg = "")
	{
		if(!condition)
			throw new Exception(string.Format("{0} failed!", optionalMsg) );
	}
}
