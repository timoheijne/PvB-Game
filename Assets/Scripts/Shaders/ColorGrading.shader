Shader "PVB/ColorGrading" {

	Properties{
		_mainTex("Base (RGB)", 2D) = "white" {}
		_secondTex("Color Gradient", 2D) = "white"{}
		_lightTex("The amount of light", 2D) = "white"{}
		_maskSkin("The Skin Mask", 2D) = "white"{}
		_maskEye("The Eye Mask", 2D) = "white"{}
		_maskShirt("The Shirt Mask", 2D) = "white"{}
		_maskPant("The Pants Mask", 2D) = "white"{}
		_maskShoe("The Shoes Mask", 2D) = "white"{}
		_maskGlove("The Gloves Mask", 2D) = "white"{}
		_samplePos("Position In Gradient", Range(0,1)) = 0.25
	}

		SubShader{

			Tags {"Queue" = "Transparent" }

			Pass {
				ZWrite On
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform sampler2D _mainTex;
				uniform sampler2D _secondTex;
				uniform sampler2D _lightTex;

				uniform sampler2D _maskSkin;
				uniform sampler2D _maskEye;
				uniform sampler2D _maskShirt;
				uniform sampler2D _maskPant;
				uniform sampler2D _maskShoe;
				uniform sampler2D _maskGlove;

				float _samplePos;

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal : NORMAL;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					half3 worldNormal : TEXCOORD1;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.worldNormal = UnityObjectToWorldNormal(v.normal);
					return o;
				}

				float4 frag(v2f i) : COLOR {
					float2 uv = float2(_samplePos,0);
					float4 c = tex2D(_mainTex, i.uv);
					float4 g = tex2D(_secondTex, uv);
					float intensity = tex2D(_lightTex, uv);

					float4 result = c * g * intensity;
					return result;
				}

				ENDCG
			}
		}
}