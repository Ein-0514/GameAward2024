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
		/// �G�̃f�[�^���X�g���擾
		/// </summary>
		public BulletDataList bulletDataList => m_bulletDataList;

		/// <summary>
		/// �e���쐬
		/// </summary>
		/// <param name="bulletPrefab">�e�̎�ނ������񋓒萔</param>
		/// <param name="pos">�e�̏������W</param>
		/// <returns>�쐬�����e�ւ̎Q��</returns>
		public BulletBase CreateBullet(BulletDataList.E_BULLET_KIND bulletKind , Vector3 pos)
		{
			BulletBase prefab = m_bulletDataList.GetBulletPrefab(bulletKind);
			BulletBase bullet = Instantiate(prefab, pos ,Quaternion.identity);	// �e���쐬
			bullet.transform.SetParent(this.transform);     // �e��BulletManager�ɐݒ�
			return bullet;
		}

		/// <summary>
		/// �e���쐬
		/// </summary>
		/// <param name="bulletPrefab">�e�̎�ނ������񋓒萔</param>
		/// /// <param name="pos">�e�̏������W</param>
		/// /// <param name="rot">�e�̉�]</param>
		/// <returns>�쐬�����e�ւ̎Q��</returns>
		public BulletBase CreateBullet(BulletDataList.E_BULLET_KIND bulletKind, Vector3 pos, Quaternion rot)
		{
			BulletBase prefab = m_bulletDataList.GetBulletPrefab(bulletKind);
			BulletBase bullet = Instantiate(prefab, pos, rot);  // �e���쐬
			bullet.transform.SetParent(this.transform);			// �e��BulletManager�ɐݒ�
			return bullet;
		}
	}
}