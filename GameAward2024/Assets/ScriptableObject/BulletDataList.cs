using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletDataList", menuName = "ScriptableObjects/BulletDataList")]
public class BulletDataList : ScriptableObject
{
	public enum E_BULLET_KIND
	{
		// TODO:�e�̔z��ɃA�N�Z�X����ׂ̗񋓒萔��ǉ�
		NONE = -1,
		NORMAL,
		ACCELERATE,
		SIN_WAVE,
		TRACKING,
		RIPPLE,
		SPIRAL,
		DEBRI,
		MAX
	}

	readonly string[] CSV_FILE_PATH =
	{
		// TODO:csv��ǂݍ��ވׂ̃t�@�C���p�X��ǉ�
		"SettingCSV/BulletData/NormalBullet.csv",
		"SettingCSV/BulletData/AccelerateBullet.csv",
		"SettingCSV/BulletData/SinWaveBullet.csv",
		"SettingCSV/BulletData/TrackingBullet.csv",
		"SettingCSV/BulletData/RippleBullet.csv",
		"SettingCSV/BulletData/SpiralBullet.csv",
		"SettingCSV/BulletData/DebriBullet.csv",
	};

	[SerializeField]
	BulletBase[] m_bulletPrefabList = new BulletBase[(int)E_BULLET_KIND.MAX];
	[SerializeField]
	TextAsset[] m_csvTexts = new TextAsset[(int)E_BULLET_KIND.MAX];
	CSVReader[] m_csvReaders = Enumerable.Repeat(new CSVReader(), (int)E_BULLET_KIND.MAX).ToArray();

	/// <summary>
	/// �w��̒e�̃v���n�u���擾
	/// </summary>
	/// <param name="bulletKind">�e�̎�ނ������񋓒萔</param>
	/// <returns>�e�̃f�[�^</returns>
	public BulletBase GetBulletPrefab(E_BULLET_KIND bulletKind)
	{
		return m_bulletPrefabList[(int)bulletKind];
	}

	/// <summary>
	/// �w��̒e�̃f�[�^��ǂݍ���(BulletBase�ŌĂяo��)
	/// </summary>
	/// <param name="bulletKind">�e�̎�ނ������񋓒萔</param>
	public void Load(E_BULLET_KIND bulletKind)
	{
		//--- CSV�t�@�C����ǂݍ���
#if DEVELOPMENT_BUILD
		m_csvReaders[(int)bulletKind].LoadCSV(CSV_FILE_PATH[(int)bulletKind]);
#else
		m_csvReaders[(int)bulletKind].LoadCSV(m_csvTexts[(int)bulletKind]);
#endif
	}

	/// <summary>
	/// �w��̒e�̃f�[�^���擾(BulletBase�ŌĂяo��)
	/// </summary>
	/// <param name="bulletKind">�e�̎�ނ������񋓒萔</param>
	public Dictionary<string, CSVParamData> GetData(E_BULLET_KIND bulletKind)
	{
		return m_csvReaders[(int)bulletKind].m_csvDatas;
	}
}