using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class GameObserver : MonoBehaviour
{
	[SerializeField]
	SceneData m_sceneData;

    GameTimer m_gameTimer;
	PlayerControler m_player;
	EnemyBase m_enemy;
    FadeController m_fadeController;
       
    void Start()
    {
		ManagerContainer managerContainer = ManagerContainer.instance;
        m_gameTimer = managerContainer.gameManager.gameTimer;
	 	CharacterManager characterManager = managerContainer.characterManager;
		m_player = characterManager.playerController;
		m_enemy = characterManager.enemyData;
		m_fadeController = FadeController.instance;
	}

    void Update()
    {
		//--- �t�F�[�h��������
		bool isfade = (m_fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE);
		if (isfade) return;

		//--- �^�C���A�b�v�̏ꍇ���Q�[���I�[�o�[
		if (m_gameTimer.IsTimeUp())
		{
			m_fadeController.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_OVER_SCENE));
			return;
		}
	
		//--- �v���C���[�����S�����ꍇ���Q�[���I�[�o�[
		if(m_player.IsDead())
		{
			m_fadeController.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_OVER_SCENE));
			return;
		}

		//--- �G�����S�����ꍇ���Q�[���N���A
		if (m_enemy.IsDead())
		{
			m_fadeController.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_CLEAR_SCENE));
			return;
		}
	}
}