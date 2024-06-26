using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class BulletManager : ManagerBase
	{
		[SerializeField]
		BulletDataList m_bulletDataList;
		/// <summary>
		/// 敵のデータリストを取得
		/// </summary>
		public BulletDataList bulletDataList => m_bulletDataList;

		/// <summary>
		/// 弾を作成
		/// </summary>
		/// <param name="bulletPrefab">弾の種類を示す列挙定数</param>
		/// <returns>作成した弾への参照</returns>
		public BulletBase CreateBullet(BulletDataList.E_BULLET_KIND bulletKind , Vector3 pos)
		{
			BulletBase prefab = m_bulletDataList.GetBulletPrefab(bulletKind);
			BulletBase bullet = Instantiate(prefab, pos ,Quaternion.identity);	// 弾を作成
			bullet.transform.SetParent(this.transform);     // 親をBulletManagerに設定
			return bullet;
		}
	}
}