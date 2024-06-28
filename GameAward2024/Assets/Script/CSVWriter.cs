using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVWriter
{
	public static void WriteCSV(string line)
	{
		FadeController fadeController = FadeController.instance;
		if (fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE) return;

		string filePath;

#if UNITY_EDITOR
		filePath = Application.dataPath + "/Output/Log.csv";
#else
		filePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "/Output/Log.csv";
#endif
		//--- ���t�Ǝ��Ԃ�擪�ɒǉ�
		DateTime dt = DateTime.Now;
		line = dt.ToString("yyyy,MM,dd,HH,mm,ss") + "," + line;

		//--- csv�t�@�C���֒ǋL
		using (StreamWriter sw = new StreamWriter(filePath, true))
		{
			sw.WriteLine(line);
		}
	}
}