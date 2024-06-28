using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerHpGaugeController : MonoBehaviour
{
	[SerializeField] Material m_gaugeMaterial;	//�Q�[�W�̃}�e���A��
	float m_gaugeValue  =1.0f;					//�}�e���A���ɐݒ肷�鐔�l
	PlayerControler m_playerControler;
	PlayerData m_playerData;

    void Start()
    {
		//--- �v���C���[�Ɋւ���f�[�^���擾
		var characterManager = ManagerContainer.instance.characterManager;
		m_playerData = characterManager.playerData;
		m_playerControler = characterManager.playerController;
	}

    void Update()
    {
		// �}�e���A�����ݒ肳��Ă��Ȃ��������I������
		if (m_gaugeMaterial == null) return;

		//--- �ݒ肷�鐔�l���擾���Čv�Z
		m_gaugeValue = m_playerControler.m_hp / m_playerData.MAX_HP;
		m_gaugeValue = Mathf.Clamp01(m_gaugeValue);

		// �}�e���A���ɓK��
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}