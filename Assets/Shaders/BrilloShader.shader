Shader "Custom/BrilloShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Brightness ("Brightness", Range(-1, 1)) = 0.5
        _Threshold ("Threshold", Range(0, 1)) = 0.1
        _Zoom ("Zoom Level", Range(0.1, 10.0)) = 1.0
        _Alpha ("Alpha", Range(0, 1)) = 1
    }
     
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100
         
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
             
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
             
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
             
            sampler2D _MainTex;
            float _Brightness;
            float _Threshold;
            float _Zoom;
            float _Alpha;
            // Función logarítmica de ajuste de brillo
            float LogBrightness(float brightness, float threshold)
            {
                return (brightness < threshold) ? (threshold * (1 - exp(-brightness / threshold))) : brightness;
            }
             
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Calcula las coordenadas de textura relativas al centro
                float2 centerUV = v.uv - 0.5;
                // Aplica el zoom centrado en el centro de la textura
                o.uv = centerUV * _Zoom + 0.5;
                return o;
            }
             
            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);
                float redComponent = col.r;
                col.a *= _Alpha;
                if (redComponent > _Threshold)
                {
                    // Aplica el ajuste de brillo utilizando la función logarítmica
                    col.rgb = LogBrightness(col.rgb, _Threshold) + (_Brightness * (1.0 - LogBrightness(1.0 - col.rgb, _Threshold)));
                    col.a *= _Alpha;
                }
                
                return col;
            }
            ENDCG
        }
    }
}
