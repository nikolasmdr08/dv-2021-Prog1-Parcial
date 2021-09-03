using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisilControler : MonoBehaviour
{
    public List<Transform> enemiesInRange;
    public Transform target;
    public GameObject Player;
    public float speed = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        Invoke("Destroy", 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemiesInRange = RangoMisilControler.enemiesInRange;

        if(enemiesInRange.Count == 0)
        {
            target = null;
        }
        else
        {
            target = buscarCercano();
            Vector2 direccion = target.position - transform.position;
            float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            rb.rotation = angle-90;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    Transform buscarCercano()
    {
        float maxDistance = 1000; // ejemplo base exagerado
        Transform enemyTarget = null;
        foreach (Transform enemy in enemiesInRange)
        {
            if (Vector2.Distance(enemy.position, Player.transform.position) < maxDistance)
            {
                maxDistance = Vector2.Distance(enemy.position, Player.transform.position);
                enemyTarget = enemy;
            }
        }

        return enemyTarget;
    }
}
