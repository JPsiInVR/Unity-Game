Shader "InvertColor" { 
    Properties { 
        _MainTex ("Alpha (A) only", 2D) = "white" {} 
        _AlphaCutOff ("Alpha cut off", Range(0,1)) = 1 
    } 
    SubShader { 
        Tags { 
            "RenderPipeline" = "UniversalRenderPipeline"
            "Queue" = "Overlay"
} 
        Pass { 
            Fog { Mode Off } 
            Blend OneMinusDstColor Zero
            ZWrite Off
            ZTest Always
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            CBUFFER_START(UnityPerMaterial)
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _AlphaCutOff;
            CBUFFER_END

            struct appdata
            {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
             
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            fixed4 frag( v2f i ) : SV_Target
            {
                fixed4 c = 1;
                c.a = 1 - tex2D(_MainTex, i.uv).a;
                clip(_AlphaCutOff -  c.a);
                return c;
            }
            ENDCG
        }
    }
}