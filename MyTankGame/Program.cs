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

           
            Console.ReadLine();
        }
    }
}
