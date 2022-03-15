using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallBrain : MonoBehaviour
{
    [SerializeField] GameObject tile;
    List<GameObject> wallBase = new List<GameObject>(), corners = new List<GameObject>();
    [HideInInspector] public List<GameObject> mayDoor = new List<GameObject>();
    int wallHeight = 5;
    float offset = 0.5f;
    public void BuildWalls(Vector3 center, List<GameObject> borderCells)
    {
        foreach (GameObject borderCell in borderCells)
        {
            Vector3 cellPos = borderCell.transform.position;
            Vector3 relCellPos = cellPos - center;
            float relX = relCellPos.x, relZ = relCellPos.z, cenX = center.x, cenZ = center.z, cellX = cellPos.x, cellZ = cellPos.z;
            if (Mathf.Abs(relX) == cenX)
            {
                GameObject created = Instantiate(tile, new Vector3(cellX + Mathf.Sign(relX) * offset, offset, cellPos.z), Quaternion.Euler(new Vector3(0, 0, 90 * Mathf.Sign(relX))));
                wallBase.Add(created);
                if (Mathf.Abs(relZ) == cenZ)
                {
                    corners.Add(created);
                }
            }
            if (Mathf.Abs(relZ) == cenZ)
            {
                GameObject created = Instantiate(tile, new Vector3(cellX, offset, cellZ + Mathf.Sign(relZ) * offset), Quaternion.Euler(new Vector3(-90 * Mathf.Sign(relZ), 0, 0)));
                wallBase.Add(created);
                if (Mathf.Abs(relX) == cenX)
                {
                    corners.Add(created);
                }
            }
        }
        foreach (GameObject first in wallBase)
        {
            Vector3 tilePos = first.transform.position;
            for (float i = 1.5f; i <= wallHeight; i += 1)
            {
                Instantiate(tile, new Vector3(tilePos.x, i, tilePos.z), first.transform.rotation);
            }
        }
        mayDoor = wallBase.Except(corners).ToList();
    }
}
