using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerGaugeController : MonoBehaviour
{
	[SerializeField] Material m_gaugeMaterial;	//�Q�[�W�̃}�e���A��
	float m_gaugeValue;							//�}�e���A���ɐݒ肷�鐔�l
	PlayerActionControler m_playerActionControler;

    void Start()
    {
		m_playerActionControler = ManagerContainer.instance.characterManager.playerActionController;
		m_gaugeValue = m_playerActionControler.m_actionValue / PlayerActionControler.MAX_GAUGE_VALUE;
	}

    void FixedUpdate()
    {
		// �}�e���A�����ݒ肳��Ă��Ȃ��������I������
		if (m_gaugeMaterial == null) return;

		// �ݒ肷�鐔�l���擾���Čv�Z
		m_gaugeValue = m_playerActionControler.m_actionValue / PlayerActionControler.MAX_GAUGE_VALUE;

		// �}�e���A���ɓK��
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}