Shader "Custom/NitroTubeReductor" {
	Properties
	{
		_Alpha("Alpha", range(0,1)) = 1
		_BaseTex("Base Alpha",2D) = "white"{}
		_MainTex("Texture", 2D) = "white" {}

	}
		SubShader
	{
		Pass
		{
			AlphaTest LEqual[_Alpha]
			SetTexture[_BaseTex]
			SetTexture[_MainTex]
			{
				Combine Texture, Previous
			}
		}
	}
}
