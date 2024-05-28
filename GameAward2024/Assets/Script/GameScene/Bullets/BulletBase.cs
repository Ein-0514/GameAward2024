using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
	[SerializeField]
	float m_destroyTime;
	float m_deltaTime = 0.0f;

    protected void Update()
    {
		//--- ���ł���܂ł̎��Ԃ��J�E���g
		if (m_deltaTime < m_destroyTime)
		{
			Destroy(this);  // ���g��j��
			return;
		}
		m_deltaTime += Time.deltaTime;
    }
}