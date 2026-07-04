using UnityEngine;

public class Portal : MonoBehaviour
{
    //��Դ���������
    [SerializeField] private Portal otherPortal;

    //�������
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AnchorAbility _anchorAB;

    // 传送
    public void InteractiveEffect()
    {
        //����Ҵ��͵���һ��������
        playerController.gameObject.transform.position = otherPortal.gameObject.transform.position;
    }

    // 修改为 stay
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(playerController == null) playerController = collision.GetComponent<PlayerController>();
            if(_anchorAB == null) _anchorAB = playerController.GetComponentInChildren<AnchorAbility>();

            if(_anchorAB.IsInteractPressed)
            {
                _anchorAB.IsInteractPressed = false;
                InteractiveEffect();
            }
            
        }
    }
}
