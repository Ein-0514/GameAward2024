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
    private Vector2 Period;

    // Start is called before the first frame update
    void Start()
    {
        AvoidFlag = false;
        Period = new Vector2();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!AvoidFlag) return;

        PlayerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.E_AVOID);

        if (Period.x != 0.0f) GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.GetComponent<PlayerMove>().PlayerCircularRotation(Period.x, this.transform.up);
        if (Period.y != 0.0f) GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.GetComponent<PlayerMove>().PlayerCircularRotation(Period.y, this.transform.right);

        Period *= 1.3f;

        if (Period.x >= 8.0f || Period.x <= -8.0f)
        {
            Period.x = 0.0f;
        }
        if (Period.y >= 8.0f || Period.y <= -8.0f)
        {
            Period.y = 0.0f;
        }

        if (Period.x == 0.0f && Period.y == 0.0f)
        {
            AvoidFlag = false;
        }

    }

    public void OnAvoid()
    {
        Vector2 dir = PlayerActionControler.PParam.m_moveDirect;

        if (!AvoidFlag)
        {
            AvoidFlag = true;
            if (dir.x < 0.0f)
            {
                Period.x = -0.3f;
            }
            else if (dir.x > 0.0f)
            {
                Period.x = 0.3f;
            }
            if (dir.y < 0.0f)
            {
                Period.y = -0.3f;
            }
            else if (dir.y > 0.0f)
            {
                Period.y = 0.3f;
            }
        }
    }
}
