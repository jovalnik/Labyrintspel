using System;
using System.Collections.Generic;

//using System.Collections.Generic;
//using System.Collections.Generic;
//using System.Collections.Generic;
//using System.Collections.Generic;
//using System.Collections.Generic;
//using System.Collections.Generic;
//using System.Collections.Generic;

namespace TheGame001
{
    class Program
    {
        static int Main(string[] args)
        {

            //ConsoleHelper.SetCurrentFont("Consolas", 10);
            ConsoleHelper.SetCurrentFont("Terminal", 8);                //Ville ha 8x8
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            while (true)
            {
                Console.Write("Mata in ditt namn: ");
                string input = Console.ReadLine();
                Player player = new Player(input, '0');
                Console.WriteLine(" ♦† þð¤¶µ†○");
                Console.WriteLine(" Välkommen, " + input);
                Console.WriteLine(" Framför dig ser du ett hål i golvet med en brandstång     ");
                Console.WriteLine(" samt en skylt med påskriften \"Minotaurens Labyrint => \"  ");
                Console.WriteLine("   ");
                Console.WriteLine(" Om du hoppar på brandstången finns uppenbarligen ingen återvändo.  ");
                Console.WriteLine("   ");
                while (player.IsAlive)
                {
                    int answer = 0;
                    do
                    {
                        Console.WriteLine("Välj:   ");
                        Console.WriteLine("   ");
                        Console.WriteLine("1) Hoppa på brandstången och kliv in i Minotaurens labyrint   ");
                        Console.WriteLine("2) Visa info om dig själv.  ");
                        Console.WriteLine("3) Hjälp, detta är för kusligt - avsluta!   ");
                        Console.WriteLine("   ");
                        int.TryParse(Console.ReadLine(), out answer);
                    } while (answer < 1 || answer > 3);

                    switch (answer)
                    {
                        case 1:
                            Game myNewGame = new Game();
                            myNewGame.NewGame(player);
                            break;
                        case 2:
                            player.Display();
                            break;
                        case 3:
                            return 0;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public class TestoTaur : Monster
    {
        private Stack<(int, int)> Path = new Stack<(int, int)>();
        //private bool[,] visited = new bool[]

        public void Move ( Player player, Game myGame)
        {
            //int y = myGame.Labyrint.GetLength(0);
            //int x = myGame.Labyrint.GetLength(1);
            bool[,] visited = new bool[myGame.Labyrint.GetLength(0), myGame.Labyrint.GetLength(1)];
            //bool[,] visited = new bool[y,x];
            (int, int) myPos;
            myPos.Item1 = position[0];
            myPos.Item2 = position[1];
            Path.Push(myPos);
            foreach (var item in Path)
            {
                visited[item.Item1, item.Item2] = true;
            }


            (int, int) playerPos ;
            playerPos.Item1 = player.Position[0];
            playerPos.Item2 = player.Position[1];

        }

        public TestoTaur (string myName, char sign) : base ( myName,  sign)
        {

        }

    }




}





