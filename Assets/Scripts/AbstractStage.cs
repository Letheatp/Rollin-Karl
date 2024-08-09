using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStage : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public void DestroyStage()
    {
        Debug.Log("Stage‚ð”j‰ó‚µ‚Ü‚·");
        Destroy(gameObject);
    }
}
