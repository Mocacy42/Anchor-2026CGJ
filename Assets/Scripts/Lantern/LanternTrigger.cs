using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternTrigger : MonoBehaviour
{

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

            //物体消失效果
            collision.GetComponent<AppearItem>().EffectDisappear();
            //关闭物体交互
            collision.GetComponent<AppearItem>().enabled = false;
        }
    }
}
