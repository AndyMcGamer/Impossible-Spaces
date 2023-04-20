Shader "Custom/DebugShader"
{
    Properties
    {
        [IntRange] _StencilID("Stencil ID", Range(0,255)) = 0
    }
        SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }

        Pass
        {
            ZWrite On
            ZTest Always

            Stencil
            {
                Ref[_StencilID]
                Comp Equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                return half4(1,1,0,1);
            }
            ENDCG
        }

    }
}
