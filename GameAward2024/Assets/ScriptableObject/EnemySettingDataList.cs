using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettingDataList", menuName = "ScriptableObjects/EnemySettingDataList")]
public class EnemySettingDataList : ScriptableObject
{
	public enum E_ENEMY_KIND
	{
		// TODO:�G�̔z��ɃA�N�Z�X����ׂ̗񋓒萔��ǉ�
	}

	[SerializeField]
	List<EnemySettingDataBase> m_enemySettingDataList = new List<EnemySettingDataBase>();

	/// <summary>
	/// �w��̓G�̃f�[�^���擾
	/// </summary>
	/// <typeparam name="T">EnemySettingDataBase���p�������h���N���X</typeparam>
	/// <param name="enemyKind">�G�̎�ނ������񋓒萔</param>
	/// <returns>�G�̃f�[�^</returns>
	public T GetEnemySettingData<T>(E_ENEMY_KIND enemyKind) where T : EnemySettingDataBase
	{
		EnemySettingDataBase data = this.m_enemySettingDataList[(int)enemyKind];
		return data as T;	// �w��̓G�̃f�[�^�փL���X�g
	}
}