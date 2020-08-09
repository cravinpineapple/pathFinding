using System;
using System.IO;

namespace SPath
{
    class Program
    {
        static void Main(string[] args)
        {
            PathSol test = new PathSol();

            test.setStart(0, 0);
            test.setEnd(5, 5);

            test.printcMaze();
        }
    }
}
