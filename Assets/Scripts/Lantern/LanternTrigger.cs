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
            //物体出现效果
            collision.GetComponent<AppearItem>()?.EffectAppear();

            
        }
        else if(collision.CompareTag("disappearItem"))
        {
            var item = collision.GetComponent<DisappearItem>();
            item?.EffectDisappear();
            
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
            collision.GetComponent<AppearItem>()?.EffectDisappear();
        }
        else if(collision.CompareTag("disappearItem"))
        {
            var item = collision.GetComponent<DisappearItem>();
            item?.EffectAppear();
        }
    }
}
