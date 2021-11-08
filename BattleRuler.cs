using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static TestClass;

[System.Serializable]
public class StageData
{
    public int regen_point;
    public int energy_max;
    public int init_energy;

}

    public class BattleRuler : MonoBehaviour
{

    public enum FORCE_SIDE { BLUE = 0, RED = 1 };

    public List<GameObject> Blue;
    public List<GameObject> Red;

    public TextMeshProUGUI ceTxt;
    public int cosmoEnergy;
    

    //private string jsonTxt = @"{""tname"":""t name"",""top"":5}";
    public UnitData unitData;
    public StageData stageData;


    private void Awake()
    {
        InitializeBattle();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Battle Start!");
        InvokeRepeating("CosmoRegen", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeBattle()
    {
        TextAsset textData;

        textData = Resources.Load("txt_Stagedata") as TextAsset;        
        stageData = JsonUtility.FromJson<StageData>(textData.ToString());
        if (stageData != null) Debug.Log("StageData loading completed.");
        cosmoEnergy = stageData.init_energy;

        textData = Resources.Load("txt_unitdata") as TextAsset;        
        unitData = UnitData.CreateFromJSON(textData.ToString());
        if (unitData != null) Debug.Log("UnitData loading completed. version : "+unitData.version);

        Debug.Log("Units length : " + unitData.units.Length);
    }

    public void BattleOver(FORCE_SIDE fs)
    {
        Debug.Log("Game End : " + fs);
        Time.timeScale = 0;
        Application.Quit();
    }


    // Call in InitializeBattle()
    private void CosmoRegen()
    {
        cosmoEnergy += stageData.regen_point;
        if (cosmoEnergy > stageData.energy_max ) cosmoEnergy = stageData.energy_max;
        ceTxt.text = "Cosmo Energy : " + cosmoEnergy;
    }

}
