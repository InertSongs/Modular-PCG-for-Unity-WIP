using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridBrain : IRoom
{
    [SerializeField] GameObject tile;
    
    List<GameObject> interiorCells = new List<GameObject>(), grid = new List<GameObject>();
    public override void BuildRoom(Vector3 origin, List<int> dimensions)
    {
        //Assigning ints to all dimensions
        int height = dimensions[0], width = dimensions[1], xOrigin = (int)origin.x, zOrigin = (int)origin.z,worldHeight = xOrigin + height, worldWidth = zOrigin + width;
        //building grid from tiles
        for (int ih = xOrigin; ih < worldHeight; ih++)
        {
            for (int iw = zOrigin; iw < worldWidth; iw++)
            {
                GameObject created = Instantiate(tile, new Vector3(ih, 0, iw), tile.transform.rotation);
                grid.Add(created);
                if (ih != xOrigin && ih != worldHeight-1 && iw != zOrigin && iw != worldWidth-1)
                {
                    interiorCells.Add(created);
                }
            }
        }
        List<GameObject> border = grid.Except(interiorCells).ToList();
        Vector3 center = new Vector3(height - 1, 0,width - 1)/2;
        gameObject.GetComponent<WallBrain>().BuildWalls(border, center, origin);
    }
    
}
