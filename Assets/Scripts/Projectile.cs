using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] public GameObject target;
    public float duration;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Reticle");      
        distance = Vector3.Distance(transform.position, target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = target.transform.position - transform.position;
        float angle = Mathf.Atan2(rot.y,rot.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,1f);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, (distance/duration)*Time.deltaTime);
        if(transform.position == target.transform.position)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    
}
