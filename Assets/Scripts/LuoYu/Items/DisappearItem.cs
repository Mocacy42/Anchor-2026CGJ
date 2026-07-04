using UnityEngine;

public abstract class DisappearItem : MonoBehaviour
{
    private void Start()
    {
        //默认可见
        GetComponent<SpriteRenderer>().enabled = true;
        //默认开启互动
        if(GetComponent<InteractiveItem>()) GetComponent<InteractiveItem>().enabled = true;
    }
    //抽象方法，消失时效果
    public abstract void EffectDisappear();
    //抽象方法，出现时效果
    public abstract void EffectAppear();
}
