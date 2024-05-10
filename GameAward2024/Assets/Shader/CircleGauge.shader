Shader "UI/Unlit/CircleGauge"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_GaugeValue("GaugeValue", Range(0.0, 1.0)) = 0.0
		_InnerRadius("InnerRadisu", Range(0.0, 0.5)) = 0.0
		_OuterRadius("OuterRadisu", Range(0.0, 0.5)) = 0.5
    }
    SubShader
    {
		Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata vin)
            {
                v2f vout;
                vout.vertex = UnityObjectToClipPos(vin.vertex);
				vout.uv = vin.uv;
                return vout;
            }

            sampler2D _MainTex;
            float _GaugeValue;
            float _InnerRadius;
            float _OuterRadius;

            float4 frag (v2f pin) : SV_Target
            {
				//--- UV�̒l����A���S�܂ł̋��������߂�
				float2 center = float2(0.5f,0.5f);
				float2 offset = pin.uv - center;
				float r = length(offset);

				//--- �\�����Ȃ��������v�Z
				// step(a, b) (a <= b) ? 1 : 0
				// ��a��b�ȉ��̏ꍇ��1��Ԃ�
				float inner = step(_InnerRadius, r);	// ��������
				float outer = step(r, _OuterRadius);	// �O������
				if (inner * outer <= 0.0f) discard;		// �~�̓����ƊO���͕\�����Ȃ�

				//--- �ɍ��W�̊p�x�����߂�
				float pi = 3.141592f;
				//NOTE:Y�������]���Ă��邽��
				float rad = atan2(offset.y, offset.x);	// ���W����p�x���v�Z
				rad += pi * 0.5f;						// �p�x��ς��ăQ�[�W�̃X�^�[�g�n�_��ύX
				rad /= pi;								// -3.14�`3.14��-1�`1�ɕϊ�
				rad = rad * 0.5f + 0.5f;				// -1�`1 �� -0.5�`0.5 �� 0�`1�ɕω�
				// frac(a)�������������o��
				rad = frac(rad);						// �Q�[�W�̃X�^�[�g�n�_�̕ύX��0.25����1.25�ɂȂ��Ă�̂ŁA

				//--- �Q�[�W���v�Z
				float gage = rad;
				gage = 1.0f - step(_GaugeValue, gage);	
				if (gage <= 0.0f) discard;	// �\�����ׂ�����������

				//--- �Q�[�W�̕\�������̐F���v�Z
				float4 color = tex2D(_MainTex, float2(rad, r * 2.0f));
				float4 zero = float4(0.0f, 0.0f, 0.0f, 0.0f);	// �\���Ȃ��̐F
				color = lerp(zero, color, gage);				// �\�������̂ݐF��ݒ�

				return color;
            }
            ENDHLSL
        }
    }
}