Shader "PVB/Lit ColorMask Gradient V2"{
	Properties
	{
		[PerRendererData] _mainTex("Sprite Texture", 2D) = "white" {}
		_swapTex("Color Data", 2D) = "transparent" {}
		_color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200
			Cull Off


		CGPROGRAM
		#pragma surface surf ToonRamp

		sampler2D _Ramp;
		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float _AlphaSplitEnabled;

		sampler2D _SwapTex;

		fixed4 SampleSpriteTexture(float2 uv)
		{
			fixed4 color = tex2D(_MainTex, uv);
			if (_AlphaSplitEnabled)
				color.a = tex2D(_AlphaTex, uv).r;

			return color;
		}

		fixed4 frag(v2f IN) : SV_Target
		{
			fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
			c.rgb *= c.a;
			return c;
		}
			ENDCG

			}

				Fallback "Diffuse"
}