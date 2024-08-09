using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStage : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public void DestroyStage()
    {
        Destroy(gameObject);
    }
}
