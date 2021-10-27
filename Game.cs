using System;
using System.Collections.Generic;


namespace TheGame001
{
    public class Game
    {
        public char[,] Labyrint { get; set; }//  

        //public List<(string,int)> Monsters { get; set; }
        public List<Monster> Monsters { get; set; } = new List<Monster>();
        public void NewGame(Player player)              // Kanske inte player här heller
        {                                               // fast kanske ändå..?,
            Monsters.Add(new Monster("minitaur", 'µ'));              
            Monsters.Add(new Monster("minitaur", 'µ'));
            PlaceMonster(player, 5, 10, 5, 10, player);
            foreach (var item in Monsters)
            {
                PlaceMonster(item, 5, Labyrint.GetLength(0), 5, Labyrint.GetLength(1), player);
            }
            while (player.IsAlive)
            {
                Console.Clear();
                Console.WriteLine("þð¤¶µ†○");
                for (int i = 0; i < Labyrint.GetLength(0); i++)
                {
                    for (int j = 0; j < Labyrint.GetLength(1); j++)
                    {
                        //foreach (var item in Monsters)
                        //{
                        //    if (i == item.Position[0] && j == item.Position[1])
                        //    {
                        //        Console.Write(item.Symbol);
                        //    }
                         

                        //}

                        if (i == player.Position[0] && j == player.Position[1])
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write(player.Symbol);
                        }
 
                        else if (!CheckForMonsters(i,j,player) )
                        {
                            if (Labyrint[i, j] == ' '|| Labyrint[i,j]=='o')
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Write(Labyrint[i, j]);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.Write(Labyrint[i, j]);
                            }
                        }

                        if (j == Labyrint.GetLength(1) - 1) Console.WriteLine("");
                    }

                }
                List<Monster> tmpList = new List<Monster>();
                foreach (var beast in Monsters)                         // man slipper tydligen den här sortens
                {                                                       // krångel med linq, men...
                    if (!beast.IsAlive) tmpList.Add(beast);
                }
                foreach (var creature in tmpList)
                {
                    Monsters.Remove(creature);
                }

                foreach (var critter in Monsters)
                {
                    //if (item2.Position[0] == player.Position[0] - 1 || item2.Position[1] == player.Position[1] - 1 || item2.Position[0] == player.Position[0] + 1 || item2.Position[1] == player.Position[1] + 1)
                    if (GetDistance(player,critter) <= 1)
                    {
                        if (critter.IsAlive) FightOrTrade(player, critter);
                        //else Loot(player, critter);
                    }
                }

                ConsoleKey readKey = Console.ReadKey().Key;
                if (readKey == ConsoleKey.LeftArrow && Labyrint[player.Position[0],(player.Position[1] -1)] ==' ')
                {
                    player.Position[1]--;
                }
                else if (readKey == ConsoleKey.RightArrow && Labyrint[player.Position[0], (player.Position[1] + 1)] == ' ')
                {
                    player.Position[1]++;
                }
                else if (readKey == ConsoleKey.UpArrow && Labyrint[player.Position[0] -1, (player.Position[1])] == ' ')
                {
                    player.Position[0]--;
                }
                else if (readKey == ConsoleKey.DownArrow && Labyrint[player.Position[0] +1, (player.Position[1])] == ' ')
                {
                    player.Position[0]++;
                }
                else if (readKey == ConsoleKey.Home)
                {
                    player.Display();
                }


