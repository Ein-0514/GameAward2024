using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameScene;

public class PlayerControler : MonoBehaviour
{
    PlayerData m_playerData;   // �v���C���[�f�[�^�̎擾�p�ϐ�
	public float m_hp { get; private set; }
	Transform m_enemyTrans;

	void Start()
    {
		//--- �e��ϐ�������
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
        m_playerData = characterManager.playerData;
		m_hp = m_playerData.MAX_HP;     // HP�̐��l������
		m_enemyTrans = characterManager.enemyTrans;
    }

	void Update()
	{
		//--- �G�ɐ��ʂ�������悤�ɉ�]
		Vector3 targetPos = m_enemyTrans.position;
		Vector3 up = transform.up;
		transform.LookAt(targetPos, up);
	}

	/// <summary>
	/// �v���C���[��HP�����Z
	/// </summary>
	/// <param name="damage">�_���[�W��</param>
	public void SubHp(float damage)
	{
		m_hp -= damage;
		m_hp = Mathf.Clamp(m_hp, 0.0f, m_playerData.MAX_HP);
	}

	/// <summary>
	/// ���S���Ă��邩�𔻒�
	/// </summary>
	/// <returns>���S�t���O</returns>
	public bool IsDead()
	{
		return m_hp <= 0.0f;
	}
}