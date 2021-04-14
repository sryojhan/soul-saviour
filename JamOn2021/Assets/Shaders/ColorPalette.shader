Shader "Custom/ColorPalette"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_H("Hue", Range(0,1)) = 0.5
		_S("Saturation", Float) = 0.5
		_Treshold("Gray", Range(0,1)) = 0.5
	}
		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				float3 hsv_to_rgb(float3 HSV)
				{
					float3 RGB = HSV.z;

					float var_h = HSV.x * 6;
					float var_i = floor(var_h);   // Or ... var_i = floor( var_h )
					float var_1 = HSV.z * (1.0 - HSV.y);
					float var_2 = HSV.z * (1.0 - HSV.y * (var_h - var_i));
					float var_3 = HSV.z * (1.0 - HSV.y * (1 - (var_h - var_i)));
					if (var_i == 0) { RGB = float3(HSV.z, var_3, var_1); }
					else if (var_i == 1) { RGB = float3(var_2, HSV.z, var_1); }
					else if (var_i == 2) { RGB = float3(var_1, HSV.z, var_3); }
					else if (var_i == 3) { RGB = float3(var_1, var_2, HSV.z); }
					else if (var_i == 4) { RGB = float3(var_3, var_1, HSV.z); }
					else { RGB = float3(HSV.z, var_1, var_2); }

					return (RGB);
				}


				sampler2D _MainTex;
				float _ColorValue;
				float _S;
				float _H;
				float _Treshold;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);

					if (col.r == col.g && col.g == col.b)
						if(col.r <= _Treshold)
						return col;
					float v = (col.r + col.g + col.b) / 3;
					float3 hsv = float3(_H, _S, v);
					float3 rgb = hsv_to_rgb(hsv);
					col.rgb = fixed4(rgb.r, rgb.g, rgb.b, 1);

					return col;
				}
				ENDCG
			}
		}
}
