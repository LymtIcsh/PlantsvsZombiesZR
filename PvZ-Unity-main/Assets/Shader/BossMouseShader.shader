Shader "Custom/BossMouseShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Add Color", Color) = (1,1,1,0.7)
        _TintColor("Tint Color", Color) = (1, 1, 1, 1)
        _BlendColor("Blend Color", Color) = (1, 1, 1, 1)
    }
    
    SubShader
    {
        Tags { "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            int _BeAttack;
            fixed4 _TintColor;
            fixed4 _BlendColor;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                
                    fixed4 highlight = tex + _Color * tex.a;
                    tex = lerp(tex, highlight, _Color.a);
                

                tex.rgb *= _TintColor.rgb;
                tex.a *= _TintColor.a;

                tex.rgb *= _BlendColor.rgb;
                tex.a *= _BlendColor.a;

                return tex;
            }
            ENDCG
        }
    }
}
