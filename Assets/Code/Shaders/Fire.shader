Shader "Unlit/Fire"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texure", 2D) = "black" {}
		_Speed("Noise panning speed", Float) = 1
		_NoiseStepMin("Outer Fire Tolerance", Range(0, 1)) = 0.1
		_NoiseStepMax("Inner Fire Tolerance", Range(0, 1)) = 0.2
		_DisplacementForce("Displacement Force", Range(0, 1)) = 0.15
		_InnerFireColor("Inner Fire Color", Color) = (1, 1, 1, 1)
		_OuterFireColor("Outer Fire Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
		ZWrite Off
		Lighting Off
		Fog { Mode Off }

		Blend SrcAlpha OneMinusSrcAlpha

        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
			sampler2D _NoiseTex;
			float _Speed;
			float _NoiseStepMin;
			float _NoiseStepMax;
			float _DisplacementForce;
            float4 _MainTex_ST;
			fixed4 _InnerFireColor;
			fixed4 _OuterFireColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 offset = -_Speed * float2(0, _Time.x);
                float noise1 = tex2D(_NoiseTex, 1.5 * i.uv + offset).r;
				float noise2 = tex2D(_NoiseTex, 0.5 * i.uv + offset).r;
				float totalNoise = noise1 * noise2;

				float masked = tex2D(_MainTex, i.uv).r;
				float fire = (totalNoise + masked) * masked;
				float transparency = step(_NoiseStepMin, fire);
				float isInner = step(_NoiseStepMax, fire);
				fixed3 col = isInner * _InnerFireColor + (1 - isInner) * _OuterFireColor;

                return fixed4(col, transparency);
            }
            ENDCG
        }
    }
}
