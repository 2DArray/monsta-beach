Shader "Custom/SandParticles" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Cutoff ("Cutoff", Range(0,1)) = 0.5
		_Emission ("Emission", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			fixed4 color:COLOR;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _Emission;
		half _Cutoff;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color * IN.color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Emission = o.Albedo*_Emission;
			o.Alpha = c.a;

			clip(c.a-_Cutoff);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
