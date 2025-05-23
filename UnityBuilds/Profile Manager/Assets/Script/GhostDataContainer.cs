using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GhostDataContainer
{
    public List<float> posX, posY, posZ;

    int index;

    public GhostDataContainer()
    {
        posX = new List<float>();
        posY = new List<float>();
        posZ = new List<float>();
    }

    public void Reset()
    {
        posX.Clear();
        posY.Clear();
        posZ.Clear();
    }

    public void AddPosition(Vector3 position)
    {
        posX.Add(position.x);
        posY.Add(position.y);
        posZ.Add(position.z);
    }
    public bool GetNextPosition(ref Vector3 vec)
    {
        index++;

        if (index >= 0 && index < posX.Count)
        {
            vec = new Vector3(posX[index], posY[index], posZ[index]);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetFirstPosition(ref Vector3 vec)
    {
        if (posX.Count > 0)
        {
            index = 0;

            vec = new Vector3(posX[index], posY[index], posZ[index]);
            return true;
        }
        else
        {
            return false;
        }
    }
}