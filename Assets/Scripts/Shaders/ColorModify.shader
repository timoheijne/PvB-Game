// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PVB/ColorModify" {
	Properties{
		_Color1("Color R", Color) = (1,0,0,1) //Color R - opacity based on the Red channel of the Decal texture
		_Color2("Color G", Color) = (0,1,0,1) //Color G - opacity based on the Green channel of the Decal texture
		_Color3("Color B", Color) = (0,0,1,1) //Color B - opacity based on the Blue channel of the Decal texture
		_Color4("Color A", Color) = (1,1,1,0) //Color A - opacity based on the Alpha channel of the Decal texture
		_MainTex("Base (RGB)", 2D) = "white" { } //Base (RGB) - the base texture
		_DecalTex("Decal (RGBA)", 2D) = "white" { } //Decal (RGBA) - the decal texture
	}
		SubShader{

			Pass {
			 CGPROGRAM //Shader Start, Vertex Shader named vert, Fragment shader named frag
			 #pragma vertex vert
			 #pragma fragment frag
			 #include "UnityCG.cginc"
			 //Link properties to the shader
			 float4 _Color1;
			 float4 _Color2;
			 float4 _Color3;
			 float4 _Color4;
			 sampler2D _MainTex;
			 sampler2D _DecalTex;

			 struct v2f
			 {
			 float4  pos : SV_POSITION;
			 float2  uv : TEXCOORD0;
			 };

			 float4 _MainTex_ST;

			 v2f vert(appdata_base v)
			 {
			 v2f o;
			 o.pos = UnityObjectToClipPos(v.vertex); //Transform the vertex position
			 o.uv = TRANSFORM_TEX(v.texcoord, _MainTex); //Prepare the vertex uv
			 return o;
			 }

			 half4 frag(v2f i) : COLOR
			 {
				 float4 texcol = tex2D(_MainTex, i.uv); //base texture
				 float4 deccol = tex2D(_DecalTex, i.uv); //decal texture
				 float4 temp = float4(0,0,0,0);
				 temp += _Color1 * _Color1.a * deccol.r; //get the Red channels color
				 temp += _Color2 * _Color2.a * deccol.g; //get the Green channels color
				 temp += _Color3 * _Color3.a * deccol.b; //get the Blue channels color
				 temp += _Color4 * _Color4.a * deccol.a; //get the Alpha channels color
			 return lerp(texcol, temp, temp.a); //mix the base texture and the new decal
			 }

			 ENDCG //Shader End
			}
	}