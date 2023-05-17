using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SaberTest
{
    class Tests
    {
        private const string FilePath = "C:\\Users\\NotForYou\\Desktop\\FileStream.txt";

        public void TestZeroNodesSerializeDeserialize()
        {
            var serializeListRand = new ListRand();
            var deserializeListRand = new ListRand();

            serializeListRand.Head = null;
            serializeListRand.Tail = null;
            serializeListRand.Count = 0;

            Console.WriteLine($"Expected result: Exception");

            Serialize(serializeListRand);
            Deserialize(deserializeListRand);

            CheckFields(deserializeListRand, serializeListRand, nameof(TestZeroNodesSerializeDeserialize));
        }

        public void TestOneNodeSerializeDeserialize()
        {
            var serializeListRand = new ListRand();
            var deserializeListRand = new ListRand();

            var node = new ListNode("First");

            node.Rand = node;

            serializeListRand.Head = node;
            serializeListRand.Tail = node;
            serializeListRand.Count = 1;

            PrintExceptedValues(node);

            Serialize(serializeListRand);
            Deserialize(deserializeListRand);

            CheckFields(deserializeListRand, serializeListRand, nameof(TestOneNodeSerializeDeserialize));

            CheckList(deserializeListRand.Head);
        }

        public void TestTwoNodesSerializeDeserialize()
        {
            var serializeListRand = new ListRand();
            var deserializeListRand = new ListRand();

            var node1 = new ListNode("First");
            var node2 = new ListNode("Second");

            node2.Prev = node1;
            node1.Next = node2;

            node1.Rand = null;
            node2.Rand = node1;

            serializeListRand.Head = node1;
            serializeListRand.Tail = node2;
            serializeListRand.Count = 2;

            PrintExceptedValues(node1);
            PrintExceptedValues(node2);

            Serialize(serializeListRand);
            Deserialize(deserializeListRand);

            CheckFields(deserializeListRand, serializeListRand, nameof(TestTwoNodesSerializeDeserialize));

            CheckList(deserializeListRand.Head);
        }

        public void TestThreeNodesSerializeDeserialize()
        {
            var serializeListRand = new ListRand();
            var deserializeListRand = new ListRand();

            var node1 = new ListNode("First");
            var node2 = new ListNode("Second");
            var node3 = new ListNode("Third");

            node2.Prev = node1;
            node3.Prev = node2;

            node1.Next = node2;
            node2.Next = node3;

            node1.Rand = null;
            node2.Rand = node2;
            node3.Rand = node1;

            serializeListRand.Head = node1;
            serializeListRand.Tail = node3;
            serializeListRand.Count = 3;

            PrintExceptedValues(node1);
            PrintExceptedValues(node2);
            PrintExceptedValues(node3);

            Serialize(serializeListRand);
            Deserialize(deserializeListRand);

            CheckFields(deserializeListRand, serializeListRand, nameof(TestThreeNodesSerializeDeserialize));

            CheckList(deserializeListRand.Head);
        }

        public void TestFourNodesSerializeDeserialize()
        {
            var serializeListRand = new ListRand();
            var deserializeListRand = new ListRand();

            var node1 = new ListNode("First");
            var node2 = new ListNode("Second");
            var node3 = new ListNode("Third");
            var node4 = new ListNode("Fourth");

            node2.Prev = node1;
            node3.Prev = node2;
            node4.Prev = node3;

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;

            node1.Rand = null;
            node2.Rand = node2;
            node3.Rand = node1;
            node3.Rand = node3;

            serializeListRand.Head = node1;
            serializeListRand.Tail = node4;
            serializeListRand.Count = 4;

            PrintExceptedValues(node1);
            PrintExceptedValues(node2);
            PrintExceptedValues(node3);
            PrintExceptedValues(node4);

            Serialize(serializeListRand);
            Deserialize(deserializeListRand);

            CheckFields(deserializeListRand, serializeListRand, nameof(TestFourNodesSerializeDeserialize));

            CheckList(deserializeListRand.Head);
        }

        private void PrintExceptedValues(ListNode node)
        {
            Console.WriteLine($"Expected result: " +
                              $"Data = {node.Data}, " +
                              $"Previous = {node.Prev?.Data}, " +
                              $"Next = {node.Next?.Data}, " +
                              $"Rand = {node.Rand?.Data}");
        }

        private void Serialize(ListRand listRand)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            using (var stream = File.OpenWrite(FilePath))
            {
                listRand.Serialize(stream);
            }
        }

        private void Deserialize(ListRand listRand)
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine($"{nameof(Tests)} {nameof(Deserialize)} File does not exists.");

                return;
            }

            using (var stream = File.OpenRead(FilePath))
            {
                listRand.Deserialize(stream);
            }
        }

        /// <summary>
        /// Checking equality of Head.Data and Tail.Data (not reference check because of serialization)
        /// </summary>
        private void CheckFields(ListRand deserializeListRand, ListRand serializeListRand, string methodName)
        {
            if (deserializeListRand.Count != serializeListRand.Count)
            {
                Console.WriteLine($"{nameof(Tests)} – {methodName} – WrongCount");
            }

            if (deserializeListRand.Head.Data != serializeListRand.Head.Data)
            {
                Console.WriteLine($"{nameof(Tests)} – {methodName} – Wrong Head");
            }

            if (deserializeListRand.Tail.Data != serializeListRand.Tail.Data)
            {
                Console.WriteLine($"{nameof(Tests)} – {methodName} – Wrong Tail");
            }
        }

        private void CheckList(ListNode startNode)
        {
            while (startNode != null)
            {
                Console.WriteLine($"Data = {startNode.Data}, " +
                                  $"Previous = {startNode.Prev?.Data}, " +
                                  $"Next = {startNode.Next?.Data}, " +
                                  $"Rand = {startNode.Rand?.Data}");
                startNode = startNode.Next;
            }
        }
    }

    class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand;

        public string Data;

        public ListNode()
        {
        }

        public ListNode(string data)
        {
            Data = data;
        }
    }

    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;

        public int Count;

        public void Serialize(FileStream s)
        {
            if (Count == 0)
                return;

            var startBuilder = new StringBuilder();

            if (Count == 1)
            {
                startBuilder.Append(GetCurrentItemBuilder(Head, 0, GetItemIndex(Head.Rand, 0, 0)));
            }
            else
            {
                var endBuilder = new StringBuilder();

                int halfCount = (int) Math.Round(Count / 2d, MidpointRounding.ToZero);

                var nodesIndexes = new Dictionary<ListNode, string>(Count);

                AddRandomIndexes(halfCount, nodesIndexes);

                SerializeItemsToString(halfCount, nodesIndexes, startBuilder, endBuilder);

                startBuilder.Append(endBuilder);
            }

            var buffer = Encoding.UTF8.GetBytes(startBuilder.ToString().TrimEnd(','));

            s.Write(buffer);
        }

        private void AddRandomIndexes(int halfCount, Dictionary<ListNode, string> nodesIndexes)
        {
            var leftCurrentItem = Head;
            var rightCurrentItem = Tail;

            for (int i = 0; i < halfCount; i++)
            {
                nodesIndexes.Add(leftCurrentItem, i.ToString());
                nodesIndexes.Add(rightCurrentItem, (Count - 1 - i).ToString());

                leftCurrentItem = leftCurrentItem.Next;
                rightCurrentItem = rightCurrentItem.Prev;
            }

            if (Count % 2 > 0)
            {
                nodesIndexes.Add(leftCurrentItem, halfCount.ToString());
            }
        }

        private void SerializeItemsToString(int halfCount, Dictionary<ListNode, string> nodesIndexes,
            StringBuilder startBuilder,
            StringBuilder endBuilder)
        {
            var leftCurrentItem = Head;
            var rightCurrentItem = Tail;

            for (int i = 0; i < halfCount; i++)
            {
                var leftRandIndex = leftCurrentItem.Rand == null
                    ? NodeConstants.NullStringPattern
                    : nodesIndexes[leftCurrentItem.Rand];

                var rightRandIndex = rightCurrentItem.Rand == null
                    ? NodeConstants.NullStringPattern
                    : nodesIndexes[rightCurrentItem.Rand];

                // note: we can reduce Dictionary calls if we use check at first and last elements like:
                // if(currentItem.Rand == Head) => its index == 0
                // if(currentItem.Rand == Tail) => its index == Count

                startBuilder.Append(GetCurrentItemBuilder(leftCurrentItem, i, leftRandIndex));
                endBuilder.Insert(0,
                    GetCurrentItemBuilder(rightCurrentItem, Count - 1 - i, rightRandIndex));

                leftCurrentItem = leftCurrentItem.Next;
                rightCurrentItem = rightCurrentItem.Prev;
            }

            if (Count % 2 > 0)
            {
                startBuilder.Append(GetCurrentItemBuilder(leftCurrentItem, halfCount,
                    nodesIndexes[leftCurrentItem.Rand]));
            }
        }

        public void Deserialize(FileStream s)
        {
            var buffer = new byte[s.Length];

            s.Read(buffer, 0, buffer.Length);

            var data = Encoding.UTF8.GetString(buffer);

            if (data == string.Empty)
            {
                throw new Exception($"{nameof(ListRand)} – {nameof(Deserialize)} – Data is empty.");
            }

            DeserializeNodes(data);
        }

        private StringBuilder GetCurrentItemBuilder(ListNode currentItem, int currentIndex, string randIndex)
        {
            var previousItemIndex = GetItemIndex(currentItem.Prev, currentIndex, -1);
            var nextItemIndex = GetItemIndex(currentItem.Next, currentIndex, 1);

            return GetNodeJson(currentItem.Data, previousItemIndex, nextItemIndex, randIndex);
        }

        private string GetItemIndex(ListNode node, int currentIndex, int indexTerm)
        {
            return node == null ? NodeConstants.NullStringPattern : (currentIndex + indexTerm).ToString();
        }

        private StringBuilder GetNodeJson(string data, string prevIndex, string nextIndex, string randIndex)
        {
            var result = new StringBuilder();

            result.Append($"{data},");
            result.Append($"{prevIndex},");
            result.Append($"{nextIndex},");
            result.Append($"{randIndex},");

            return result;
        }

        private void DeserializeNodes(string data)
        {
            var stringBuffer = data.Split(',');

            var fieldCounter = 0;

            var nodesArray = new ListNode[stringBuffer.Length / NodeConstants.NodeFieldsCount];

            for (int i = 0; i < nodesArray.Length; i++)
            {
                nodesArray[i] = new ListNode();
            }

            for (var j = 0; j < nodesArray.Length; j++)
            {
                var node = nodesArray[j];

                var startIndex = j * NodeConstants.NodeFieldsCount;

                node.Data = stringBuffer[startIndex];
                node.Prev = GetNodeOrDefault(stringBuffer[startIndex + 1], nodesArray);
                node.Next = GetNodeOrDefault(stringBuffer[startIndex + 2], nodesArray);
                node.Rand = GetNodeOrDefault(stringBuffer[startIndex + 3], nodesArray);

                if (++fieldCounter >= NodeConstants.NodeFieldsCount)
                {
                    fieldCounter = 0;
                }
            }

            Count = nodesArray.Length;
            Head = nodesArray.First();
            Tail = nodesArray.Last();
        }

        private ListNode GetNodeOrDefault(string item, ListNode[] nodesArray)
        {
            return item == NodeConstants.NullStringPattern
                ? null
                : nodesArray[TryParseIndex(item)];
        }

        private int TryParseIndex(string value)
        {
            if (!int.TryParse(value, out int result))
            {
                throw new Exception($"{nameof(ListRand)} {nameof(TryParseIndex)} Parsing index error.");
            }

            return result;
        }
    }

    public static class NodeConstants
    {
        public const int NodeFieldsCount = 4;
        public const string NullStringPattern = "null";
    }
}