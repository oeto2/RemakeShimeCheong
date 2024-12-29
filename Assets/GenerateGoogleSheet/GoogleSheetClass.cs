using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>You must approach through `GoogleSheetManager.SO<GoogleSheetSO>()`</summary>
public class GoogleSheetSO : ScriptableObject
{
	public List<Item_Table> Item_TableList;
	public List<Clue_Table> Clue_TableList;
	public List<Dialogue_Table> Dialogue_TableList;
	public List<Event_Table> Event_TableList;
}

[Serializable]
public class Item_Table
{
	public int Id;
	public string ObjectType;
	public string Name;
	public string Comment;
	public bool IsUsing;
	public int IndexNum;
	public string SpritePath;
}

[Serializable]
public class Clue_Table
{
	public int Id;
	public string ObjectType;
	public string Name;
	public string Comment;
	public bool IsUsing;
	public int IndexNum;
	public string SpritePath;
}

[Serializable]
public class Dialogue_Table
{
	public int Id;
	public string SpeakerType;
	public string Name;
	public string Comment;
	public bool IsUsing;
	public int IndexNum;
	public int NextCommentNum;
	public int EquipCondition;
	public int EventCondition;
	public int StartEventID;
	public int EndEventID;
	public int RewardID;
}

[Serializable]
public class Event_Table
{
	public int Id;
	public string EventType;
	public string Name;
	public string Comment;
	public string Description;
	public bool IsClear;
	public int IndexNum;
}

