using UnityEngine;
using UnityEngine.InputSystem;

public class KeyRepeatInteraction : IInputInteraction
{
    // �{�^�����ŏ��ɉ�����Ă��烊�s�[�g�������n�܂�܂ł̎���[s]
    public float m_repeatDelay = 0.01f;

    // �{�^����������Ă���Ԃ̃��s�[�g�����̊Ԋu[s]
    public float m_repeatInterval = 0.01f;

    // �{�^����臒l�i0�̏ꍇ�̓f�t�H���g�ݒ�l���g�p�j
    public float m_pressPoint = 0;

    // �ݒ�l���f�t�H���g�l�̒l���i�[����t�B�[���h
    private float PressPointOrDefault => m_pressPoint > 0 ? m_pressPoint : InputSystem.settings.defaultButtonPressPoint;
    private float ReleasePointOrDefault => PressPointOrDefault * InputSystem.settings.buttonReleaseThreshold;

    // ���̃��s�[�g����[s]
    private double m_nextRepeatTime;

#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
    public static void Initialize()
    {
        // �����Interaction��o�^����K�v������
        InputSystem.RegisterInteraction<KeyRepeatInteraction>();
    }

    public void Process(ref InputInteractionContext context)
    {
        // �ݒ�l�̃`�F�b�N
        if (m_repeatDelay <= 0 || m_repeatInterval <= 0)
        {
            Debug.LogError("initialDelay��repeatInterval��0���傫���l��ݒ肵�Ă��������B");
            return;
        }

        if (context.timerHasExpired)
        {
            // ���s�[�g�����ɒB������Ă�Performed�ɑJ��
            if (context.time >= m_nextRepeatTime)
            {
                // ���s�[�g�����̎�����s������ݒ�
                m_nextRepeatTime = context.time + m_repeatInterval;

                // ���s�[�g���̏���
                context.PerformedAndStayPerformed();

                // ���̃��s�[�g������Process���\�b�h���Ă΂��悤�Ƀ^�C���A�E�g��ݒ�
                context.SetTimeout(m_repeatInterval);
            }

            return;
        }

        switch (context.phase)
        {
            case InputActionPhase.Waiting:
                // �{�^���������ꂽ��Started�ɑJ��
                if (context.ControlIsActuated(PressPointOrDefault))
                {
                    // �{�^���������ꂽ���̏���
                    context.Started();
                    context.PerformedAndStayPerformed();

                    // ���s�[�g�����̏�����s������ݒ�
                    m_nextRepeatTime = context.time + m_repeatDelay;

                    // ���̃��s�[�g������Process���\�b�h���Ă΂��悤�Ƀ^�C���A�E�g��ݒ�
                    context.SetTimeout(m_repeatDelay);
                }

                break;

            case InputActionPhase.Performed:
                // �{�^���������ꂽ��Canceled�ɑJ��
                if (!context.ControlIsActuated(ReleasePointOrDefault))
                {
                    // �{�^���������ꂽ���̏���
                    context.Canceled();
                }

                break;
        }
    }

    public void Reset()
    {
        m_nextRepeatTime = 0;
    }
}