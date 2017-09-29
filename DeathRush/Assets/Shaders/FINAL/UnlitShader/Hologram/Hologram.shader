// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32761,y:32751,varname:node_3138,prsc:2|emission-1518-OUT,alpha-2789-OUT,clip-6597-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32311,y:32703,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0,c4:0;n:type:ShaderForge.SFN_If,id:7386,x:32001,y:32855,cmnt: Sí los valores del noise son menores al valor EdgeWidth lo redondea a 1 y si no 0,varname:node_7386,prsc:2|A-4736-OUT,B-6614-OUT,GT-3256-OUT,EQ-6444-OUT,LT-6444-OUT;n:type:ShaderForge.SFN_Slider,id:6614,x:31513,y:32820,ptovrint:False,ptlb:EdgeWidth,ptin:_EdgeWidth,varname:node_6614,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.08,max:1;n:type:ShaderForge.SFN_Vector1,id:6444,x:31718,y:33206,cmnt:Blanco,varname:node_6444,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:3256,x:31718,y:33126,cmnt:Negro,varname:node_3256,prsc:2,v1:0;n:type:ShaderForge.SFN_Power,id:4736,x:31532,y:32971,cmnt:Exponencial para incrementar el noise,varname:node_4736,prsc:2|VAL-6597-OUT,EXP-2168-OUT;n:type:ShaderForge.SFN_Tex2d,id:5436,x:30968,y:33070,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_5436,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:6597,x:31266,y:33014,varname:node_6597,prsc:2|A-2451-OUT,B-5436-R;n:type:ShaderForge.SFN_Slider,id:2451,x:30847,y:32964,ptovrint:False,ptlb:OpacityClip,ptin:_OpacityClip,varname:node_2451,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.55,cur:0.4183543,max:0.6;n:type:ShaderForge.SFN_Vector1,id:2168,x:31392,y:33048,varname:node_2168,prsc:2,v1:4;n:type:ShaderForge.SFN_Multiply,id:2996,x:32340,y:32896,varname:node_2996,prsc:2|A-7386-OUT,B-7976-RGB;n:type:ShaderForge.SFN_Color,id:7976,x:32179,y:33049,ptovrint:False,ptlb:EdgeColor,ptin:_EdgeColor,varname:node_7976,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Add,id:1518,x:32588,y:32852,varname:node_1518,prsc:2|A-7241-RGB,B-2996-OUT;n:type:ShaderForge.SFN_Time,id:6389,x:31075,y:33303,varname:node_6389,prsc:2;n:type:ShaderForge.SFN_Cos,id:6751,x:31332,y:33311,varname:node_6751,prsc:2|IN-6389-T;n:type:ShaderForge.SFN_Multiply,id:5643,x:31573,y:33297,varname:node_5643,prsc:2|A-6751-OUT,B-6528-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6528,x:31371,y:33477,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_6528,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:4149,x:31777,y:33331,varname:node_4149,prsc:2|A-5643-OUT,B-6229-OUT,C-5006-OUT,D-2756-OUT;n:type:ShaderForge.SFN_Slider,id:4054,x:31050,y:33523,ptovrint:False,ptlb:MovPosX,ptin:_MovPosX,varname:node_4054,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6229,x:31470,y:33550,varname:node_6229,prsc:2|A-4054-OUT,B-2265-X;n:type:ShaderForge.SFN_FragmentPosition,id:2265,x:31099,y:33662,varname:node_2265,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5006,x:31470,y:33671,varname:node_5006,prsc:2|A-2265-Y,B-4935-OUT;n:type:ShaderForge.SFN_Slider,id:4935,x:31051,y:33830,ptovrint:False,ptlb:MovPosY,ptin:_MovPosY,varname:node_4935,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:2756,x:31470,y:33819,varname:node_2756,prsc:2|A-2265-Z,B-5945-OUT;n:type:ShaderForge.SFN_Slider,id:5945,x:31051,y:34018,ptovrint:False,ptlb:MovPosZ,ptin:_MovPosZ,varname:node_5945,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:5527,x:32024,y:33331,varname:node_5527,prsc:2|A-4149-OUT,B-3368-OUT;n:type:ShaderForge.SFN_Slider,id:3368,x:31710,y:33527,ptovrint:False,ptlb:Divisions,ptin:_Divisions,varname:node_3368,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:20;n:type:ShaderForge.SFN_Frac,id:1489,x:32232,y:33354,varname:node_1489,prsc:2|IN-5527-OUT;n:type:ShaderForge.SFN_OneMinus,id:2789,x:32433,y:33381,varname:node_2789,prsc:2|IN-1489-OUT;proporder:7241-6614-5436-2451-7976-6528-4054-4935-5945-3368;pass:END;sub:END;*/

Shader "Final/Hologram" {
    Properties {
        _Color ("Color", Color) = (0,1,0,0)
        _EdgeWidth ("EdgeWidth", Range(0, 1)) = 0.08
        _Noise ("Noise", 2D) = "white" {}
        _OpacityClip ("OpacityClip", Range(-0.55, 0.6)) = 0.4183543
        _EdgeColor ("EdgeColor", Color) = (1,0,0,1)
        _Speed ("Speed", Float ) = 1
        _MovPosX ("MovPosX", Range(0, 1)) = 0
        _MovPosY ("MovPosY", Range(0, 1)) = 1
        _MovPosZ ("MovPosZ", Range(0, 1)) = 0
        _Divisions ("Divisions", Range(0, 20)) = 10
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
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _EdgeWidth;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _OpacityClip;
            uniform float4 _EdgeColor;
            uniform float _Speed;
            uniform float _MovPosX;
            uniform float _MovPosY;
            uniform float _MovPosZ;
            uniform float _Divisions;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_6597 = (_OpacityClip+_Noise_var.r);
                clip(node_6597 - 0.5);
////// Lighting:
////// Emissive:
                float node_7386_if_leA = step(pow(node_6597,4.0),_EdgeWidth);
                float node_7386_if_leB = step(_EdgeWidth,pow(node_6597,4.0));
                float node_6444 = 1.0; // Blanco
                float3 emissive = (_Color.rgb+(lerp((node_7386_if_leA*node_6444)+(node_7386_if_leB*0.0),node_6444,node_7386_if_leA*node_7386_if_leB)*_EdgeColor.rgb));
                float3 finalColor = emissive;
                float4 node_6389 = _Time + _TimeEditor;
                return fixed4(finalColor,(1.0 - frac((((cos(node_6389.g)*_Speed)+(_MovPosX*i.posWorld.r)+(i.posWorld.g*_MovPosY)+(i.posWorld.b*_MovPosZ))*_Divisions))));
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
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _OpacityClip;
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
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_6597 = (_OpacityClip+_Noise_var.r);
                clip(node_6597 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