                foreach (var item in Monsters)
                {
                    item.Move(this,player); // måste skicka med både player & game. Vad är egentligen poängen med uppdelningen i klasser nu?
                }
                if (player.XP /( player.Level * 1000) > 1)
                {
                    player.LevelUp();
                }
            }
        }

        public void FightOrTrade (Player player, Monster monster)
        {
            //Console.WriteLine("fight!");

            Random rnd = new Random();
            if (monster.IsHostile)
            {
                while (monster.IsAlive && player.IsAlive)
                {
                    int attackRollPlayer = rnd.Next(1, 101);
                    int attackRollMonster = rnd.Next(1, 101);
                    if (attackRollPlayer <= player.HitChance)
                    {
                        Console.WriteLine(player.Name + " träffade " + monster.Name + " och gjorde " + player.HitDamage + " hp skada");
                        monster.HP -= player.HitDamage;
                    }
                    else
                    {
                        Console.WriteLine(player.Name + " missade");
                    }
                    ConsoleKey readKey=Console.ReadKey().Key;
                     if (readKey == ConsoleKey.Home)
                    {
                        player.Display();
                    }
                    if (attackRollMonster <= monster.HitChance)
                    {
                        Console.WriteLine(monster.Name + " träffade " + player.Name + " och gjorde " + monster.HitDamage + " hp skada");
                        player.HP -= monster.HitDamage;
                    }
                    else
                    {
                        Console.WriteLine(monster.Name + " missade");
                    }

                    if (player.HP < 0)
                    {
                        player.IsAlive = false;
                        Console.WriteLine(player.Name + " dog");
                    }
                    if (monster.HP < 0)
                    {
                        monster.IsAlive = false;
                        Console.WriteLine(monster.Name + " dog");
                        player.XP += monster.XP;
                        player.Gold += monster.Gold;
                        List<Item> tmpList = new List<Item>();
                        foreach (var item in monster.Inventory)
                        {
                            player.Inventory.Add(item);
                            tmpList.Add(item);
                           
                        }
                        foreach (var item2 in tmpList)
                        {
                            monster.Inventory.Remove(item2);
                        }
                        //Monsters.Remove(monster);         // går inte, gör i nästa runda. kolla IsAlive.
                    }
                }
            }
            else
            {
                string input;
                do
                {
                    Console.WriteLine(monster.Name + " vill idka byteshandel.\n" +
                        "\n" +
                        "1) handla \n" +
                        "2) slåss");
                    input = Console.ReadLine();
                } while (input != "1" && input != "2");
                if (input == "1")
                {
                    Trade(player, monster);
                }
                else
                {
                    monster.IsHostile=true;
                    FightOrTrade(player, monster);
                }
            }

            Console.ReadKey();
        }

        public void Loot (Player player, Monster monster) // TODO: TA BORT MIG. autolootar istället.
        {
            //Console.WriteLine(" TAKE STUFF");
            //Console.ReadKey();
            // something
        }

        public void PlaceMonster ( Monster monster, int minY, int maxY, int minX, int maxX, Player player )
        {
            int x = 0;
            int y = 0;
            Random rnd = new Random();
            do
            {
                 x = rnd.Next(minX, maxX);
                 y = rnd.Next(minY, maxY);
            } while (Labyrint[y, x] != ' ' && !(CheckForMonsters(y,x, player )));
            monster.Position[0] = y;
            monster.Position[1] = x;
            monster.RecentlyVisitedPositions.Add((y, x));
        }
        public Game()
        {
                                                        // åkalla labyrintgeneratorn sen, för nu använd förbyggd
            //List<string> labyrinttemp= new List<string> 
            //string[] labyrinttemp = new string[]
            //{ 
            //    "###########################################",
            //    "#             #         #     #        #  #",
            //    "#     #########      #     #   #   #   #  #",
            //    "#             #  #############     #   #  #",
            //    "#             #  #             #   #   #  #",
            //    "###########   #  #  ############   #      #",
            //    "#   #            #             ########   #",
            //    "#   #  #     ###############   #          #",
            //    "#   #  #                       #### # #####",
            //    "#   #  #     ###################          #",
            //    "#      #                                  #",
            //    "###########################################",
            //};
            ///////////////////////UTKOMMENTERAR GAMLA MAZEN^^^^^lämnar detta tills vidare
            ////int p = 0;
            ////int q = 0;
            ////foreach (var item in labyrinttemp)
            ////{
            ////    char[] temparr = item.ToCharArray();
            ////    foreach (var item2 in temparr)
            ////    {
            ////        Labyrint[p, q] = item2;
            ////        q++;
            ////    }
            ////    p++;
            ////}

            //Labyrint = new char[labyrinttemp.Length, labyrinttemp[0].Length];

            //for (int i = 0; i < labyrinttemp.Length; i++)
            //{
            //    for (int j = 0; j < labyrinttemp[i].Length; j++)
            //    {
            //        Labyrint[i, j] = (labyrinttemp[i])[j];
            //    }
            //}


            int size = 24;
            char[,] maze = new char[size * 2 + 2, size * 2 + 2];
            (int, int) currentCell = (4, 4);
            Random rnd = new Random();

            //vvvv fyll labyrinten med väggar & obesökta kammare
            //  omge lab med tecken som är annat än ' ' & '#', exvis '!'
            for (int i = 0; i < (size * 2) + 2; i++)
            {
                for (int j = 0; j < (size * 2) + 2; j++)
                {
                    if (i != 0 && j != 0 && i != size * 2 + 1 && j != size * 2 + 1 && i != 1 && j != 1 && i != size * 2 && j != size * 2)
                    {
                        maze[i, j] = '#';
                    }
                    else
                    {
                        maze[i, j] = '!';
                    }

                }
            }
            Generate(currentCell);

            Labyrint=DoubleSize(maze);
             void Generate((int, int) current)
            {
                //https://en.wikipedia.org/wiki/Maze_generation_algorithm#Randomized_depth-first_search


                //maze[current.Item1, current.Item2] = ' ';
                while
                    (
                    maze[current.Item1 + 2, current.Item2] == '#' ||
                    maze[current.Item1 - 2, current.Item2] == '#' ||
                    maze[current.Item1, current.Item2 + 2] == '#' ||
                    maze[current.Item1, current.Item2 - 2] == '#'
                    )
                {
                    maze[current.Item1, current.Item2] = ' ';
                    int val = rnd.Next(1, 5);
                    switch (val)
                    {
                        case 1:
                            if (maze[current.Item1 + 2, current.Item2] == '#' && maze[current.Item1 + 1, current.Item2 + 1] != ' ' && maze[current.Item1 + 1, current.Item2 - 1] != ' ')
                            {
                                maze[current.Item1 + 1, current.Item2] = ' ';
                                current.Item1 = current.Item1 + 2;
                                Generate(current);
                            }
                            break;
                        case 2:
                            if (maze[current.Item1 - 2, current.Item2] == '#' && maze[current.Item1 - 1, current.Item2 + 1] != ' ' && maze[current.Item1 - 1, current.Item2 - 1] != ' ')
                            {
                                maze[current.Item1 - 1, current.Item2] = ' ';
                                current.Item1 = current.Item1 - 2;
                                Generate(current);
                            }
                            break;
                        case 3:
                            if (maze[current.Item1, current.Item2 + 2] == '#' && maze[current.Item1 + 1, current.Item2 + 1] != ' ' && maze[current.Item1 - 1, current.Item2 - 1] != ' ')
                            {
                                maze[current.Item1, current.Item2 + 1] = ' ';
                                current.Item2 = current.Item2 + 2;
                                Generate(current);
                            }
                            break;
                        case 4:
                            if (maze[current.Item1, current.Item2 - 2] == '#' && maze[current.Item1 + 1, current.Item2 - 1] != ' ' && maze[current.Item1 - 1, current.Item2 - 1] != ' ')
                            {
                                maze[current.Item1, current.Item2 - 1] = ' ';
                                current.Item2 = current.Item2 - 2;
                                Generate(current);
                            }
                            break;


                    }

                }
            }
            char[,] DoubleSize(char[,] mazen)
            {
                int height = mazen.GetLength(0);
                int width = mazen.GetLength(1);
                char c = ' ';
                char[,] bigger = new char[(height * 2), (width * 2)];
                for (int i = 0; i < height * 2; i++)
                {
                    for (int j = 0; j < width * 2; j++)
                    {
                        c = mazen[i / 2, j / 2];
                        bigger[i, j] = c;
                        //Console.Write (bigger[i,j]);
                        //if ( j == width*2 - 1)
                        //{
                        //	Console.WriteLine(" ");
                        //}
                    }

                }//dit 1
                for (int k = 0; k < height * 2; k++)
                {
                    for (int l = 0; l < width * 2; l++)
                    {
                        //Thread.Sleep(5);

                        if (k != height * 2 - 1 && (bigger[k, l] == '!' && bigger[k + 1, l] == ' '))                         //(bigger[k,l]=='#' && bigger[k+1,l]==' ')  
                        {
                            c = 'o';
                            bigger[k + 1, l] = c;
                        }

                        if (l != width * 2 - 1 && (bigger[k, l] == '!' && bigger[k, l + 1] == ' '))                         //(bigger[k,l]=='#' && bigger[k+1,l]==' ')  
                        {
                            c = 'o';
                            bigger[k, l + 1] = c;
                        }
                        if (bigger[k, l] == '!')                         //(bigger[k,l]=='#' && bigger[k+1,l]==' ')  
                        {
                            c = 'o';
                            bigger[k, l] = c;
                        }

                        if (bigger[k, l] == '#' && bigger[k + 1, l] == ' ')   //(bigger[k,l]=='#' && bigger[k+1,l]==' ')  
                        {
                            c = 'o';
                            bigger[k + 1, l] = c;
                        }

                        if (bigger[k, l] == '#' && bigger[k + 1, l + 1] == ' ')   //(bigger[k,l]=='#' && bigger[k+1,l]==' ')  
                        {
                            c = 'o';
                            bigger[k + 1, l + 1] = c;
                        }

                        if (bigger[k, l] == '#' && bigger[k, l + 1] == ' ')   //(bigger[k,l]=='#' && bigger[k+1,l]==' ')  
                        {
                            c = 'o';
                            bigger[k, l + 1] = c;
                        }


                        if (bigger[k, l] == '!')
                        {
                            c = '#';
                            bigger[k, l] = c;
                        }
                        if (bigger[k, l] == 'o')
                        {
                            c = '#';
                            bigger[k, l] = c;
                        }
                        if (bigger[k, l] == '#' || bigger[k, l] == '!')
                        {
                            //Console.ForegroundColor = ConsoleColor.Blue;
                            //Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            //Console.ForegroundColor = ConsoleColor.White;
                            //Console.BackgroundColor = ConsoleColor.Black;

                        }
                        //Console.Write(bigger[k, l]);
                        if (l == width * 2 - 1)
                        {
                            //Console.WriteLine(" ");
                        }
                    }

                }
                return bigger;

            }




        } //END GAME Constructor

        public bool CheckForMonsters(int i, int j, Player player)
        {

            foreach (var item in Monsters)
            {
                if (i == item.Position[0] && j == item.Position[1])
                {
                    Console.Write(item.Symbol);
                    return true;
                }                
            }

            if (i == player.Position[0] && j == player.Position[1]) return true;

                return false;
        } 

        public void Trade (Monster player, Monster monster)
        {
            string input;
            do
            {
                Console.WriteLine("0) Avsluta");
                Console.WriteLine("1.Köp");
                Console.WriteLine("2.Sälj");
                Console.WriteLine(" ");
                input = Console.ReadLine();
                //} while (input != "1" && input != "2");

                if (input == "1")
                {
                    int i = 1;
                    int result = 0;
                    List<Item> sell = new List<Item>();
                    do
                    {
                        Console.WriteLine("0) ångra");
                        foreach (var item in monster.WantsToSell)
                        {
                            Console.WriteLine((i) + " " + item.Name + " " + item.Description + "\t" + item.SuggestedPrice + " guld");
                            sell.Add(item);
                            i++;
                        }
                        Console.WriteLine("välj vad");
                        i = 1;
                    } while (!int.TryParse(Console.ReadLine(), out result));

                    if (result == 0)
                    {
                        Console.WriteLine("OK.");
                    }
                    else
                    {
                        if (player.Gold > sell[i - 1].SuggestedPrice)
                        {
                            player.Gold -= sell[i - 1].SuggestedPrice;
                            monster.Gold += sell[i - 1].SuggestedPrice;
                            player.Inventory.Add(sell[i - 1]);
                            monster.Inventory.Remove(sell[i - 1]);
                        }
                    }
                }
                /////////////////////////////////
                if (input == "2")
                {
                    int i = 1;
                    int result = 0;
                    int offer=0;
                    List<Item> buy = new List<Item>();
                    do
                    {
                        Console.WriteLine("0) ångra");
                        foreach (var item in monster.WantsToBuy)
                        {
                            foreach (var thing in player.Inventory)
                            {
                                if (thing == item)
                                {

                                    if (item.SuggestedPrice > monster.Gold) offer = monster.Gold;
                                    else offer = item.SuggestedPrice;
                                    Console.WriteLine((i) + monster.Name + " vill köpa " + item.Name + "för " + offer + " guld");
                                    buy.Add(item);
                                    i++;
                                }
                            }
                        }
                        Console.WriteLine("välj vad");
                        i = 1;
                    } while (!int.TryParse(Console.ReadLine(), out result));

                    if (result == 0)
                    {
                        Console.WriteLine("OK.");
                    }
                    else
                    {                        
                            player.Gold += offer;
                            monster.Gold -= offer;

                            player.Inventory.Remove(buy[i - 1]);
                            monster.Inventory.Add(buy[i - 1]);                        
                    }
                }



            } while (input != "0" );
        }

        public double GetDistance (Player player, Monster monster)
        {

            double distance = 0;
            distance = Math.Sqrt(Math.Pow((player.Position[0] - monster.Position[0]), 2) + Math.Pow((player.Position[1] - monster.Position[1]), 2));
            return distance;
        }
    }



}





