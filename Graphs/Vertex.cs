using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    internal class Vertex
    {
        private string name;
        private float distanceFromSource;
        private Vertex neighborOnPath;
        private bool permanent;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public float DistanceFromSource
        {
            get { return distanceFromSource; }
            set { distanceFromSource = value; }
        }

        public Vertex NeighborOnPath
        {
            get { return neighborOnPath; }
            set { neighborOnPath = value; }
        }

        public bool Permanent
        {
            get { return permanent; }
            set { permanent = value; }
        }

        public Vertex(string name)
        {
            this.name = name;
            Reset();
        }

        public void Reset()
        {
            permanent = false;
            distanceFromSource = float.MaxValue;
            neighborOnPath = null;
        }
    }
}
