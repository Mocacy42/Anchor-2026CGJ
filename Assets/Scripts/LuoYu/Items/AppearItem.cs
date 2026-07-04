using UnityEngine;

public abstract class AppearItem : MonoBehaviour
{
    void Start()
    {
        //Ĭ�Ͽɼ�
        GetComponent<SpriteRenderer>().enabled = true;
        //Ĭ�Ͻ��û���
        //if(GetComponent<InteractiveItem>()) GetComponent<InteractiveItem>().enabled = false;
    }
    //���󷽷�������ʱЧ��
    public abstract void EffectAppear();
    //���󷽷�����ʧʱЧ��
    public abstract void EffectDisappear();
}
