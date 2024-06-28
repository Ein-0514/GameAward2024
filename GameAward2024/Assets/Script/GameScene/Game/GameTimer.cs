using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class GameTimer : MonoBehaviour
{
	public float m_stageLimitTime { get; private set; } = 1.0f;
	public float m_remainTime { get; private set; } = 1.0f;
	public bool m_isStart { get; private set; } = false;

	void FixedUpdate()
    {
		if (!m_isStart) return;
		if (IsTimeUp()) return;

		// �o�ߎ��Ԃ����Z
		m_remainTime -= Time.deltaTime;
	}

	public bool IsTimeUp()
	{
		return m_isStart && m_remainTime <= 0.0f;
	}

	public void StartCount()
	{
		if (m_isStart) return;

		// �������Ԃ��擾
		m_stageLimitTime = ManagerContainer.instance.characterManager.enemyData.limitTime;
		m_remainTime = m_stageLimitTime;

		m_isStart = true;
	}
}