using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

namespace MechaOctopus
{
	public class AnimeIdle : StateMachineBehaviour
	{
		float m_deltatime;
		Transform m_playerTrans;
		Transform m_enemyTrans;

		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			m_deltatime = 0.0f;

			//--- �����ƃv���C���[��Transform���擾
			CharacterManager characterManager = ManagerContainer.instance.characterManager;
			m_playerTrans = characterManager.playerTrans;
			m_enemyTrans = animator.transform;
		}

		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			// �v���C���[�̕�������
			m_enemyTrans.LookAt(m_playerTrans.position, m_enemyTrans.up);

			// 3�b�o�ߌ�ɃA�^�b�N�ɐ؂�ւ���
			if (m_deltatime >= 3.0f)
			{
				animator.SetBool("IsAttack", true);
			}

			//�o�ߎ��Ԃ��X�V����
			m_deltatime += Time.deltaTime;
		}

		// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		//{
		//    
		//}

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