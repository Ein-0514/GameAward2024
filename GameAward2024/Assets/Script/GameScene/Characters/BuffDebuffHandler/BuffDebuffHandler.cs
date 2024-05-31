using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffData
{
	public PlayerParamCoefficient m_playerParamCoefficient;
	public float m_remainingDuration;	// �c����ʌp������
}

public class BuffDebuffHandler : MonoBehaviour
{
	List<BuffDebuffData> m_buffDebuffDatas = new List<BuffDebuffData>();

    void Update()
    {
		foreach(BuffDebuffData data in m_buffDebuffDatas)
		{
			//--- �W���̏����v���C���[�֗���
			PlayerParamCoefficient playerParamCoefficient = PlayerActionControler.PParam;
			playerParamCoefficient = data.m_playerParamCoefficient;

			// ���ʂ̎c�莞�Ԃ����炵�Ă���
			data.m_remainingDuration -= Time.deltaTime;

			if (data.m_remainingDuration > 0.0f) continue;
			// �c�莞�Ԃ�0�b�ɂȂ����ꍇ�A�f�[�^������
			m_buffDebuffDatas.Remove(data);
		}
    }

	public void AddBuffDebuff(BuffDebuffData data)
	{
		m_buffDebuffDatas.Add(data);
	}
}