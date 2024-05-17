using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGaugeController : MonoBehaviour
{
	[SerializeField] Material m_gaugeMaterial;	//�Q�[�W�̃}�e���A��
	float m_gaugeValue = 0.4f;	//�}�e���A���ɐݒ肷�鐔�l

    // Start is called before the first frame update
    void Start()
    {
		//������
		m_gaugeValue = PlayerActionControler.ActionValue / 100.0f;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		//�}�e���A�����ݒ肳��Ă��Ȃ��������I������
		if (m_gaugeMaterial == null) return;

		//�ݒ肷�鐔�l���擾���Čv�Z
		m_gaugeValue = PlayerActionControler.ActionValue / 100.0f;

		//�}�e���A���ɓK��
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}