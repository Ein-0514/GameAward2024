using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject bulletPrefab; // �e�̃v���n�u
    public Transform player;        // �v���[���[��Transform
    public float bulletSpeed;       // �e�̑��x
    public float shootInterval;     // �e�𔭎˂���Ԋu
    
    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[�X�V
        shootTimer++;

        // �^�C�}�[�����ˊԊu���z������
        if (shootTimer >= shootInterval)
        {
            // �^�C�}�[���Z�b�g
            shootTimer = 0.0f;
            // �e����
            Shoot();
        }
    }

    void Shoot()
    {
        // �e�𐶐�
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // �v���[���[�ւ̕������v�Z
        Vector3 direction = (player.position - transform.position).normalized;
        
        // �e�ɗ͂�������
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}
