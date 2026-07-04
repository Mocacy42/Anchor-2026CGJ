using UnityEngine;

public class Portal : MonoBehaviour
{
    //配对传送门引用
    [SerializeField] private Portal otherPortal;

    //玩家引用
    [SerializeField] private PlayerController playerController;

    //互动效果
    public void InteractiveEffect()
    {
        //将玩家传送到另一个传送门
        playerController.transform.position = otherPortal.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerController = collision.GetComponent<PlayerController>();
        }
    }
}
