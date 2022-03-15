using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridBrain : MonoBehaviour
{
    [SerializeField] GameObject tile;
    
    List<GameObject> interiorCells = new List<GameObject>(), grid = new List<GameObject>();
    public void BuildRoom(Vector3 origin, List<int> dimensions)
    {
        int height = dimensions[0];
        int width = dimensions[1];
        int xOrigin = (int)origin.x;
        int zOrigin = (int)origin.z;
        for (int ih = xOrigin; ih < height+xOrigin; ih++)
        {
            for (int iw = zOrigin; iw < width+zOrigin; iw++)
            {
                GameObject created = Instantiate(tile, new Vector3(ih, 0, iw), tile.transform.rotation);
                grid.Add(created);
                if (ih != xOrigin && ih != xOrigin+height - 1 && iw != zOrigin && iw != zOrigin + width - 1)
                {
                    interiorCells.Add(created);
                }
            }
        }
        gameObject.GetComponent<WallBrain>().BuildWalls(new Vector3(xOrigin+height - 1, 0, zOrigin + width - 1) / 2, grid.Except(interiorCells).ToList());
    }
}
