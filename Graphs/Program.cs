using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph myGraph = new Graph();
            myGraph.ShortestPath("Living Room");

            Console.WriteLine("You always start in the Living Room");
            string input = "";
            while(input != "quit")
            {
                Console.Write("Enter a room: ");
                input = Console.ReadLine();

                myGraph.PrintPath(input);
            }
            
        }
    }
}
