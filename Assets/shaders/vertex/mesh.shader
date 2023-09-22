$ShaderType VertexShader;
#version 330

layout (location = 0) in vec3 Position;
layout (location = 1) in vec2 Texcoord;
layout (location = 2) in vec3 Normal;

uniform mat4 transform;

smooth out vec3 pos;
smooth out vec2 texcoord;
smooth out vec3 normal;

void main(){
	gl_Position = vec4(Position, 1) * transform;

	pos = Position;
	texcoord = Texcoord;
	normal = Normal;
}