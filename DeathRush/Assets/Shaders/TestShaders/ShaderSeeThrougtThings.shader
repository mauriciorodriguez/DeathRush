Shader "Custom/SeeThrougtThings" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
		_Color ("Color", color) = (0,0,0,0)
	}
	SubShader {
		Tags { "Queue" = "Geometry+1" }
		Pass
		{
			ZTest Greater //Greater, Less, GEqual, LEqual, Equal, NotEqual, Always
			Color[_Color]
		}
	} 
}
