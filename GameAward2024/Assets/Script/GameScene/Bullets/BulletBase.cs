using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class BulletBase : MonoBehaviour
{
	[SerializeField]
	float m_destroyTime;
    protected BuffDebuffData m_buffDebuffData = new BuffDebuffData();

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
		//--- ���ł���܂ł̎��Ԃ��J�E���g
		if (m_destroyTime < 0.0f)
		{
			Destroy(gameObject);  // ���g��j��
			return;
		}
        m_destroyTime -= Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        // �v���C���[�Ƀo�t��t�^����
        ManagerContainer.instance.characterManager.
            buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, gameObject.name);

        // �v���C���[�ɓ���������e���폜����
        Destroy(gameObject);
    }
}