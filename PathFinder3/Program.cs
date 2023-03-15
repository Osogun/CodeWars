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
                t1 = "",
                t2 = "0",
                t3 = "00",
                t4 = "9999090\n" +
                   "9999999",
                t5="0212\n" +
                    "0111";

        Console.WriteLine(PathFinder(a));
        Console.WriteLine(PathFinder(b));
        Console.WriteLine(PathFinder(c));
        Console.WriteLine(PathFinder(d));
        Console.WriteLine(PathFinder(e));
        Console.WriteLine(PathFinder(f));
        Console.WriteLine(PathFinder(g));
    }

    public static int PathFinder(string maze)
    {
        // Convert map
        if (maze.Length == 0) return 0;
        string[] textMap = maze.Split("\n");
        int mapWidth = textMap[0].Length;
        int mapHeight = textMap.Length;
        List<Cell> cellMap = new List<Cell>();
        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                Cell node = new Cell(i, j, (int)Char.GetNumericValue(textMap[j][i]));
                if ((i == mapWidth - 1) && (j == mapHeight - 1)) node.isTargetCell = true;
                cellMap.Add(node);
            }
        }
        // Handle to the function that sorts the cells
        Comparison<Cell> cellComparison = new Comparison<Cell>(SortCells);        
        // A* Alghorithm  
        List<Cell> possiblePaths = new List<Cell>();
        List<Cell> foundedNodes = new List<Cell>();
        Cell currentCell = cellMap[0];
        possiblePaths.Add(currentCell);
        foundedNodes.Add(currentCell);
        while (currentCell.isTargetCell == false)
        {
            possiblePaths.Remove(currentCell);
            if(currentCell.Conncections.Count==0) currentCell.FoundConnections(cellMap, mapWidth, mapHeight);
            foreach(Cell node in currentCell.Conncections) {
                if(node.cost==0 || node.cost>currentCell.cost+Math.Abs(currentCell.height-node.height))
                {
                    node.cost=currentCell.cost+Math.Abs(currentCell.height-node.height);
                    if(!foundedNodes.Contains(node)){
                        possiblePaths.Add(node);
                        foundedNodes.Add(currentCell);
                    }
                }
            }
            possiblePaths.Sort(cellComparison);
            currentCell = possiblePaths.Last();
        }
        return currentCell.cost;
    }

    public static int SortCells(Cell a, Cell b)
    {
        if(a.cost<b.cost) return 1;
        else if (a.cost==b.cost) return 0;
        else return -1;
    }
}

public class Cell
{
    public int x;
    public int y;
    public int height;
    public bool isTargetCell = false;
    public int cost = 0;
    public List<Cell> Conncections = new List<Cell>();

    public Cell(int x, int y, int h)
    {
        this.x = x;
        this.y = y;
        this.height = h;
    }

    public static Cell? FoundCell(int x, int y, List<Cell> map)
    {
        foreach (Cell node in map) if (node.x == x && node.y == y) return node;
        return null;
    }

    public void FoundConnections(List<Cell> map, int mapWidth, int mapHeight)
    {
        if (this.x != 0) this.Conncections.Add(FoundCell(this.x - 1, this.y, map));
        if (this.x != mapWidth - 1) this.Conncections.Add(FoundCell(this.x + 1, this.y, map));
        if (this.y != 0) this.Conncections.Add(FoundCell(this.x, this.y - 1, map));
        if (this.y != mapHeight - 1) this.Conncections.Add(FoundCell(this.x, this.y + 1, map));
    }
}
