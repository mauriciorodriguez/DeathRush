// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-4939-OUT;n:type:ShaderForge.SFN_Tex2d,id:755,x:32201,y:32414,ptovrint:False,ptlb:GlobeTexture,ptin:_GlobeTexture,varname:node_755,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:94a7c24c00018e6418083264d589ebda,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9460,x:31543,y:32957,ptovrint:False,ptlb:EdgeTexture,ptin:_EdgeTexture,varname:node_9460,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3423335a02c320c489b50344deffdf45,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:2796,x:31809,y:32944,varname:node_2796,prsc:2|A-1822-OUT,B-9460-R;n:type:ShaderForge.SFN_Slider,id:1822,x:31429,y:32855,ptovrint:False,ptlb:OpacityClip,ptin:_OpacityClip,varname:node_1822,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.55,cur:-0.5,max:0.55;n:type:ShaderForge.SFN_Power,id:8030,x:31999,y:32956,varname:node_8030,prsc:2|VAL-2796-OUT,EXP-6524-OUT;n:type:ShaderForge.SFN_Vector1,id:6524,x:31841,y:33096,varname:node_6524,prsc:2,v1:4;n:type:ShaderForge.SFN_If,id:9284,x:32181,y:32814,varname:node_9284,prsc:2|A-8030-OUT,B-8994-OUT,GT-1296-OUT,EQ-9568-OUT,LT-9568-OUT;n:type:ShaderForge.SFN_Slider,id:8994,x:31785,y:32703,ptovrint:False,ptlb:EdgeWidth,ptin:_EdgeWidth,varname:node_8994,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.06,max:1;n:type:ShaderForge.SFN_Vector1,id:1296,x:31917,y:32772,varname:node_1296,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:9568,x:31917,y:32833,varname:node_9568,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:5470,x:32348,y:32990,varname:node_5470,prsc:2|A-9284-OUT,B-5008-RGB;n:type:ShaderForge.SFN_Color,id:5008,x:32137,y:33084,ptovrint:False,ptlb:EdgeColor,ptin:_EdgeColor,varname:node_5008,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Add,id:4939,x:32524,y:32728,varname:node_4939,prsc:2|A-6695-OUT,B-5470-OUT;n:type:ShaderForge.SFN_Multiply,id:6695,x:32425,y:32497,varname:node_6695,prsc:2|A-755-RGB,B-2243-OUT;n:type:ShaderForge.SFN_Slider,id:2243,x:32150,y:32704,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_2243,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:15,max:50;proporder:755-9460-1822-8994-5008-2243;pass:END;sub:END;*/

Shader "Deathrush/GlobeHologram" {
    Properties {
        _GlobeTexture ("GlobeTexture", 2D) = "white" {}
        _EdgeTexture ("EdgeTexture", 2D) = "white" {}
        _OpacityClip ("OpacityClip", Range(-0.55, 0.55)) = -0.5
        _EdgeWidth ("EdgeWidth", Range(0, 1)) = 0.06
        _EdgeColor ("EdgeColor", Color) = (0,1,1,1)
        _Brightness ("Brightness", Range(0, 50)) = 15
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GlobeTexture; uniform float4 _GlobeTexture_ST;
            uniform sampler2D _EdgeTexture; uniform float4 _EdgeTexture_ST;
            uniform float _OpacityClip;
            uniform float _EdgeWidth;
            uniform float4 _EdgeColor;
            uniform float _Brightness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _GlobeTexture_var = tex2D(_GlobeTexture,TRANSFORM_TEX(i.uv0, _GlobeTexture));
                float4 _EdgeTexture_var = tex2D(_EdgeTexture,TRANSFORM_TEX(i.uv0, _EdgeTexture));
                float node_9284_if_leA = step(pow((_OpacityClip+_EdgeTexture_var.r),4.0),_EdgeWidth);
                float node_9284_if_leB = step(_EdgeWidth,pow((_OpacityClip+_EdgeTexture_var.r),4.0));
                float node_9568 = 1.0;
                float3 emissive = ((_GlobeTexture_var.rgb*_Brightness)+(lerp((node_9284_if_leA*node_9568)+(node_9284_if_leB*0.0),node_9568,node_9284_if_leA*node_9284_if_leB)*_EdgeColor.rgb));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
