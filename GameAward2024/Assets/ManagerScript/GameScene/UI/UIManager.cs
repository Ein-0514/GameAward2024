using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		PlayerGaugeController m_playerGaugeController;
		/// <summary>
		/// PlayerGaugeController���擾
		/// </summary>
		public PlayerGaugeController playerGaugeController => m_playerGaugeController;

		[SerializeField]
		TimeGaugeController m_timeGaugeController;
		/// <summary>
		/// TimeGaugeController���擾
		/// </summary>
		public TimeGaugeController timeGaugeController => m_timeGaugeController;
	}
}