$ShaderType FragmentShader;
#version 330

uniform sampler2D tex;

uniform vec3 landColor;
uniform vec3 waterColor;

smooth in vec3 pos;
smooth in vec2 texcoord;
smooth in vec3 normal;

out vec4 outColor;

float shape(float r) {
	return (1.570796 - acos(2 * r)) * 0.31832 / r;
}

void main(){
	vec2 tc = texcoord - vec2(0.5);
	float r = length(tc);

	if (r >= 0.499) outColor.a = 0;
	else {
		outColor = texture(tex, tc * shape(r) + vec2(0.5));
		outColor.rgb = mix(landColor, waterColor, outColor.r);
	}

}