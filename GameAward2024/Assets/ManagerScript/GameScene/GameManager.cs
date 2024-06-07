using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class GameManager : ManagerBase
	{
		[SerializeField]
		GameTimer m_gameTimer;
		/// <summary>
		/// �Q�[���^�C�}�[���擾
		/// </summary>
		public GameTimer gameTimer => m_gameTimer;

		[SerializeField]
		SelectStageData m_selectStageData;
		/// <summary>
		/// �I�����ꂽ�X�e�[�W�̏����擾
		/// </summary>
		public SelectStageData selectStageData => m_selectStageData;
	}
}