using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�ڕW�i���W���g�p�j
    [SerializeField] private Transform Enemy;      //TODO:CharacterManager����Q�Əo����悤�ɕύX

    // ��]��
    [SerializeField] private Vector3 _axis = Vector3.up;

    // �~�^������
    [SerializeField] private float _period = 2;

    // �������X�V���邩�ǂ���
    [SerializeField] private bool _updateRotation = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var tr = transform;
        float period;
        
        //TODO:InputSystem�ɒu������
        if (Input.GetKey(KeyCode.A))
        {
            period = _period;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            period = -_period;
        } 
        else return;

        var angleAxis = Quaternion.AngleAxis(360 / period * Time.deltaTime, _axis);
        Vector3 _center = Enemy.position;

        var pos = tr.position;
        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        tr.position = pos;
    }
}
