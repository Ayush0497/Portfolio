using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public Material Material;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", 3.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PaintHere")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Material.color;
        }
        DestroySelf();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
