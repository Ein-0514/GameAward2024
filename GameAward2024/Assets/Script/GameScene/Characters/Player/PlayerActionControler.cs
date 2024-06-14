using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerActionControler : MonoBehaviour
{
	public const float MAX_GAUGE_VALUE = 100.0f;

    // �v���C���[�f�[�^�̎擾�p�ϐ�
    private PlayerData m_playerData;

	// �Q�[�W�̑������Ǘ�����ϐ�
	PlayerParamCoefficient m_paramCoefficient;

	// �v���C���[�̓����̗�����ۑ����郊�X�g
	List<PlayerData.E_PLAYER_ACTION> m_actionList = new List<PlayerData.E_PLAYER_ACTION>();

    // �v���C���[�̌��݂̍s��
    PlayerData.E_PLAYER_ACTION m_action;

    // �v���C���[�Q�[�W�ɔ��f�����鐔�l��ێ�����
    public float m_actionValue { get; private set; }	// 0-100�ŊǗ�����

    void Start()
    {
		//�e��ϐ�������
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
        m_playerData = characterManager.playerData;
		m_paramCoefficient = characterManager.buffDebuffHandler.m_paramCoefficient;
        m_action = PlayerData.E_PLAYER_ACTION.STOP;

		m_actionValue = m_playerData.START_GAUGE_VALUE;     //�Q�[�W�̐��l������
    }

    void FixedUpdate()
    {
        if (m_actionList.Count == 0)
        {//�t���[���ԂŃA�N�V�������N�����Ă��Ȃ� �� ��~���Ă���
            m_paramCoefficient.m_subGaugeValue += m_playerData.STOP_GAUGE_VALUE;        //��~���Ă��鎞
            m_action = PlayerData.E_PLAYER_ACTION.STOP;
        }
        else
        {//�t���[���ԂŃA�N�V�������N�����Ă���
            while (m_actionList.Count != 0)
            {
                //�o�^����Ă���A�N�V�����ɉ����鐔�l��ǉ�
                switch (m_actionList[0])
                {
                    case PlayerData.E_PLAYER_ACTION.MOVE:    //�ړ����Ă��鎞
                        m_paramCoefficient.m_addGaugeValue += m_playerData.MOVE_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.DASH:    //�_�b�V�����Ă��鎞
                        m_paramCoefficient.m_addGaugeValue += m_playerData.DASH_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.AVOID:   //������Ă��鎞
                        m_paramCoefficient.m_addGaugeValue += m_playerData.AVOID_GAUGE_VALUE;
                        break;
                    default:
                        break;
                }
                m_action = m_actionList[0];
                //�����ς݂̃A�N�V�������폜
                m_actionList.RemoveAt(0);
            }
        }

        // �Q�[�W�̕ω��ʂ̔��f
        m_actionValue += m_paramCoefficient.m_addGaugeValue * m_paramCoefficient.m_gaugeUpSpeed;
        m_actionValue -= m_paramCoefficient.m_subGaugeValue * m_paramCoefficient.m_gaugeDownSpeed;

		// ���l�𒴂����艺��������̕␳����
		m_actionValue = Mathf.Clamp(m_actionValue, 0.0f, MAX_GAUGE_VALUE);

        Debug.Log(IsMove());
    }

    /// <summary>
    /// �v���C���[�̋N�������A�N�V������o�^����֐�
    /// </summary>
    /// <param name="act"> �N�������A�N�V�����̎�� </param>
    public void AddAction(PlayerData.E_PLAYER_ACTION act)
    {
        m_actionList.Add(act);
    }

	/// <summary>
	/// �ړ����Ă��邩����
	/// </summary>
	/// <returns>�ړ��t���O</returns>
	public bool IsMove()
	{
		return m_action != PlayerData.E_PLAYER_ACTION.STOP ? true : false;
	}
}