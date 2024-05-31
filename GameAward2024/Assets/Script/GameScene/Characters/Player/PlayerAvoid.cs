using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvoid : MonoBehaviour
{
    //�ϐ��錾
    private float rotXZ = 3.0f;  //�v���C���[��]�p�xXZ
    private float rotY = 0.0f;   //�v���C���[��]�p�xY
    private float lateRotXZ;
    private float lateRotY;
    private float radius = -5.0f;

    private Vector3 playerPos;  //�v���C���[���W
    private Vector3 bossPos;    //�{�X���W
    private Vector3 distance;   //�v���C���[�ƃ{�X�̋���
    private Vector3 dodgeDistance;  //�v���C���[�̉�������v�Z�p

    private bool AvoidFlag;
    private float Period;

    // Start is called before the first frame update
    void Start()
    {
        ////�}�l�[�W���[�N���X����擾�������W���v�Z�p�̕ϐ��Ɋi�[
        //playerPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.position;
        //bossPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy.position;

        ////�v���C���[����{�X�܂ł̋������v�Z
        //distance = playerPos - bossPos;

        //lateRotXZ = (rotXZ - lateRotXZ) * 0.1f + lateRotXZ;
        //lateRotY = (rotY - lateRotY) * 0.1f + lateRotY;

        AvoidFlag = false;
        Period = 0.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("������");
            if (!AvoidFlag)
            {
                AvoidFlag = true;
                Period = 0.3f;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (!AvoidFlag) return;

        GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.GetComponent<PlayerMove>().PlayerCircularRotation(Period, Vector3.up);

        Period *= 1.3f;

        if(Period >= 8.0f)
        {
            Period = 0.0f;
            AvoidFlag = false;
        }

    }

    public void OnAvoid()
    {

    }
}
