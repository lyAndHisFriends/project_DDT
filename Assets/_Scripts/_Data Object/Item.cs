﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏中购买的道具.
/// </summary>
public class Item
{
	//目前看来，action和id都可以做唯一标识，id为0的道具就可以认为是不存在的
	public int id;
	public int price;
	public string name;
	public string desc;
	public string action;

	private Item ()
	{
		
	}

	public Item (int id, int price, string name, string desc, string action)
	{
		this.id = id;
		this.price = price;
		this.name = name;
		this.desc = desc;
		this.action = action;
	}

}

public class Drug:Item
{
	public Drug (int id, int price, string name, string desc, string action) : base (id, price, name, desc, action)
	{
	}

	public static Drug GetDrug (int id)
	{
		Drug drug = null;
		switch (id) {
		case 0:
			drug = new Drug (0, 0, "空", "", "");
			break;
		case 1:
			drug = new Drug (1, 150, "营养快线", "喝下营养快线，体力恢复5", Constants.USE_ITEM_1);
			break;
		case 2:
			drug = new Drug (2, 350, "汇仁肾宝", "喝下汇仁肾宝，体力恢复100", Constants.USE_ITEM_2);
			break;
		}
		return drug;
	}
}
