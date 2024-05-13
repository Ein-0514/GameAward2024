using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodgeFront : MonoBehaviour
{
    private Vector3 playerPos;  //�v���C���[���W
    private Vector3 bossPos;    //�{�X���W
    private Vector3 distance;   //�v���C���[�ƃ{�X�̋���

    void Start()
    {
        //�}�l�[�W���[�N���X����擾�������W���v�Z�p�̕ϐ��Ɋi�[
        playerPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.position;
        bossPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy.position;

        //�v���C���[����{�X�܂ł̋������v�Z
        distance = playerPos - bossPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
