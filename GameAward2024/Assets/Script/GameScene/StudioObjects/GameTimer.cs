using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
	public float m_stageLimitTime { get; private set; }
	public float m_remainTime { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
		// TODO:�O�V�[���̃X�e�[�W�I������Ɉ������w��
		//int stageNum = 1;
		// �X�e�[�W�̏����擾
		//StageInfo stageInfo = GameScene.ManagerContainer.GetManagerContainer().m_stageInfo;
		// �X�e�[�W���ɐݒ肳�ꂽ�������Ԃ��擾
		//m_stageLimitTime = stageInfo.GetStageInfo(stageNum).STAGE_LIMIT_TIME;
		m_stageLimitTime = 10.0f;
		m_remainTime = m_stageLimitTime;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if (IsTimeUp()) return;

		// �o�ߎ��Ԃ����Z
		m_remainTime -= Time.deltaTime;
	}

	public bool IsTimeUp()
	{
		return m_remainTime <= 0.0f;
	}
}