using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyTankGame
{
    class GameGrid
    {

        public GameEntity[,] gameGrid = new GameEntity[10, 10];
        int[,] damgesLevel = new int[10, 10];
        List<string> bricks = new List<string>();
        List<string> stones = new List<string>();
        List<string> water = new List<string>();

        public void setMapDetails(String m) //add received map data in to the game grid
        {
            string[] mapDetails = new string[5];
            mapDetails = m.Split(':');
            bricks.AddRange(mapDetails[2].Split(';'));
            stones.AddRange(mapDetails[3].Split(';'));
            water.AddRange(mapDetails[4].Split(';'));
            string[] bricks_array = bricks.ToArray();
            string[] stones_array = stones.ToArray();
            string[] water_array = water.ToArray();
            foreach (string s in bricks_array)
            {
                int q = Int32.Parse(s.Split(',')[0]);
                int w = Int32.Parse(s.Split(',')[1]);
                gameGrid[q, w] = new Brick(q, w);
            }
            foreach (string s in stones_array)
            {
                int q = Int32.Parse(s.Split(',')[0]);
                int w = Int32.Parse(s.Split(',')[1]);
                gameGrid[q, w] = new Stone(q, w);
            }
            foreach (string s in water_array)
            {
                int q = Int32.Parse(s.Split(',')[0]);
                int w = Int32.Parse(s.Split(',')[1]);
                gameGrid[q, w] = new Water(q, w);
            }

        }

        public void displayGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (gameGrid[i, j] != null)
                    {
                        Console.Write(gameGrid[i, j].ToString() + " ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine("\n");
            }
        }

        public void getGlobalUpdate(string updatedValues) // once per second server will broadcast all the details about what happend in the gamegrid.
        {
            List<string> updatedGrid = new List<string>();
            updatedGrid.AddRange(updatedValues.Split(':'));
            int numberOfPlayers = updatedGrid.Count - 2;
            if (numberOfPlayers > 0)
            {
                Hashtable playerOne = new Hashtable();
                List<string> list1 = new List<string>();
                list1.AddRange(updatedGrid[1].Split(';'));
                createHashmaps(list1, playerOne);

            }
            if (numberOfPlayers > 1)
            {
                Hashtable playerTwo = new Hashtable();
                List<string> list2 = new List<string>();
                list2.AddRange(updatedGrid[2].Split(';'));
                createHashmaps(list2, playerTwo);
            }
            if (numberOfPlayers > 2)
            {
                Hashtable playerThree = new Hashtable();
                List<string> list3 = new List<string>();
                list3.AddRange(updatedGrid[3].Split(';'));
                createHashmaps(list3, playerThree);
            }
            if (numberOfPlayers > 3)
            {
                Hashtable playerFour = new Hashtable();
                List<string> list4 = new List<string>();
                list4.AddRange(updatedGrid[4].Split(';'));
                createHashmaps(list4, playerFour);
            }
            if (numberOfPlayers > 4)
            {
                Hashtable playerFive = new Hashtable();
                List<string> list5 = new List<string>();
                list5.AddRange(updatedGrid[5].Split(';'));
                createHashmaps(list5, playerFive);
            }
            updateDamages(updatedGrid[6]);
        }

        public void createHashmaps(List<string> s, Hashtable name)
        {

            name["playerLocation"] = s[1].Split(';')[1];
            name["Direction"] = s[1].Split(';')[1];
            name["whethershot"] = s[1].Split(';')[1];
            name["health"] = s[1].Split(';')[1];
            name["coins"] = s[1].Split(';')[1];
            name["points"] = s[1].Split(';')[1];
        }
        public void updateDamages(string command)
        {
            List<string> damages = new List<string>();
            damages.AddRange(command.Split(';'));
            foreach (string s in damages)
            {
                int c = Int32.Parse(s.Split(';')[0]);
                int d = Int32.Parse(s.Split(';')[1]);
                damgesLevel[c, d] = Int32.Parse(s.Split(';')[2]);
            }
        }
        public void getCoinsDetails(string coinmessage)
        {
            string[] coinDetails = new string[4];
            coinDetails = coinmessage.Split(':');
            int coin_x = Int32.Parse(coinDetails[1].Split(',')[0]);
            int coin_y = Int32.Parse(coinDetails[1].Split(',')[1]);
            int LT = Int32.Parse(coinDetails[2]);
            int val = Int32.Parse(coinDetails[3]);
            Coin c = new Coin(coin_x, coin_y, LT, val);
        }
        public void getLifePacksDetails(string lpmessage)
        {
            string[] lpDetails = new string[3];
            lpDetails = lpmessage.Split(':');
            int coin_x = Int32.Parse(lpDetails[1].Split(',')[0]);
            int coin_y = Int32.Parse(lpDetails[1].Split(',')[1]);
            int LT = Int32.Parse(lpDetails[2]);
        }

        public void readServerMessage(string message)
        {
            if (message[0] == 'S')
            {
                Console.Write("\n \n \n \n JOINED THE GAME \n \n \n \n");

                //getLocation(message);
            }
            else if (message[0] == 'I')
            {
                Console.Write("\n \n \n \n Game initialised \n \n \n \n");
                //this.setMapDetails(message);
            }
            else if (message[0] == 'G')
            {
                Console.Write("\n \n \n \n GLOBAL UPDATE \n \n \n \n");
                //getGlobalUpdate(message);
            }
            else if (message[0] == 'C')
            {
                Console.Write("\n \n \n \n coins!! \n \n \n \n");
                //getCoinsDetails(message);
            }
            else if (message[0] == 'L')
            {
                Console.Write("\n \n \n \n life packs!! \n \n \n \n");
                //getLifePacksDetails(message);
            }
            this.displayGrid();
        }
    }

    abstract class GameEntity
    {
        public int x; //horizontal position
        public int y; //vertical position
        public string name;
        public GameEntity(int xx, int yy)
        {
            x = xx;
            y = yy;
        }

        public override string ToString()
        {
            return name;
        }
    }

    class Brick : GameEntity
    {
        int health;
        public Brick(int xx, int yy)
            : base(xx, yy)
        {
            health = 100;
            name = "B";
        }
        public void updateDamage()
        {

        }
    }
    class Stone : GameEntity
    {
        public Stone(int xx, int yy)
            : base(xx, yy)
        {
            name = "S";
        }
    }
    class Water : GameEntity
    {
        public Water(int xx, int yy)
            : base(xx, yy)
        {
            name = "W";
        }
    }

    class LifePack : GameEntity
    {
        public int lifetime;
        public LifePack(int xx, int yy, int lt)
            : base(xx, yy)
        {
            x = xx;
            y = yy;
            lifetime = lt;
        }
        public void updateRandom()
        {
            //till lifetime expires - appear 
        }
    }
    class Coin : LifePack
    {
        int value;

        public Coin(int xx, int yy, int lt, int val)
            : base(xx, yy, lt)
        {
            x = xx;
            y = yy;
            lifetime = lt;
            value = val;
        }
    }

}
