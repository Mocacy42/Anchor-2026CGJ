using UnityEngine;

public class AppearItem : MonoBehaviour
{
    void Start()
    {
        //设置默认不可见
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
