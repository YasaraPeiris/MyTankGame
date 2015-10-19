using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTankGame
{
    class Program
    {

        int x = 0;
        int y = 0;
        int direction=0;
        int nextX = 0;
        int nextY = 0;
        string player = "";
        string[][] gameGrid = new string[10][];
        List<string> bricks = new List<string>();
        List<string> stones = new List<string>();
        List<string> water = new List<string>();
        public void getLocation(String l) // Go to the initial setup location.
        {
            string[] message = new string[5];
            message=l.Split(':',',');
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
            foreach (string s in bricks_array){
                int q = Int32.Parse(s.Split(',')[0]);
                Console.WriteLine(q);
                int w = Int32.Parse(s.Split(',')[1]);
                Console.WriteLine(w);
                gameGrid[q][w] = "B";
            }
            foreach (string s in stones_array){
                int q = Int32.Parse(s.Split(',')[0]);
                int w = Int32.Parse(s.Split(',')[1]);
                gameGrid[q][w] = "S";
            }
            foreach (string s in water_array){
                int q = Int32.Parse(s.Split(',')[0]);
                int w = Int32.Parse(s.Split(',')[1]);
                gameGrid[q][w] = "W";
            }
            
        }
        public void displayGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(gameGrid[i][j]);
                }
                Console.WriteLine("\n");
            }
        }
        public void getOpponentsLocations() { }
        public void move(string command){ // get the command and check the irection of the tank facing
            int dir = -1;
            if(command=="UP"){
               dir=0;
           }
            else if(command=="DOWN"){
                dir=1;
            }
            else if(command=="RIGHT"){
                dir=1;
            }
            else if(command=="LEFT"){
                dir=1;
            }
            rotate(dir);
            if (gameGrid[nextX][nextY] == "") {
                x = nextX;
                y = nextY;
            }
                   
        }
        public void rotate(int dir){ // get the direction of the next command.
            if(dir ==0){
                nextY=y+1;
            }
            else if(dir == 1){
                nextX = x+1;
            }
             else if(dir == 2){
                nextY = y-1;
            }
             else if(dir == 3){
                nextX = x-1;
            }
        }
        public void getGlobalUpdate(string updatedValues) // once per second server will broadcast all the details about what happend in the gamegrid.
        {
            List<string> updatedGrid = new List<string>();
            updatedGrid.AddRange(updatedValues.Split(':'));
            Hashtable playerOne = new Hashtable();
           


        }
        public void createHashmaps(List<string> s,Hashtable name){
         
            name["playerLocation"] = s[1].Split(';')[1];
            name["Direction"] = s[1].Split(';')[1];
            name["whethershot"] = s[1].Split(';')[1];
            name["health"] = s[1].Split(';')[1];
            name["coins"] = s[1].Split(';')[1];
            name["points"] = s[1].Split(';')[1];




    }
        public void shoot(string command)
        {

        }

        static void Main(string[] args)
        {
            Program newGame = new Program();
            newGame.getLocation("S:P1:1,1:0");
            newGame.getMapDetails("I:P1:2,3;4,3;3,4;5,6:5,3;2,8;3,8;5,8:8,3;0,9;0,3;1,0");
            newGame.displayGrid();
            Console.ReadLine();
        }
    }
}
