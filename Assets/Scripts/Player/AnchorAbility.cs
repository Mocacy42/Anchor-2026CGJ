using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnchorAbility : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Rigidbody2D _anchorTargetRB;
    [SerializeField] private HingeJoint2D _anchorJoint;
    [SerializeField] private Collider2D _anchorColl;
    [SerializeField] private LineRenderer _trajectoryLine;
    [Header("PickDrop Logic")]
    [SerializeField] private bool canPickUp = false; // 是否处于触发器中
    [SerializeField] private bool isPicking = false; // 是否已经装备锚点
    [SerializeField] private bool isPickDropPressed = false; 
    [SerializeField] private bool isPickDropHold = false;
    [SerializeField] private bool isPickDropUp = false;
    [SerializeField] private bool isTeleportPressed = false; // 传送交互
    public bool IsInteractPressed {get; set;}
    [SerializeField] private bool isInteractPressed;
    [Header("Anchor Position")]
    [SerializeField] private int lastDir = 1;
    [SerializeField] private int currDir = 1;
    [SerializeField] private Vector3 pickupPos = new Vector2(0.5f,0.2f);
    [Header("Throw Settings")]
    [SerializeField] private float _maxThorwDist = 7.0f;
    [SerializeField] private int sampleCount = 10;
    [SerializeField] private int _displayCount = 2;
    [SerializeField] private float _thorwForce = 5.0f;
    
    void Awake()
    {
        
        if(_rb == null)  _rb = gameObject.GetComponentInParent<Rigidbody2D>();
        if(_anchorJoint == null) LanternController.instance.gameObject.TryGetComponent<HingeJoint2D>(out _anchorJoint);
        if(_anchorColl == null) LanternController.instance.gameObject.TryGetComponent<Collider2D>(out _anchorColl);
        if(_trajectoryLine == null) gameObject.TryGetComponent<LineRenderer>(out _trajectoryLine);
        if(_anchorTargetRB == null) _anchorTargetRB = gameObject.GetComponentInChildren<Rigidbody2D>();
        
        InputInstance.Instance.PInput.Player.PickDrop.started += ctx => {isPickDropPressed = true; isPickDropHold = false; isPickDropUp = false;};
        InputInstance.Instance.PInput.Player.PickDrop.performed += ctx => {isPickDropHold = true; isPickDropUp = false;};

        InputInstance.Instance.PInput.Player.PickDrop.canceled += OnPickDrop;

        InputInstance.Instance.PInput.Player.TELEPORT.started += ctx => isTeleportPressed = true;
        InputInstance.Instance.PInput.Player.TELEPORT.canceled += ctx => isTeleportPressed = false;

        InputInstance.Instance.PInput.Player.INTERACT.performed += ctx => {IsInteractPressed = true;isInteractPressed = true;};
        InputInstance.Instance.PInput.Player.INTERACT.canceled += ctx => {IsInteractPressed = false;isInteractPressed = false;};
    }

    // 传送至锚点，后续可以修改为协程控制
    public void Teleport(Vector2 targetPos)
    {
        if(isTeleportPressed && isPicking == false)
        {
            _rb.gameObject.transform.position = targetPos;
            _rb.velocity = Vector2.zero;
            isTeleportPressed = false;
        }
    }

    // 拾取放下锚点
    private void OnPickDrop(InputAction.CallbackContext ctx)
    {
        isPickDropUp = true;

        if(canPickUp && isPicking == false && isPickDropPressed == true) // 拾取
        {
            PickUp();
            isPicking = true;
        }
        else if( isPicking == true && isPickDropUp == true && isPickDropHold == false && isPickDropPressed == true) // 放下
        {            
            DropAnchor();
            isPicking = false;
        }
        else if(isPicking && isPickDropUp && isPickDropHold == true) // 扔出
        {
            ThorwAnchor();
            isPicking = false;
        }


        if(_trajectoryLine && _trajectoryLine.enabled == true) _trajectoryLine.enabled = false;
        isPickDropPressed = false;
        isPickDropHold = false;
    }
    // 将锚点放下
    private void DropAnchor()
    {
        _anchorJoint.enabled = false;
        _anchorColl.enabled = true;
    }
    // 将锚点扔出
    private void ThorwAnchor()
    {
        _anchorJoint.enabled = false;
       
        Vector2 p0 = LanternController.instance.transform.position;

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = (mouseWorld - (Vector2)p0).normalized;

        Vector2 p1 = p0 + dir * _maxThorwDist;
        Vector2 p2 = new Vector2(p1.x + p1.x - p0.x , p0.y);
        Vector2 tangentDir = BezierHelper.GetStartTangent(p0,p1);

        // throw 
        LanternController.instance.GetComponent<Rigidbody2D>().AddForce(tangentDir * _thorwForce, ForceMode2D.Impulse);
        _anchorColl.enabled = true;

    }
    // 将锚点拾起
    private void PickUp()
    {
        LanternController.instance.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        Vector3 facedir = new Vector3( _rb.transform.localScale.x >= 0 ? 1: -1,  1, 1);
        LanternController.instance.gameObject.transform.position = _anchorTargetRB.transform.position ;
        _anchorJoint.enabled = true;
        _anchorColl.enabled = false;

    }

    // 渲染贝塞尔曲线
    private void DrawBezierLine()
    {
        if( isPicking && isPickDropHold == true && isPickDropUp == false)
        {
            if(_trajectoryLine && _trajectoryLine.enabled == false) _trajectoryLine.enabled = true;

            Vector2 p0 = LanternController.instance.transform.position;
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 dir = (mouseWorld - (Vector2)p0).normalized;

            Vector2 p1 = p0 + dir * _maxThorwDist;
            Vector2 p2 = new Vector2(p1.x + p1.x - p0.x , p0.y);
            Vector2 tangentDir = BezierHelper.GetStartTangent(p0,p1);

            // render line
            Vector2[] curvePoints = BezierHelper.GetBezierPoints(p0, p1, p2, sampleCount);
            List<Vector3> lineVerts = new List<Vector3>();
            for(int i = 0; i < _displayCount && i < sampleCount; ++i)
                lineVerts.Add(curvePoints[i]);
            if(_trajectoryLine) _trajectoryLine.positionCount = lineVerts.Count;
            if(_trajectoryLine) _trajectoryLine.widthMultiplier = 0.1f; 
            if(_trajectoryLine) _trajectoryLine.SetPositions(lineVerts.ToArray());
        }
    }


    void Update()
    {
        currDir = (int)_rb.transform.localScale.x;
        if(lastDir != currDir && isPicking)
        {
            LanternController.instance.gameObject.transform.position = _anchorTargetRB.transform.position;
            
        }
        lastDir = currDir;
        Teleport(LanternController.instance.transform.position + Vector3.up);
        DrawBezierLine();
            
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Anchor"))
        {
            canPickUp = true;
        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Anchor"))
        {
            canPickUp = false;
        }
    }
}
