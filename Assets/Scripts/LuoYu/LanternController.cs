using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LanternController : MonoBehaviour
{
    //����
    public static LanternController instance;

    //��ɫ��������
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
