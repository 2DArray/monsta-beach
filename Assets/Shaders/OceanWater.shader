Shader "Custom/OceanWater" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_WaveAmp ("Wave Amp", Float) = 0
		_NormalStr ("Normal Str", Float) = 0
		_TimeScale ("Timescale", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _WaveAmp;
		half _NormalStr;
		half _TimeScale;

		// Vertex Shader
		void vert (inout appdata_full v) {
			half wave1 = sin(_Time.y*_TimeScale+v.vertex.x*1.27+v.vertex.y*1.13)*.5+.5;

			half wave2 = cos(_Time.y*_TimeScale+v.vertex.x*.77-v.vertex.y*.83)*.5+.5;

			half wave3 = sin(_Time.y*_TimeScale-v.vertex.x*.21-v.vertex.y*.13);

			v.vertex.z+=wave1*wave2*wave3*_WaveAmp;

			v.normal.x+=(wave1*wave3*2-1)*_NormalStr;
			v.normal.y+=(wave2*wave3*2-1)*_NormalStr;

			v.normal.xyz=normalize(v.normal.xyz);
		}

		// Pixel/Surface Shader
		void surf (Input IN, inout SurfaceOutputStandard o) {
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
