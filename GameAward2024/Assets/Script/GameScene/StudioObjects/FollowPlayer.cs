using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class FollowPlayer : MonoBehaviour
{
	const float HALF_DEGREE = 180.0f;

	[SerializeField]
	Vector3 m_offset = new Vector3(0.0f, 0.5f, -2.5f);
	Transform m_playerTrans;
	Transform m_enemyTrans;
	Quaternion m_initialQuaternion;

	void Start()
	{
		// ���������擾
		m_initialQuaternion = this.transform.rotation;

		//--- �v���C���[�E�G�̏����擾
		CharacterManager characterManager = ManagerContainer.GetManagerContainer().m_characterManager;
		m_playerTrans = characterManager.m_player;
		m_enemyTrans = characterManager.m_enemy;
	}

	void Update()
	{
		//--- �v���C���[�E�G�̍��W
		Vector3 playerPos = m_playerTrans.position;
		Vector3 enemyPos  = m_enemyTrans.position;

		//--- �I�t�Z�b�g�ɉ�]��K�p
		Quaternion quaternion = m_playerTrans.rotation;
		Vector3 newOffset = quaternion * m_offset;

		//--- ���W�E��]���w��
		transform.position = playerPos + newOffset;
		// TODO:���������o���ꍇ�́A���̍s���̗p
		transform.rotation = quaternion * m_initialQuaternion;
		//transform.rotation = m_initialQuaternion * quaternion;
	}
}