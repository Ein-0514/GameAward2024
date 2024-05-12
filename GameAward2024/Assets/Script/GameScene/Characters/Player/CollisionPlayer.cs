using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CollisionPlayer : MonoBehaviour
{

    [SerializeField] private GameObject damageTexture;
    private Volume volume;
    Vignette vignette;
    bool isVignette = false;
    [SerializeField] float displaytime = 1.0f;
    float countdownTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        volume = GameScene.ManagerContainer.GetManagerContainer().m_studioObjectManager.m_volume;
        volume.profile.TryGet(out vignette);
        countdownTimer = displaytime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isVignette)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer < 0)
            {
                isVignette = false;
                vignette.active = false;
            }
        }

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BoxCollider> (out var EnemyAttackComponent))   // todo:BoxCollider����G�̒e�p�R���|�[�l���g�Ɍ�Œ���
        {

            //�e�N�X�`������ʂɏo��
            Instantiate(damageTexture, transform.position, Quaternion.identity);

            //�������u�_�ł�����|�X�g�G�t�F�N�g
            vignette.active = true;
            isVignette = true;

            
        }

    }
}
