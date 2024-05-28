using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
	//--- �v���C���[�A�N�V�����̗񋓌^
	public enum E_PLAYER_ACTION
	{
		E_MOVE = 0,     //�ړ�
		E_DASH,         //����
		E_AVOID,        //���
		E_MAX,
	}

	[Header("�������p")]
	//--- �Q�[���J�n���̃Q�[�W�̐��l�̒萔
	[SerializeField]
	float m_startGaugeValue = 60.0f;
	public float START_GAUGE_VALUE => m_startGaugeValue;

	[Space(10)]
	[Header("�v���C���[�p�����[�^")]
	[SerializeField]
	float m_verticalMoveSpeed = 0.1f;
	/// <summary>
	/// �c�̈ړ����x
	/// </summary>
	public float VERTICAL_MOVE_SPEED => m_verticalMoveSpeed;

	[SerializeField]
	float m_horizontalMoveSpeed = 4.0f;
	/// <summary>
	/// ���̈ړ����x
	/// </summary>
	public float HORIZONTAL_MOVE_SPEED => m_horizontalMoveSpeed;

	[SerializeField]
	float m_dodgeRotXZ = 3.0f;
	/// <summary>
	/// �c�̉�����̈ړ��p�x
	/// </summary>
	public float DODGE_ROT_XZ => m_dodgeRotXZ;

	[SerializeField]
	float m_dodgeRotY = 3.0f;
	/// <summary>
	/// ���̉�����̈ړ��p�x
	/// </summary>
	public float DODGE_ROT_Y => m_dodgeRotY;

	[Space(10)]
	[Header("���̑�")]
	//--- �Q�[�W�̒l��ω�������萔
	[SerializeField]
	float m_stopGaugeValue = -0.3f;
	/// <summary>
	/// ��~
	/// </summary>
	public float STOP_GAUGE_VALUE => m_stopGaugeValue;

	[SerializeField]
	float m_moveGaugeValue = 0.1f;
	/// <summary>
	/// �ړ�
	/// </summary>
	public float MOVE_GAUGE_VALUE => m_moveGaugeValue;

	[SerializeField]
	float m_dashGaugeValue = 0.2f;
	/// <summary>
	/// ����
	/// </summary>
	public float DASH_GAUGE_VALUE => m_dashGaugeValue;

	[SerializeField]
	float m_avoidGaugeValue = 0.5f;
	/// <summary>
	/// ���
	/// </summary>
	public float AVOID_GAUGE_VALUE => m_avoidGaugeValue;
}