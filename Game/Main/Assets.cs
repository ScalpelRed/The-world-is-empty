//using Game.Audio;
using Game.Graphics;
using Game.OtherAssets;
using Game.Phys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Main
{
    public class Assets
    {
        public readonly GameCore Core;

        public Assets(GameCore core)
        {
            Core = core;

            Textures.Add("", new Texture());
            Shaders.Add("", new Shader(core.OpenGL));
        }

        private readonly SortedDictionary<string, Shader> Shaders = new();
        public Shader GetShader(string name)
        {
            try
            {
                return Shaders[name];
            }
            catch (KeyNotFoundException)
            {
                Shader shader = new(name, Core.OpenGL);
                Shaders.Add(name, shader);
                return shader;
            }
        }

        private readonly SortedDictionary<string, Texture> Textures = new();
        public Texture GetTexture(string name)
        {
            try
            {
                return Textures[name];
            }
            catch (KeyNotFoundException)
            {
                Texture texture = new(name, Core.OpenGL);
                Textures.Add(name, texture);
                return texture;
            }

        }

        private readonly SortedDictionary<string, Mesh> Meshes = new();
        public Mesh GetMesh(string name)
        {
            try
            {
                return Meshes[name];
            }
            catch (KeyNotFoundException)
            {
                string[] source;
                try
                {
                    source = Core.Platform.ReadAllLinesReadable("meshes/" + name + ".obj");
                }
                catch (FileNotFoundException)
                {
                    throw new AssetNotFoundException("Mesh", name);
                }
                Mesh mesh = new(source);
                Meshes.Add(name, mesh);
                return mesh;
            }

        }

        private readonly SortedDictionary<string, GlMesh> GlMeshes = new();
        public GlMesh GetGlMesh(string name)
        {
            try
            {
                return GlMeshes[name];
            }
            catch (KeyNotFoundException)
            {
                GlMesh glmesh = new(Core.OpenGL, GetMesh(name));
                GlMeshes.Add(name, glmesh);
                return glmesh;
            }
        }

        private readonly SortedDictionary<string, PhysicalMesh2D> PhysMeshes = new();
        public PhysicalMesh2D GetPhysicalMesh(string name)
        {
            try
            {
                return PhysMeshes[name];
            }
            catch (KeyNotFoundException)
            {
                PhysicalMesh2D physMesh = GetMesh(name).ToPhysicalMesh();
                PhysMeshes.Add(name, physMesh);
                return physMesh;
            }
        }

        private readonly SortedDictionary<string, Charmap> Charmaps = new();
        public Charmap GetCharmap(string name)
        {
            try
            {
                return Charmaps[name];
            }
            catch (KeyNotFoundException)
            {
                Charmap charmap = new(name, Core);
                Charmaps.Add(name, charmap);
                return charmap;
            }

        }

        /*private readonly SortedDictionary<string, MemorySound> MemorySounds = new();
        public MemorySound GetMemorySound(string name)
        {
            try
            {
                return MemorySounds[name];
            }
            catch (KeyNotFoundException)
            {
                return new MemorySound(Core.Audio, name);
            }

        }*/
    }
}
