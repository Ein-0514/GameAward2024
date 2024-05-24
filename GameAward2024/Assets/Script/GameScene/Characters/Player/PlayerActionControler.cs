using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionControler : MonoBehaviour
{
    ////�v���C���[�A�N�V�����̗񋓌^
    //public enum E_PLAYER_ACTION
    //{
    //    E_MOVE = 0,     //�ړ�
    //    E_DASH,         //����
    //    E_AVOID,        //���
    //    E_MAX,          
    //}

    ////�Q�[�W�̒l��ω�������萔
    //const float STOP_VALUE = -0.3f;     //��~
    //const float MOVE_VALUE = 0.1f;      //�ړ�
    //const float DASH_VALUE = 0.2f;      //����
    //const float AVOID_VALUE = 0.5f;     //���

    ////�Q�[���J�n���̃Q�[�W�̐��l�̒萔
    //const float START_VALUE = 60.0f;

    // �v���C���[�f�[�^�̎擾�p�ϐ�
    [SerializeField] private PlayerData PData;

    //�v���C���[���Ȃ�̓������������ۑ����郊�X�g
    static List<PlayerData.E_PLAYER_ACTION> m_actionList;

    //�v���C���[�Q�[�W�ɔ��f�����鐔�l��ێ�����
    [SerializeField] private static float m_actionValue;        //0-100�ŊǗ�����

    // Start is called before the first frame update
    void Start()
    {
        //�e��ϐ�������
        m_actionList = new List<PlayerData.E_PLAYER_ACTION>();     //���X�g�̐���
        m_actionValue = PData.START_GAUGE_VALUE;     //�Q�[�W�̐��l������
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_actionList.Count == 0)
        {//�t���[���ԂŃA�N�V�������N�����Ă��Ȃ� �� ��~���Ă���
            m_actionValue += PData.STOP_GAUGE_VALUE;        //��~���Ă��鎞
        }
        else
        {//�t���[���ԂŃA�N�V�������N�����Ă���
            while(m_actionList.Count != 0)
            {
                //�o�^����Ă���A�N�V�����ɉ����鐔�l��ǉ�
                switch (m_actionList[0])
                {
                    case PlayerData.E_PLAYER_ACTION.E_MOVE:    //�ړ����Ă��鎞
                        m_actionValue += PData.MOVE_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_DASH:    //�_�b�V�����Ă��鎞
                        m_actionValue += PData.DASH_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_AVOID:   //������Ă��鎞
                        m_actionValue += PData.AVOID_GAUGE_VALUE;
                        break;
                    default:
                        break;
                }
                //�����ς݂̃A�N�V�������폜
                m_actionList.RemoveAt(0);
            }
        }

        //���l�𒴂����艺��������̕␳����
        if(m_actionValue < 0.0f)
        {
            m_actionValue = 0.0f;
        }
        else if(m_actionValue > 100.0f)
        {
            m_actionValue = 100.0f;
        }
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
}
