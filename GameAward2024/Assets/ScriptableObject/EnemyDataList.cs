using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataList", menuName = "ScriptableObjects/EnemyDataList")]
public class EnemyDataList : ScriptableObject
{
	public enum E_ENEMY_KIND
	{
		// TODO:�G�̔z��ɃA�N�Z�X����ׂ̗񋓒萔��ǉ�
		NONE = -1,
		MECHA_OCTOPUS,
		MAX
	}

	readonly string[] CSV_FILE_PATH =
	{
		"SettingCSV/BulletData/TrackingBullet.csv",
	};

	[SerializeField]
	EnemyBase[] m_enemyPrefabList = new EnemyBase[(int)E_ENEMY_KIND.MAX];
	[SerializeField]
	TextAsset[] m_csvTexts = new TextAsset[(int)E_ENEMY_KIND.MAX];
	CSVReader[] m_csvReaders = Enumerable.Repeat<CSVReader>(new CSVReader(), (int)E_ENEMY_KIND.MAX).ToArray();

	/// <summary>
	/// �w��̓G�̃v���n�u���擾
	/// </summary>
	/// <param name="enemyKind">�G�̎�ނ������񋓒萔</param>
	/// <returns>�G�̃f�[�^</returns>
	public EnemyBase GetEnemyPrefab(E_ENEMY_KIND enemyKind)
	{
		return m_enemyPrefabList[(int)enemyKind];
	}

	/// <summary>
	/// �w��̓G�̃f�[�^��ǂݍ���(EnemyBase�ŌĂяo��)
	/// </summary>
	/// <param name="enemyKind">�G�̎�ނ������񋓒萔</param>
	public void Load(E_ENEMY_KIND enemyKind)
	{
		//--- CSV�t�@�C����ǂݍ���
#if DEVELOPMENT_BUILD
		m_csvReaders[(int)enemyKind].LoadCSV(CSV_FILE_PATH[(int)enemyKind]);
#else
		m_csvReaders[(int)enemyKind].LoadCSV(m_csvTexts[(int)enemyKind]);
#endif
	}

	/// <summary>
	/// �w��̓G�̃f�[�^���擾(EnemyBase�ŌĂяo��)
	/// </summary>
	/// <param name="enemyKind">�G�̎�ނ������񋓒萔</param>
	public Dictionary<string, CSVParamData> GetData(E_ENEMY_KIND enemyKind)
	{
		return m_csvReaders[(int)enemyKind].m_csvDatas;
	}
}