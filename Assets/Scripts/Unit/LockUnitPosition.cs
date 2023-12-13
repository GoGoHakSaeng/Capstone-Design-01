using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockUnitPosition : MonoBehaviour
{
    private int desiredZPosition = 77;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler (new Vector3 (200, 0, 0));
        Vector3 newPosition = transform.position;
        newPosition.z = desiredZPosition;
        transform.position = newPosition;
    }
}
