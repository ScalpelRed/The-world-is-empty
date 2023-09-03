using Game.OtherAssets;
using Genbox.VelcroPhysics.Shared;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Phys
{
    public class PhysicalMesh2D
    {
        public List<Vertices> Verts = new();

        public PhysicalMesh2D()
        {

        }

        public void AddPolygon(Vertices pol)
        {
            Verts.Add(pol);
        }
    }
}
