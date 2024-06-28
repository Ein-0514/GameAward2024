using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class AccelerateBullet : BulletBase
{
	float m_speed;          // �e�̑��x
	float m_maxSpeed;		// �e�̍ő呬�x
	float m_accelerate;     // �e�̉����x

	float m_timer = 0.0f;
	Rigidbody m_rigidbody;
	Vector3 m_vToTarget;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.ACCELERATE;

		base.Start();

		TryGetComponent(out m_rigidbody);
	}

	protected override void Update()
	{
		base.Update();

		// �o�ߎ��Ԃ����Z
		m_timer += Time.deltaTime;

		//--- ���x���v�Z
		m_speed += m_accelerate * Mathf.Pow(m_timer, 2.0f);
		m_speed = Mathf.Clamp(m_speed, 0.0f, m_maxSpeed);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		// �v�Z���ꂽ�x�N�g����ݒ�
		m_rigidbody.velocity = m_vToTarget * m_speed;	
	}

	protected override void OnChangeTarget(E_TARGET_KIND targetKind)
	{
		//--- �^�[�Q�b�g�ւ̃x�N�g�����v�Z
		m_vToTarget = m_target.position - transform.position;
		m_vToTarget.Normalize();
	}

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);
		
		//--- �l�̋z�o��
		data[nameof(m_speed		)].TryGetData(out m_speed);
		data[nameof(m_maxSpeed	)].TryGetData(out m_maxSpeed);
		data[nameof(m_accelerate)].TryGetData(out m_accelerate);
	}
}