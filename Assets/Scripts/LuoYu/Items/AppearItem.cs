using UnityEngine;

public abstract class AppearItem : MonoBehaviour
{
    void Start()
    {
        //默认可见
        GetComponent<SpriteRenderer>().enabled = true;
        //默认禁用互动
        if(GetComponent<InteractiveItem>()) GetComponent<InteractiveItem>().enabled = false;
    }
    //抽象方法，出现时效果
    public abstract void EffectAppear();
    //抽象方法，消失时效果
    public abstract void EffectDisappear();
}
