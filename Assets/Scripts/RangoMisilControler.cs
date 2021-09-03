using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoMisilControler : MonoBehaviour
{
    public static List<Transform> enemiesInRange;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //print("enemigo en rango");
            enemiesInRange.Add(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //print("enemigo fuera de rango");
            enemiesInRange.Remove(collision.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
