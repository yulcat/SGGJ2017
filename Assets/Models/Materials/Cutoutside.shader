// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Cutoutside" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_CutColor("Cutout Color", Color) = (0,0,0,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
		cull front
 
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
	
		struct vertexInput
		{
			float4 vertex : POSITION;
		};
	
		struct vertexOutput
		{
			float4 pos : POSITION;
			float4 worldPos : TEXCOORD0;
		};

		fixed4 _CutColor;
	
		vertexOutput vert(vertexInput v) {
			vertexOutput o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.worldPos = mul(unity_ObjectToWorld, v.vertex);
			return o;
		}
	
		fixed4 frag( vertexOutput i) : COLOR {
			if(i.worldPos.z < -3) discard;
			return _CutColor;
		}
	
		ENDCG
		}

		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			if(IN.worldPos.z < -3) discard;
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
