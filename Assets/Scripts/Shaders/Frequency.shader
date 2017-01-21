Shader "Custom/Frequency" {
	Properties {
		_ColorA ("Color A", Color) = (1,1,1,1)
		_ColorB ("Color B", Color) = (1,1,1,1)
		_Speed("Wave Speed", Range(0.1,80)) = 5
		_Frequency("Wave Frequency", Range(0,5)) = 2 
		_Amplitude("Wave Amplitude", Range (-1,1)) =1
		_MainTex ("Albedo (RGB)", 2D) = "white" 

	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		PASS
		{
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fog

		#include "UnityCG.cginc"

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		float4 _ColorA;
		float4 _ColorB;
		float _tintAmount;
		float _Speed;
		float _Frequency;
		float _Amplitude;
		float _OffsetVal;


		
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


		v2f vert(appdata v) {
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			float time = _Time * _Speed;
			float waveValueA = sin(time + v.vertex * _Frequency) * _Amplitude;
			o.vertex.xyz = float3(v.vertex.x, v.vertex.y + waveValueA,v.vertex.z);
			return o;
		}

			
            
        fixed4 frag (v2f i) : SV_Target
        {
            // sample the texture
            fixed4 col = tex2D(_MainTex, i.uv);
            // apply fog
            UNITY_APPLY_FOG(i.fogCoord, col);
            return col;
        }

		ENDCG
		}
	}
}
	