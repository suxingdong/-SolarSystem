Shader "shadertoy/TotalNoob" {  //https://www.shadertoy.com/view/XdlSDs
	Properties{
		iMouse("Mouse Pos", Vector) = (100,100,0,0)
		iChannel0("iChannel0", 2D) = "white" {}
	iChannelResolution0("iChannelResolution0", Vector) = (100,100,0,0)
	}

		CGINCLUDE
#include "UnityCG.cginc"   
#pragma target 3.0      
#pragma glsl

#define vec2 float2
#define vec3 float3
#define vec4 float4
#define mat2 float2x2
#define iGlobalTime _Time.y
		//  		#define mod fmod  // mod = sign*fmod
#define mix lerp
#define atan atan2
#define fract frac 
#define texture2D tex2D
		// 屏幕的尺寸
#define iResolution _ScreenParams
		// 屏幕中的坐标，以pixel为单位
#define gl_FragCoord ((_iParam.srcPos.xy/_iParam.srcPos.w)*_ScreenParams.xy) 

#define PI2 6.28318530718
#define pi 3.14159265358979
#define halfpi (pi * 0.5)
#define oneoverpi (1.0 / pi)

		fixed4 iMouse;
	sampler2D iChannel0;
	fixed4 iChannelResolution0;

	struct v2f {
		float4 pos : SV_POSITION;
		float4 srcPos : TEXCOORD0;
	};

	//   precision highp float;
	v2f vert(appdata_base v) {
		v2f o;
		/*o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.srcPos = o.pos;// ComputeScreenPos(o.pos);
		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);
		o.srcPos.xy += offset * o.pos.z * 0.1;*/
		/*
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.srcPos = mul(UNITY_MATRIX_MVP, v.vertex);
		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);
		o.srcPos.xy += offset * o.pos.z *0.1;
		return o;*/

		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.srcPos = mul(UNITY_MATRIX_MVP, v.vertex);
		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);
		o.srcPos.xy += offset * o.pos.z *0.1;
		o.pos.xy += offset * o.pos.z * 0.1;
		return o;
	}

	vec4 main(v2f _iParam);

	fixed4 frag(v2f _iParam) : COLOR0{
		//return fixed4(1,1,1,1);
		return main(_iParam);
	}

		vec4 main(v2f _iParam) {
		vec2 p = _iParam.srcPos.xy;// (2.0*((_iParam.srcPos.xy / _iParam.srcPos.w)*_ScreenParams.xy).xy - _ScreenParams.xy) / _ScreenParams.y;
		//float tau = 3.1415926535*2.0;
		//float a = atan(p.x, p.y);
		float r = length(p)*0.75;
		//vec2 uv = vec2(a / tau, r);

		//get the color
		//float xCol = (uv.x - (iGlobalTime / 3.0)) * 3.0;
		//xCol = sign(xCol)*fmod(xCol, 3.0);
		vec3 horColour = vec3(1, 1, 1);
		/*
		if (xCol < 1.0) {
			horColour.r += 1.0 - xCol;
			horColour.g += xCol;
		}
		else if (xCol < 2.0) {
			xCol -= 1.0;
			horColour.g += 1.0 - xCol;
			horColour.b += xCol;
		}
		else {
			xCol -= 2.0;
			horColour.b += 1.0 - xCol;
			horColour.r += xCol;
		}
		*/
		// draw color beam
		//uv = (2.0 * uv) - 1.0;
		r = 2.0*r - 1.0;
		float beamWidth = abs(1.0 / (30.0 * r));
		//float beamWidth = (0.7 + 0.5*cos(uv.x*10.0*tau*0.15*clamp(floor(5.0 + 10.0*cos(iGlobalTime)), 0.0, 10.0))) * abs(1.0 / (30.0 * uv.y));
	
		vec3 horBeam = vec3(beamWidth, beamWidth, beamWidth);
		vec4 gl_FragColor = vec4((horBeam), 1.0);
		//return float4(1, 1, 1, 1);
		return gl_FragColor;
	}

	ENDCG
		SubShader{
		Pass{
		CGPROGRAM
#pragma vertex vert    
#pragma fragment frag    
#pragma fragmentoption ARB_precision_hint_fastest     
		ENDCG
	}
	}
		FallBack Off
}
