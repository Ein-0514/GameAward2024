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

	[field: Header("�������p")]
	//--- �Q�[���J�n���̃Q�[�W�̐��l�̒萔
	[field: SerializeField]
	public float START_GAUGE_VALUE { get; private set; } = 60.0f;

	[field: Space(10)]
	[field: Header("�v���C���[�p�����[�^")]
	[field: SerializeField]
	public float VERTICAL_MOVE_SPEED { get; private set; } = 0.1f;		// �c�̈ړ����x
	[field: SerializeField]
	public float HORIZONTAL_MOVE_SPEED { get; private set; } = 4.0f;    // ���̈ړ����x
	[field: SerializeField]
	public float DODGE_ROT_XZ { get; private set; } = 3.0f;				// �c�̉�����̈ړ��p�x
	[field: SerializeField]
	public float DODGE_ROT_Y { get; private set; } = 3.0f;              // ���̉�����̈ړ��p�x

	[field: Space(10)]
	[field: Header("���̑�")]
	//--- �Q�[�W�̒l��ω�������萔
	[field: SerializeField]
	public float STOP_GAUGE_VALUE { get; private set; } = -0.3f;     // ��~
	[field: SerializeField]
	public float MOVE_GAUGE_VALUE { get; private set; } = 0.1f;      // �ړ�
	[field: SerializeField]
	public float DASH_GAUGE_VALUE { get; private set; } = 0.2f;      // ����
	[field: SerializeField]
	public float AVOID_GAUGE_VALUE { get; private set; } = 0.5f;     // ���
}