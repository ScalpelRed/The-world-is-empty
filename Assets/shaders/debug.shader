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
	gl_Position.z /= 127.0;

	pos = Position;
	texcoord = Texcoord;
	normal = Normal;
}



$ShaderType FragmentShader;
#version 330

smooth in vec3 pos;
smooth in vec2 texcoord;
smooth in vec3 normal;

smooth out vec4 outColor;

void main(){
	outColor = vec4(pos, 1);
}