﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMeetGirl : UIBase
{
	public static PanelMeetGirl Instance{ private set; get; }

	public RawImage imgGirlPortrait;
	public Text txtGirlName;
	public Text txtGirlSalary;
	public Button btnMeetGirlYes;
	public Button btnMeetGirlNo;

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
	}


	public override void SetParams<T> (ref T arg, params object[] args)
	{
		base.SetParams (ref arg, args);

		Girl leisureGirl = arg as Girl;//Unboxing
		txtGirlName.text = leisureGirl.Name;
		txtGirlSalary.text = "薪水:" + leisureGirl.Salary;

		btnMeetGirlYes.onClick.AddListener (() => {
			leisureGirl.SetGirl (GameManager.Instance.GetCurrentPlayer ().Index);
			GameManager.Instance.SetAction (Constants.ACTION_MEET_GIRL_YES);
		});
		btnMeetGirlNo.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_MEET_GIRL_NO);
		});
	}
}
