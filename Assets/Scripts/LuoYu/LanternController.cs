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
        //������ʧ����
        //if (collision.gameObject.tag == "disappearItem")
        //{
        //    collision.GetComponent<SpriteRenderer>().enabled = false;
        //}
        //������������
        if (collision.gameObject.tag == "appearItem")
        {
            //开启物体可见
            collision.GetComponent<SpriteRenderer>().enabled = true;
            //开启物体交互
            collision.GetComponent<AppearItem>().enabled = true;
            //物体出现效果
            collision.GetComponent<AppearItem>().EffectAppear();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ////�뿪��ʧ����
        //if (collision.gameObject.tag == "disappearItem")
        //{
        //    collision.GetComponent<SpriteRenderer>().enabled = true;
        //}
        //�뿪��������
        if (collision.gameObject.tag == "appearItem")
        {
            //关闭物体可见
            collision.GetComponent<SpriteRenderer>().enabled = false;
            //物体消失效果
            collision.GetComponent<AppearItem>().EffectDisappear();
            //关闭物体交互
            collision.GetComponent<AppearItem>().enabled = false;
        }
    }
}
