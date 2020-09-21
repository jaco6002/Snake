using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Snake
{
    public class WorkerUDLR
    {
        private bool playing = true;
        //private int[,] playground;
        private int playgroundWidth = 20;
        private int playgroundHeight = 20;
        private List<(int,int)> TheSnake;
        private List<(int, int)> Food;
        private int foodinterval = 20;
        int FoodAdder;

        Random rnd = new Random();
        public void Start()
        {
            FoodAdder = foodinterval - 1;
            Console.CursorVisible = false;
            StateMachineUDLR sm = new StateMachineUDLR();
            //playground = new int[playgroundHeight, playgroundWidth];
            TheSnake = new List<(int, int)>{(playgroundWidth/2,playgroundHeight/2)};
            DrawPlayground();

            void GiveInput(string inp)
            {
                switch (inp)
                {
                    case "a":
                        sm.ChangeDirection(Input2.Left);
                        break;
                    case "w":
                        sm.ChangeDirection(Input2.Up);
                        break;
                    case "d":
                        sm.ChangeDirection(Input2.Right);
                        break;
                    case "s":
                        sm.ChangeDirection(Input2.Down);
                        break;
                    default:
                        break;
                }
            }

            reset:
            while (playing)
            {
                Task.Run(() =>
                {
                    if (Console.KeyAvailable)
                    {
                        GiveInput(Console.ReadKey(true).KeyChar.ToString());
                    }

                });
                //Console.WriteLine(sm.CurretDirection.ToString());
                if (FoodAdder==20)
                {
                    AddFood();
                    FoodAdder = 0;
                }
                Move();
                DrawFood();
                EatFood();
                DrawSnake();
                if (TheSnake[0].Item1==0|| TheSnake[0].Item1==playgroundWidth|| TheSnake[0].Item2==0|| TheSnake[0].Item2==playgroundHeight)
                {
                    ResetGame();
                    //ResetApples();
                    //Console.ForegroundColor = ConsoleColor.White;
                    //Console.SetCursorPosition(0,playgroundHeight+1);
                    Console.WriteLine("the snake is out of bounds");
                    playing = false;
                }



                FoodAdder++;
                Thread.Sleep(100);
            }

            while (!playing)
            {
                Console.SetCursorPosition(0, playgroundHeight+2);
                Console.WriteLine("Press the R key to restart");
                Console.WriteLine("Press the Q key to exit the app");
                var cmd = Console.ReadKey(true);
                if (cmd.KeyChar == 'r' || cmd.KeyChar == 'R')
                {
                    Console.Clear();
                    DrawPlayground();
                    TheSnake = new List<(int, int)>{ (playgroundWidth / 2, playgroundHeight / 2) };
                    sm.ResetDirection();
                    int FoodAdder = foodinterval-1;
                    playing = true;
                    goto reset;
                }
                if (cmd.KeyChar == 'q' || cmd.KeyChar == 'Q')
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("please press either R or Q");
                }
            }

            void ResetGame()
            {
                FoodAdder = 19;
                ResetApples();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, playgroundHeight + 1);
            }

            void EatFood()
            {
                if (Food!=null)
                {
                    if (Food.Contains(TheSnake[0]))
                    {
                        (int, int) currentspot = TheSnake[0];
                        Food.Remove(currentspot);
                        TheSnake.Add(TheSnake.Last());
                        
                    }
                }
            }

            void AddFood()
            {
                if (Food!=null)
                {
                    Food.Add((rnd.Next(1, playgroundWidth), rnd.Next(1, playgroundHeight)));
                }
                else
                {
                    Food = new List<(int, int)>{(rnd.Next(1, playgroundWidth), rnd.Next(1, playgroundHeight))};
                }
            }

            void DrawFood()
            {
                if (Food!=null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var i in Food)
                    {
                        Console.SetCursorPosition(i.Item1*2, i.Item2);
                        Console.Write("O");
                    }
                }
                
            }

            void Move()
            {
                switch (sm.CurretDirection)
                {
                    case Direction.North:

                        Console.ForegroundColor = ConsoleColor.White;
                        ClearLast();
                        TheSnake.Insert(0,(TheSnake[0].Item1, TheSnake[0].Item2 - 1));
                        TheSnake.Remove(TheSnake.Last());
                        //TheSnake[0].Item2 -= 1;
                        if (TheSnake.Contains((TheSnake[0].Item1, TheSnake[0].Item2 - 1)))
                        {
                            ResetGame();
                            //ResetApples();
                            //Console.SetCursorPosition(0, playgroundHeight + 1);
                            Console.WriteLine("the snake ate its tail");
                            playing = false;
                        }
                        break;

                    case Direction.East:
                        Console.ForegroundColor = ConsoleColor.White;
                        ClearLast();
                        TheSnake.Insert(0,(TheSnake[0].Item1 + 1, TheSnake[0].Item2));
                        TheSnake.Remove(TheSnake.Last());
                        //TheSnake[0].Item1 += 1;
                        if (TheSnake.Contains((TheSnake[0].Item1 + 1, TheSnake[0].Item2)))
                        {
                            ResetGame();
                            //ResetApples();
                            //Console.SetCursorPosition(0, playgroundHeight + 1);
                            Console.WriteLine("the snake ate its tail");
                            playing = false;
                        }
                        break;

                    case Direction.South: 
                        Console.ForegroundColor = ConsoleColor.White;
                        ClearLast();
                        TheSnake.Insert(0,(TheSnake[0].Item1, TheSnake[0].Item2 + 1));
                        TheSnake.Remove(TheSnake.Last());
                        //TheSnake[0].Item2 += 1;
                        if (TheSnake.Contains((TheSnake[0].Item1, TheSnake[0].Item2 + 1)))
                        {
                            ResetGame();
                            //ResetApples();
                            //Console.SetCursorPosition(0, playgroundHeight + 1);
                            Console.WriteLine("the snake ate its tail");
                            playing = false;
                        }
                        break;

                    case Direction.West:
                        Console.ForegroundColor = ConsoleColor.White;
                        ClearLast();
                        TheSnake.Insert(0,(TheSnake[0].Item1 - 1, TheSnake[0].Item2));
                        TheSnake.Remove(TheSnake.Last());
                        //TheSnake[0].Item1 -= 1;
                        if (TheSnake.Contains((TheSnake[0].Item1 - 1, TheSnake[0].Item2)))
                        {
                            ResetGame();
                            //ResetApples();
                            //Console.SetCursorPosition(0, playgroundHeight + 1);
                            Console.WriteLine("the snake ate its tail");
                            playing = false;
                        }
                        break;
                }
            }

            void ClearLast()
            {
                Console.SetCursorPosition(TheSnake.Last().Item1 * 2, TheSnake.Last().Item2);
                Console.Write(" ");
            }

            void DrawPlayground()
            {
                DrawBox(playgroundWidth,playgroundHeight);

            }

            void ResetApples()
            {
                foreach (var i in Food)
                {
                    Console.SetCursorPosition(i.Item1*2,i.Item2);
                    Console.Write(" ");
                }
                Food = new List<(int, int)>();
            }

            void DrawSnake()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                //foreach (var (item1, item2) in TheSnake)
                //{
                //    Console.SetCursorPosition(item1 * 2, item2);
                //    Console.Write("X");
                //}
                //ClearLast();

                Console.SetCursorPosition(TheSnake[0].Item1 * 2, TheSnake[0].Item2);
                Console.WriteLine("X");

            }

            void DrawBox(int width, int height)
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                int LastIndex = 0;
                //height -= 1;
                //width -= 1;
                Console.SetCursorPosition(0, 1);
                for (int h_i = 0; h_i <= height; h_i++)
                {
                    for (int w_i = 0; w_i <= width; w_i++)
                    {
                        if (h_i % height == 0 || w_i % width == 0)
                        {
                            Console.SetCursorPosition(w_i * 2,h_i);
                            Console.Write("X");
                        }
                    }
                }
            }
        }
        
    }
}