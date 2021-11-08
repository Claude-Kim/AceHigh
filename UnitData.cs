using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public int id;
    public string name;
    public int hp;
    public int cost;
}

[System.Serializable]
public class UnitData
{
    public static UnitData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<UnitData>(jsonString);
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    // 유닛 인덱스를 리스트로 만들고, 항목 안에 값을 넣는 스키마를 만들자.
    // {"id": 1, "name":"L-Tank", "hp":100, "Cost":100},


    //"{'tname':'j name','top':5}";
    public string tname;
    public string version;
    public Unit[] units;

}
