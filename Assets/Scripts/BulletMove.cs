using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("selfDestrution", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime) ;
    }
    
    void selfDestrution() {
        Destroy(gameObject);
    }
}
