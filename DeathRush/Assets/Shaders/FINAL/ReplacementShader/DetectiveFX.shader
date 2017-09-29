Shader "Parcial2/DetectiveFX"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DetectiveMap ("Detective Map", 2D) = "white"  {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _DetectiveMap;

			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				half2 fixedUV = i.uv * half2(1, -1);
				fixed4 detCol = tex2D(_DetectiveMap, fixedUV);
				return col + detCol;
			}
			ENDCG
		}
	}
}
