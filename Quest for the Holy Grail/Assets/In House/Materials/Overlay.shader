Shader "Custom/Overlay"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" }
        LOD 200

        Pass {
            Cull Off
            ZTest Always
            ZWrite On
            ColorMask RGB

            SetTexture [_MainTex]
            {
                Combine Texture * Primary
            }
        }
    }
    FallBack "Diffuse"
}
