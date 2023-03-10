using System;
using System.Collections.Generic;
namespace BirdMountain
{
    public class Simulation
    {
        public static int[] DryGround(char[][] terrain)
        {
            int[] dryAreas = new int[4];

            // Implement the cell map
            List<Cell> map = new List<Cell>();
            List<Cell> river = new List<Cell>();
            mapCells(terrain, map, river);
            if (river.Count == 0 && map.Count == 0)
            {
                // If there is no map at all
                return new int[] { 0, 0, 0, 0 };
            }
            if (river.Count == 0)
            {
                // If there is no river
                return new int[] { map.Count, map.Count, map.Count, map.Count };
            }
            foreach (Cell c in map)
                c.foundConnectedCells(map);

            // Raise the mountains
            while (true)
            {
                int raisedPeaks = RaiseTheMountains(map);
                if (raisedPeaks == 0)
                    break;
            }

            // Trigger the flood
            for (int i = 0; i < 4; i++)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("------- Day " + i + " -------");
                DrawTheView(terrain, map, river);
                dryAreas[i] = map.Count - river.Count;
                TiggerTheFlood(map, river);
            }

            return dryAreas;
        }
        public static void mapCells(char[][] terrain, List<Cell> map, List<Cell> river)
        {
            for (int i = 0; i < terrain.Length; i++)
                for (int j = 0; j < terrain[i].Length; j++)
                {
                    int value;
                    if (terrain[i][j] == '^')
                        value = 1;
                    else
                        value = 0;
                    Cell cell = new Cell(j, i, value);
                    map.Add(cell);
                    if (terrain[i][j] == '-')
                        river.Add(cell);
                    if (j == terrain[i].Length - 1)
                        cell.isLastX = true;
                    if (i == terrain.Length - 1)
                        cell.isLastY = true;
                }
        }
        public static int RaiseTheMountains(List<Cell> map)
        {
            List<Cell> peaks = new List<Cell>();
            int raisedPeaks = 0;
            foreach (Cell cell in map)
            {
                if (cell.level > 0 && cell.connectedCells.Count == 4)
                {
                    bool b = true;
                    foreach (Cell neigh in cell.connectedCells)
                    {
                        if (neigh.level != cell.level)
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b == true)
                    {
                        peaks.Add(cell);
                        raisedPeaks++;
                    }
                }
            }
            foreach (Cell peak in peaks)
            {
                peak.level++;
            }
            return raisedPeaks;
        }
        public static void TiggerTheFlood(List<Cell> map, List<Cell> river)
        {
            Stack<Cell> floodedCells = new Stack<Cell>();
            Cell cell = river[0];
            cell.level++;
            floodedCells.Push(cell);
            while (true)
            {
                var dir = cell.choseRandomDir(cell.connectedCells);
                if (dir != null)
                {
                    cell = dir;
                    cell.level++;
                    floodedCells.Push(cell);
                    if (!(river.Contains(cell)))
                        river.Add(cell);
                }
                if (dir == null)
                {
                    cell = floodedCells.Pop();
                    if (floodedCells.Count == 0)
                        break;
                    cell = floodedCells.Peek();
                }
            }
        }
        public static void DrawTheMap(char[][] terrain, List<Cell> map, List<Cell> river)
        {
            int index = 0;
            for (int i = 0; i < terrain.Length; i++)
            {
                for (int j = 0; j < terrain[i].Length; j++)
                {
                    if (river.Contains(map[index]))
                        System.Console.Write("~");
                    else
                        System.Console.Write(map[index].level);
                    index++;
                }
                System.Console.WriteLine();
            }
        }

        public static void DrawTheView(char[][] terrain, List<Cell> map, List<Cell> river)
        {
            int index = 0;
            for (int i = 0; i < terrain.Length; i++)
            {
                for (int j = 0; j < terrain[i].Length; j++)
                {
                    if (river.Contains(map[index]))
                        System.Console.Write("~");
                    else if(map[index].level==0)
                        System.Console.Write(" ");
                    else
                        System.Console.Write("^");
                    index++;
                }
                System.Console.WriteLine();
            }
        }
    }
}
