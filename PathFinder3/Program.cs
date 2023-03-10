using System;
using System.Collections.Generic;
using System.Linq;
public class Program
{
    public static void Main()
    {
        string a = "000\n" +
                   "000\n" +
                   "000",

               b = "010\n" +
                   "010\n" +
                   "010",

               c = "010\n" +
                   "101\n" +
                   "010",

               d = "0707\n" +
                   "7070\n" +
                   "0707\n" +
                   "7070",

               e = "700000\n" +
                   "077770\n" +
                   "077770\n" +
                   "077770\n" +
                   "077770\n" +
                   "000007",

               f = "777000\n" +
                   "007000\n" +
                   "007000\n" +
                   "007000\n" +
                   "007000\n" +
                   "007777",

               g = "000000\n" +
                   "000000\n" +
                   "000000\n" +
                   "000010\n" +
                   "000109\n" +
                   "001010",
                t1="",
                t2="0",
                t3="00",
                t4="9999090\n"+
                   "9999999";

        Console.WriteLine(Finder.PathFinder(a));

    }
}

public class Finder
{
    public static int PathFinder(string maze)
    {
        if(maze.Length==0) return 0;
        string[] stringMap = maze.Split("\n");
        int mapLengthX = stringMap[0].Length;
        int mapLengthY = stringMap.Length;
        RouteFinder rf = new RouteFinder();

        // Maping nodes
        int n = 0;
        for (int i = 0; i < mapLengthY; i++)
            for (int j = 0; j < mapLengthX; j++)
            {
                rf.node.Add(new Node(j, i, (int)Char.GetNumericValue(stringMap[i][j])));
                rf.cost.Add(0);
                rf.route.Add(null);
                rf.state.Add(State.Undefined);
                if (i == mapLengthY - 1 && j == mapLengthX - 1)
                    rf.node[n].isEndPoint = true;
                n++;
            }

        // Conecting nodes
        foreach (Node nd in rf.node)
        {
            nd.FoundConncections(rf.node, mapLengthX, mapLengthY);
        }


        // Dijkstra's algorithm
        Node currentNode = rf.node[0];
        while (currentNode.isEndPoint == false)
        {
            rf.CountRoutes(currentNode);
            currentNode = rf.CheapestNode();
        }

        return rf.cost[rf.node.IndexOf(currentNode)];
    }
}

public class Node
{
    public int x;
    public int y;
    public int altitude;
    public bool isEndPoint = false;
    public List<Connection> routes = new List<Connection>();

    public Node(int x, int y, int height)
    {
        this.x = x;
        this.y = y;
        altitude = height;
    }

    public static Node? FoundNode(int x, int y, List<Node> map)
    {
        foreach (Node nd in map)
        {
            if (nd.x == x && nd.y == y)
            {
                return nd;
            }
        }
        return null;
    }

    public void FoundConncections(List<Node> map, int mapLengthX, int mapLengthY)
    {
        if (x != 0)
            routes.Add(new Connection(this, FoundNode(x - 1, y, map)));
        if (x != mapLengthX - 1)
            routes.Add(new Connection(this, FoundNode(x + 1, y, map)));
        if (y != 0)
            routes.Add(new Connection(this, FoundNode(x, y - 1, map)));
        if (y != mapLengthY - 1)
            routes.Add(new Connection(this, FoundNode(x, y + 1, map)));
    }
}

public class Connection
{
    public Node startNode;
    public Node endNode;
    public int cost;

    public Connection(Node startNode, Node endNode)
    {
        this.startNode = startNode;
        this.endNode = endNode;
        cost = Math.Abs(startNode.altitude - endNode.altitude);
    }
}

public enum State
{
    Undefined = 0,
    Defined = 1,
    Counted = 2

}

public class RouteFinder
{
    public List<Node> node = new List<Node>();
    public List<int> cost = new List<int>();
    public List<Connection> route = new List<Connection>();
    public List<State> state = new List<State>();

    public void CountRoutes(Node currentNode)
    {
        int currentIndex = node.IndexOf(currentNode);
        foreach (Connection route in currentNode.routes)
        {
            Node nextNode = route.endNode;
            int nextIndex = node.IndexOf(nextNode);
            if (state[nextIndex] == 0 || cost[nextIndex] > cost[currentIndex] + route.cost)
            {
                cost[nextIndex] = cost[currentIndex] + route.cost;
                this.route[nextIndex] = route;
                state[nextIndex] = State.Defined;
            }
        }
        state[currentIndex] = State.Counted;
    }

    public Node? CheapestNode()
    {
        (Node? node, int? cost) cheapestNode = (null, null);
        foreach (Node nd in node)
        {
            int index = node.IndexOf(nd);
            if(cheapestNode==(null,null) && state[index]==State.Defined) cheapestNode=(nd, cost[index]);
            else if (cheapestNode!=(null, null) && state[index]==State.Defined){
                if(cost[index]<cheapestNode.cost) cheapestNode=(nd, cost[index]);
            }

        }
        return cheapestNode.node;
    }

}
