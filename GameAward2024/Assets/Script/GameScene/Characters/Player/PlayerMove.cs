using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameScene;

public class PlayerMove : MonoBehaviour
{
	const float MAX_DEGREE = Mathf.PI * 2.0f * Mathf.Rad2Deg;

	// �v���C���[�f�[�^�̎擾�p�ϐ�
	PlayerData m_playerData;

	// �v���C���[�p�����[�^�W��
	PlayerParamCoefficient m_paramCoefficient;

	PlayerActionControler m_playerActionControler;

	// �ڕW�i���W���g�p�j
	Transform m_enemyTrans;

    // ����t���O
    bool m_isDash = false;

    // �O�t���[���̃��[���h���W
    Vector3 m_prePos;

    // �v���C���[�̃|�W�V����
    Vector3 m_pos;

	// ���ۂɎg�p����~�^������
	Vector2 m_period = new Vector2();

    void Start()
    {
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
        m_playerData = characterManager.playerData;
		m_paramCoefficient = characterManager.buffDebuffHandler.m_paramCoefficient;
		m_playerActionControler = characterManager.playerActionController;
		m_enemyTrans = characterManager.enemyTrans;
		m_pos = m_prePos = transform.position;
    }

    void FixedUpdate()
    {
        // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
        m_prePos = m_pos;

        //--- ���E���E�̈ړ����Ȃ���ΏI������
        if (Mathf.Approximately(m_period.x, 0.0f) && Mathf.Approximately(m_period.y, 0.0f))
			return;

		// �_�b�V����
        if (m_isDash)	m_period /= m_playerData.DASH_MOVE_SPEED_MULTIPLIER;

		// �ړ����x��K�p
		m_period /= m_paramCoefficient.m_moveSpeed;
		m_period *= m_paramCoefficient.m_moveDirect;

		//--- �ړ�����
		if (m_period.x != 0.0f) PlayerCircularRotation(m_period.x, this.transform.up);
        if (m_period.y != 0.0f) PlayerCircularRotation(m_period.y, this.transform.right);
        transform.position = m_pos;
		m_paramCoefficient.m_moveDirect = m_period.normalized;

		// �ړ��ʂ����Z�b�g
		m_period.x = m_period.y = 0.0f;
    }

    /// <summary>
    /// ����͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveUp()
    {
		m_period.y = m_playerData.VERTICAL_MOVE_SPEED;
        ActionEntry();
    }

    /// <summary>
    /// �����͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveDown()
    {
		m_period.y = -m_playerData.VERTICAL_MOVE_SPEED;
        ActionEntry();
    }

    /// <summary>
    /// �����͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveLeft()
    {
		m_period.x = m_playerData.HORIZONTAL_MOVE_SPEED;
        ActionEntry();
    }

    /// <summary>
    /// �E���͂����Ƃ��̏����֐�
    /// </summary>
    public void OnMoveRight()
    {
		m_period.x = -m_playerData.HORIZONTAL_MOVE_SPEED;
        ActionEntry();
    }

    /// <summary>
    /// ����n�߂����̏����֐�
    /// </summary>
    public void OnDashStart(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_isDash = true;
    }

    /// <summary>
    /// ����I��������̏����֐�
    /// </summary>
    public void OnDashEnd(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        m_isDash = false;
    }

    /// <summary>
    /// �ړ����@�ɉ����ăA�N�V������o�^����֐�
    /// </summary>
    private void ActionEntry()
    {
        if(!m_isDash)
        {
			m_playerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.MOVE);
        }
        else
        {
			m_playerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.DASH);
        }
    }

    public void PlayerCircularRotation(float p, Vector3 axis)
    {
        //--- �ϐ��錾
        Vector3 center = m_enemyTrans.position;   //��]�̒��S
        var angleAxis = Quaternion.AngleAxis(MAX_DEGREE / p * Time.deltaTime, axis);     //�N�I�[�^�j�I���̌v�Z

        // �ړ�����Z�o
        m_pos -= center;
        m_pos = angleAxis * m_pos;
        m_pos += center;
    }
}