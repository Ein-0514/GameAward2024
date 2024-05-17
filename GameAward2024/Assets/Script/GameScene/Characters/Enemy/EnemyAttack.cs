using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public MeshRenderer meshRenderer;   //�ύX�������I�u�W�F�N�g�̃��b�V�������_���[��ݒ�
    public Material[] Materials1;   //�ύX�O�̃}�e���A��
    public Material[] Materials2;   //�ύX��̃}�e���A��
    public GameObject Object;       //�G�̓�������I�u�W�F�N�g�ݒ�p�̕ϐ�
    public float AttackRate = 5.0f; //�G�̍U���Ԋu�ݒ�p�̕ϐ�
    private bool AttackFlag = false;        //�U�����̃J���[�ύX�p�̃t���O

    // Start is called before the first frame update
    void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        //CreateObj��3.5�b��ɌĂяo���A�ȍ~�� AttackRate �b���Ɏ��s
        InvokeRepeating(nameof(CreateObj), 3.5f, AttackRate);

    }

    void Update()
    {
        //�A�^�b�N�t���O��true�ɂȂ�����}�e���A���̃J���[��ύX���邽�߂̃R���[�`�����N������
        if (AttackFlag == true)
        {
            StartCoroutine("ATTACKFLAG");
        }
    }

    void CreateObj()�@// �G�̓�������I�u�W�F�N�g�𐶐�����
    {
        meshRenderer.materials = Materials2;

        //Instantiate( ��������I�u�W�F�N�g,  �ꏊ, ��] );
        //���݂̓G�l�~�[�̓���ɐ�������悤�ɂ��Ă��܂�
        //Instantiate(Object, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 2, this.transform.localPosition.z), Quaternion.identity);

        Debug.Log("�G����̍U��!!");
        AttackFlag = true;
    }

    IEnumerator ATTACKFLAG()
    {

        yield return new WaitForSeconds(1.0f);  //�����̒x��
        meshRenderer.materials = Materials1;
        AttackFlag = false;
    }

}
