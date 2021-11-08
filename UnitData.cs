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

    // ���� �ε����� ����Ʈ�� �����, �׸� �ȿ� ���� �ִ� ��Ű���� ������.
    // {"id": 1, "name":"L-Tank", "hp":100, "Cost":100},


    //"{'tname':'j name','top':5}";
    public string tname;
    public string version;
    public Unit[] units;

}
