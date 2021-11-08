using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleRuler;

public class SpawnManager : MonoBehaviour
{
    //public GameObject SpawnPos;
    public GameObject[] unitPrefabs;
    public int unitIndex;
    public bool AutoMatic = false;

    public float spawnInterval = 3.0f;
    public float spawnDelay;

    private BattleRuler BR;

    // Start is called before the first frame update
    void Start()
    {
        BR = FindObjectOfType<BattleRuler>();
        if (BR == null )
        {
            Debug.Log("Can't find BattleRuler.");
        }

        unitIndex = 0;
        spawnDelay = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!AutoMatic)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SpawnBlueForce(0);
                
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SpawnBlueForce(1);

            } else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SpawnBlueForce(2);
            }
        }
        else {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay <= 0)
            {
                SpawnRedForce( Random.Range(0, unitPrefabs.Length) );
                spawnDelay = spawnInterval + spawnInterval * Random.Range(-0.5f, 0.5f); 
            }
        }
    }

    public void SpawnBlueForce(int unitIndex)
    {

        if (unitIndex == 0 && BR.cosmoEnergy >= BR.unitData.units[0].cost)
        {
            BR.cosmoEnergy -= BR.unitData.units[0].cost;
        } else if (unitIndex == 1 && BR.cosmoEnergy >= BR.unitData.units[1].cost)
        {
            BR.cosmoEnergy -= BR.unitData.units[1].cost;
        } else if (unitIndex == 2 && BR.cosmoEnergy >= BR.unitData.units[2].cost)
        {
            BR.cosmoEnergy -= BR.unitData.units[2].cost;
        } else
        {
            return;
        }

        GameObject gou = Instantiate(unitPrefabs[unitIndex], transform.position, unitPrefabs[unitIndex].transform.rotation);
        gou.GetComponent<GroundUnitBehaviour>().forceSide = FORCE_SIDE.BLUE;
        gou.layer = 8; // BlueForce
        gou.transform.Find("Sight").gameObject.layer = 10;// Blue Sight

        BR.Blue.Add(gou);
    }

    private void SpawnRedForce(int unitIndex)
    {       
        GameObject gou = Instantiate(unitPrefabs[unitIndex], transform.position, unitPrefabs[unitIndex].transform.rotation);
        gou.GetComponent<GroundUnitBehaviour>().forceSide = FORCE_SIDE.RED;
        gou.layer = 9; // RedForce
        gou.transform.Find("Sight").gameObject.layer = 11;// Red Sight
        Debug.Log("Spawn Red : index " + unitIndex + "  pos : " + transform.position);

        BR.Red.Add(gou);
    }
}
