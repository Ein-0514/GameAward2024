using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodgeFront : MonoBehaviour
{
    //�I�u�W�F�N�g�擾
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
