using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class GameObserver : MonoBehaviour
{
	[SerializeField]
	SceneData m_sceneData;

    GameTimer m_gameTimer;
	PlayerGaugeController m_playerGaugeController;
    FadeController m_fadeController;
       
    void Start()
    {
		ManagerContainer managerContainer = ManagerContainer.instance;
        m_gameTimer = managerContainer.gameManager.gameTimer;
	 	m_playerGaugeController = managerContainer.uiManager.playerGaugeController;

		m_fadeController = FadeController.instance;
    }

    void Update()
    {
		//--- �t�F�[�h��������
		bool isfade = (m_fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE);
		if (isfade) return;

		//--- �^�C���A�b�v�̏ꍇ���Q�[���N���A
		if (m_gameTimer.IsTimeUp())
			m_fadeController.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_CLEAR_SCENE));

		//--- �Q�[�W��0or100�͈̔͂𒴂����ꍇ���Q�[���I�[�o�[
		if(m_playerGaugeController.IsOutOfGaugeValue())
			m_fadeController.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_OVER_SCENE));
	}
}