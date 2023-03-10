using System;
using System.Collections.Generic;
namespace BirdMountain
{
    public class Cell
    {
        public int x;
        public int y;
        public int level;
        public bool flooded;
        public bool isLastX;
        public bool isLastY;
        public List<Cell> connectedCells = new List<Cell>();

        public Cell(int x, int y, int level)
        {
            this.x = x;
            this.y = y;
            this.level = level;
            isLastX = false;
            isLastY = false;
        }

        public static Cell? foundCell(List<Cell> map, int x, int y)
        {
            foreach (Cell cell in map)
            {
                if (cell.x == x && cell.y == y)
                    return cell;
            }
            return null;
        }

        public void foundConnectedCells(List<Cell> mapCells)
        {
            if (x != 0)
                connectedCells.Add(Cell.foundCell(mapCells, (x - 1), y));
            if (this.isLastX != true)
                connectedCells.Add(Cell.foundCell(mapCells, (x + 1), y));
            if (y != 0)
                connectedCells.Add(Cell.foundCell(mapCells, x, (y - 1)));
            if (this.isLastY != true)
                connectedCells.Add(Cell.foundCell(mapCells, x, (y + 1)));
        }

        public Cell? choseRandomDir(List<Cell> connectedCells)
        {
            List<Cell> anivableCells = new List<Cell>();
            foreach (Cell cell in connectedCells)
            {
                if (cell.level < this.level)
                    anivableCells.Add(cell);
            }
            if (anivableCells.Count > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(anivableCells.Count);
                return anivableCells[index];
            }
            else
                return null;
        }
    }
}
