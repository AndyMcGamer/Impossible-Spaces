Shader "Custom/StencilMask"
{
    Properties
    {
        [IntRange] _StencilID ("Stencil ID", Range(0,255)) = 0
    }
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry-50"
        }

        Pass
        {
            Blend Zero One
            ZWrite Off
            ColorMask 0

            Stencil
            {
                Ref [_StencilID]
                Comp Always
                Pass Replace
            }  
        
        }
    }
}
