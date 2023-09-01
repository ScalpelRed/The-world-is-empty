using Silk.NET.OpenGL;
using System.Numerics;
using System.Diagnostics;
using Game.Graphics;

namespace Game.Assets
{
    public class Shader
    {
        public static string AppendShaderKeyword = "$AppendShader";
        public static string ShaderTypeKeyword = "$ShaderType";
        public int AppendDepth = 4;

        public readonly uint Handle;
        private readonly Dictionary<string, int> UniformPos = new();
        private readonly OpenGL Gl;

        public readonly string Name;

        public Shader(OpenGL gl)
        {
            Gl = gl;
            Handle = 0;
            Name = "Empty";
        }

        public Shader(string name, OpenGL opengl)
        {
            Gl = opengl;
            Name = name;

            string appendRecursive(string code, int recIteration)
            {
                if (recIteration >= AppendDepth) throw new Exception(
                    $"Only {AppendDepth} nested appendings are allowed (this is done in order to avoid recursive appending)");

                Dictionary<string, string> appendings = new();

                string[] split = code.Split();

                bool isWordAppendingName = false;
                foreach (string word in split)
                {
                    if (isWordAppendingName)
                    {
                        if (word[^1] != ';') throw new Exception($"{AppendShaderKeyword} {word} - ';' expected.");
                        appendings.TryAdd(word, appendRecursive(opengl.Core.Platform.ReadAllTextReadable(
                            string.Format($"shaders/{word[..^1]}.shader")), recIteration + 1));
                        isWordAppendingName = false;
                    }
                    else if (word == AppendShaderKeyword) isWordAppendingName = true;
                }

                foreach (var app in appendings)
                {
                    code = code.Replace(AppendShaderKeyword + " " + app.Key, app.Value);
                }
                return code;
            }

            uint compileShader(string code, ShaderType shaderType)
            {
                uint handle = opengl.Api.CreateShader(shaderType);
                opengl.Api.ShaderSource(handle, code);
                opengl.Api.CompileShader(handle);
                opengl.Core.Log("--- " + shaderType + " log ---\n" + opengl.Api.GetShaderInfoLog(handle));
                return handle;
            }

            opengl.Core.Log("Compiling shader " + Name);

            string sourceCode = opengl.Core.Platform.ReadAllTextReadable(string.Format("shaders/{0}.shader", name));
            sourceCode = appendRecursive(sourceCode, 0);

            List<string> uniforms = new();
            List<uint> shaders = new();
            string[] codes = sourceCode.Split(ShaderTypeKeyword + " ", StringSplitOptions.RemoveEmptyEntries);

            foreach (string code in codes)
            {
                //TODO: this is too slow

                string[] split = code.Split();

                if (split[0][^1] != ';') throw new Exception(ShaderTypeKeyword + " " + split[0] + " - ';' expected.");

                // 0 - waiting for "uniform"
                // 1 - type
                // 2 - name
                byte uniformWord = 0;
                foreach (string word in split)
                {
                    switch (uniformWord)
                    {
                        case 0:
                            if (word == "uniform") uniformWord = 1;
                            break;
                        case 1:
                            uniformWord = 2;
                            break;
                        case 2:
                            uniforms.Add(word[..^1]);
                            uniformWord = 0;
                            break;
                    }
                }

                // first word is shader type (since $ShaderType was removed)
                if (!Enum.TryParse(split[0][..^1], out ShaderType shaderType))
                {
                    throw new Exception("Invalid shader type: " + split[0]);
                }

                // shader code is source code with shader type removed
                shaders.Add(compileShader(code[(code.IndexOf(';') + 1)..], shaderType));
            }

            uint prog = Gl.Api.CreateProgram();
            foreach (uint shader in shaders) Gl.Api.AttachShader(prog, shader);
            Gl.Api.LinkProgram(prog);
            foreach (uint shader in shaders) Gl.Api.DeleteShader(shader);
            foreach (string uniform in uniforms) UniformPos.TryAdd(uniform, Gl.Api.GetUniformLocation(prog, uniform));
            Handle = prog;

            string l;
            try
            {
                // Sometimes it throws exception because some inner array is empty
                l = opengl.Api.GetProgramInfoLog(prog);

            }
            catch (ArgumentNullException)
            {
                l = "";
            }
            opengl.Core.Log("--- Shader '" + Name + "' log ---\n" + l);
        }

        public bool IsEmpty() => Handle <= 0;


        public void UniformInt(string name, int value)
        {
            if (UniformPos.TryGetValue(name, out var pos))
            {
                Gl.Api.Uniform1(pos, value);
            }
        }

        public void UniformFloat(string name, float value)
        {
            if (UniformPos.TryGetValue(name, out var pos))
            {
                Gl.Api.Uniform1(pos, value);
            }
        }

        public void UniformDouble(string name, double value)
        {
            if (UniformPos.TryGetValue(name, out var pos))
            {
                Gl.Api.Uniform1(pos, value);
            }
        }

        public void UniformVec2(string name, Vector2 value)
        {
            if (UniformPos.TryGetValue(name, out var pos))
            {
                Gl.Api.Uniform2(pos, ref value);
            }
        }

        public void UniformVec3(string name, Vector3 value)
        {
            if (UniformPos.TryGetValue(name, out var pos))
            {
                Gl.Api.Uniform3(pos, ref value);
            }
        }

        public void UniformVec4(string name, Vector4 value)
        {
            if (UniformPos.TryGetValue(name, out var pos))
            {
                Gl.Api.Uniform4(pos, ref value);
            }
        }

        public unsafe void UniformMat4(string name, Matrix4x4 value)
        {
            if (UniformPos.TryGetValue(name, out var pos))
            {
                Gl.Api.UniformMatrix4(pos, 1, true, (float*)&value);
            }
        }
    }
}
