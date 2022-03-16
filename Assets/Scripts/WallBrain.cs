using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBrain : MonoBehaviour
{
    [SerializeField] GameObject tile;
    List<GameObject> wallBase = new List<GameObject>();
    float offset = 0.5f;
    public void BuildWalls(List<GameObject> border, Vector3 center, Vector3 origin)
    {
        foreach(GameObject borderTile in border)
        {
            Vector3 celPos = borderTile.transform.position;
            Vector3 relPos = celPos - origin - center;
            if(Mathf.Abs(relPos.x)==center.x)
            {
                wallBase.Add(Instantiate(tile,new Vector3(celPos.x + Mathf.Sign(relPos.x) * offset, offset,celPos.z), Quaternion.Euler(new Vector3(0,0,90*Mathf.Sign(relPos.x)))));
            }
            if (Mathf.Abs(relPos.z) == center.z)
            {
                wallBase.Add(Instantiate(tile, new Vector3(celPos.x , offset, celPos.z + Mathf.Sign(relPos.z) * offset), Quaternion.Euler(new Vector3(90 * -Mathf.Sign(relPos.z),0, 0))));
            }
        }
        
    }
    
}
