# LinkedListSerialization
Saber Interactive Test

In this test I must create Serialize and Deserialize functions for ListNodes.
Start code is:
    
	class ListNode
	{   
        public ListNode Prev;
        public ListNode Next;
	    public ListNode Rand; // произвольный элемент внутри списка
	    public string Data;
	}


	class ListRand
	{
	    public ListNode Head;
	    public ListNode Tail;
	    public int Count;

	    public void Serialize(FileStream s)
	    {
	    }

	    public void Deserialize(FileStream s)
	    {
	    }
    }

Because of test rules I can't add fields in ListRand class and ListNode class. Also I can't use standart or third party serialization tools.

**Algoritm complexity should be less than O(n²).**
