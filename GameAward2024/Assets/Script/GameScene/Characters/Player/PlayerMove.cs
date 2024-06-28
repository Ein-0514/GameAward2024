using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameScene;

public class PlayerMove : MonoBehaviour
{
	const float MAX_DEGREE = Mathf.PI * 2.0f * Mathf.Rad2Deg;


	PlayerData m_playerData;    // �v���C���[�f�[�^�̎擾�p�ϐ�
	PlayerParamCoefficient m_paramCoefficient;  // �v���C���[�p�����[�^�W��

	Transform m_enemyTrans; // �ڕW�i���W���g�p�j

	bool m_isDash = false;	// ����t���O
	bool m_isAvoid = false;	// ����t���O
	
	Vector2 m_moveDirect = new Vector2();	// �ړ��������
	Vector2 m_period = new Vector2();		// �~�^������

	void Start()
    {
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
        m_playerData = characterManager.playerData;
		m_paramCoefficient = characterManager.buffDebuffHandler.m_paramCoefficient;
		m_enemyTrans = characterManager.enemyTrans;
		
	}

    void FixedUpdate()
	{
		if (m_isAvoid) Avoid();
		else Move();
	}

	/// <summary>
	/// �ړ�����
	/// </summary>
	void Move()
	{
		//--- �~�^���������v�Z
		m_moveDirect.Normalize();
		m_period.x = -m_moveDirect.x * m_playerData.HORIZONTAL_MOVE_SPEED;
		m_period.y =  m_moveDirect.y * m_playerData.VERTICAL_MOVE_SPEED;
		if (m_isDash) m_period /= m_playerData.DASH_MOVE_SPEED_MULTIPLIER;

		//--- �ړ����x��K�p
		m_period /= m_paramCoefficient.m_moveSpeed;
		m_period *= m_paramCoefficient.m_moveDirect;

		//--- �ړ�����
		CircularRotation(m_period.x, this.transform.up);
		CircularRotation(m_period.y, this.transform.right);
	}

	/// <summary>
	/// �������
	/// </summary>
	void Avoid()
	{
		//--- �ړ�����
		CircularRotation(m_period.x, this.transform.up);
		CircularRotation(m_period.y, this.transform.right);

		// ���������Ă���
		m_period *= m_playerData.AVOID_ANCE_MULTIPLIER;

		//--- ����̌��E�l�Œ�~������
		if (Mathf.Abs(m_period.x) >= m_playerData.AVOID_RIMIT_VALUE)
			m_period.x = 0.0f;
		if (Mathf.Abs(m_period.y) >= m_playerData.AVOID_RIMIT_VALUE)
			m_period.y = 0.0f;

		// ����̏I���𔻒�
		m_isAvoid = (m_period != Vector2.zero);
	}

	/// <summary>
	/// �G�𒆐S�Ɏw��̎��ŉ�]
	/// </summary>
	/// <param name="p">��]��</param>
	/// <param name="axis">��]��</param>
	public void CircularRotation(float p, Vector3 axis)
	{
		if (Mathf.Approximately(p, 0.0f)) return;

		//--- �ϐ��錾
		Vector3 center = m_enemyTrans.position;   //��]�̒��S
		var angleAxis = Quaternion.AngleAxis(MAX_DEGREE / p * Time.deltaTime, axis);     //�N�I�[�^�j�I���̌v�Z

		// �ړ�����Z�o
		Vector3 pos = transform.position;
		pos -= center;
		pos = angleAxis * pos;
		pos += center;
		transform.position = pos;
	}

	/// <summary>
	/// �����]������
	/// </summary>
	public void OnChangeDirect(InputAction.CallbackContext context)
	{
		if (!context.performed) return;

		Vector2 axis = context.ReadValue<Vector2>();
		m_moveDirect = axis.normalized;

		//--- �^�C�}�[�̃J�E���g���n�߂�
		GameTimer gameTimer = ManagerContainer.instance.gameManager.gameTimer;
		if (gameTimer.m_isStart) return;
		gameTimer.StartCount();
	}
	
    /// <summary>
    /// ����n�߂����̏����֐�
    /// </summary>
    public void OnDashStart(InputAction.CallbackContext context)
    {
       if (!context.performed) return;
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

	public void OnAvoid(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		if (m_isAvoid) return;

		//--- �������������v�Z
		m_moveDirect.Normalize();
		m_period.x = -m_playerData.AVOID_START_VALUE * m_moveDirect.x;
		m_period.y =  m_playerData.AVOID_START_VALUE * m_moveDirect.y;
		m_period *= m_paramCoefficient.m_moveDirect;
		m_period /= m_paramCoefficient.m_moveSpeed;

		m_isAvoid = true;   // ����t���O�𗧂Ă�
	}
}