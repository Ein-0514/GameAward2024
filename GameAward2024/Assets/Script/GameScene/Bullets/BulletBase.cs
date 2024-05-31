using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class BulletBase : MonoBehaviour
{
	[SerializeField]
	float m_destroyTime;
	float m_deltaTime = 0.0f;
    GameObject m_playerObj;
    [SerializeField]
    protected BuffDebuffData m_buffDebuffData;

    protected void start()
    {
        // �v���C���[�̃Q�[���I�u�W�F�N�g���擾
        m_playerObj = GameObject.Find("Player");
    }

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

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_playerObj)
        {
            // �v���C���[�Ƀo�t��t�^����
            ManagerContainer.GetManagerContainer().m_characterManager.
                m_buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, gameObject.name);

            // �v���C���[�ɓ���������e���폜����
            Destroy(gameObject);
        }
    }
}