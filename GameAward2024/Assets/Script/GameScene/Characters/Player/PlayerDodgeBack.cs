using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeBack : MonoBehaviour
{
    //�ϐ�
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
        //����ɉ������ꍇ�̂݁A�v���C���[�ƃ{�X�̋�����}��
        //����𐳋K�����A�}�C�i�X���|����A����Ƀ��[�u�X�s�[�h���|���邱�ƂŁA����ւ̈ړ����ł���
    }
}
