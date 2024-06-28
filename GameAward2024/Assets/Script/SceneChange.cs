using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	FadeController m_fadeController;
	[SerializeField]
	SceneData m_sceneData;
	[SerializeField]
	SceneData.E_SCENE_KIND m_nextScene;

	void Start()
	{
		m_fadeController = FadeController.instance;
	}

	void Update()
    {
		// �t�F�[�h���͏������Ȃ�
		if (m_fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE) return;

		// ���݂̃L�[�{�[�h���
		var keyboardCurrent = Keyboard.current;

		// �L�[�{�[�h�ڑ��`�F�b�N
		if (keyboardCurrent != null)
		{
			// Space�L�[�̓��͏�Ԏ擾
			var spaceKey = keyboardCurrent.spaceKey;

			//--- Space�L�[�������ꂽ�u�Ԃ��ǂ���
			if (spaceKey.wasPressedThisFrame)
			{
				FadeController.instance.FadeSceneLoad(m_sceneData.GetSceneName(m_nextScene));
				return;
			}
		}

		// ���݂̃Q�[���p�b�h���
		var gamepadCurrent = Gamepad.current;

		// �Q�[���p�b�h�ڑ��`�F�b�N
		if (gamepadCurrent != null)
		{
			// A�{�^���̓��͏�Ԏ擾
			var aButton = gamepadCurrent.aButton;

			//--- A�{�^���������ꂽ�u�Ԃ��ǂ���
			if (aButton.wasPressedThisFrame)
				FadeController.instance.FadeSceneLoad(m_sceneData.GetSceneName(m_nextScene));
		}
	}
}