using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
	public enum E_BUTTON_STATE
	{
		SELECTABLE,		// �I����
		SELECTED,		// �I��
		UNSELECTABLE,	// �I��s��
		PUSHED			// �����ꂽ
	}

	readonly Color[] STATE_COLOR =
	{
		new Color(0.75f, 0.75f, 0.75f, 1.0f),
		new Color(1.00f, 1.00f, 1.00f, 1.0f),
		new Color(0.25f, 0.25f, 0.25f, 1.0f),
	};

	[SerializeField]
	RawImage m_rawImage;

	E_BUTTON_STATE m_state;

	/// <summary>
	/// RawImage�̃e�N�X�`����ݒ�
	/// </summary>
	/// <param name="texture">�e�N�X�`��</param>
	public void SetTexture(Texture texture)
	{
		if (m_rawImage == null) return;
		if (texture == null) return;

		m_rawImage.texture = texture;
	}

	/// <summary>
	/// �{�^���̏�Ԃ�ݒ�
	/// </summary>
	/// <param name="state">�{�^���̏�</param>
	public void SetState(E_BUTTON_STATE state)
	{
		if (m_rawImage == null) return;

		m_rawImage.color = STATE_COLOR[(int)state];
	}

	/// <summary>
	/// �{�^���������ꂽ���̏���
	/// </summary>
	public void OnPush()
	{
		if (m_state == E_BUTTON_STATE.UNSELECTABLE) return;


	}
}