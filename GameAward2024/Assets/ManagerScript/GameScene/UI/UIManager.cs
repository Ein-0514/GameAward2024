using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class UIManager : ManagerBase
	{
		[SerializeField]
		PlayerHpGaugeController m_playerGaugeController;
		/// <summary>
		/// PlayerGaugeController���擾
		/// </summary>
		public PlayerHpGaugeController playerGaugeController => m_playerGaugeController;

		[SerializeField]
		EnemyHpGaugeController m_enemyGaugeController;
		/// <summary>
		/// EnemyHpGaugeController���擾
		/// </summary>
		public EnemyHpGaugeController enemyGaugeController => m_enemyGaugeController;

		[SerializeField]
		TimeGaugeController m_timeGaugeController;
		/// <summary>
		/// TimeGaugeController���擾
		/// </summary>
		public TimeGaugeController timeGaugeController => m_timeGaugeController;
	}
}