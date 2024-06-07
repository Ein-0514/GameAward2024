using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class ManagerContainer : ManagerBase
	{
		static ManagerContainer m_managerContainer;
		/// <summary>
		/// ManagerContainer���擾
		/// </summary>
		public static ManagerContainer instance => m_managerContainer;

		[SerializeField]
		GameManager m_gameManager;
		/// <summary>
		/// GameManager���擾
		/// </summary>
		public GameManager gameManager => m_gameManager;

		[SerializeField]
		StudioObjectManager m_studioObjectManager;
		/// <summary>
		/// StudioObjectManager���擾
		/// </summary>
		public StudioObjectManager studioObjectManager => m_studioObjectManager;

		[SerializeField]
		CharacterManager m_characterManager;
		/// <summary>
		/// CharacterManager���擾
		/// </summary>
		public CharacterManager characterManager => m_characterManager;

		[SerializeField]
		BulletManager m_bulletManger;
		/// <summary>
		/// BulletManager���擾
		/// </summary>
		public BulletManager bulletManger => m_bulletManger;

		[SerializeField]
		BackgroundManager m_backgroundManager;
		/// <summary>
		/// BackgroundManager���擾
		/// </summary>
		public BackgroundManager backgroundManager => m_backgroundManager;

		[SerializeField]
		UIManager m_uiManager;
		public UIManager uiManager => m_uiManager;

		private void Awake()
		{
			// �}�l�[�W���[�R���e�i���擾
			TryGetComponent<ManagerContainer>(out m_managerContainer);
		}

		private void OnDestroy()
		{
			// �}�l�[�W���[�R���e�i��j��
			m_managerContainer = null;
		}
	}
}