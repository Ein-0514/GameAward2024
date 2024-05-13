using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodgeRight : MonoBehaviour
{
    //�ϐ��錾
    private float rotY = 5.0f;  //�v���C���[��]�p�x

    private Vector3 playerPos;  //�v���C���[���W
    private Vector3 bossPos;    //�{�X���W
    private Vector3 distance;   //�v���C���[�ƃ{�X�̋���

    void Start()
    {
        //�}�l�[�W���[�N���X������W���擾���v�Z�p�̕ϐ��Ɋi�[
        playerPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.position;
        bossPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy.position;

        //�v���C���[����{�X�܂ł̋������v�Z
        distance = playerPos - bossPos;
    }

    void Update()
    {
        //�v���C���[�̉����̍��W���v�Z
        playerPos.x += distance.x * Mathf.Cos(rotY) - distance.z * Mathf.Sin(rotY);
        playerPos.x += distance.x * Mathf.Sin(rotY) - distance.z * Mathf.Cos(rotY);    
    }    
}
