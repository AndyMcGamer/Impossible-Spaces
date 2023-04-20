Shader "Custom/DepthClear"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        ZTest Always
        ZWrite On

        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry-25"
        }
        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                fixed2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
 
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
#if UNITY_REVERSED_Z
                o.vertex.z = 1;
#else
                o.vertex.z = 0;
#endif
                o.uv = v.uv;
                return o;
            }
 
            #if !defined(SHADER_API_D3D9) && !defined(SHADER_API_D3D11_9X)
            fixed frag(v2f i) : SV_Depth
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return 0;
            }
            #else
            void frag(v2f i, out float4 dummycol:COLOR, out float depth : DEPTH)
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                dummycol = col;
 
                depth = 0;
            }
            #endif
            ENDCG
        }
    }
}
