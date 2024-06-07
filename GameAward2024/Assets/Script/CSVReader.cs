using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ���f�[�^�̕���
 * �p�����[�^�̐���	,�����o�ϐ���	,���l...
 * 
 * ���g�p��
 * ���C�t			,m_life			,3
 * �ړ����x			,m_moveSpeed	,0.3
 * �f�o�b�O���[�h	,m_isDebug		,true
 * ���O				,m_name			,Player
 * ���W				,m_pos			,0.0 5.0 10.0
 * 
 * ��Vector3�̏ꍇ�͔��p�X�y�[�X�Œl����؂�
 * ���Ή����Ă���^��int, float, bool, string, Vector3
 * ����s��1�s�ڂ̃Z���̕�����̐擪�Ɂu//�v���t�^����Ă���ꍇ�͖��������
 */

public class CSVReader
{
	const char CSV_SPLIT = ',';

	public Dictionary<string, CSVParamData> m_csvDatas { get; private set; }
	string m_filePath;

	/// <summary>
	/// CSV�t�@�C����ǂݍ���
	/// </summary>
	/// <param name="filePath">�t�@�C���p�X</param>
	public void LoadCSV(string filePath)
	{
		m_filePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "/" + filePath;	// �t�@�C���p�X���L��

		StringReader reader = new StringReader(ReadAllText(m_filePath));
		ParseCSVData(reader);   // CSV�f�[�^��CSVParamData�֕ϊ�

#if DEVELOPMENT_BUILD
		FileSystemWatcher watcher = new FileSystemWatcher();

		//--- �Ď�����t�H���_�[�E�t�@�C����ݒ�
		watcher.Path = Path.GetDirectoryName(m_filePath);
		watcher.Filter = Path.GetFileName(m_filePath);

		//---- �t�@�C�����ύX���ꂽ�Ƃ��̃C�x���g��ݒ�
		watcher.Changed += (sender, e) =>
		{
			// �t�@�C�����ύX���ꂽ��A�����[�h���������s
			ReloadCSV(m_filePath);
		};

		// �Ď����J�n����
		watcher.EnableRaisingEvents = true;
#endif
	}

	/// <summary>
	/// CSV�t�@�C����ǂݍ���
	/// </summary>
	/// <param name="textAsset">�e�L�X�g�f�[�^</param>
	public void LoadCSV(TextAsset textAsset)
	{
		StringReader reader = new StringReader(textAsset.text);
		ParseCSVData(reader);   // CSV�f�[�^��CSVParamData�֕ϊ�
	}

	void ReloadCSV(string filePath)
	{
		StringReader reader = new StringReader(ReadAllText(m_filePath));
		ParseCSVData(reader);   // CSV�f�[�^��CSVParamData�֕ϊ�
	}

	void ParseCSVData(StringReader reader)
	{
		//--- �f�[�^���N���A
		m_csvDatas = new Dictionary<string, CSVParamData>();
		m_csvDatas.Clear();

		//--- �Ō�܂őS�ēǂ�
		while (reader.Peek() != -1)
		{
			// 1�s���擾
			string line = reader.ReadLine();

			//--- �R�����g����(// �R�����g���e)�����O
			if (line.TrimStart().StartsWith("//"))	continue;
		
			// �u,�v�ŕ������ă��X�g��
			List<string> columns = new List<string>(line.Split(CSV_SPLIT));

			//--- ��s�����O
			bool isNull = false;
			foreach (string str in columns)
				isNull |= string.IsNullOrWhiteSpace(str);
			if (isNull) continue;

			//--- �L�[(�ϐ���)�A�ϐ��^�A�l�𕶎���Ŏ擾
			columns.RemoveAt(0);		// �擪(�p�����[�^�̐�������)���폜
			string key = columns[0];
			columns.RemoveAt(0);		// �L�[�̕������폜
			string value = columns[0];

			// �f�[�^��ǉ�
			m_csvDatas.Add(key, new CSVParamData(value));
		}
	}

	static string ReadAllText(string filePath)
	{
		//--- �t�@�C����ǂݎ���p�����L�A�N�Z�X�����w�肵�ĊJ��
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			using (StreamReader sr = new StreamReader(fs))
			{
				return sr.ReadToEnd();
			}
		}
	}
}

public class CSVParamData
{
	const char PARAM_LIST_SPLIT = ' ';

	string m_data;

	public CSVParamData(string output)
	{
		m_data = output;
	}

	public bool TryGetData(out int output)
	{
		return int.TryParse(m_data, out output);
	}

	public bool TryGetData(out float output)
	{
		return float.TryParse(m_data, out output);
	}

	public bool TryGetData(out bool output)
	{
		return bool.TryParse(m_data, out output);
	}

	public bool TryGetData(out string output)
	{
		output = m_data;
		return true;
	}

	public bool TryGetData(out Vector2 output)
	{
		output = new Vector2();

		string[] str = m_data.Split(PARAM_LIST_SPLIT);
		if (str.Length < 2) return false;

		//--- xyz�ɒl��K�p���Ă���
		bool isFail = !float.TryParse(str[0], out output.x);
		if (isFail) return false;
		isFail = !float.TryParse(str[1], out output.y);
		if (isFail) return false;

		return true;
	}

	public bool TryGetData(out Vector3 output)
	{
		output = new Vector3();

		string[] str = m_data.Split(PARAM_LIST_SPLIT);
		if (str.Length < 3) return false;

		//--- xyz�ɒl��K�p���Ă���
		bool isFail = !float.TryParse(str[0], out output.x);
		if (isFail) return false;
		isFail = !float.TryParse(str[1], out output.y);
		if (isFail) return false;
		isFail = !float.TryParse(str[2], out output.z);
		if (isFail) return false;

		return true;
	}

	public bool TryGetData(out List<int> output)
	{
		output = new List<int>();

		string[] str = m_data.Split(PARAM_LIST_SPLIT);
		if (str.Length < 1) return false;

		//--- ���X�g�ɒl��ǉ����Ă���
		foreach (string data in str)
		{
			int value = 0;
			bool isFail = !int.TryParse(data, out value);
			if (isFail) return false;
			output.Add(value);
		}

		return true;
	}

	public bool TryGetData(out List<float> output)
	{
		output = new List<float>();

		string[] str = m_data.Split(PARAM_LIST_SPLIT);
		if (str.Length < 1) return false;

		//--- ���X�g�ɒl��ǉ����Ă���
		foreach (string data in str)
		{
			float value = 0.0f;
			bool isFail = !float.TryParse(data, out value);
			if (isFail) return false;
			output.Add(value);
		}

		return true;
	}

	public bool TryGetData(out List<BulletDataList.E_BULLET_KIND> output)
	{
		output = new List<BulletDataList.E_BULLET_KIND>();

		string[] str = m_data.Split(PARAM_LIST_SPLIT);
		if (str.Length < 1) return false;

		//--- ���X�g�ɒl��ǉ����Ă���
		foreach (string data in str)
		{
			int value = 0;
			bool isFail = !int.TryParse(data, out value);
			if (isFail) return false;
			output.Add((BulletDataList.E_BULLET_KIND)value);
		}

		return true;
	}
}