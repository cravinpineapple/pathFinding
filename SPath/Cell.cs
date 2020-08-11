using System;
using System.Collections.Generic;
using System.Text;

namespace SPath
{
    class Cell
    {
        public bool isWall;
        public bool isEnd;
        public bool isStart;
        public bool isOpen;
        public bool visited;
        public bool pathFound;
        public int prevCell_i, prevCell_j;
        public int i, j;

        public Cell()
        {
            isWall = isEnd = isStart = isOpen = false;
            visited = pathFound = false;
            prevCell_i = prevCell_j = i = j = 0;
        }
    }
}
