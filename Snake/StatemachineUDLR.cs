using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class StateMachineUDLR
    {
        public int[,] Table = {
            {0,0,2,0 },
            { 0,2,2,2},
            { 3,1,3,3},
            {1,1,1,3 }
        };

        protected Direction _currentDirecton;

        public Direction CurretDirection
        {
            get { return _currentDirecton; }
            set { _currentDirecton = value; }
        }

        public Direction ChangeDirection(Input2 i)
        {
            _currentDirecton = (Direction) Enum.ToObject(typeof(Direction), Table[(int) i, (int) _currentDirecton]);
            //Console.WriteLine(_currentDirecton.ToString());
            return _currentDirecton;
        }

        //public Direction Output(Input i)
        //{
        //    ChangeDirection(i);
        //    return _currentDirecton;
        //}

        public Direction ResetDirection()
        {
            _currentDirecton = Direction.North;
            return _currentDirecton;
        }
    }


}
