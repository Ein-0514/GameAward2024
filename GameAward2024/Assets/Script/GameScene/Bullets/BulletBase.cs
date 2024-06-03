using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class BulletBase : MonoBehaviour
{
	[SerializeField]
	float m_destroyTime;
	float m_deltaTime = 0.0f;
    protected BuffDebuffData m_buffDebuffData;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
		//--- ���ł���܂ł̎��Ԃ��J�E���g
		if (m_deltaTime < m_destroyTime)
		{
			Destroy(gameObject);  // ���g��j��
			return;
		}
		m_deltaTime += Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƀo�t��t�^����
        ManagerContainer.GetManagerContainer().m_characterManager.
            m_buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, gameObject.name);

        // �v���C���[�ɓ���������e���폜����
        Destroy(gameObject);
    }
}