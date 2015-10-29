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
            Client communicator = new Client(grid);
            
            communicator.startSend(); 
            communicator.startRecieve();
            communicator.SendData();
            

            
            //grid.setMapDetails("I:P1: 0,0;2,3;3,4 : 4,5;5,6;6,7:  1,2;2,3;3,4 #?");
            //grid.mytank.setLocation("S:P1:1,0:0#?");
            //grid.displayGrid();
            //Console.ReadLine();
        }
    }
}
