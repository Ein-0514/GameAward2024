using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public Material[] Materials1;   //変更前のマテリアル
    public Material[] Materials2;   //変更後のマテリアル
    public GameObject Object;       //敵の投擲するオブジェクト設定用の変数
    public float AttackRate = 5.0f; //敵の攻撃間隔設定用の変数
    private bool AttackFlag = false;        //攻撃時のカラー変更用のフラグ

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //CreateObjを3.5秒後に呼び出し、以降は AttackRate 秒毎に実行

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

    void CreateObj()　// 敵の投擲するオブジェクトを生成する
    {
        meshRenderer.materials = Materials2;

        //Instantiate( 生成するオブジェクト,  場所, 回転 );
        //現在はエネミーの頭上に生成するようにしています
        //Instantiate(Object, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 2, this.transform.localPosition.z), Quaternion.identity);

        Debug.Log("敵からの攻撃!!");
        AttackFlag = true;
    }

    IEnumerator ATTACKFLAG()
    {

        yield return new WaitForSeconds(1.0f);  //処理の遅延
        meshRenderer.materials = Materials1;
        AttackFlag = false;
    }
}
