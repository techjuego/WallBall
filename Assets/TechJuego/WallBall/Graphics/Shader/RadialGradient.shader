Shader "TechJuego/RadialGradient" {
    Properties{
         _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color1("Color 1", Color) = (1, 1, 1, 1)
        _Color2("Color 2", Color) = (0, 0, 0, 1)
        _Radius("Radius", Range(0, 1)) = 0.5
    }

        SubShader{
            Tags {"RenderType" = "Opaque"}
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
                      sampler2D _MainTex;
                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };
                
                float4 _Color1;
                float4 _Color2;
                float _Radius;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                float4 frag(v2f i) : SV_Target {
                    float2 center = float2(0.5, 0.5);
                    float dist = length(i.uv - center);
                    float4 col = lerp(_Color1, _Color2, smoothstep(0, _Radius, dist));
                    return col;
                }
                ENDCG
            }
    }
        FallBack "Diffuse"
}
