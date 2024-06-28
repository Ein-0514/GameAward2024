using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class NormalBullet : BulletBase
{
	float m_speed;    // �e�̑��x
	Rigidbody m_rigidbody;
	Vector3 m_vToTarget;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.NORMAL;

		base.Start();

		TryGetComponent(out m_rigidbody);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		// �v�Z���ꂽ�x�N�g����ݒ�
		m_rigidbody.MovePosition(transform.position + m_vToTarget * m_speed * Time.fixedDeltaTime);
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
		data[nameof(m_speed)].TryGetData(out m_speed);
	}
}