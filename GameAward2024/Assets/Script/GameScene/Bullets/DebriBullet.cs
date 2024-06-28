using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class DebriBullet : BulletBase
{
	float m_speed;			// �e�̑��x
	float m_stopDistance;	// ��~���鋗��(�G�Ƃ̋���)

	Rigidbody m_rigidbody;
	Vector3 m_vToTarget;
	Transform m_enemyTrans;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.DEBRI;

		TryGetComponent(out m_rigidbody);
		base.Start();

		// �G�̏����擾
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
		m_enemyTrans = characterManager.enemyTrans;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		float toEnemyDist	= Vector3.Distance(transform.position, m_target.position);
		if (toEnemyDist > 10.0f) return;

		// �v�Z���ꂽ�x�N�g����ݒ�
		m_rigidbody.velocity = m_vToTarget * m_speed;
	}

	protected override void FixedUpdateToPlayer()
	{
		//--- �e���v���C���[�̋O����ɓ��B������Î~�w����
		float toEnemyDist = Vector3.Distance(transform.position, m_enemyTrans.position);
		if (toEnemyDist >= m_stopDistance)
		{
			m_rigidbody.isKinematic = true;
			return;
		}

		// �v�Z���ꂽ�x�N�g����ݒ�
		m_rigidbody.velocity = m_vToTarget * m_speed;
	}

	protected override void FixedUpdateToEnemy()
	{
		// �v�Z���ꂽ�x�N�g����ݒ�
		m_rigidbody.velocity = m_vToTarget * m_speed;
	}

	protected override void OnChangeTarget(E_TARGET_KIND targetKind)
	{
		//--- �^�[�Q�b�g�ւ̃x�N�g�����v�Z
		m_vToTarget = m_target.position - transform.position;
		m_vToTarget.Normalize();

		m_rigidbody.isKinematic = false;
	}

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);
		
		//--- �l�̋z�o��
		data[nameof(m_speed			)].TryGetData(out m_speed);
		data[nameof(m_stopDistance	)].TryGetData(out m_stopDistance);
	}
}