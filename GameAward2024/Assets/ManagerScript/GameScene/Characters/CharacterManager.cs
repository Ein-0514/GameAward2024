using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class CharacterManager : MonoBehaviour
	{
		[SerializeField]
		PlayerData m_playerData;
		/// <summary>
		/// �v���C���[�̃f�[�^���擾
		/// </summary>
		public PlayerData playerData => m_playerData;

		[SerializeField]
		Transform m_playerTrans;
		/// <summary>
		/// �v���C���[��Transform���擾
		/// </summary>
		public Transform playerTrans => m_playerTrans;

		[SerializeField]
		PlayerActionControler m_playerActionController;
		/// <summary>
		/// PlayerActionController���擾
		/// </summary>
		public PlayerActionControler playerActionController => m_playerActionController;

		[SerializeField]
		Transform m_enemyTrans;
		/// <summary>
		/// �G��Transform���擾
		/// </summary>
		public Transform enemyTrans => m_enemyTrans;

		[SerializeField]
		BuffDebuffHandler m_buffDebuffHandler;
		/// <summary>
		/// BuffDebuffHandler���擾
		/// </summary>
		public BuffDebuffHandler buffDebuffHandler => m_buffDebuffHandler;

		void Start()
		{
			m_playerData.Load();
			m_playerData.GetDatas();
		}

		void Update()
		{
#if DEVELOPMENT_BUILD
			m_playerData.GetDatas();
#endif
		}
	}
}