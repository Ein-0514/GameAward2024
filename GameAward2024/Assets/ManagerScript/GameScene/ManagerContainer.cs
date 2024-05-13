using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class ManagerContainer : MonoBehaviour
	{
		static ManagerContainer m_managerContainer;

		public GameScene.GameManager m_gameManager;
		public GameScene.UIManager m_uiManager;
		public GameScene.StudioObjectManager m_studioObjectManager;
		public GameScene.CharacterManager m_characterManager;
		public GameScene.BackgroundManager m_backgroundManager;

		private void Awake()
		{
			// �}�l�[�W���[�R���e�i���擾
			TryGetComponent<ManagerContainer>(out m_managerContainer);
		}

		private void OnDestroy()
		{
			// �}�l�[�W���[�R���e�i�̎Q�Ƃ�j��
			m_managerContainer = null;
		}

		/// <summary>
		/// �}�l�[�W���[�R���e�i�̎Q�Ƃ��擾
		/// </summary>
		/// <returns>�}�l�[�W���[�R���e�i�̎Q��</returns>
		public static ManagerContainer GetManagerContainer()
		{
			return m_managerContainer;
		}
	}
}