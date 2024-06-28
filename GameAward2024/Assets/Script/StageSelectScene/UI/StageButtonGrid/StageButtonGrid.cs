using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StageButtonGrid : MonoBehaviour
{
	const int ERROR_IDX = -1;

	[SerializeField]
	SceneData m_sceneData;
	[SerializeField]
	EnemyDataList m_enemyDataList;
	[SerializeField]
	SelectStageData m_stageData;
	[SerializeField]
	GridLayoutGroup m_grid;
	[SerializeField]
	StageButton m_stageButton;

	List<StageButton> m_buttons = new List<StageButton>();
	Vector2Int m_selectPos = new Vector2Int();

    void Start()
    {
		EnemyDataList.E_ENEMY_KIND maxEnemyNum = EnemyDataList.E_ENEMY_KIND.MAX;

		for(int i = 0; i < (int)maxEnemyNum; ++i)
		{
			// �{�^�����쐬
			StageButton button = Instantiate(m_stageButton);

			//--- �{�^���̐ݒ�
			EnemyDataList.E_ENEMY_KIND enemyKind = (EnemyDataList.E_ENEMY_KIND)i;
			button.SetTexture(m_enemyDataList.GetStageImage(enemyKind));
			button.SetState(StageButton.E_BUTTON_STATE.SELECTABLE); // TODO:�Z�[�u�f�[�^�ɂ���ĕύX���Ă���

			// ���g�̎q�ɐݒ�
			button.transform.SetParent(transform);

			// ���X�g�Ƀ{�^����ǉ�
			m_buttons.Add(button);
		}

		// �擪�̗v�f��I�𒆂ɂ���
		m_buttons[0].SetState(StageButton.E_BUTTON_STATE.SELECTED);
	}

	/// <summary>
	/// �J�[�\���ړ����̏���
	/// </summary>
	public void OnMoveCursor(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		FadeController fadeController = FadeController.instance;
		if (fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE) return;

		Vector2Int maxElementNum = CalcGridElement(m_grid);

		//--- ���ݑI�𒆂̃{�^����I���\��Ԃɂ���
		int idx = ToIndex(m_selectPos, maxElementNum);
		m_buttons[idx].SetState(StageButton.E_BUTTON_STATE.SELECTABLE);
		Vector2Int prePos = m_selectPos;	// �ߋ��̍��W

		//--- �ړ���̃{�^����I�𒆂̏�Ԃɂ���
		Vector2 inputVector2 = context.ReadValue<Vector2>();
		Vector2Int value = new Vector2Int((int)inputVector2.x, (int)-inputVector2.y);
		while(true)
		{
			m_selectPos += value;   // �I�𒆂̍��W���ړ�
			m_selectPos += maxElementNum;

			//--- �O���b�h�̒��Ń��[�v������
			m_selectPos.x %= maxElementNum.x;
			m_selectPos.y %= maxElementNum.y;

			idx = ToIndex(m_selectPos, maxElementNum);

			// �G���[�l�łȂ��A�����X�g�͈͓̔��ł���ꍇ�̂�
			if (idx != ERROR_IDX && idx < m_buttons.Count) break;
		}
		m_buttons[idx].SetState(StageButton.E_BUTTON_STATE.SELECTED);
	}

	/// <summary>
	/// �X�e�[�W�I�����̏���
	/// </summary>
	public void OnPushSelectButton(InputAction.CallbackContext context)
	{
		if (!context.performed) return;

		//--- �G�̔ԍ����v�Z���A�f�[�^��n��
		Vector2Int maxElementNum = CalcGridElement(m_grid);
		EnemyDataList.E_ENEMY_KIND enemyKind;
		enemyKind = (EnemyDataList.E_ENEMY_KIND)ToIndex(m_selectPos, maxElementNum);
		m_stageData.m_enemyKind = enemyKind;

		//--- �Q�[���V�[���֑J��
		FadeController instance = FadeController.instance;
		instance.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_SCENE));
	}

	Vector2Int CalcGridElement(GridLayoutGroup grid)
	{
		Vector2Int result = new Vector2Int();

		//--- �v�Z�ɕK�v�ȏ����擾
		RectTransform rt;
		grid.TryGetComponent(out rt);
		int childCnt = grid.transform.childCount;

		//--- GridLayoutGroup�̃Z���T�C�Y�ƊԊu
		Vector2 cellSize = grid.cellSize;
		Vector2 spacing = grid.spacing;

		//--- �s�E��̐����v�Z
		float width = rt.rect.width;
		result.x = Mathf.FloorToInt((width + spacing.x) / (cellSize.x + spacing.x));
		result.y = Mathf.CeilToInt((float)childCnt / result.x);

		return result;
	}

	int ToIndex(Vector2Int elementNum, Vector2Int maxElementNum)
	{
		//--- �͈͊O�A�N�Z�X�̏ꍇ�̓G���[�l��Ԃ�
		if (elementNum.x < 0) return ERROR_IDX;
		if (elementNum.y < 0) return ERROR_IDX;
		if (elementNum.x > maxElementNum.x) return ERROR_IDX;
		if (elementNum.y > maxElementNum.y) return ERROR_IDX;

		return elementNum.x + (elementNum.y * maxElementNum.x);
	}
}