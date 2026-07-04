using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LanternController : MonoBehaviour
{
    //����
    public static LanternController instance;

    //��ɫ��������
    [SerializeField] private PlayerController playerController;


    //[Header("�׳��ٶ�����")]
    //[SerializeField] private float HorizontalSpeed;
    //[SerializeField] private float VerticalSpeed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }





}
