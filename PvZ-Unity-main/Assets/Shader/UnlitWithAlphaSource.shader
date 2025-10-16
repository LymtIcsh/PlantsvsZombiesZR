Shader "Custom/Particles/UnlitWithAlphaSource"
{
    Properties
    {
        _MainTex("Particle Texture", 2D) = "white" {}
        _Color("Tint Color", Color) = (1,1,1,1)
        [Enum(FromTextureAlpha,0, FromGrayScale,1)]
        _AlphaSource("Alpha Source", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
                float4 color      : COLOR;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv         : TEXCOORD0;
                float4 color      : COLOR;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            float4 _Color;
            float _AlphaSource;

            Varyings Vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color = IN.color * _Color;
                return OUT;
            }

            half4 Frag(Varyings IN) : SV_Target
            {
                half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                half gray = dot(tex.rgb, half3(0.299, 0.587, 0.114));
                half alpha = (_AlphaSource < 0.5) ? tex.a : gray;
                half4 result = half4(tex.rgb * IN.color.rgb, alpha * IN.color.a);
                return result;
            }

            ENDHLSL
        }
    }

    FallBack Off
}
