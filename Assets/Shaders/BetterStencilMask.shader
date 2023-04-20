Shader "Custom/BetterStencilMask"
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
            "Queue" = "Geometry-100"
        }

        Pass
        {
            Blend Zero One
            ZWrite Off
            ColorMask 0

            Stencil
            {
                Ref[_StencilID]
                Comp Always
                Pass Replace
                ZFail Replace
            }

        }
        //Pass
        //{
        //    ColorMask 0
        //    ZWrite On
        //    Stencil
        //    {
        //        Ref[_StencilMask]
        //        Comp equal
        //    }

        //    CGPROGRAM
        //    #pragma vertex vert
        //    #pragma fragment frag

        //    struct appdata
        //    {
        //        float4 vertex : POSITION;
        //    };

        //    struct v2f
        //    {
        //        float4 pos : SV_POSITION;
        //    };

        //    v2f vert(appdata v)
        //    {
        //        v2f o;
        //        o.pos = UnityObjectToClipPos(v.vertex);
        //        //clear the depth buffer by setting Z to the far plane
        //        #if UNITY_REVERSED_Z
        //        o.pos.z = 1;
        //        #else
        //        o.pos.z = 0;
        //        #endif
        //        return o;
        //    }

        //    half4 frag(v2f i) : COLOR
        //    {
        //        return half4(1,1,0,1);
        //    }
        //    ENDCG
        //}
    }
}
