using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LanternController : MonoBehaviour
{
    //单例
    public static LanternController instance;

    //角色控制引用
    [SerializeField] private PlayerController playerController;

    //检测物体是否被持有
    [SerializeField] private bool isUsing = false;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        if(isUsing)
        {
            //如果被持有，跟随玩家移动
            transform.position = playerController.transform.position;
        }
    }

    //被拿起
    public void PickUp(bool isUsing)
    {
        isUsing = true;
    }
    //被放下
    public void PutDown()
    {
        isUsing = true;
        //提灯旋转90度
        transform.Rotate(0, 0, 90);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //碰到消失物体
    //    if(collision.gameObject.tag == "disappearItem")
    //    {
    //        collision.GetComponent<SpriteRenderer>().enabled = false;
    //    }
    //    //碰到出现物体
    //    if(collision.gameObject.tag == "appearItem")
    //    {
    //        collision.GetComponent<SpriteRenderer>().enabled = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    //离开消失物体
    //    if(collision.gameObject.tag == "disappearItem")
    //    {
    //        collision.GetComponent<SpriteRenderer>().enabled = true;
    //    }
    //    //离开出现物体
    //    if(collision.gameObject.tag == "appearItem")
    //    {
    //        collision.GetComponent<SpriteRenderer>().enabled = false;
    //    }
    //}
}
