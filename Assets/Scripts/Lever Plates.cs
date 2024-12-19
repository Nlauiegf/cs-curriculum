using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPlates : MonoBehaviour
{
  
    public float speed = 4;
    private int targetWaypoint = 0;
    public List<GameObject> waypoints;
    public Vector3 mtpv;
    public Lever Lv;
    public string direction;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        Lv = FindObjectOfType<Lever>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = waypoints[targetWaypoint].transform.position;
        mtpv = (targetPosition - transform.position).normalized;
        if (Vector3.Distance(waypoints[targetWaypoint].transform.position, transform.position) < .015 && Lv.StartPlatformMoving)
        {
            targetWaypoint++;
            targetWaypoint %= waypoints.Count;
        }
        transform.Translate(mtpv * (speed * Time.deltaTime));
    }
}
