using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace MyTankGame
{
    class Program
    {


        int x = 0;
        int y = 0;
        int direction = 0;
        int nextX = 0;
        int nextY = 0;
        string player = "";
        string[,] gameGrid = new string[10, 10];
        int[,] damgesLevel = new int[10, 10];
        List<string> bricks = new List<string>();
        List<string> stones = new List<string>();
        List<string> water = new List<string>();
       
        public void getLocation(String l) // Go to the initial setup location.
        {
            string[] message = new string[5];
            message = l.Split(':', ',');
            x = Int32.Parse(message[2]);
            y = Int32.Parse(message[3]);
            direction = Int32.Parse(message[4]);
            player = message[1];
            Console.WriteLine("x: {0}", x);
            Console.WriteLine("y: {0}", y);
        }
        public void getMapDetails(String m) //add received map data in to the game grid
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
                gameGrid[q, w] = "B";
            }
            foreach (string s in stones_array)
            {
                int q = Int32.Parse(s.Split(',')[0]);
                int w = Int32.Parse(s.Split(',')[1]);
                gameGrid[q, w] = "S";
            }
            foreach (string s in water_array)
            {
                int q = Int32.Parse(s.Split(',')[0]);
                int w = Int32.Parse(s.Split(',')[1]);
                gameGrid[q, w] = "W";
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
                        Console.Write(gameGrid[i, j] + " ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine("\n");
            }
        }
        public void getOpponentsLocations() { }
        public void move(string command)
        { // get the command and check the irection of the tank facing
            int dir = -1;
            if (command == "UP")
            {
                dir = 0;
            }
            else if (command == "DOWN")
            {
                dir = 1;
            }
            else if (command == "RIGHT")
            {
                dir = 1;
            }
            else if (command == "LEFT")
            {
                dir = 1;
            }
            rotate(dir);
            if (gameGrid[nextX, nextY] == "")
            {
                x = nextX;
                y = nextY;
            }

        }
        public void rotate(int dir)
        { // get the direction of the next command.
            if (dir == 0)
            {
                nextY = y + 1;
            }
            else if (dir == 1)
            {
                nextX = x + 1;
            }
            else if (dir == 2)
            {
                nextY = y - 1;
            }
            else if (dir == 3)
            {
                nextX = x - 1;
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
            shoot(updatedGrid[6]);
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
        public void shoot(string command)
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
                Console.Write("JOIN THE GAME");
                //getLocation(message);
            } 
            else if (message[0] == 'I')
            {
                Console.Write("Game init");
                //getMapDetails(message);
            } 
            else if (message[0] == 'G')
            {
                Console.Write("moving and shooting");
                //getGlobalUpdate(message);
            }
            else if (message[0] == 'C')
            {
                Console.Write("coins");
                //getCoinsDetails(message);
            }
            else if (message[0] == 'L')
            {
                Console.Write("life packs");
                //getLifePacksDetails(message);
            }
        }
        public void WorkThreadFunction(Class1 communicator)
        {
            try
            {
               
                communicator.ReceiveData();
            }
            catch (Exception ex)
            {
                // log errors
            }
        }
        static void Main(string[] args)
        {
            Program newGame = new Program();
            Class1 communicator = new Class1(newGame);
            communicator.SendData();
            communicator.startRecieve();

            newGame.getLocation("S:P1:1,1:0");
            newGame.getMapDetails("I:P1:2,3;4,3;3,4;5,6:5,3;2,8;3,8;5,8:8,3;0,9;0,3;1,0");
            newGame.displayGrid();
            Console.ReadLine();
        }
    }
}
