using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class BulletManager : MonoBehaviour
	{
		/// <summary>
		/// �e���쐬
		/// </summary>
		/// <typeparam name="T">BulletBase���p�������N���X�Ɍ���</typeparam>
		/// <param name="bulletPrefab">�e�̃v���n�u</param>
		/// <returns>�쐬�����e�ւ̎Q��</returns>
		public T CreateBullet<T>(BulletBase bulletPrefab) where T : BulletBase
		{
			BulletBase bullet = Instantiate(bulletPrefab);	// �e���쐬
			bullet.transform.SetParent(this.transform);		// �e��BulletManager�ɐݒ�
			return bullet as T;	// �ړI�̃N���X�փL���X�g
		}
	}
}