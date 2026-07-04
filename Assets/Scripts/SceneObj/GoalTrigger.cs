using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{

    [SerializeField] private Animator _anim;
    public static GoalTrigger Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if(_anim == null) TryGetComponent(out _anim);
    }

    
    public bool GetGoal {get; set;} = false;
    [SerializeField] private bool isFinishing = false;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetGoal = true;
            
        }
    }

    void Update()
    {
       CheckLevelFinish();       
    }

    void CheckLevelFinish()
    {
        if(GetGoal && isFinishing == false)
        {
            isFinishing = true;
            StartCoroutine(IELevelfinish());

        }
    }
    // 到达终点
    IEnumerator IELevelfinish()
    {
        _anim.SetBool("OpenGate",true);
        yield return null;
        GetGoal = false;
    }
}
