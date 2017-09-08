using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour {
    private Transform ts;
    private Ray ray;
    private RaycastHit hit;
    private bool isMoving = false;
    private CharacterController control;
    public float speed = 2;
    public PlayerState state = PlayerState.Idle;
    public GameObject effection;

    void Start () {
        ts = gameObject.GetComponent<Transform>();
        control = gameObject.GetComponent<CharacterController>();
	}

	void Update () {
        GetMouse();
        if (isMoving)
        {
            LookTo(hit.point);
            MoveTo(hit.point);
        }
	}

    void GetMouse()
    {
        if (Input.GetMouseButtonDown(1)&&!EventSystem.current.IsPointerOverGameObject() )//�������¼������ҵ���UI��ʱ��Ч
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == Tag.ground)
                {
                    isMoving = true;
                    ShowMouseDownEffect(new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z));
                }
            }
        }
    }

    //���ʱ��Ч��Ч���û��뾵ͷλ���йأ�
    void ShowMouseDownEffect(Vector3 position)
    {
        Instantiate(effection, position, Quaternion.identity,ts);
    }
    //���Ƴ���
    void LookTo(Vector3 position)
    {
        Vector3 to = new Vector3( position.x,ts.position.y, position.z);
        ts.LookAt(to);
    }
    //�����ƶ�
    void MoveTo(Vector3 position)
    {
        Vector3 to = new Vector3(position.x, ts.position.y, position.z);
        float distance = Vector3.Distance(to, ts.position);
        if (distance > 0.1f)
        {
            state = PlayerState.Moving;
            control.SimpleMove(ts.forward * speed);
        }else
        {
            state = PlayerState.Idle;
        }
    }

}
