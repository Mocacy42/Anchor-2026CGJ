using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LanternController : MonoBehaviour
{
    //����
    public static LanternController instance;

    //��ɫ��������
    [SerializeField] private PlayerController playerController;

    //��������Ƿ񱻳���
    [SerializeField] private bool isUsing = false;

    //[Header("�׳��ٶ�����")]
    //[SerializeField] private float HorizontalSpeed;
    //[SerializeField] private float VerticalSpeed;

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
            //��������У���������ƶ�
            transform.position = playerController.transform.position;
        }
    }

    //������
    public void PickUp()
    {
        isUsing = true;
    }
    //������
    public void PutDown()
    {
        isUsing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            collision.GetComponentInParent<Collider2D>().isTrigger = !collision.GetComponentInParent<Collider2D>().isTrigger;
        }
        //������ʧ����
        if (collision.gameObject.tag == "disappearItem")
        {
            //物体消失效果
            if (collision.GetComponent<DisappearItem>()) collision.GetComponent<DisappearItem>().EffectDisappear();
            //关闭刚体碰撞
            collision.GetComponent<Collider2D>().isTrigger = true;
            //关闭物体交互
            if (collision.GetComponent<InteractiveItem>()) collision.GetComponent<InteractiveItem>().enabled = false;
        }
        //������������
        if (collision.gameObject.tag == "appearItem")
        {
            //开启物体交互
            if(collision.GetComponent<InteractiveItem>()) collision.GetComponent<InteractiveItem>().enabled = true;
            //开启刚体碰撞
            collision.GetComponent<Collider2D>().isTrigger = false;
            //物体出现效果
            if (collision.GetComponent<AppearItem>()) collision.GetComponent<AppearItem>().EffectAppear();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.GetComponentInParent<Collider2D>().isTrigger = !collision.GetComponentInParent<Collider2D>().isTrigger;
        }
        ////�뿪��ʧ����
        if (collision.gameObject.tag == "disappearItem")
        {
            //开启物体交互
            if (collision.GetComponent<InteractiveItem>()) collision.GetComponent<InteractiveItem>().enabled = true;
            //开启刚体碰撞
            collision.GetComponent<Collider2D>().isTrigger = false;
            //物体出现效果
            if (collision.GetComponent<DisappearItem>()) collision.GetComponent<DisappearItem>().EffectAppear();
        }
        //�뿪��������
        if (collision.gameObject.tag == "appearItem")
        {
            //物体消失效果
            if (collision.GetComponent<AppearItem>()) collision.GetComponent<AppearItem>().EffectDisappear();
            //关闭刚体碰撞
            collision.GetComponent<Collider2D>().isTrigger = true;
            //关闭物体交互
            if (collision.GetComponent<InteractiveItem>()) collision.GetComponent<InteractiveItem>().enabled = false;
        }
    }
}
