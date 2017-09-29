Shader "Final/Saturation"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "white" {}
		_RampTex ("RampTex", 2D) = "white" {}
        _Saturation ("Saturation", Range(0, 5)) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		Tags {
            "Queue"="Geometry+1"
            "RenderType"="Opaque"
			"ImageVehicle"="ImageVehicle"
        }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"
	
			sampler2D _MainTex;
			sampler2D _RampTex; float4 _RampTex_ST;
			float _Intensity;
            float _Saturation;

			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
/*
				fixed greyscale = (col.r + col.g + col.b) / 3;
				float2 stepPost = floor(float3(greyscale,grayscale,grayscale) * _PosterizationSteps) / (_PosterizationSteps - 1).rr;
                float4 rampCol = tex2D(_RampTex,TRANSFORM_TEX(stepPost, _RampTex));
                float3 emissive = lerp(rampCol.rgb,col.rgb,_Slider);
                float3 finalColor = emissive;
                return fixed4(finalColor,1*/

				float greyscale = (col.r + col.g + col.b) / 3;
				col.rgb = lerp(float3(greyscale, greyscale, greyscale), col.rgb, _Saturation);

				return col;
			}
			ENDCG
		}
	}
}
