$ShaderType FragmentShader;
#version 330

uniform sampler2D tex;

smooth in vec3 pos;
smooth in vec2 texcoord;
smooth in vec3 normal;

out vec4 outColor;

void main(){
	outColor = texture(tex, texcoord);
}