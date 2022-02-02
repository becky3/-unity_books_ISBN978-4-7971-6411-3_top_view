using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class ObjectGenPoint : MonoBehaviour
{
    public GameObject objPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectCrater()
    {
        var pos = new Vector3(transform.position.x, transform.position.y, -1f);
        Instantiate(objPrefab, pos, Quaternion.identity);
    }
}
