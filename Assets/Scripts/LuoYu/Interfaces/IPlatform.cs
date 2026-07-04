using UnityEngine;

public interface IPlatform
{
    public void SetIsTrigger(bool _isTrigger);
    //改变为开启状态
    public void ChangeOpenEffect();
    //改变为关闭状态
    public void ChangeCloseEffect();
}
