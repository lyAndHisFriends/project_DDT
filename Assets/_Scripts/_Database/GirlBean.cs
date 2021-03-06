﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Newtonsoft.Json;

public class GirlBean : MonoBehaviour
{
	public static GirlBean Instance{ get; private set; }

	private DbAccess db;
	private SqliteDataReader sqReader;
	private string tableName = Constants.TableNameGirl;

	private string[] colName = new string[] {
		"[Index]", "Name", "Job", "Type", "Character",
		"Love", "Salary", "Patient", "Pressure", "Grade", "Owner", "LastOwner",
		"HistoryOwner1", "HistoryOwner2", "HistoryOwner3", "HistoryOwner4"
	};
	private string[] colType = new string[] {
		"integer", "text", "text", "text", "text",
		"integer", "integer", "integer", "integer", "integer", "integer", "integer", 
		"integer", "integer", "integer", "integer"
	};

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	//	void Start ()
	//	{
	//
	//	}
	
	// Update is called once per frame
	//	void Update ()
	//	{
	//
	//	}

	//-------------------------public function-------------

	//Girl包含：初始化表，反序列化，保存表，读取表，逐条更新,清空表

	public void InitialTable (DbAccess db)
	{
		this.db = db;
		//创建数据库表，与字段
		db.CreateTable (tableName, colName, colType, false);
	}

	//新游戏的时候一次性存入表
	public void SaveGirlList2DB ()
	{
		StartCoroutine (DoWriteJob (ReadGirlListFromJson ()));
	}

	IEnumerator DoWriteJob (List<Girl> gList)
	{
		List<Girl>.Enumerator ie = gList.GetEnumerator ();
		while (ie.MoveNext ()) {
			yield return new WaitForEndOfFrame ();//wait
			Girl girl = ie.Current as Girl;
//			print ("保存:" + girl.Name);
			db.InsertInto (tableName, new object[] {
				girl.Index,
				"'" + girl.Name + "'",
				"'" + girl.Job + "'",
				"'" + girl.Type + "'",
				"'" + girl.Character + "'",
//				girl.UseCustom,
				girl.Love,
				girl.Salary,
				girl.Patient,
				girl.Pressure, 
				girl.Grade, 
				girl.Owner,
				girl.LastOwner,
				girl.HistoryOwner [0],
				girl.HistoryOwner [1], 
				girl.HistoryOwner [2], 
				girl.HistoryOwner [3]
			});
		}

	}

	private List<Girl> ReadGirlListFromJson ()
	{
		TextAsset paraAsset = Resources.Load<TextAsset> ("json/girl json");
		print ("json:" + paraAsset.text);
		GirlJson mJson = JsonConvert.DeserializeObject<GirlJson> (paraAsset.text);
		Resources.UnloadUnusedAssets ();
//		print ("gggdgdgdg:" + mJson.girlList.Count);
//		for (int i = 0; i < mJson.girlList.Count; i++) {
//			print ("name:" + mJson.girlList [i].Name);
//		}
		return mJson.girlList;
	}

	public List<Girl> GetGirlListFromDB ()
	{
		sqReader = db.ReadFullTable (tableName);
		List<Girl> pList = new List<Girl> ();
		while (sqReader.Read ()) {
			Girl g = new Girl (
				         index: sqReader.GetInt32 (sqReader.GetOrdinal ("Index")),//读取不需要加[]括号
				         name: sqReader.GetString (sqReader.GetOrdinal ("Name")),
				         job: sqReader.GetString (sqReader.GetOrdinal ("Job")),
				         type: sqReader.GetString (sqReader.GetOrdinal ("Type")),
				         character: sqReader.GetString (sqReader.GetOrdinal ("Character")),
//				         useCustom: sqReader.GetInt32 (sqReader.GetOrdinal ("UseCustom")),
				         love: sqReader.GetInt32 (sqReader.GetOrdinal ("Love")),
				         salary: sqReader.GetInt32 (sqReader.GetOrdinal ("Salary")),
				         patient: sqReader.GetInt32 (sqReader.GetOrdinal ("Patient")),
				         pressure: sqReader.GetInt32 (sqReader.GetOrdinal ("Pressure")),
				         grade: sqReader.GetInt32 (sqReader.GetOrdinal ("Grade")),
				         owner: sqReader.GetInt32 (sqReader.GetOrdinal ("Owner")),
				         lastOwner: sqReader.GetInt32 (sqReader.GetOrdinal ("LastOwner")),
				         historyOwner1: sqReader.GetInt32 (sqReader.GetOrdinal ("HistoryOwner1")),
				         historyOwner2: sqReader.GetInt32 (sqReader.GetOrdinal ("HistoryOwner2")),
				         historyOwner3: sqReader.GetInt32 (sqReader.GetOrdinal ("HistoryOwner3")),
				         historyOwner4: sqReader.GetInt32 (sqReader.GetOrdinal ("HistoryOwner4"))
			         );
			pList.Add (g);
//			print ("读取:" + g.Name);
		}
		return pList;
	}

	//单条更新
	public void UpdateGirl2DB (Girl girl)
	{
		db.UpdateInto (tableName, new string[] {
			"[Index]", "Name", "Job", "Type", "Character",
			"Love", "Salary", "Patient", "Pressure", "Grade", "Owner", "LastOwner",
			"HistoryOwner1", "HistoryOwner2", "HistoryOwner3", "HistoryOwner4"
		}, new object[] {
			girl.Index,
			"'" + girl.Name + "'",
			"'" + girl.Job + "'",
			"'" + girl.Type + "'",
			"'" + girl.Character + "'",
//			girl.UseCustom,
			girl.Love,
			girl.Salary,
			girl.Patient,
			girl.Pressure, 
			girl.Grade, 
			girl.Owner,
			girl.LastOwner,
			girl.HistoryOwner [0],
			girl.HistoryOwner [1], 
			girl.HistoryOwner [2], 
			girl.HistoryOwner [3]
		}, "[Index]", girl.Index);
	}

	public void DeleteGirlListFromDB ()
	{
		sqReader = db.DeleteContents (tableName);
	}
}
