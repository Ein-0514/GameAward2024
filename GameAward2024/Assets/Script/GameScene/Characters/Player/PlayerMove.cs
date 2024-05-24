using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameScene;

public class PlayerMove : MonoBehaviour
{
    //�ړ����x�Ɋւ���萔
    const float VERTICAL_MOVE = 0.1f;       //�c
    const float HORIZONTAL_MOVE = 4.0f;     //��

    // �ڕW�i���W���g�p�j
    private Transform Enemy;

    // ��]��
    [SerializeField] private Vector3 _axis = Vector3.up;

    // ��b�~�^������
    [SerializeField] private float _period = HORIZONTAL_MOVE;

    //�㉺�̐����ix:max y:min�j
    [SerializeField] private Vector2 VerticalRemit;

    // �㉺�̈ړ���
    private float _vertical = VERTICAL_MOVE;

    // ����t���O
    private bool DashFlag;

    // �O�t���[���̃��[���h���W
    private Vector3 _prevPosition;

    //�v���C���[�̏��
    Transform tr;

    //�v���C���[�̃|�W�V����
    Vector3 pos;

    //���ۂɎg�p����~�^������
    float period;

    void Start()
    {
        Enemy = ManagerContainer.GetManagerContainer().m_characterManager.m_enemy;
        DashFlag = false;
        _prevPosition = transform.position;
        tr = transform;
        pos = tr.position;
        period = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���E�̈ړ����Ȃ���ΏI������
        if (period == 0.0)
        {
            // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
            _prevPosition = pos;

            return;
        }

        //�ϐ��錾
        Vector3 _center = Enemy.position;   //��]�̒��S
        
        //�㉺�̈ړ��ʂ𔽉f
        tr.position = pos;

        if(DashFlag)
        {
            period /= 2.0f;
        }

        var angleAxis = Quaternion.AngleAxis(360 / period * Time.deltaTime, _axis);     //�N�I�[�^�j�I���̌v�Z

        //�ړ�����Z�o
        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        //�Z�o�������ʂ𔽉f
        tr.position = pos;

        //�v���C���[�̑̂̌������s���ɍ��킹�Ē���
        if(!DashFlag)
        {//�����Ă���
            Vector3 trans = Enemy.position; //�G�̍��W�擾
            trans = new Vector3(trans.x, tr.position.y, trans.z);   //Y�������𖳌���
            tr.LookAt(trans);   //�G�̕����ɉ�]
        }
        else
        {//�����Ă���
            //�O�t���[������̈ړ��ʂ��v�Z
            var delta = pos - _prevPosition;

            // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
            var rotation = Quaternion.LookRotation(delta, Vector3.up);

            // �I�u�W�F�N�g�̉�]�ɔ��f
            tr.rotation = rotation;
        }

        // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
        _prevPosition = pos;

        //�e�ϐ��̃��Z�b�g
        period = 0.0f;      //���E�̈ړ��ʂ����Z�b�g
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
        pos.y += _vertical;
        if (pos.y > VerticalRemit.x) pos.y = VerticalRemit.x;
        tr.position = pos;
        ActionEntry();
    }

    /// <summary>
    /// �����͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveDown()
    {
        pos.y -= _vertical;
        if (pos.y < VerticalRemit.y) pos.y = VerticalRemit.y;
        tr.position = pos;
        ActionEntry();
    }

    /// <summary>
    /// �����͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveLeft()
    {
        period = _period;
        ActionEntry();
    }

    /// <summary>
    /// �E���͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveRight()
    {
        period = -_period;
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
            PlayerActionControler.AddAction(PlayerActionControler.E_PLAYER_ACTION.E_MOVE);
        }
        else
        {
            PlayerActionControler.AddAction(PlayerActionControler.E_PLAYER_ACTION.E_DASH);
        }
    }
}


