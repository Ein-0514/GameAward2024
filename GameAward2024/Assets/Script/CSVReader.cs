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

			//--- ��s��R�����g����(// �R�����g���e)�����O
			if (string.IsNullOrWhiteSpace(line))	continue;
			if (line.TrimStart().StartsWith("//"))	continue;

			// �u,�v�ŕ������ă��X�g��
			List<string> str = new List<string>(line.Split(CSV_SPLIT));

			//--- �L�[(�ϐ���)�A�ϐ��^�A�l�𕶎���Ŏ擾
			str.RemoveAt(0);		// �擪(�p�����[�^�̐�������)���폜
			string key = str[0];
			str.RemoveAt(0);		// �L�[�̕������폜
			string value = str[0];

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
	const char VECTOR_SPLIT = ' ';

	string m_value;

	public CSVParamData(string value)
	{
		m_value = value;
	}

	public bool TryGetData(out int value)
	{
		return int.TryParse(m_value, out value);
	}

	public bool TryGetData(out float value)
	{
		return float.TryParse(m_value, out value);
	}

	public bool TryGetData(out bool value)
	{
		return bool.TryParse(m_value, out value);
	}

	public bool TryGetData(out string value)
	{
		value = m_value;
		return true;
	}

	public bool TryGetData(out Vector3 value)
	{
		value = new Vector3();

		string[] str = m_value.Split(VECTOR_SPLIT);
		if (str.Length < 3) return false;

		//--- xyz�ɒl��K�p���Ă���
		bool isFail = !float.TryParse(str[0], out value.x);
		if (isFail) return false;
		isFail = !float.TryParse(str[1], out value.y);
		if (isFail) return false;
		isFail = !float.TryParse(str[2], out value.z);
		if (isFail) return false;

		return true;
	}
}