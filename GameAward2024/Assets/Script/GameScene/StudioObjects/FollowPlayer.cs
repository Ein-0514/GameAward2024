using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

//--- �Q�l�T�C�g
// https://qiita.com/No2DGameNoLife/items/d1497a2f98f95a5194ac
[DefaultExecutionOrder(1)]  // HACK:�v���C���[�̍X�V����Ɏ��s�����
public class FollowPlayer : MonoBehaviour
{
	[SerializeField]
	Vector3 m_offset = new Vector3(0.0f, 0.5f, -2.5f);
	Transform m_playerTrans;
	Quaternion m_initialQuaternion;

	void Start()
	{
		// ���������擾
		m_initialQuaternion = this.transform.rotation;

		//--- �v���C���[�̏����擾
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
		m_playerTrans = characterManager.playerTrans;
	}

	void Update()
	{
		//--- �I�t�Z�b�g�ɉ�]��K�p
		Quaternion quaternion = m_playerTrans.rotation;
		Vector3 newOffset = quaternion * m_offset;

		//--- ���W�E��]���w��
		Vector3 playerPos = m_playerTrans.position;
		transform.position = playerPos + newOffset;
		// TODO:���������o���ꍇ�́A���̍s���̗p
		transform.rotation = quaternion * m_initialQuaternion;
		//transform.rotation = m_initialQuaternion * quaternion;
	}
}