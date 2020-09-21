using System;
using System.Security.Cryptography.X509Certificates;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerUDLR w2 = new WorkerUDLR();
            w2.Start();

        }
        
    }
    public enum Input2
    {
        Up, Down, Left, Right
    }
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}
