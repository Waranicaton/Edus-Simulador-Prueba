Shader "Custom/Brillo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Brightness ("Brightness", Range(-1, 1)) = 0.0
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
            float _Zoom;
            float _Alpha;
            
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
                
                // Ajusta el brillo relativo al valor original
                col.rgb = col.rgb + col.rgb * _Brightness;
                
                col.a *= _Alpha;
                return col;
            }
            ENDCG
        }
    }
}