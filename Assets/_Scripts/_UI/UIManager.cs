﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class UIManager : MonoBehaviour
{

	public static UIManager Instance{ private set; get; }

	public Transform transCanvas;
	//所有panel都要注册在此
	public PanelMeetGirl panelMeetGirl;
	public PanelBuyGround panelBuyGround;
	public PanelBuyGroundNoMoney panelBuyGroundNoMoney;
	public PanelPayToll panelPayToll;
	public PanelEndTurn panelEndTurn;

	ArrayList uiList = new ArrayList ();

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	void Start ()
	{
		//把全部界面添加进来
		uiList.Add (panelMeetGirl);
		uiList.Add (panelBuyGround);
		uiList.Add (panelBuyGroundNoMoney);
		uiList.Add (panelPayToll);
		uiList.Add (panelEndTurn);
	}

	void Update ()
	{
		
	}

	/// <summary>
	/// 打开对应界面
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="args">Arguments.</param>
	public void Open<T> (Type type, ref T arg, params object[] args)
	{
//		print (panelMeetGirl.GetType ().Name);

		foreach (var item in uiList) {
			if (item.GetType ().Equals (type)) {
//				print ("对比成功");
//				GameObject ui = GameObject.Instantiate<GameObject> (((UIBase)item).gameObject, transCanvas);
				UIBase ui = GameObject.Instantiate<UIBase> ((UIBase)item, transCanvas);
				ui.SetParams (ref arg, args);
			} else {
//				print ("对比失败");
			}
		}
	}

	public void Open (Type type, params object[] args)
	{
		foreach (var item in uiList) {
			if (item.GetType ().Equals (type)) {
				UIBase ui = GameObject.Instantiate<UIBase> ((UIBase)item, transCanvas);
				ui.SetParams (args);
			} else {
//				print ("对比失败");
			}
		}
	}

	/// <summary>
	/// 关闭对应界面
	/// </summary>
	/// <param name="type">Type.</param>
	public void Close (Type type)
	{

		foreach (var item in transCanvas.GetComponentsInChildren(type)) {
			GameObject.Destroy (item.gameObject);
		}

	}
}
