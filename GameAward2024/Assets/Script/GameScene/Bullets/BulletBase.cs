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
    [SerializeField]
    protected LayerMask m_playerLayerMask;

    protected void Start()
    {
        // �v���C���[�̃Q�[���I�u�W�F�N�g���擾
        //m_playerObj = ManagerContainer.GetManagerContainer().m_characterManager.m_playerData;
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
        if ((m_playerLayerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            // �v���C���[�Ƀo�t��t�^����
            ManagerContainer.GetManagerContainer().m_characterManager.
                m_buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, gameObject.name);

            // �v���C���[�ɓ���������e���폜����
            Destroy(this);
        }
    }
}