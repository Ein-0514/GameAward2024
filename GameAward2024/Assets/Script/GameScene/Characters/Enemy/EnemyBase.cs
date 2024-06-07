using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class EnemyBase : MonoBehaviour
{
	protected EnemyDataList.E_ENEMY_KIND m_enemyKind;
	EnemyDataList m_enemyDataList;
	List<BulletDataList.E_BULLET_KIND> m_shotBulletKinds = new List<BulletDataList.E_BULLET_KIND>();   // ���e�̃f�[�^
	public List<BulletDataList.E_BULLET_KIND> shotBulletKinds => m_shotBulletKinds;

	protected virtual void Start()
	{
		//--- �G�f�[�^�̓ǂݍ��݂Ɛݒ�
		m_enemyDataList = ManagerContainer.instance.characterManager.enemyDataList;
		m_enemyDataList.Load(m_enemyKind);
		var data = m_enemyDataList.GetData(m_enemyKind);
		SetData(data);
	}

	protected virtual void Update()
    {
#if DEVELOPMENT_BUILD
		//--- �G�f�[�^�̍Đݒ�
		m_enemyDataList.Load(m_enemyKind);
		var data = m_enemyDataList.GetData(m_enemyKind);
		SetData(data);
#endif
	}

	protected virtual void SetData(Dictionary<string, CSVParamData> data)
	{
		//--- �l�̋z�o��
		data[nameof(m_shotBulletKinds)].TryGetData(out m_shotBulletKinds);
	}
}