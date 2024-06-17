using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//--- �Q�l�T�C�g
// https://kurokumasoft.com/2022/09/03/unity-dont-destroy-on-load/
// https://zenn.dev/daichi_gamedev/articles/unity-fadeout

public class FadeController : MonoBehaviour
{
	public enum E_FADE_STATE
	{
		NONE,
		FADE_OUT,
		FADE_IN,
		END
	}

	readonly Color MIN_ALPHA_COLOR = new Color(0.0f, 0.0f, 0.0f, 0.0f);
	readonly Color MAX_ALPHA_COLOR = new Color(0.0f, 0.0f, 0.0f, 1.0f);
	const float FADE_TIME = 1.0f;

	public static FadeController instance = null;

	[SerializeField]
	Image m_fadePlane;
	public E_FADE_STATE m_fadeState { get; private set; } = E_FADE_STATE.NONE;

	void Awake()
	{
		CheckInstance();    // �V���O���g���̏���
	}

	void Start()
	{
		// �V�[�����ׂ��ő���
		DontDestroyOnLoad(gameObject);
	}

	void CheckInstance()
	{
		if (FadeController.instance == null)
		{
			// ���쐬�̏ꍇ�A�Q�ƃf�[�^���i�[
			FadeController.instance = this;
			return;
		}

		// �쐬�ς݂̏ꍇ�A�I�u�W�F�N�g���폜
		Destroy(gameObject);
	}

	public void FadeSceneLoad(string sceneName)
	{
		// �t�F�[�h���͏������Ȃ�
		if (m_fadeState != E_FADE_STATE.NONE) return;
		StartCoroutine(Fade(sceneName));	// �t�F�[�h���������s
	}

	IEnumerator Fade(string sceneName)
	{
		m_fadeState = E_FADE_STATE.FADE_OUT;	// ��Ԃ��t�F�[�h�A�E�g���ɕύX
		m_fadePlane.enabled = true;  // �p�l����L����
		float deltaTime = 0.0f;
		
		//--- �t�F�[�h�A�E�g����
		while(deltaTime <= FADE_TIME)
		{
			deltaTime += Time.deltaTime;    // �o�ߎ��Ԃ����Z
			float t = Mathf.Clamp01(deltaTime / FADE_TIME);  // �t�F�[�h�̐i�s�x���v�Z
			m_fadePlane.color = 
				Color.Lerp(MIN_ALPHA_COLOR, MAX_ALPHA_COLOR, t); // �p�l���̐F��ύX���ăt�F�[�h�A�E�g
			AudioListener.volume = 1.0f - t;
			yield return null;
		}

		m_fadePlane.color = MAX_ALPHA_COLOR;	// �t�F�[�h�A�E�g������������ŏI�F�ɐݒ�
		SceneManager.LoadScene(sceneName);  // ���̃V�[�������[�h
		yield return null;

		//--- �t�F�[�h�C������
		m_fadeState = E_FADE_STATE.FADE_IN;  // ��Ԃ��t�F�[�h�C�����ɕύX
		deltaTime = 0.0f;
		while (deltaTime <= FADE_TIME)
		{
			deltaTime += Time.deltaTime;    // �o�ߎ��Ԃ����Z
			float t = 1.0f - Mathf.Clamp01(deltaTime / FADE_TIME);  // �t�F�[�h�̐i�s�x���v�Z
			m_fadePlane.color =
				Color.Lerp(MIN_ALPHA_COLOR, MAX_ALPHA_COLOR, t); // �p�l���̐F��ύX���ăt�F�[�h�A�E�g
			AudioListener.volume = 1.0f - t;
			yield return null;
		}

		m_fadePlane.enabled = false;	// �p�l���𖳌���
		m_fadeState = E_FADE_STATE.END;  // ��Ԃ��t�F�[�h�I���ɕύX
		yield return null;

		m_fadeState = E_FADE_STATE.NONE; // ��Ԃ��t�F�[�h���ɕύX
	}
}