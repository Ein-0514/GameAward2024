using System.Security.Cryptography;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffData
{
	public PlayerParamCoefficient m_playerParamCoefficient;
	public float m_remainingDuration;   // �c����ʌp������
	public int m_buffDebuffKey;			// ���ʂ𔭊�����e�̖��O
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