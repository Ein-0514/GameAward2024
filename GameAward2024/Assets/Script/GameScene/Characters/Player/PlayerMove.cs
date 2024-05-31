using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameScene;

public class PlayerMove : MonoBehaviour
{
    // �v���C���[�f�[�^�̎擾�p�ϐ�
    private PlayerData PData;

    // �ڕW�i���W���g�p�j
    private Transform Enemy;

    // ��]��
    [SerializeField] private Vector3 _axis = Vector3.up;

    // ��b�~�^������
    [SerializeField] private float _period;

    // �㉺�̐����ix:max y:min�j
    [SerializeField] private Vector2 VerticalRemit;

    // �㉺�̈ړ���
    private float _vertical;

    // ����t���O
    private bool DashFlag;

    // �O�t���[���̃��[���h���W
    private Vector3 _prevPosition;

    // �v���C���[�̏��
    Transform tr;

    // �v���C���[�̃|�W�V����
    Vector3 pos;

    // ���ۂɎg�p����~�^������
    float VPeriod;
    float HPeriod;

    //i�ړ��������i�[
    Vector2 Dir;

    void Start()
    {
        PData = PlayerDataParam.data;
        Enemy = ManagerContainer.GetManagerContainer().m_characterManager.m_enemy;
        _period = PData.HORIZONTAL_MOVE_SPEED;
        _vertical = PData.VERTICAL_MOVE_SPEED;
        DashFlag = false;
        _prevPosition = transform.position;
        tr = transform;
        pos = tr.position;
        VPeriod = 0.0f;
        HPeriod = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���E�̈ړ����Ȃ���ΏI������
        if (VPeriod == 0.0 && HPeriod == 0.0f)
        {
            // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
            _prevPosition = pos;

            return;
        }

        // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
        _prevPosition = pos;

        if (DashFlag)
        {
            VPeriod /= 2.0f;
            HPeriod /= 2.0f;
        }

        if (VPeriod != 0.0f) PlayerCircularRotation(VPeriod, this.transform.up);
        if (HPeriod != 0.0f) PlayerCircularRotation(HPeriod, this.transform.right);

        

        transform.position = pos;

        //if(VPeriod < 0.0f)
        //{
        //    Dir.x = -1.0f;
        //}
        //else if(VPeriod > 0.0f)
        //{
        //    Dir.x = 1.0f;
        //}

        //Vector3 trans = Enemy.position; //�G�̍��W�擾
        //trans = new Vector3(trans.x, tr.position.y, trans.z);   //Y�������𖳌���
        //tr.LookAt(trans);   //�G�̕����ɉ�]

        //PlayerActionControler.PParam.m_moveDirect = Dir.normalized;

        //�v���C���[�̑̂̌������s���ɍ��킹�Ē���
        //if(!DashFlag)
        //{//�����Ă���
        //    Vector3 trans = Enemy.position; //�G�̍��W�擾
        //    trans = new Vector3(trans.x, tr.position.y, trans.z);   //Y�������𖳌���
        //    tr.LookAt(trans);   //�G�̕����ɉ�]
        //}
        //else
        //{//�����Ă���
        //    //�O�t���[������̈ړ��ʂ��v�Z
        //    var delta = pos - _prevPosition;

        //    // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
        //    var rotation = Quaternion.LookRotation(delta, Vector3.up);

        //    // �I�u�W�F�N�g�̉�]�ɔ��f
        //    tr.rotation = rotation;
        //}



        //�e�ϐ��̃��Z�b�g
        VPeriod = 0.0f;      //���E�̈ړ��ʂ����Z�b�g
        HPeriod = 0.0f;      //�c�̈ړ��ʂ����Z�b�g

    }

    //public void OnMove()
    //{
    //    Debug.Log("MoveIvent");
    //}

    /// <summary>
    /// ����͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveUp()
    {
        //pos.y += _vertical;
        //if (pos.y > VerticalRemit.x)
        //{
        //    pos.y = VerticalRemit.x;
        //    return;
        //}
        //tr.position = pos;
        //ActionEntry();
        //Dir.y = 1.0f;
        HPeriod = _period;
        ActionEntry();
    }

    /// <summary>
    /// �����͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveDown()
    {
        //pos.y -= _vertical;
        //if (pos.y < VerticalRemit.y)
        //{
        //    pos.y = VerticalRemit.y;
        //    return;
        //}
        //tr.position = pos;
        //ActionEntry();
        //Dir.y = -1.0f;
        HPeriod = -_period;
        ActionEntry();
    }

    /// <summary>
    /// �����͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveLeft()
    {
        VPeriod = _period;
        ActionEntry();
    }

    /// <summary>
    /// �E���͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveRight()
    {
        VPeriod = -_period;
        ActionEntry();
    }

    /// <summary>
    /// ����n�߂����̏����֐�
    /// </summary>
    public void OnDashStart(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        DashFlag = true;
    }

    /// <summary>
    /// ����I��������̏����֐�
    /// </summary>
    public void OnDashEnd(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        DashFlag = false;
    }

    /// <summary>
    /// �ړ����@�ɉ����ăA�N�V������o�^����֐�
    /// </summary>
    private void ActionEntry()
    {
        if(!DashFlag)
        {
            PlayerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.E_MOVE);
        }
        else
        {
            PlayerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.E_DASH);
        }
    }

    public void PlayerCircularRotation(float p, Vector3 axis)
    {
        Debug.Log(axis);

        //�ϐ��錾
        Vector3 _center = Enemy.position;   //��]�̒��S
        var angleAxis = Quaternion.AngleAxis(360 / p * Time.deltaTime, axis);     //�N�I�[�^�j�I���̌v�Z

        //�ړ�����Z�o
        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;
    }
}


