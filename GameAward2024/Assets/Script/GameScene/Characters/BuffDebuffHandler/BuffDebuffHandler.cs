using System.Security.Cryptography;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffData
{
	public PlayerParamCoefficient m_playerParamCoefficient = new PlayerParamCoefficient();
	public float m_remainingDuration = 0.0f;   // �c����ʌp������
	public int m_buffDebuffKey = 0;				// ���ʂ𔭊�����e�̖��O
}

public class BuffDebuffHandler : MonoBehaviour
{
	List<BuffDebuffData> m_buffDebuffDatas = new List<BuffDebuffData>();

    void Update()
    {
		for(int i = 0; i < m_buffDebuffDatas.Count; ++i)
		{
			BuffDebuffData data = m_buffDebuffDatas[i];

			//--- �W���̏����v���C���[�֗���
			PlayerParamCoefficient playerParamCoefficient = PlayerActionControler.PParam;
			playerParamCoefficient.m_addGaugeValue = data.m_playerParamCoefficient.m_addGaugeValue;

			// ���ʂ̎c�莞�Ԃ����炵�Ă���
			data.m_remainingDuration -= Time.deltaTime;

			if (data.m_remainingDuration > 0.0f) continue;
			// �c�莞�Ԃ�0�b�ɂȂ����ꍇ�A�f�[�^������
			m_buffDebuffDatas.Remove(data);
			--i;
		}
	}

	/// <summary>
	/// �o�t�E�f�o�t��ǉ�
	/// </summary>
	/// <param name="data">�o�t�E�f�o�t�̃f�[�^</param>
	/// <param name="bulletName">�e�̖��O(gameObject.name)</param>
	public void AddBuffDebuff(BuffDebuffData data, string bulletName)
	{
		//--- �e�̖��O�����ӂ̃o�t�E�f�o�t�̃L�[���v�Z
		using (var sha256 = SHA256.Create())
		{
			var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(bulletName));
			data.m_buffDebuffKey = BitConverter.ToInt32(hashBytes, 0);
		}

		foreach(BuffDebuffData listData in m_buffDebuffDatas)
		{
			//--- �L�[����v����ꍇ�́A���ʂ̎c�莞�Ԃ����Z�b�g
			if (listData.m_buffDebuffKey != data.m_buffDebuffKey) continue;
			listData.m_remainingDuration = data.m_remainingDuration;
			return;
		}

		// ���X�g�ɊY���̃f�[�^��������Βǉ�
		m_buffDebuffDatas.Add(data);
	}
}