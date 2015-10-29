using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyTankGame
{
    class Tank : GameEntity
    {

        int direction;
        int nextX;
        int nextY;
        int health;
        public bool status;
        GameGrid grid;//the game grid this tank belongs to

        public Tank()
            : base()
        {
            direction = 0;
            nextX = 0;
            nextY = 0;
            health = 0;
            status = true;
        }
        public void setGrid(GameGrid g)
        {
            this.grid = g;
        }
        public void setLocation(String l) // Go to the initial setup location.
        {
            l = l.Remove(l.Length - 2); //remove trailing # and ?

            string[] message = new string[5];
            message = l.Split(':', ';',',');
            x = Int32.Parse(message[2]);
            y = Int32.Parse(message[3]);
            direction = Int32.Parse(message[4]);
            name = message[1];
            grid.gameGrid[x, y] = this;
            //Console.WriteLine("x: {0}", x);
            //Console.WriteLine("y: {0}", y);
            Console.WriteLine("HELLOOO");

        }
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
            if (grid.gameGrid[nextX, nextY].ToString() == "")
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
    }

    class MyTank : Tank
    {

        public void shoot()
        {

        }


    }


}
