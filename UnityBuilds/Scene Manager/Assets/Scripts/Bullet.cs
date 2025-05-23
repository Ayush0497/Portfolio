using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyAfter1Second(this.gameObject));
    }
    private IEnumerator DestroyAfter1Second(GameObject bullet)
    {
        yield return new WaitForSeconds(1);
        Destroy(bullet);
    }  
}
