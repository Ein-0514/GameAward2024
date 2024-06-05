using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAnime : StateMachineBehaviour
{
    // �e�̃v���n�u
    [SerializeField] BulletBase m_bulletprefab;
    
    float m_deltatime;
    Vector3 m_enemytransform;
    Vector3 m_playertransform;
    Vector3 m_enemyforward;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_deltatime = 0.0f;

        // �e�̔���
        GameScene.CharacterManager enemypos =
            GameScene.ManagerContainer.instance.characterManager;
        m_enemytransform = enemypos.enemyTrans.transform.localPosition;
        GameScene.ManagerContainer.instance.bulletManger.CreateBullet<BulletBase>(m_bulletprefab , m_enemytransform);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���O�o��
        //Debug.Log("���:Attack");
        //Debug.Log("�o�ߎ���:" + m_deltatime + "�b");

        // �v���C���[�̃g�����X�t�H�[���擾
        GameScene.CharacterManager characterManager =
            GameScene.ManagerContainer.instance.characterManager;
        m_playertransform = characterManager.playerTrans.position;
        m_enemytransform = characterManager.enemyTrans.position;

        characterManager.enemyTrans.forward = m_playertransform;


        // 1�b�o�ߌ�ɃA�^�b�N�ɐ؂�ւ���
        if (m_deltatime >= 1.0f)
        {
            animator.SetBool("IsAttack", false);
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
