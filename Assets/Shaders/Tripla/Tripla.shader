// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33477,y:32688,varname:node_4013,prsc:2|diff-5147-OUT,normal-5147-OUT,emission-5147-OUT,custl-4569-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:3863,x:32355,y:32880,varname:node_3863,prsc:2;n:type:ShaderForge.SFN_Append,id:9588,x:32584,y:32781,varname:node_9588,prsc:2|A-3863-Y,B-3863-Y;n:type:ShaderForge.SFN_Append,id:758,x:32584,y:32926,varname:node_758,prsc:2|A-3863-Z,B-3863-X;n:type:ShaderForge.SFN_Append,id:5454,x:32584,y:33057,varname:node_5454,prsc:2|A-3863-X,B-3863-Y;n:type:ShaderForge.SFN_Tex2d,id:6312,x:32803,y:32742,ptovrint:False,ptlb:Tex_X_Y,ptin:_Tex_X_Y,varname:node_6312,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ac039bcb68ae19e4690dc43ab4882858,ntxv:0,isnm:False|UVIN-9588-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:5147,x:33065,y:32737,varname:node_5147,prsc:2,chbt:0|M-1224-OUT,R-6312-RGB,G-2027-RGB,B-9258-RGB;n:type:ShaderForge.SFN_NormalVector,id:1148,x:32331,y:32599,prsc:2,pt:False;n:type:ShaderForge.SFN_Abs,id:3986,x:32489,y:32599,varname:node_3986,prsc:2|IN-1148-OUT;n:type:ShaderForge.SFN_Multiply,id:1224,x:32733,y:32559,varname:node_1224,prsc:2|A-3986-OUT,B-3986-OUT;n:type:ShaderForge.SFN_Tex2d,id:2027,x:32791,y:32938,ptovrint:False,ptlb:Tex_X_Z,ptin:_Tex_X_Z,varname:_Tex_X_Z,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ac039bcb68ae19e4690dc43ab4882858,ntxv:0,isnm:False|UVIN-758-OUT;n:type:ShaderForge.SFN_Tex2d,id:9258,x:32821,y:33160,ptovrint:False,ptlb:Tex_X_W,ptin:_Tex_X_W,varname:_Tex_X_W,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ac039bcb68ae19e4690dc43ab4882858,ntxv:0,isnm:False|UVIN-5454-OUT;n:type:ShaderForge.SFN_Vector3,id:624,x:32955,y:32606,varname:node_624,prsc:2,v1:0.9450981,v2:0.5137255,v3:0.003921569;n:type:ShaderForge.SFN_Vector3,id:4569,x:33183,y:32976,varname:node_4569,prsc:2,v1:0,v2:0,v3:0;proporder:6312-2027-9258;pass:END;sub:END;*/

Shader "Shader Forge/Tripla" {
    Properties {
        _Tex_X_Y ("Tex_X_Y", 2D) = "white" {}
        _Tex_X_Z ("Tex_X_Z", 2D) = "white" {}
        _Tex_X_W ("Tex_X_W", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Tex_X_Y; uniform float4 _Tex_X_Y_ST;
            uniform sampler2D _Tex_X_Z; uniform float4 _Tex_X_Z_ST;
            uniform sampler2D _Tex_X_W; uniform float4 _Tex_X_W_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 node_3986 = abs(i.normalDir);
                float3 node_1224 = (node_3986*node_3986);
                float2 node_9588 = float2(i.posWorld.g,i.posWorld.g);
                float4 _Tex_X_Y_var = tex2D(_Tex_X_Y,TRANSFORM_TEX(node_9588, _Tex_X_Y));
                float2 node_758 = float2(i.posWorld.b,i.posWorld.r);
                float4 _Tex_X_Z_var = tex2D(_Tex_X_Z,TRANSFORM_TEX(node_758, _Tex_X_Z));
                float2 node_5454 = float2(i.posWorld.r,i.posWorld.g);
                float4 _Tex_X_W_var = tex2D(_Tex_X_W,TRANSFORM_TEX(node_5454, _Tex_X_W));
                float3 node_5147 = (node_1224.r*_Tex_X_Y_var.rgb + node_1224.g*_Tex_X_Z_var.rgb + node_1224.b*_Tex_X_W_var.rgb);
                float3 normalLocal = node_5147;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
////// Lighting:
////// Emissive:
                float3 emissive = node_5147;
                float3 finalColor = emissive + float3(0,0,0);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
