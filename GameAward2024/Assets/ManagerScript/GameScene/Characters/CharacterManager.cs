using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class CharacterManager : ManagerBase
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
		EnemyDataList m_enemyDataList;
		/// <summary>
		/// �G�̃f�[�^���X�g���擾
		/// </summary>
		public EnemyDataList enemyDataList => m_enemyDataList;

		EnemyBase m_enemyData;
		/// <summary>
		/// �G�̏����擾
		/// </summary>
		public EnemyBase enemyData => m_enemyData;

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
			//--- �G���쐬
			EnemyDataList.E_ENEMY_KIND enemyKind = ManagerContainer.instance.gameManager.selectStageData.m_enemyKind;
			m_enemyData = CreateEnemy(enemyKind, new Vector3(0.0f, 0.0f, 0.0f));
			m_enemyTrans = m_enemyData.transform;

			m_playerData.Load();
			m_playerData.GetDatas();
		}

		void Update()
		{
#if DEVELOPMENT_BUILD
			m_playerData.GetDatas();
#endif
		}

		/// <summary>
		/// �G���쐬
		/// </summary>
		/// <param name="enemyPrefab">�G�̎�ނ������񋓒萔</param>
		/// <returns>�쐬�����G�ւ̎Q��</returns>
		EnemyBase CreateEnemy(EnemyDataList.E_ENEMY_KIND enemyKind, Vector3 pos)
		{
			EnemyBase prefab = m_enemyDataList.GetEnemyPrefab(enemyKind);
			EnemyBase enemy = Instantiate(prefab, pos, Quaternion.identity);    // �G���쐬
			enemy.transform.SetParent(this.transform);     // �e��CharacterManager�ɐݒ�
			return enemy;
		}
	}
}