using UnityEngine;

public abstract class AppearItem : MonoBehaviour
{
    void Start()
    {
        //默认不可见
        GetComponent<SpriteRenderer>().enabled = false;
        //默认禁用互动
        GetComponent<AppearItem>().enabled = false;
    }
    //抽象方法，出现时效果
    public abstract void EffectAppear();
    //抽象方法，消失时效果
    public abstract void EffectDisappear();
}
