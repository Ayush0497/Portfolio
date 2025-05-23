using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    protected PrefabPool prefabPool;
    public Transform target;
    public int wave01NumShips;
    public float wave01DistanceFromCenter;

    private void Awake()
    {
        prefabPool = GameObject.Find("PrefabPool").GetComponent<PrefabPool>();
    }

    private void Start()
    {
        SpawnWave01();
    }

    protected void SpawnWave01()
    {
        Transform[] ships = new Transform[wave01NumShips];
        for(int c = 0; c < wave01NumShips; c++) 
        {
            ships[c] = prefabPool.EnemyShip;
            ships[c].GetComponent<Ship>().target = this.target;
        }

        Vector3 centerPos = target.position;
        for (int pointNum = 0; pointNum < wave01NumShips; pointNum++)
        {
            //dividing by numShipInWave01 spaces the  ships  evenly around the circle
            float i = (pointNum * 1.0f) / wave01NumShips;
            // get the angle for this step (in radians, not degrees)
            float angle = i * Mathf.PI * 2;
            // the X & Y position for this angle are calculated using Sin & Cos
            float x = Mathf.Sin(angle) * wave01DistanceFromCenter;
            float y = Mathf.Cos(angle) * wave01DistanceFromCenter;
            Vector3 pos = new Vector3(x, y, 0) + centerPos;
            // no need to assign the instance to a variable unless you're using it afterwards:
            ships[pointNum].transform.position = pos;
        }
    }
}
