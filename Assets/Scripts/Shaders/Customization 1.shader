Shader "PVB/Lit ColorMask Gradient"{
	Properties{
			_Color("Main Color", Color) = (0.5,0.5,0.5,1)
			_MainTex("Base (RGB)", 2D) = "white" {}
			_Mask("ColorMask (Red = Prim Green = Sec Blue = Ter)", 2D) = "black" {} // mask texture

			_ColorPrim1("Primary Color Top", Color) = (0.5,0.5,0.5,1) // primary color, replaces the red masked area
			_ColorPrim2("Primary Color Bottom", Color) = (0.5,0.5,0.5,1) // primary color, replaces the red masked area	
			_Weight1("Primary: Gradient Placing", Range(-0.8,0.2)) = -0.2 // blend value with the original texture

			_ColorSec1("Secondary Color Top", Color) = (0.5,0.5,0.5,1) // secondary color, replaces green masked area
			_ColorSec2("Secondary Color  Bottom", Color) = (0.5,0.5,0.5,1) // secondary color, replaces green masked area		
			_Weight2("Secondary: Gradient Placing",  Range(-0.8,0.2)) = -0.2 // blend value with the original texture

			_ColorTert1("Tertiary Color Top", Color) = (0.5,0.5,0.5,1)// tertiary color, replaces blue masked area
			_ColorTert2("Tertiary Color Bottom", Color) = (0.5,0.5,0.5,1)// tertiary color, replaces blue masked area		
			_Weight3("Tertiary: Gradient Placing",  Range(-0.8,0.2)) = -0.2 //// blend value with the original texture

	}

		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200
			Cull Off


		CGPROGRAM
		#pragma surface surf ToonRamp

		sampler2D _Ramp;

			// custom lighting function that uses a texture ramp based
			// on angle between light direction and normal
			#pragma lighting ToonRamp exclude_path:prepass
			inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
			{

				#ifndef USING_DIRECTIONAL_LIGHT
				lightDir = normalize(lightDir);
				#endif

				float d = dot(s.Normal, lightDir);
				float dChange = fwidth(d);
				float3 lightIntensity = smoothstep(0, dChange + 0.03, d) + 0.4;//+ (_ToonRamp);

				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * (lightIntensity) * (atten * 2);

				c.a = 0;
				return c;
			}


			sampler2D _MainTex;
			sampler2D _Mask; // mask texture
			float4 _Color, _ColorPrim1, _ColorPrim2, _ColorSec1, _ColorSec2, _ColorTert1, _ColorTert2;// custom colors
			float _Weight1, _Weight2, _Weight3;// original texture blend values

			struct Input {
				float2 uv_MainTex : TEXCOORD0;
				float3 viewDir;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				half3 m = tex2D(_Mask, IN.uv_MainTex); // mask based on the uvs
				float3 PrimaryColor = lerp(_ColorPrim2, _ColorPrim1, saturate(IN.uv_MainTex.y + _Weight1)) * m.r;
				float3 SecondaryColor = lerp(_ColorSec2, _ColorSec1, saturate(IN.uv_MainTex.y + _Weight2)) * m.g;
				float3 TertiaryColor = lerp(_ColorTert2, _ColorTert1, saturate(IN.uv_MainTex.y + _Weight3)) * m.b;

				float3 NonMasked = c.rgb * saturate(1 - m.r - m.g - m.b); // the part of the model thats not affected by the colour customisation

				half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));

				o.Albedo = NonMasked + PrimaryColor + SecondaryColor + TertiaryColor; // all parts added together form the new look for the model
				o.Emission = pow(rim, 5) * 3 * o.Albedo;
				o.Alpha = c.a;

			}
			ENDCG

			}

				Fallback "Diffuse"
}