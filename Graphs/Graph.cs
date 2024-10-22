using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    internal class Graph
    {
        int[,] adjMatrix;

        Dictionary<string, Vertex> layout;
        List<Vertex> vertices;

        public Graph()
        {
            Vertex livingRoom = new Vertex("Living Room");
            Vertex bedroom = new Vertex("Bedroom");
            Vertex diningRoom = new Vertex("Dining Room");
            Vertex kitchen = new Vertex("Kitchen");
            Vertex bathroom = new Vertex("Bathroom");

            vertices = new List<Vertex>();
            vertices.Add(livingRoom);
            vertices.Add(bedroom);
            vertices.Add(diningRoom);
            vertices.Add(kitchen);
            vertices.Add(bathroom);

            layout = new Dictionary<string, Vertex>();
            foreach (Vertex v in vertices)
                layout.Add(v.Name, v);

            adjMatrix = new int[5, 5]
            {  //LR BR DN KI BA
                {0, 4, 1, 0, 0 }, // living room 0
                {4, 0, 2, 0, 1 }, // bedroom 1
                {1, 2, 0, 4, 0 }, // dining room 2
                {0, 0, 4, 0, 2 }, // kitchen 3
                {0, 1, 0, 2, 0 }  // bathroom 4
            };

            // Looks like this:
            //       Living Room
            //      |           |
            // Dining Room -- BedRoom
            //     |            |
            //  Kitchen  --  Bathroom
        }

        public List<Vertex> GetNonPermanentNeighbors(Vertex v)
        {
            List<Vertex> neighbors = new List<Vertex>();

            int index = vertices.IndexOf(v);

            for(int i = 0; i < 5; i++)
            {
                if (adjMatrix[index, i] > 0 && !vertices[i].Permanent) neighbors.Add(vertices[i]);
            }
            
            return neighbors;
        }

        public void ShortestPath(string name)
        {
            // make sure the name is valid
            if (!layout.ContainsKey(name)) return;

            // reset the data
            foreach (Vertex v in vertices) v.Reset();

            // how many non-permanent nodes?
            int nonPermCount = vertices.Count;

            // find the starting vertex, make it current, set up labels
            Vertex curr = layout[name];
            curr.Permanent = true;
            curr.DistanceFromSource = 0;
            nonPermCount--;

            while(nonPermCount > 0)
            {
                int currIndex = vertices.IndexOf(curr);

                List<Vertex> nonPermNeighbors = GetNonPermanentNeighbors(curr);
                foreach(Vertex n in nonPermNeighbors)
                {
                    int neighborIndex = vertices.IndexOf(n);

                    if(n.NeighborOnPath == null)
                    {
                        n.NeighborOnPath = curr;
                        n.DistanceFromSource = curr.DistanceFromSource + adjMatrix[currIndex, neighborIndex];
                    }
                    else
                    {
                        float potentialDistance = curr.DistanceFromSource + adjMatrix[currIndex, neighborIndex];

                        if(potentialDistance < n.DistanceFromSource)
                        {
                            n.NeighborOnPath = curr;
                            n.DistanceFromSource = potentialDistance;
                        }
                    }
                }

                Vertex shortest = FindShortestDistanceNonPermanent();

                if(shortest != null)
                {
                    curr = shortest;
                    curr.Permanent = true;
                    nonPermCount--;
                }
            }
        }

        public Vertex FindShortestDistanceNonPermanent()
        {
            Vertex smallest = null;
            float smallestDist = float.MaxValue;

            foreach(Vertex v in vertices)
            {
                if(v.DistanceFromSource < smallestDist && !v.Permanent)
                {
                    smallest = v;
                    smallestDist = v.DistanceFromSource;
                }
            }

            return smallest;
        }

        public void PrintPath(string room)
        {
            if (!layout.ContainsKey(room))
            {
                Console.WriteLine("invalid room");
                return;
            }

            Console.WriteLine("\nShortest path backwards: ");

            Vertex v = layout[room];

            while(v != null)
            {
                Console.WriteLine(v.Name);
                v = v.NeighborOnPath;
            }

            Console.WriteLine();
        }
    }
}
