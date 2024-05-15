using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject Object;       //�G�̓�������I�u�W�F�N�g�ݒ�p�̕ϐ�
    public float AttackRate = 5.0f; //�G�̍U���Ԋu�ݒ�p�̕ϐ�
    private bool AttackFlag = false;        //�U�����̃J���[�ύX�p�̃t���O

    // Start is called before the first frame update
    void Start()
    {
        //CreateObj��3.5�b��ɌĂяo���A�ȍ~�� AttackRate �b���Ɏ��s
        InvokeRepeating(nameof(CreateObj), 3.5f, AttackRate);

        this.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_BaseColor", Color.white);

    }

    void Update()
    {
        if (AttackFlag == true)
        {
            StartCoroutine("ATTACKFLAG");
        }
    }

    void CreateObj()�@// �G�̓�������I�u�W�F�N�g�𐶐�����
    {
        //Instantiate( ��������I�u�W�F�N�g,  �ꏊ, ��] );
        //���݂̓G�l�~�[�̓���ɐ�������悤�ɂ��Ă��܂�
        //this.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_BaseColor", Color.red); //�F��ς���
        Instantiate(Object, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 2, this.transform.localPosition.z), Quaternion.identity);
        AttackFlag = true;
    }

    IEnumerator ATTACKFLAG()
    {

        yield return new WaitForSeconds(1.0f);  //�����̒x��
        //this.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_BaseColor", Color.white);   //�F�����ɖ߂�
        AttackFlag = false;
    }

}
