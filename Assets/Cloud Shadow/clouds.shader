// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32954,y:33178,varname:node_2865,prsc:2|emission-131-OUT;n:type:ShaderForge.SFN_TexCoord,id:4219,x:31878,y:33217,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:6699,x:31573,y:33483,varname:node_6699,prsc:2,spu:1,spv:0|UVIN-6402-UVOUT,DIST-4238-OUT;n:type:ShaderForge.SFN_Tex2d,id:3346,x:31792,y:33513,ptovrint:False,ptlb:node_3346,ptin:_node_3346,varname:_node_3346,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c85ea90507b3a5745afaf9e10e56c5b4,ntxv:0,isnm:False|UVIN-6699-UVOUT;n:type:ShaderForge.SFN_Time,id:4906,x:31272,y:33678,varname:node_4906,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4238,x:31453,y:33622,varname:node_4238,prsc:2|A-6425-OUT,B-4906-TSL;n:type:ShaderForge.SFN_Vector1,id:6947,x:31272,y:33622,varname:node_6947,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:6630,x:32734,y:33370,varname:node_6630,prsc:2|A-131-OUT;n:type:ShaderForge.SFN_ScreenPos,id:6402,x:31342,y:33128,varname:node_6402,prsc:2,sctp:0;n:type:ShaderForge.SFN_Tex2d,id:2749,x:32200,y:33242,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4219-UVOUT;n:type:ShaderForge.SFN_Lerp,id:131,x:32542,y:33240,varname:node_131,prsc:2|A-2749-RGB,B-9963-RGB,T-1633-OUT;n:type:ShaderForge.SFN_Color,id:9963,x:32200,y:33062,ptovrint:False,ptlb:CloudColor,ptin:_CloudColor,varname:_CloudColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:1633,x:32379,y:33464,varname:node_1633,prsc:2|A-3346-A,B-2660-OUT;n:type:ShaderForge.SFN_Slider,id:2660,x:31976,y:33655,ptovrint:False,ptlb:CloudOpacity,ptin:_CloudOpacity,varname:_CloudOpacity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:6425,x:31272,y:33893,ptovrint:False,ptlb:Spped_cloud,ptin:_Spped_cloud,varname:_Spped_cloud,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_FragmentPosition,id:754,x:31069,y:33405,varname:node_754,prsc:2;n:type:ShaderForge.SFN_Append,id:2374,x:31300,y:33405,varname:node_2374,prsc:2|A-754-X,B-754-Z;proporder:3346-2749-9963-2660-6425;pass:END;sub:END;*/

Shader "Shader Forge/clouds" {
    Properties {
        _node_3346 ("node_3346", 2D) = "white" {}
        _MainTex ("MainTex", 2D) = "white" {}
        _CloudColor ("CloudColor", Color) = (0.5,0.5,0.5,1)
        _CloudOpacity ("CloudOpacity", Range(0, 1)) = 0
        _Spped_cloud ("Spped_cloud", Range(0, 10)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_3346; uniform float4 _node_3346_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _CloudColor;
            uniform float _CloudOpacity;
            uniform float _Spped_cloud;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_4906 = _Time;
                float2 node_6699 = ((sceneUVs * 2 - 1).rg+(_Spped_cloud*node_4906.r)*float2(1,0));
                float4 _node_3346_var = tex2D(_node_3346,TRANSFORM_TEX(node_6699, _node_3346));
                float3 node_131 = lerp(_MainTex_var.rgb,_CloudColor.rgb,(_node_3346_var.a*_CloudOpacity));
                float3 emissive = node_131;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
