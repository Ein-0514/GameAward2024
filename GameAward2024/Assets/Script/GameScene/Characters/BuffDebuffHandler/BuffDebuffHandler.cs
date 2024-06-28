using System.Security.Cryptography;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerParamCoefficient // �v���C���[�Ɋւ��W��
{
	public Vector2 m_moveDirect
		= new Vector2(1.0f, 1.0f);			// �ړ������̌W��
	public float m_moveSpeed = 1.0f;		// �ړ����x�̌W��

	public void Init()
	{
		m_moveDirect	= new Vector2(1.0f, 1.0f);
		m_moveSpeed		= 1.0f;
	}

	public void InsertData(PlayerParamCoefficient insertData)
	{
		m_moveDirect	*= insertData.m_moveDirect;
		m_moveSpeed		*= insertData.m_moveSpeed;
	}
}

public class BuffDebuffData
{
	public PlayerParamCoefficient m_paramCoefficient = new PlayerParamCoefficient();
	public float m_remainingDuration = 0.0f;   // �c����ʌp������
	public int m_buffDebuffKey = 0;				// ���ʂ𔭊�����e�̖��O
}

[DefaultExecutionOrder(-1)]
public class BuffDebuffHandler : MonoBehaviour
{
	public PlayerParamCoefficient m_paramCoefficient { get; private set; } = new PlayerParamCoefficient();
	List<BuffDebuffData> m_buffDebuffDatas = new List<BuffDebuffData>();

	void FixedUpdate()
    {
		m_paramCoefficient.Init();	// �W���̏������Z�b�g

		for (int i = 0; i < m_buffDebuffDatas.Count; ++i)
		{
			BuffDebuffData data = m_buffDebuffDatas[i];

			// �W���̏����v���C���[�֗���
			m_paramCoefficient.InsertData(data.m_paramCoefficient);

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
	/// <param name="bulletName">�e�̖��O(this.GetType().Name)</param>
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