using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class EnemyHpGaugeController : MonoBehaviour
{
	[SerializeField] Material m_gaugeMaterial;	//�Q�[�W�̃}�e���A��
	float m_gaugeValue = 1.0f;					//�}�e���A���ɐݒ肷�鐔�l
	EnemyBase m_enemy;

    void Start()
    {
		m_enemy = ManagerContainer.instance.characterManager.enemyData;
	}

    void Update()
    {
		// �}�e���A�����ݒ肳��Ă��Ȃ��������I������
		if (m_gaugeMaterial == null) return;

		//--- �ݒ肷�鐔�l���擾���Čv�Z
		m_gaugeValue = m_enemy.hp / m_enemy.m_maxHp;
		m_gaugeValue = Mathf.Clamp01(m_gaugeValue);

		// �}�e���A���ɓK��
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}