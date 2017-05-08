Shader "Custom/ShulkHairShader" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_Color("Color", Color) = (1,1,1,1)
	_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline Width", Range(0, 0.01)) = 0.0001
		_Brightness("Shadow Factor", Range(0, 5)) = 2 //How dark the colours should be shifted when not being hit by light
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		//The Outline Pass
		Pass
	{
		Name "OUTLINE"
		Tags{ "LightMode" = "Always" }
		Cull Front

		Blend One OneMinusDstColor //Make sure that the outline is not covering up the model

		CGPROGRAM

		//Define what the functions are
#pragma vertex vertexFunction
#pragma fragment fragmentFunction
#include "UnityCG.cginc" //Helper functions

		struct appdata
	{
		float4 vertex : POSITION; //xyzw
		float3 normal : NORMAL;
	};

	struct v2f
	{
		float4 position : SV_POSITION; //SV_ PS4 and DX9 needs it
		float2 uv : TEXCOORD0;
	};

	//Redefine stuff in properties so cg know what it is
	float4 _OutlineColor;
	float _Outline;
	float4 _Color;

	//v2f (Take to screen) (vertex to fragment) AKA build object
	//Modifies vertexes. SO can animate here
	v2f vertexFunction(appdata v)
	{
		v2f o;


		o.position = mul(UNITY_MATRIX_MVP, v.vertex);

		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.position.xy += offset * o.position.z * _Outline;


		return o;
	}

	//Draw pixil to the screen
	//Modifies pixils
	fixed4 fragmentFunction(v2f IN) : SV_Target //SV_Target so targets and puts it to the screen
	{
		return _OutlineColor;
	}

		ENDCG
	}

		//Pass for the model
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float3 normal : NORMAL;
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float4 _OutlineColor;
	float _Outline;
	float _Brightness;
	float4 _Color;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.uv;

		//Convert normals to world space
		o.normal = mul(float4(v.normal, 0.0), unity_ObjectToWorld).xyz;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 texCol = tex2D(_MainTex, i.uv);

	float4 newColour = texCol * _Color;;

	float3 normalDirection = normalize(i.normal);
	float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

	//Adjust the colours so that all the colours in a certain range are all the same colour to achieve the cel shading effect




	//If the pixil is hit by the light
	if (dot(normalDirection, lightDirection) > 0)
	{

	}
	else //In shadow
	{
		newColour.x = newColour.x / _Brightness;
		newColour.y = newColour.y / _Brightness;
		newColour.z = newColour.z / _Brightness;
	}

	return newColour;

	}
		ENDCG
	}
	}
}