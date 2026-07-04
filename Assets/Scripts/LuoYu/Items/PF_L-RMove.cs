using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PF_LeftToRightMove :DisappearItem ,IPlatform
{
    //ƽ̨����״̬
    [SerializeField] private bool isOpen = false;
    //ƽ̨�ƶ��ٶ�
    [SerializeField] private float moveSpeed = 1f;

    [Header("ƽ̨���/С�ƶ��߶�")]
    //���߶�
    [SerializeField] private float maxHeight;
    //��С�߶�
    [SerializeField] private float minHeight;

    [SerializeField] private float direction = 1f; //�ƶ����� 1������,-1������

    [SerializeField] private Collider2D _realColl;


    //����״̬ת��
    public void ChangeOpenEffect()
    {
        isOpen = true;
    }
    //�ر�״̬ת��
    public void ChangeCloseEffect()
    {
        isOpen = false;
    }

    private void FixedUpdate()
    {
        //����״̬ʱ�����ƶ�
        if(isOpen)
        {
            transform.Translate(Vector3.up * moveSpeed * direction * Time.deltaTime);
        }
        //�������/С�߶ȱ任����
        if(transform.position.y < minHeight || transform.position.y > maxHeight)
        {
            direction *= -1;
        }
    }

    public override void EffectAppear()
    {
        _realColl.enabled = true;
    }

    public override void EffectDisappear()
    {
       _realColl.enabled = false;
    }
}
