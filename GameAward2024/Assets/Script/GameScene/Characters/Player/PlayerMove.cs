using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // �ڕW�i���W���g�p�j
    private Transform Enemy;      //TODO:CharacterManager����Q�Əo����悤�ɕύX

    // ��]��
    [SerializeField] private Vector3 _axis = Vector3.up;

    // ��b�~�^������
    [SerializeField] private float _period = 4;

    // �㉺�̈ړ���
    private float _vertical = 0.1f;

    // ����t���O
    private bool DashFlag;

    // �O�t���[���̃��[���h���W
    private Vector3 _prevPosition;

    Transform tr;
    Vector3 pos;

    void Start()
    {
        DashFlag = false;
        _prevPosition = transform.position;
        Enemy = GameObject.Find("Enemy").transform;        //TODO:CharacterManager����Q�Əo����悤�ɕύX
        tr = transform;
        pos = tr.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�ϐ��錾
        //var tr = transform; //�v���C���[�̏��
        float period;   //���ۂɎg�p����~�^������
        Vector3 _center = Enemy.position;   //��]�̒��S
        DashFlag = false;   //�����Ă���t���O�̃��Z�b�g
        //var pos = tr.position;  //�v���C���[�̃|�W�V����

        //TODO:InputSystem�ɒu������
        //�㉺�̈ړ�
        //if (Input.GetKey(KeyCode.W))
        //{//��
        //    pos.y += _vertical;
        //}
        //else if(Input.GetKey(KeyCode.S))
        //{//��
        //    pos.y -= _vertical;
        //}

        //�㉺�̈ړ��ʂ𔽉f
        tr.position = pos;

        //���E�̈ړ��i��]�ړ��j
        if (Input.GetKey(KeyCode.A))
        {//��
            period = _period;
        }
        else if (Input.GetKey(KeyCode.D))
        {//�E
            period = -_period;
        }
        else return;    //�ړ��L�[���͂�����ĂȂ���ΏI��

        if (Input.GetKey(KeyCode.LeftShift))
        {
            period /= 2.0f;
            DashFlag = true;
        }

        var angleAxis = Quaternion.AngleAxis(360 / period * Time.deltaTime, _axis);     //�N�I�[�^�j�I���̌v�Z
       
        //�ړ�����Z�o
        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        //�Z�o�������ʂ𔽉f
        tr.position = pos;

        if(!DashFlag)
        {//�����Ă���
            //�G�̕���������
            //tr.rotation = tr.rotation * angleAxis;
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
    }

    public void OnMove()
    {
        Debug.Log("MoveIvent");
    }

    public void OnMoveUp()
    {
        Debug.Log("A");
        pos.y += _vertical;
    }

    public void OnMoveDown()
    {
        pos.y -= _vertical;
    }

    public void OnMoveLeft()
    {
        Debug.Log("MoveIvent");
    }

    public void OnMoveRight()
    {
        Debug.Log("MoveIvent");
    }

    public void OnDash()
    {
        Debug.Log("MoveIvent");
    }
}


