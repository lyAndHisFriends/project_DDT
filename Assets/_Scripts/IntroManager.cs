﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	public static IntroManager Instance{ get; private set; }

	public GameObject panelIntroMain;
	public Button btnNewGame;
	public Button btnContinue;
	public Button btnCustomGirl;
	public Button btnSetting;
	public InputField inputFieldPlayerNumber;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		btnNewGame.onClick.AddListener (() => {
			Constants.FromBeginning = true;
			Constants.PlayerNumber = int.Parse (inputFieldPlayerNumber.text);
			SceneManager.LoadScene ("[LoadingScene2]");
		});
		btnContinue.onClick.AddListener (() => {
			Constants.FromBeginning = false;
			SceneManager.LoadScene ("[LoadingScene2]");
		});
		btnCustomGirl.onClick.AddListener (() => {
			panelIntroMain.SetActive (false);
			UIManager.Instance.Open (typeof(PanelCustomGirl));
		});
		btnSetting.onClick.AddListener (() => {
			panelIntroMain.SetActive (false);
			UIManager.Instance.Open (typeof(PanelSetting));
		});
	}

	void Update ()
	{
		#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.P)) {
			DatabaseManager.Instance.CleanDB ();
			PlayerPrefs.SetInt (Constants.FLAG_FIRST_GAME, 0);
		}
		#endif
	}
}
