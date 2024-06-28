using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterArea : MonoBehaviour
{
	List<BulletBase> m_bullets = new List<BulletBase>();

	void Start()
	{
		StartCoroutine(ListNullCheck());
	}

	private void OnTriggerEnter(Collider other)
	{
		BulletBase bullet;
		if (!other.TryGetComponent(out bullet)) return;

		// �Փ˂���Bullet�����X�g�ɒǉ�
		m_bullets.Add(bullet);
	}

	private void OnTriggerExit(Collider other)
	{
		BulletBase bullet;
		if (!other.TryGetComponent(out bullet)) return;

		// �̈悩��O�ꂽ�e�����X�g����폜
		m_bullets.Remove(bullet);
	}

	public void CounterBullet()
	{
		//--- �J�E���^�[�\��ɂ���e�̃^�[�Q�b�g��G�ɕύX
		foreach (BulletBase bullet in m_bullets)
		{
			if (bullet == null) continue;
			bullet.ChangeTarget(BulletBase.E_TARGET_KIND.ENEMY);
		}
	}

	IEnumerator ListNullCheck()
	{
		while (true)
		{
			//--- null�̘g���폜
			// Destroy������OnTriggerExit()���������Ȃ���
			for (int i = 0; i < m_bullets.Count; ++i)
			{
				BulletBase bullet = m_bullets[i];
				if (bullet != null) continue;

				m_bullets.Remove(bullet);
				--i;
			}

			yield return null;
		}
	}
}