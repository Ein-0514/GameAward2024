using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodgeLeft : MonoBehaviour
{
    //�I�u�W�F�N�g�擾
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

    //�ϐ��錾
    private float rotY = -5.0f;  //�v���C���[��]�p�x

    private Vector3 playerPos;  //�v���C���[���W
    private Vector3 bossPos;    //�{�X���W
    private Vector3 distance;   //�v���C���[�ƃ{�X�̋���
    void Start()
    {
        //�v�Z�p�̕ϐ��Ɋi�[
        playerPos = player.transform.position;
        bossPos = boss.transform.position;

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
