using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerAvoid : MonoBehaviour
{
	PlayerData m_playerData;
	PlayerParamCoefficient m_paramCoefficient;
	PlayerActionControler m_playerActionControler;
	PlayerMove m_playerMove;
	bool m_isAvoid;
	Vector2 m_period;

	void Start()
	{
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
		m_playerActionControler = characterManager.playerActionController;
		characterManager.playerTrans.TryGetComponent<PlayerMove>(out m_playerMove);
		m_playerData = characterManager.playerData;
		m_paramCoefficient = characterManager.buffDebuffHandler.m_paramCoefficient;
		m_isAvoid = false;
		m_period = new Vector2();
	}

	void FixedUpdate()
	{
		if (!m_isAvoid) return;

		m_playerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.AVOID);

		//--- ���
		if (Mathf.Abs(m_period.x) > 0.0f)
			m_playerMove.PlayerCircularRotation(m_period.x, this.transform.up);
		if (Mathf.Abs(m_period.y) > 0.0f)
			m_playerMove.PlayerCircularRotation(m_period.y, this.transform.right);

		// ���������Ă���
		m_period *= m_playerData.AVOID_ANCE_MULTIPLIER;

		//--- ����̌��E�l�Œ�~������
		if (Mathf.Abs(m_period.x) >= m_playerData.AVOID_RIMIT_VALUE)
			m_period.x = 0.0f;
		if (Mathf.Abs(m_period.y) >= m_playerData.AVOID_RIMIT_VALUE)
			m_period.y = 0.0f;

		// ����̏I���𔻒�
		m_isAvoid = !(m_period.x == 0.0f && m_period.y == 0.0f);
	}

	public void OnAvoid()
	{
		if (m_isAvoid) return;
		if (!m_playerActionControler.IsMove()) return;

		//--- �������������v�Z
		Vector2 dir = m_paramCoefficient.m_moveDirect;
		m_period.x = m_playerData.AVOID_START_VALUE * dir.x;
		m_period.y = m_playerData.AVOID_START_VALUE * dir.y;
		m_period /= m_paramCoefficient.m_moveSpeed;

		m_isAvoid = true;	// ����t���O�𗧂Ă�
	}
}