using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
	/// <summary>
	/// CSV�t�@�C���̃p�X
	/// </summary>
	const string CSV_FILE_PATH = "SettingCSV/Player.csv";

	//--- �v���C���[�A�N�V�����̗񋓌^
	public enum E_PLAYER_ACTION
	{
		E_MOVE = 0,     //�ړ�
		E_DASH,         //����
		E_AVOID,        //���
		E_MAX,
	}

	//--------------------
	// �������p�p�����[�^

	float m_startGaugeValue = 60.0f;
	/// <summary>
	/// �Q�[�W�̏����l
	/// </summary>
	public float START_GAUGE_VALUE => m_startGaugeValue;

	//--------------------
	// �ړ��p�p�����[�^

	float m_verticalMoveSpeed = 0.1f;
	/// <summary>
	/// �c�̈ړ����x
	/// </summary>
	public float VERTICAL_MOVE_SPEED => m_verticalMoveSpeed;

	float m_horizontalMoveSpeed = 4.0f;
	/// <summary>
	/// ���̈ړ����x
	/// </summary>
	public float HORIZONTAL_MOVE_SPEED => m_horizontalMoveSpeed;

	float m_dashMoveSpeedMultiplier = 2.0f;
	/// <summary>
	/// �_�b�V�����̈ړ����x�̏搔
	/// </summary>
	public float DASH_MOVE_SPEED_MULTIPLIER => m_dashMoveSpeedMultiplier;

	float m_avoidAnceMultiplier = 1.3f;
	/// <summary>
	/// ����̑��x�̌������i�傫���قǑ����~�܂�j
	/// </summary>
	public float AVOID_ANCE_MULTIPLIER => m_avoidAnceMultiplier;

	float m_avoidStartValue = 0.3f;
	/// <summary>
	/// ����̏����l�i�傫���قǏ������x���j
	/// </summary>
	public float AVOID_START_VALUE => m_avoidStartValue;

	float m_avoidRimitValue = 8.0f;
	/// <summary>
	/// ����̒�~����l�i�傫���قǒ�~�܂ł̓������L�т�j
	/// </summary>
	public float AVOID_RIMIT_VALUE => m_avoidRimitValue;

	//--------------------
	// �Q�[�W�̒l�̕ω���

	float m_stopGaugeValue = 0.3f;
	/// <summary>
	/// ��~���̕ω���
	/// </summary>
	public float STOP_GAUGE_VALUE => m_stopGaugeValue;

	float m_moveGaugeValue = 0.1f;
	/// <summary>
	/// �ړ����̕ω���
	/// </summary>
	public float MOVE_GAUGE_VALUE => m_moveGaugeValue;

	float m_dashGaugeValue = 0.2f;
	/// <summary>
	/// ���鎞�̕ω���
	/// </summary>
	public float DASH_GAUGE_VALUE => m_dashGaugeValue;

	float m_avoidGaugeValue = 0.5f;
	/// <summary>
	/// ������̕ω���
	/// </summary>
	public float AVOID_GAUGE_VALUE => m_avoidGaugeValue;

	[SerializeField]
	TextAsset m_csvText;
	CSVReader m_csvReader = new CSVReader();

	public void Load()
	{
		//--- CSV�t�@�C����ǂݍ���
#if DEVELOPMENT_BUILD
		m_csvReader.LoadCSV(CSV_FILE_PATH);
#else
		m_csvReader.LoadCSV(m_csvText);
#endif
	}

	public void GetDatas()
	{
		var paramDatas = m_csvReader.m_csvDatas;

		//--- �l�̋z�o��
		paramDatas[nameof(m_startGaugeValue			)].TryGetData(out m_startGaugeValue);
		paramDatas[nameof(m_verticalMoveSpeed		)].TryGetData(out m_verticalMoveSpeed);
		paramDatas[nameof(m_horizontalMoveSpeed		)].TryGetData(out m_horizontalMoveSpeed);
		paramDatas[nameof(m_dashMoveSpeedMultiplier	)].TryGetData(out m_dashMoveSpeedMultiplier);
		paramDatas[nameof(m_avoidAnceMultiplier		)].TryGetData(out m_avoidAnceMultiplier);
		paramDatas[nameof(m_avoidStartValue			)].TryGetData(out m_avoidStartValue);
		paramDatas[nameof(m_avoidRimitValue			)].TryGetData(out m_avoidRimitValue);
		paramDatas[nameof(m_stopGaugeValue			)].TryGetData(out m_stopGaugeValue);
		paramDatas[nameof(m_moveGaugeValue			)].TryGetData(out m_moveGaugeValue);
		paramDatas[nameof(m_dashGaugeValue			)].TryGetData(out m_dashGaugeValue);
		paramDatas[nameof(m_avoidGaugeValue			)].TryGetData(out m_avoidGaugeValue);
	}
}