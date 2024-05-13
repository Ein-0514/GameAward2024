using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class FollowPlayer : MonoBehaviour
{
	Transform playerTrans;
	Matrix4x4 worldMat;

	// Start is called before the first frame update
	void Start()
	{
		//--- �v���C���[�̏����擾
		CharacterManager characterManager = ManagerContainer.GetManagerContainer().m_characterManager;
		playerTrans = characterManager.m_player;
		
		// �I�u�W�F�N�g�̃��[���h�s����擾
		worldMat = this.transform.localToWorldMatrix;
	}

	// Update is called once per frame
	void Update()
	{
		// �v���C���[�̃��[���h�s��
		Matrix4x4 playerWorldMat = playerTrans.localToWorldMatrix;
		// �v���C���[�̍s��Ɏ��g�̍s���K�p
		Matrix4x4 newWorldMat = playerWorldMat * worldMat;

		//--- �ړ����������o��
		Vector3 pos = new Vector3(newWorldMat.m03, newWorldMat.m13, newWorldMat.m23);
		this.transform.position = pos;

		//--- ��]���������o��
		Quaternion rot = newWorldMat.rotation;
		this.transform.rotation = rot;
	}
}