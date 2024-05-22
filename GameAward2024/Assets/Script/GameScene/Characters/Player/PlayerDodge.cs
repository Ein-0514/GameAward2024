using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodge : MonoBehaviour
{
    //�ϐ��錾
    private float rotXZ = 3.0f;  //�v���C���[��]�p�xXZ
    private float rotY = 3.0f;   //�v���C���[��]�p�xY
    private float lateRotXZ;
    private float lateRotY;
    private float radius = -5.0f;
     
    private Vector3 playerPos;  //�v���C���[���W
    private Vector3 bossPos;    //�{�X���W
    private Vector3 distance;   //�v���C���[�ƃ{�X�̋���
    private Vector3 dodgeDistance;  //�v���C���[�̉�������v�Z�p

    //public float moveDodgeSpeed = 3.0f;

    void Start()
    {
        //�}�l�[�W���[�N���X����擾�������W���v�Z�p�̕ϐ��Ɋi�[
        playerPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.position;
        bossPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy.position;

        //�v���C���[����{�X�܂ł̋������v�Z
        distance = playerPos - bossPos;

        lateRotXZ = (rotXZ - lateRotXZ) * 0.1f + lateRotXZ;
        lateRotY  = (rotY - lateRotY)   * 0.1f + lateRotY;
    }

    void Update()
    {       
        
      
    }

    public void DodgeRight()
    {
        //�v���C���[�̍��W���X�V
        playerPos.x = Mathf.Cos(rotY) * Mathf.Sin(rotXZ) * radius + playerPos.x;
        playerPos.y = Mathf.Sin(lateRotY) * radius + playerPos.y;
        playerPos.z = Mathf.Cos(lateRotY) * Mathf.Cos(lateRotXZ) * radius + playerPos.z;

        if(Keyboard.current.lKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            Debug.Log("lllllllllldddddddddddd");
        }
        else if(Keyboard.current.lKey.isPressed)
        {
            Debug.Log("lllllllllllll");
        }
        else
        {
            Debug.Log("ddddddddddddddd");
        }
    }

    public void DodgeLeft()
    {

    }

    public void DodgeFront()
    {

    }

    public void DodgeBack()
    {

    }
}
