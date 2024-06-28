using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

namespace MechaOctopus
{
	public class AnimeAttack : StateMachineBehaviour
	{
		const int MUZZLE_NUM = 0;

		Transform m_playerTrans;
		Transform m_enemyTrans;

		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			//--- �v���C���[�ƓG��Transform���擾
			CharacterManager characterManager = ManagerContainer.instance.characterManager;
			m_playerTrans = characterManager.playerTrans;
			m_enemyTrans = characterManager.enemyTrans;
			// �G�̊�b�f�[�^���擾
			EnemyBase enemy = characterManager.enemyData;

			//--- ���e�̎�ނ�����
			BulletDataList.E_BULLET_KIND bulletKind;
			if (Random.Range(0, 3) == 0)
				bulletKind = BulletDataList.E_BULLET_KIND.SIN_WAVE;
			else
				bulletKind = BulletDataList.E_BULLET_KIND.RIPPLE;

			//--- �e�̔���
			Vector3 muzzlePos = characterManager.enemyData.GetMuzzleTrans(MUZZLE_NUM).position;
			BulletManager bulletManager = ManagerContainer.instance.bulletManger;
			bulletManager.CreateBullet(
				bulletKind, muzzlePos, m_enemyTrans.rotation);
		}

		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			// �v���C���[�̕�������
			m_enemyTrans.LookAt(m_playerTrans.position, m_enemyTrans.up);
		}

		// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			// �A�j���[�V�������I��莟��A�C�h����Ԃ֑J��
			animator.SetBool("IsAttack", false);
		}
	}
}