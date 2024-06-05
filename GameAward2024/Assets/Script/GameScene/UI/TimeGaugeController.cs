using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGaugeController : MonoBehaviour
{
	[SerializeField]
	Material m_gaugeMaterial;
	GameTimer m_gameTimer;

    // Start is called before the first frame update
    void Start()
	{
		// �Q�[���p�̃^�C�}�[���擾
		m_gameTimer = GameScene.ManagerContainer.instance.studioObjectManager.gameTimer;
	}

	// Update is called once per frame
	void Update()
    {
		if (m_gameTimer == null) return;
		if (m_gaugeMaterial == null) return;

		//--- �Q�[�W�̒l���v�Z(0.0�`1.0)
		float gaugeValue = m_gameTimer.m_remainTime / m_gameTimer.m_stageLimitTime;
		gaugeValue = Mathf.Clamp01(gaugeValue);

		// TODO:�}�W�b�N�i���o�[��ScriptableObject�ɒu��������
		m_gaugeMaterial.SetFloat("_GaugeValue", gaugeValue);
	}
}