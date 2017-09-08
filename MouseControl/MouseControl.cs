using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private Transform ts;
    public Transform ts_player;//也可在unity中指定player
    private Vector3 offset;
    private Camera ca;
    public float scrollspeed = 10;
    public float xspeed = 100; //转速，需调试
    public float yspeed = 100;
    public float x;
    public float y;
    public float xlimit_min=5;//x，y旋转角度限制，需调试
    public float xlimit_max=60;

    void Start()
    {
        ts = gameObject.GetComponent<Transform>();
        ts_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ts.LookAt(ts_player);
        offset = ts.position - ts_player.position;
        ca = gameObject.GetComponent<Camera>();
        x = ts.eulerAngles.x;
        y = ts.eulerAngles.y;
    }
    //镜头跟随主角
    void LateUpdate()
    {
        ts.position = ts_player.position + offset;
        ScrollView();
        if (Input.GetMouseButton(0))
        {
            RotateView();
        }
    }
    //鼠标滚动实现镜头拉近拉远
    void ScrollView()
    {
        float field = ca.fieldOfView - scrollspeed* Input.GetAxis("Mouse ScrollWheel");
        if (field >= 10 && field <= 100)
        {
            ca.fieldOfView = field ;
        }
    }

    //左键按住旋转视野
    void RotateView()
    {
        float x_a= Input.GetAxis("Mouse X") * xspeed * 0.02f;
        float y_a= -Input.GetAxis("Mouse Y") * yspeed * 0.02f;
        ts.RotateAround(ts_player.position, ts_player.up, x_a);
        ts.RotateAround(ts_player.position, ts_player.right, y_a);
        x = ts.eulerAngles.x;
        y = ts.eulerAngles.y;
        if (x > xlimit_max)
        {
            Quaternion rotation = Quaternion.Euler( xlimit_max,y, 0);
            ts.rotation = rotation;
        } else if (x < xlimit_min)
        {
            Quaternion rotation = Quaternion.Euler( xlimit_min,y, 0);
            ts.rotation = rotation;
        }
        else
        {
            Quaternion rotation =  Quaternion.Euler(x, y, 0);
            ts.rotation = rotation;
        }
        offset = ts.position - ts_player.position;
    }
}
