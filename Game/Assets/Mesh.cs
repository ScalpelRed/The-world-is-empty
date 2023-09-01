using System.Numerics;
using System.Linq;
using Genbox.VelcroPhysics.Shared;
using Game.Phys;
using Game.Main;

namespace Game.Assets
{
    public class Mesh
    {
        private readonly List<Vertex> Verts = new();
        private readonly List<int>[] Polygons = Array.Empty<List<int>>();
        public int VertsPerPolygon;
        public int PolygonCount
        {
            get => polygonCount;
            private set => polygonCount = value;
        }
        private int polygonCount;

        public Mesh(int vertsPerPolygon)
        {
            VertsPerPolygon = vertsPerPolygon;
            Polygons = new List<int>[vertsPerPolygon];
            for (int i = 0; i < Polygons.Length; i++)
                Polygons[i] = new List<int>();
        }

        public Mesh(string[] source)
        {
            List<Vector3> coords = new();
            List<Vector2> texcoords = new();
            List<Vector3> normals = new();

            bool VPPnotSet = true;

            foreach (string v in source)
            {
                string[] words = v.Replace('.', ',').Split(' ', '/');

                switch (words[0])
                {
                    case "v":
                        coords.Add(new Vector3(
                            float.Parse(words[1]),
                            float.Parse(words[2]),
                            float.Parse(words[3])));
                        break;
                    case "vt":
                        texcoords.Add(new Vector2(
                            float.Parse(words[1]),
                            float.Parse(words[2])));
                        break;
                    case "vn":
                        normals.Add(new Vector3(
                            float.Parse(words[1]),
                            float.Parse(words[2]),
                            float.Parse(words[3])));
                        break;
                    case "f":
                        if (VPPnotSet)
                        {
                            VPPnotSet = false;
                            VertsPerPolygon = (words.Length - 1) / 3;
                            Polygons = new List<int>[VertsPerPolygon];
                            for (int i = 0; i < Polygons.Length; i++)
                                Polygons[i] = new List<int>();
                        }

                        for (int vertexWord = 1; vertexWord < words.Length; vertexWord += 3)
                        {
                            Verts.Add(new Vertex(
                                coords[int.Parse(words[vertexWord]) - 1],
                                texcoords[int.Parse(words[vertexWord + 1]) - 1],
                                normals[int.Parse(words[vertexWord + 2]) - 1]
                                ));
                            Polygons[vertexWord / 3].Add(Verts.Count - 1);
                        }

                        polygonCount++;
                        break;
                }
            }
        }

        public void AddVertex(Vector3 coords, Vector2 texcoords, Vector3 normal)
        {
            Verts.Add(new Vertex(coords, texcoords, normal));
        }

        public int AddPolygon(params int[] verts)
        {
            if (VertsPerPolygon != verts.Length)
            {
                throw new Exception("This mesh supports only " + VertsPerPolygon
                    + " vertices per polygon, but tried to add polygon of "
                    + verts.Length / 3);
            }

            for (int i = 0; i < VertsPerPolygon; i++)
            {
                Polygons[i].Add(verts[i]);
            }
            return ++polygonCount;
        }

        public int ApplyPolygon()
        {
            if (Verts.Count < VertsPerPolygon)
                throw new Exception("Not enouth verticies to apply polygon: "
                    + VertsPerPolygon + " expected, but has " + Verts.Count);

            for (int i = 0; i < VertsPerPolygon; i++)
            {
                Polygons[i].Add(Verts.Count - VertsPerPolygon + i);
            }
            return ++polygonCount;
        }

        public float[] GetVertexDataArray()
        {
            float[] res = new float[Verts.Count * 8];

            for (int i = 0; i < Verts.Count; i++)
            {
                int s = i * 8;
                Vertex v = Verts[i];
                res[s] = v.Position.X;
                res[s + 1] = v.Position.Y;
                res[s + 2] = v.Position.Z;
                res[s + 3] = v.Texcoords.X;
                res[s + 4] = v.Texcoords.Y;
                res[s + 5] = v.Normal.X;
                res[s + 6] = v.Normal.Y;
                res[s + 7] = v.Normal.Z;
            }

            return res;
        }

        public int[] GetIndexArray()
        {
            int[] res = new int[polygonCount * VertsPerPolygon];

            for (int pol = 0; pol < polygonCount; pol++)
            {
                int polPos = pol * VertsPerPolygon;
                for (int vert = 0; vert < VertsPerPolygon; vert++)
                {
                    res[polPos + vert] = Polygons[vert][pol];
                }
            }

            return res;
        }

        public PhysicalMesh2D ToPhysicalMesh()
        {
            PhysicalMesh2D res = new();

            for (int pol = 0; pol < PolygonCount; pol++)
            {
                Vertices polygon = new Vertices();
                for (int vert = 0; vert < VertsPerPolygon; vert++)
                {
                    polygon.Add(Verts[Polygons[vert][pol]].Position.XY().ToXna());
                }
                res.AddPolygon(polygon);
            }

            return res;
        }

        public override string ToString()
        {
            return $"{Verts.Count}/{Polygons[0].Count}/{VertsPerPolygon}";
        }

        private struct Vertex
        {
            public Vector3 Position;
            public Vector2 Texcoords;
            public Vector3 Normal;

            public Vertex(Vector3 pos, Vector2 texcoords, Vector3 normal)
            {
                Position = pos;
                Texcoords = texcoords;
                Normal = normal;
            }

            public override string ToString()
            {
                return $"{Position} {Texcoords} {Normal}";
            }
        }
    }
}
