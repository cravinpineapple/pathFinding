using SPath;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SPath
{
    class PathSol
    {
        char[,] maze;
        Cell[,] cMaze;
        int bi, bj; // Beginning indices


        // incorporate properties *beginning and end indices*
        /*
        public int Bi
        {
            get => bi;
            set => bi = value;   
        }
        */

        int ci, cj; // Current indices
        int ei, ej; // End indices
        const int defHeight = 6, defWidth = 6;
        int height, width;
        bool reached_end = false;
        int pathSize;
        bool is_end = false;


        // constructor
        public PathSol()
        {
            maze = new char[defHeight, defWidth] {
                {'.', '.', '.', '.', '.', '.'},
		        { '#', '.', '#', '#', '#', '.'},
		        { '.', '.', '#', '.', '#', '.'},
		        { '.', '.', '.', '.', '.', '.'},
		        { '.', '#', '#', '#', '#', '.'},
		        { '.', '#', '.', '.', '.', '.'},
	            };

            cMaze = new Cell[defHeight, defWidth];
            populateCellMaze();
        }

        // Lets user decide grid size
        PathSol(int height, int width)
        {

        }

        // prints raw maze characters
        public void printRawMaze()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    Console.Write(maze[i, j]);
                }
                
                Console.WriteLine();
            }
        }

        // prints formatted maze characters
        public void printMaze()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    Console.Write(maze[i, j]);
                    Console.Write("   ");
                }

                Console.WriteLine();
            }
        }

        // prints maze according to cell's data
        public void printcMaze()
        {
            equalizeGrids();

            for (int i = 0; i < cMaze.GetLength(0); i++)
            {
                for (int j = 0; j < cMaze.GetLength(1); j++)
                {
                    if (cMaze[i, j].visited)
                    { 
                        Console.Write("V");
                        Console.Write("   ");
                        continue;
                    }

                    if (cMaze[i, j].isStart)
                        Console.Write("S");
                    else if (cMaze[i, j].isWall)
                        Console.Write("#");
                    else if (cMaze[i, j].isOpen)
                        Console.Write(".");
                    else if (cMaze[i, j].isEnd)
                        Console.Write("E");

                    Console.Write("   ");
                }

                Console.WriteLine();
            }
        }

        // fills cell maze (avoids filled with null)
        void populateCellMaze()
        {
            for (int i = 0; i < cMaze.GetLength(0); i++)
            {
                for (int j = 0; j < cMaze.GetLength(1); j++)
                {
                    cMaze[i, j] = new Cell();
                }
            }
        }

        // sets cMaze equal to maze properties
        void equalizeGrids()
        {
            for (int i = 0; i < cMaze.GetLength(0); i++)
            {
                for (int j = 0; j < cMaze.GetLength(1); j++)
                {
                    cMaze[i, j].i = i;
                    cMaze[i, j].j = j;

                    switch (maze[i, j])
                    {

                        case 'S':
                            cMaze[i, j].isStart = true;
                            break;
                        case '#':
                            cMaze[i, j].isWall = true;
                            break;
                        case 'E':
                            cMaze[i, j].isEnd = true;
                            break;
                        case '.':
                            cMaze[i, j].isOpen = true;
                            break;
                    }
                }
            }
        }

        public void setStart(int bi, int bj)
        {
            this.bi = bi;
            this.bj = bj;

            maze[bi, bj] = 'S';
        }

        public void setEnd(int ei, int ej)
        {
            this.ei = ei;
            this.ej = ej;

            maze[ei, ej] = 'E';
        }

        // checks if coords are within bounds and the cell is not a wall.
        bool isValidCell(int i, int j)
        {
            //return (i < defHeight && i >= 0) && (j < defWidth && j >= 0) ? true : false;

            // checks coords
            if ((i < defHeight && i >= 0) && (j < defWidth && j >= 0))
            {
                // checks if wall
                if (cMaze[i, j].isWall)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        bool enqueueDirection(List<Cell> cellQ, int ci, int cj, int[] prevCords)
        {
            return false;
        }

        public void solve()
        {
            //List ensures we will branch from each cell
            List<Cell> cellL = new List<Cell>();

            // tracks next cell we need to branch from
            int pos = 0;

            cellL.Add(cMaze[bi, bj]);
            //int[] prevCoords = new int[2] { bi, bj };

            bool isEnd = false;

            while (!isEnd)
            {
                Cell currentCell = cellL[pos];
                // branch up
                isEnd = branch(currentCell, currentCell.i - 1, currentCell.j, ref cellL);

                // branch down
                isEnd = branch(currentCell, currentCell.i + 1, currentCell.j, ref cellL);

                // branch left
                isEnd = branch(currentCell, currentCell.i, currentCell.j - 1, ref cellL);

                // branch right
                isEnd = branch(currentCell, currentCell.i, currentCell.j + 1, ref cellL);

                pos++;
            }
        }

        bool branch(Cell currentCell, int nextI, int nextJ, ref List<Cell> cellL)
        {
            if (isValidCell(nextI, nextJ))
            {
                // pushing next cell to branch from in queue
                cellL.Add(cMaze[nextI, nextJ]);

                // updates visited and remembers the previous cell it branched from
                cMaze[nextI, nextJ].visited = true;
                cMaze[nextI, nextJ].prevCell_i = currentCell.i;
                cMaze[nextI, nextJ].prevCell_j = currentCell.j;

                // check if end is found
                if (cMaze[nextI, nextJ].isEnd)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
