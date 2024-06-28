using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class BulletBase : MonoBehaviour
{
	public enum E_TARGET_KIND
	{
		PLAYER = 0,
		ENEMY
	}

	const string TO_PLAYER_BULLET_LAYER_NAME = "ToPlayerBullet";
	const string TO_ENEMY_BULLET_LAYER_NAME  = "ToEnemyBullet";
	const string COUNTER_AREA_LAYER_NAME	 = "CounterArea";

	protected BulletDataList.E_BULLET_KIND m_bulletKind;
	protected Transform m_target;
	BulletDataList m_bulletDataList;
	float m_damage;
	float m_destroyTime;
	float m_maxDestroyTime;
    protected BuffDebuffData m_buffDebuffData = new BuffDebuffData();

	Action m_bulletUpdate;
	Action m_bulletFixedUpdate;
	Action<Collider> m_onTriggerEnter;

    protected virtual void Start()
    {
		//--- �e�f�[�^�̓ǂݍ��݂Ɛݒ�
		m_bulletDataList = ManagerContainer.instance.bulletManger.bulletDataList;
		m_bulletDataList.Load(m_bulletKind);
		var data = m_bulletDataList.GetData(m_bulletKind);
		SetData(data);
		m_maxDestroyTime = m_destroyTime;

		// �^�[�Q�b�g���v���C���[�ɐݒ�
		ChangeTarget(E_TARGET_KIND.PLAYER);
	}

	protected virtual void Update()
    {
		//--- ���ł���܂ł̎��Ԃ��J�E���g
		if (m_destroyTime < 0.0f)
		{
			Destroy(gameObject);  // ���g��j��
			return;
		}
		m_destroyTime -= Time.deltaTime;

		// �e�̓���
		m_bulletUpdate.Invoke();
    }

	protected virtual void FixedUpdate()
	{
		m_bulletFixedUpdate.Invoke();
	}

	void OnTriggerEnter(Collider other)
	{
		// �J�E���^�[�̗̈�ɐڐG�����ꍇ�͏������Ȃ�
		if (other.gameObject.layer == LayerMask.NameToLayer(COUNTER_AREA_LAYER_NAME)) return;

		m_onTriggerEnter.Invoke(other);

		// �v���C���[or�G�ɓ���������e���폜����
		Destroy(gameObject);
	}

	public void ChangeTarget(E_TARGET_KIND targetKind)
	{
		// ���ł܂ł̎��Ԃ����Z�b�g
		m_destroyTime = m_maxDestroyTime;

		CharacterManager characterManager = ManagerContainer.instance.characterManager;

		switch(targetKind)
		{
			case E_TARGET_KIND.PLAYER:
				//--- �֐��������ւ�
				m_bulletUpdate = UpdateToPlayer;
				m_bulletFixedUpdate = FixedUpdateToPlayer;
				m_onTriggerEnter = OnTriggerEnterToPlayer;

				//--- ���C���[��ύX
				gameObject.layer = 
					LayerMask.NameToLayer(TO_PLAYER_BULLET_LAYER_NAME);

				// �^�[�Q�b�g���v���C���[�ɐݒ�
				m_target = characterManager.playerTrans;

				break;
			case E_TARGET_KIND.ENEMY:
				//--- �֐��������ւ�
				m_bulletUpdate = UpdateToEnemy;
				m_bulletFixedUpdate = FixedUpdateToEnemy;
				m_onTriggerEnter = OnTriggerEnterToEnemy;

				//--- ���C���[��ύX
				gameObject.layer = 
					LayerMask.NameToLayer(TO_ENEMY_BULLET_LAYER_NAME);

				// �^�[�Q�b�g��G�ɐݒ�
				m_target = characterManager.enemyTrans;
				break;
		}

		// �^�[�Q�b�g�ύX���ɌĂяo����鏈��
		OnChangeTarget(targetKind);
	}

	/// <summary>
	/// �^�[�Q�b�g���ύX���ꂽ���ɌĂяo����鏈��
	/// </summary>
	protected virtual void OnChangeTarget(E_TARGET_KIND targetKind)
	{
	}

	/// <summary>
	/// �v���C���[�֌��������̏���
	/// </summary>
	protected virtual void UpdateToPlayer()
	{	
	}

	/// <summary>
	/// �G�֌��������̏���
	/// </summary>
	protected virtual void UpdateToEnemy()
	{
	}

	/// <summary>
	/// �v���C���[�֌��������̏���
	/// </summary>
	protected virtual void FixedUpdateToPlayer()
	{
	}

	/// <summary>
	/// �G�֌��������̏���
	/// </summary>
	protected virtual void FixedUpdateToEnemy()
	{
	}

	/// <summary>
	/// �v���C���[�֏Փ˂������̏���
	/// </summary>
	protected virtual void OnTriggerEnterToPlayer(Collider other)
	{
		CharacterManager characterManager = ManagerContainer.instance.characterManager;

		// �v���C���[�Ƀo�t��t�^����
		characterManager.
			buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, this.GetType().Name);

		//--- �v���C���[�փ_���[�W��^����
		PlayerControler playerControler;
		if (other.TryGetComponent(out playerControler))
			playerControler.SubHp(m_damage);
	}

	/// <summary>
	/// �G�ɏՓ˂������̏���
	/// </summary>
	protected virtual void OnTriggerEnterToEnemy(Collider other)
	{
		// �G�Ƀ_���[�W��^����
		EnemyBase enemy;
		if (other.TryGetComponent(out enemy))
			enemy.SubHp(m_damage);
	}

	protected virtual void SetData(Dictionary<string, CSVParamData> data)
	{
		//--- �l�̋z�o��
		data[nameof(m_damage		)].TryGetData(out m_damage);
		data[nameof(m_destroyTime	)].TryGetData(out m_destroyTime);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_moveDirect	)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_moveDirect);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_moveSpeed		)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_moveSpeed);
		data[nameof(m_buffDebuffData.m_remainingDuration)].TryGetData(out m_buffDebuffData.m_remainingDuration);
	}
}