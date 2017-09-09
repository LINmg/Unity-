using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//需预设手柄，并赋予scrollrect控件
//拖动惯性与弹性一般需要设置
public class HandleControl : MonoBehaviour ,IDragHandler{
    private RectTransform ts;
    private PlayerManage pm; //角色控制相关的class
    private Vector3 offset = new Vector3(0,0,0);//手柄轴心的偏移量

	void Start () {
        ts = GameObject.Find("handle_x").GetComponent<RectTransform>();
        pm = GameObject.Find("player").GetComponent<PlayerManage>(); 
	}
	
	void FixedUpdate () {
        pm.move = new Vector3(offset.x ,0,offset.y );//移动向量，需调试
		
	}

    public void OnBeginDrag(PointerEventData point)
    {

    }
    public void OnDrag(PointerEventData point)
    {
		//速度可在palyermanage中设置，也可在此设置
        offset = ts.localPosition / 80;
    }
    public void OnEndDrag(PointerEventData point)
    {
        offset = new Vector3(0,0,0);
    }
}
