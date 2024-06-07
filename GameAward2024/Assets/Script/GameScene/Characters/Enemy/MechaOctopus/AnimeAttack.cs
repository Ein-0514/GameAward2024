using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

namespace MechaOctopus
{
	public class AnimeAttack : StateMachineBehaviour
	{
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

			//--- �e�̔���
			BulletManager bulletManager = ManagerContainer.instance.bulletManger;
			bulletManager.CreateBullet(enemy.shotBulletKinds[0], m_enemyTrans.position);
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

		// OnStateMove is called right after Animator.OnAnimatorMove()
		//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		//{
		//    // Implement code that processes and affects root motion
		//}

		// OnStateIK is called right after Animator.OnAnimatorIK()
		//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		//{
		//    // Implement code that sets up animation IK (inverse kinematics)
		//}
	}
}