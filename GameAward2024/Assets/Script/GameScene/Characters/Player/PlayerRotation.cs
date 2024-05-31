using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // �Ώە��Ǝ������g�̍��W����x�N�g�����Z�o
        Vector3 vector3 = target.transform.position - this.transform.position;
        //Debug.Log(vector3);
        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion;
    }
}
