Shader "Custom/URP/Pixelate"
{
    Properties
    {
        _MainTex    ("Base Texture", 2D) = "white" {}
        _PixelSize  ("Block Size (px)", Float) = 8.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "Pixelate"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            sampler2D _MainTex;
            float4   _MainTex_TexelSize; // x=1/width, y=1/height
            float    _PixelSize;         // ���ؿ��С������Ļ����Ϊ��λ��

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv         = v.uv;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                // ������Ļ�ֱ���
                float2 resolution = float2(1.0 / _MainTex_TexelSize.x,
                                           1.0 / _MainTex_TexelSize.y);

                // UV �ռ��µġ��顱�ߴ�
                float2 blockUV = _PixelSize / resolution;

                // �� UV ��ɢ��������
                float2 uvq = floor(i.uv / blockUV) * blockUV + blockUV * 0.5;

                // ����
                return tex2D(_MainTex, uvq);
            }
            ENDHLSL
        }
    }
}
