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
        public void WorkThreadFunction(Client communicator)
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
            GameGrid grid = new GameGrid();
            Tank mytank = new Tank();
            Client communicator = new Client(grid);
            communicator.SendData();
            communicator.startRecieve();

            //Brick b = new Brick(3, 2);
            //Stone s = new Stone(5, 3);
            //Water w = new Water(8, 3);
            //Coin c = new Coin(3, 4, 5, 6);
            //LifePack l = new LifePack(7, 5, 4);
            //grid.gameGrid[3, 2] = b;
            //grid.gameGrid[5, 3] = s;
            //grid.gameGrid[8, 3] = w;
            //grid.displayGrid();
            Console.ReadLine();
        }
    }
}
