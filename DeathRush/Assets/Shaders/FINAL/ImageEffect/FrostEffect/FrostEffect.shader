Shader "Final/FrostEffect"
{
	Properties
	{
		_MainTex ("Base", 2D) = "white" {}
		_BlendTex ("Texture", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Color("Color", Color) = (1,1,1,1)
		_FadeEffect("FadeEffect", Range(0, 1)) = 0
		_EdgeIntensity("EdgeIntensity", Range(1, 20)) = 1
		_Transparency("Transparency", Range(0, 2)) = 0
		_Distortion("Distortion", Range(0, 20)) = 0

	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag		
			#include "UnityCG.cginc"


			sampler2D _MainTex;
			sampler2D _BlendTex;
			sampler2D _BumpMap;
			half4 _Color;
			float _FadeEffect;

			float _EdgeIntensity;
			float _Transparency;
			float _Distortion;

			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed4 blendColor = tex2D(_BlendTex, i.uv);
				blendColor.a = blendColor.a + (_FadeEffect * 2 - 1);

				blendColor.a = saturate(blendColor.a * _EdgeIntensity - (_EdgeIntensity - 1) * 0.5);

				half2 bump = UnpackNormal(tex2D(_BumpMap, i.uv)).rg;
				fixed4 mainColor = tex2D(_MainTex, i.uv + bump * blendColor.a * _Distortion);

				float4 overlayColor = blendColor;

				float4 mainColorFade = lerp(mainColor,mainColor + _Color,_FadeEffect);


				overlayColor.rgb = mainColor.rgb * (blendColor.rgb + 0.5) * (blendColor.rgb + 0.5);
		
				blendColor = lerp(blendColor,overlayColor,_Transparency);


				return lerp(mainColorFade, blendColor, blendColor.a);

			}
			ENDCG
		}
	}
}
