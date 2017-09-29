// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-5921-OUT,alpha-8471-OUT,clip-4845-OUT;n:type:ShaderForge.SFN_Tex2d,id:1540,x:31984,y:32515,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1540,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:867921500b57e574d9010526c03498d6,ntxv:0,isnm:False|UVIN-8837-OUT;n:type:ShaderForge.SFN_Tex2d,id:7395,x:31984,y:32712,ptovrint:False,ptlb:DetailTex,ptin:_DetailTex,varname:node_7395,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:867921500b57e574d9010526c03498d6,ntxv:0,isnm:False|UVIN-4286-OUT;n:type:ShaderForge.SFN_Multiply,id:3594,x:32368,y:32615,varname:node_3594,prsc:2|A-1540-RGB,B-7395-RGB,C-6190-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6190,x:32142,y:32776,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_6190,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_TexCoord,id:855,x:31254,y:32539,varname:node_855,prsc:2,uv:0;n:type:ShaderForge.SFN_Color,id:4726,x:32315,y:32819,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4726,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07586193,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Add,id:5921,x:32551,y:32739,varname:node_5921,prsc:2|A-3594-OUT,B-4726-RGB;n:type:ShaderForge.SFN_Slider,id:8401,x:31668,y:33141,ptovrint:False,ptlb:ReduceLiquid,ptin:_ReduceLiquid,varname:node_8401,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:4845,x:32431,y:33110,varname:node_4845,prsc:2|A-855-U,B-9443-OUT;n:type:ShaderForge.SFN_RemapRange,id:9443,x:32170,y:33158,varname:node_9443,prsc:2,frmn:0,frmx:1,tomn:0.65,tomx:2.3|IN-8401-OUT;n:type:ShaderForge.SFN_Slider,id:8471,x:32274,y:32992,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_8471,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.9263624,max:1;n:type:ShaderForge.SFN_Add,id:8837,x:31546,y:32496,varname:node_8837,prsc:2|A-855-UVOUT,B-2899-OUT;n:type:ShaderForge.SFN_Multiply,id:2899,x:31304,y:32713,varname:node_2899,prsc:2|A-3265-OUT,B-1401-T;n:type:ShaderForge.SFN_Append,id:3265,x:31051,y:32696,varname:node_3265,prsc:2|A-8898-OUT,B-484-OUT;n:type:ShaderForge.SFN_Slider,id:484,x:30672,y:32781,ptovrint:False,ptlb:MainTexSpeedY,ptin:_MainTexSpeedY,varname:node_484,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:8898,x:30672,y:32644,ptovrint:False,ptlb:MainTexSpeedX,ptin:_MainTexSpeedX,varname:node_8898,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:-0.3,max:5;n:type:ShaderForge.SFN_Time,id:1401,x:31051,y:32852,varname:node_1401,prsc:2;n:type:ShaderForge.SFN_Add,id:4286,x:31546,y:32738,varname:node_4286,prsc:2|A-855-UVOUT,B-2396-OUT;n:type:ShaderForge.SFN_Multiply,id:2396,x:31304,y:32948,varname:node_2396,prsc:2|A-1401-T,B-8696-OUT;n:type:ShaderForge.SFN_Append,id:8696,x:31051,y:33064,varname:node_8696,prsc:2|A-3257-OUT,B-8280-OUT;n:type:ShaderForge.SFN_Slider,id:8280,x:30684,y:33140,ptovrint:False,ptlb:DetailTexSpeedY,ptin:_DetailTexSpeedY,varname:node_8280,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:3257,x:30684,y:33040,ptovrint:False,ptlb:DetailTexSpeedX,ptin:_DetailTexSpeedX,varname:node_3257,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:-0.5,max:5;proporder:1540-8898-484-7395-3257-8280-4726-6190-8471-8401;pass:END;sub:END;*/

Shader "Final/NitroLiquid" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _MainTexSpeedX ("MainTexSpeedX", Range(-5, 5)) = -0.3
        _MainTexSpeedY ("MainTexSpeedY", Range(-5, 5)) = 0
        _DetailTex ("DetailTex", 2D) = "white" {}
        _DetailTexSpeedX ("DetailTexSpeedX", Range(-5, 5)) = -0.5
        _DetailTexSpeedY ("DetailTexSpeedY", Range(-5, 5)) = 0
        _Color ("Color", Color) = (0.07586193,0,1,1)
        _Brightness ("Brightness", Float ) = 5
        _Opacity ("Opacity", Range(0, 1)) = 0.9263624
        _ReduceLiquid ("ReduceLiquid", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DetailTex; uniform float4 _DetailTex_ST;
            uniform float _Brightness;
            uniform float4 _Color;
            uniform float _ReduceLiquid;
            uniform float _Opacity;
            uniform float _MainTexSpeedY;
            uniform float _MainTexSpeedX;
            uniform float _DetailTexSpeedY;
            uniform float _DetailTexSpeedX;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                clip((i.uv0.r*(_ReduceLiquid*1.65+0.65)) - 0.5);
////// Lighting:
////// Emissive:
                float4 node_1401 = _Time + _TimeEditor;
                float2 node_8837 = (i.uv0+(float2(_MainTexSpeedX,_MainTexSpeedY)*node_1401.g));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_8837, _MainTex));
                float2 node_4286 = (i.uv0+(node_1401.g*float2(_DetailTexSpeedX,_DetailTexSpeedY)));
                float4 _DetailTex_var = tex2D(_DetailTex,TRANSFORM_TEX(node_4286, _DetailTex));
                float3 emissive = ((_MainTex_var.rgb*_DetailTex_var.rgb*_Brightness)+_Color.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_Opacity);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _ReduceLiquid;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                clip((i.uv0.r*(_ReduceLiquid*1.65+0.65)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
