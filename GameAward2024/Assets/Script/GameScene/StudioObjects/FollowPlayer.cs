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

	// Start is called before the first frame update
	void Start()
	{
		//--- �v���C���[�E�G�̏����擾
		CharacterManager characterManager = ManagerContainer.GetManagerContainer().m_characterManager;
		m_playerTrans = characterManager.m_player;
		m_enemyTrans = characterManager.m_enemy;
	}

	// Update is called once per frame
	void Update()
	{
		//--- �v���C���[�E�G�̍��W
		Vector3 playerPos = m_playerTrans.position;
		Vector3 enemyPos  = m_enemyTrans.position;

		// �p�x���v�Z
		float angle = CalcToEnemyAngleY(enemyPos - playerPos);

		//--- �I�t�Z�b�g�ɉ�]��K�p
		Quaternion quaternion = Quaternion.Euler(0.0f, angle, 0.0f);
		Vector3 newOffset = quaternion * m_offset;

		//--- ���W�E��]���w��
		transform.position = playerPos + newOffset;
		transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
	}

	/// <summary>
	/// �G�֌������x�N�g����Y���̊p�x�����߂�
	/// </summary>
	/// <param name="vToEnemy">�v���C���[����G�֌������x�N�g��</param>
	/// <returns>�G�֌������x�N�g����Y���̊p�x</returns>
	float CalcToEnemyAngleY(Vector3 vToEnemy)
	{
		vToEnemy.y = 0.0f;		// y�̈ړ������𖳎�
		vToEnemy.Normalize();	// �x�N�g���𐳋K��

		//--- �G�ւ̃x�N�g����Z�����x�N�g���̐����p�����߂�
		float dot = Vector3.Dot(Vector3.forward, vToEnemy);
		float angle = Mathf.Acos(dot);
		angle = angle * (HALF_DEGREE / Mathf.PI);

		//--- ���E����ɂ����-180.0�`180.0�ɕϊ�
		Vector3 cross = Vector3.Cross(Vector3.forward, vToEnemy);
		// Z�����x�N�g�����猩�āA�����Ɉʒu����ꍇ��[* -1.0]
		if (cross.y < 0.0f) angle = -angle;

		return angle;
	}
}