using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParamCoefficient // �v���C���[�Ɋւ��W��
{
    public float m_addGaugeValue = 0.0f;// �Q�[�W�ɉ��Z����ϐ�
    public float m_subGaugeValue = 0.0f;// �Q�[�W�Ɍ��Z����ϐ�
    public float m_gaugeUpSpeed = 1.0f;// �Q�[�W�̑����X�s�[�h
    public float m_gaugeDownSpeed = 1.0f;// �Q�[�W�̌����X�s�[�h
    public float m_moveDirect = 1.0f;// �ړ������̌W��
    public float m_moveSpeed = 1.0f;// �ړ����x�̌W��

    public PlayerParamCoefficient()
    {
        m_addGaugeValue = 0.0f;// �Q�[�W�ɉ��Z����ϐ�
        m_subGaugeValue = 0.0f;// �Q�[�W�Ɍ��Z����ϐ�
        m_gaugeUpSpeed = 1.0f;// �Q�[�W�̑����X�s�[�h
        m_gaugeDownSpeed = 1.0f;// �Q�[�W�̌����X�s�[�h
        m_moveDirect = 1.0f;// �ړ������̌W��
        m_moveSpeed = 1.0f;// �ړ����x�̌W��
    }
}

public class PlayerActionControler : MonoBehaviour
{
    // �v���C���[�f�[�^�̎擾�p�ϐ�
    private PlayerData PData;

    // �Q�[�W�̑������Ǘ�����ϐ�
    static private PlayerParamCoefficient m_PParamCoefficient;

    // �v���C���[���Ȃ�̓������������ۑ����郊�X�g
    static private List<PlayerData.E_PLAYER_ACTION> m_actionList;

    // �v���C���[�Q�[�W�ɔ��f�����鐔�l��ێ�����
    [SerializeField] private static float m_actionValue;        //0-100�ŊǗ�����

    // Start is called before the first frame update
    void Start()
    {
        //�e��ϐ�������
        PData = PlayerDataParam.data;
        m_PParamCoefficient = new PlayerParamCoefficient();
        m_actionList = new List<PlayerData.E_PLAYER_ACTION>();     //���X�g�̐���
        m_actionValue = PData.START_GAUGE_VALUE;     //�Q�[�W�̐��l������
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_actionList.Count == 0)
        {//�t���[���ԂŃA�N�V�������N�����Ă��Ȃ� �� ��~���Ă���
            m_PParamCoefficient.m_subGaugeValue += PData.STOP_GAUGE_VALUE;        //��~���Ă��鎞
        }
        else
        {//�t���[���ԂŃA�N�V�������N�����Ă���
            while (m_actionList.Count != 0)
            {
                //�o�^����Ă���A�N�V�����ɉ����鐔�l��ǉ�
                switch (m_actionList[0])
                {
                    case PlayerData.E_PLAYER_ACTION.E_MOVE:    //�ړ����Ă��鎞
                        m_PParamCoefficient.m_addGaugeValue += PData.MOVE_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_DASH:    //�_�b�V�����Ă��鎞
                        m_PParamCoefficient.m_addGaugeValue += PData.DASH_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_AVOID:   //������Ă��鎞
                        m_PParamCoefficient.m_addGaugeValue += PData.AVOID_GAUGE_VALUE;
                        break;
                    default:
                        break;
                }
                //�����ς݂̃A�N�V�������폜
                m_actionList.RemoveAt(0);
            }
        }

        //�Q�[�W�̕ω��ʂ̔��f
        m_actionValue += m_PParamCoefficient.m_addGaugeValue * m_PParamCoefficient.m_gaugeUpSpeed;
        m_actionValue -= m_PParamCoefficient.m_subGaugeValue * m_PParamCoefficient.m_gaugeDownSpeed;
        
        //���l�𒴂����艺��������̕␳����
        if (m_actionValue < 0.0f)
        {
            m_actionValue = 0.0f;
        }
        else if(m_actionValue > 100.0f)
        {
            m_actionValue = 100.0f;
        }

        //���l��������
        m_PParamCoefficient = new PlayerParamCoefficient();
    }

    /// <summary>
    /// �v���C���[�̋N�������A�N�V������o�^����֐�
    /// </summary>
    /// <param name="act"> �N�������A�N�V�����̎�� </param>
    public static void AddAction(PlayerData.E_PLAYER_ACTION act)
    {
        m_actionList.Add(act);
    }

    /// <summary>
    /// ���l�擾�p�̃v���p�e�B
    /// </summary>
    public static float ActionValue
    {
        get { return m_actionValue; }  //�擾�p
    }

    /// <summary>
    /// �Q�[�W����ϐ��擾�p�̃v���p�e�B
    /// </summary>
    public static PlayerParamCoefficient PParam
    {
        get { return m_PParamCoefficient; }  //�擾�p 
    }
}

/*----------------------------------------------------------------

�Z�Q�[�W�̓����𑬂������鎞
�@PlayerActionControler.PParam.m_gaugeUpSpeed = [����������{��];

�Z�Q�[�W�̓�����x�������鎞
�@PlayerActionControler.PParam.m_gaugeDownSpeed = [�x��������{��];

���|�����킹�邽�߃}�C�i�X�̐��l�����Ȃ�

  ----------------------------------------------------------------*/
