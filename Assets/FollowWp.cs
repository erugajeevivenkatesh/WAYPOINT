using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWp : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Target;
    public float speed = 10;
    public float rotationSpeed = 10f;

    public Transform[] waypoints;
    int CurrentWP = 0;
    public float GuidDistance = 5.0f;
    GameObject Tracker;
    void Start()
    {
        Tracker=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(Tracker.GetComponent<Collider>());
        Tracker.transform.position=this.transform.position;
        Tracker.transform.rotation=this.transform.rotation;
    }
    void ProgressTracker()
    {
        if (waypoints.Length > 0 && CurrentWP < waypoints.Length)
        {

            if (Vector3.Distance(Tracker.transform.position, waypoints[CurrentWP].position) > 3f)
            {
                Rotate(waypoints[CurrentWP], Tracker.transform,50.0f);
                Tracker.transform.position += Tracker.transform.forward * (speed+5.0f) * Time.deltaTime;
            }
            else
            {
                CurrentWP++;

            }
            if (CurrentWP >= waypoints.Length)
            {
                CurrentWP = 0;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Myway();
        if (Vector3.Distance(Tracker.transform.position, transform.position) < 5.0f)
        {
            ProgressTracker();
        }
            FollowTracker();
    }
    void FollowTracker()
    {
        Rotate(Tracker.transform, this.transform, rotationSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    void Myway()
    {
        if (waypoints.Length > 0 && CurrentWP < waypoints.Length)
        {

            if (Vector3.Distance(transform.position, waypoints[CurrentWP].position) > 3f)
            {
              //  Rotate(waypoints[CurrentWP],this.transform);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else
            {
                CurrentWP++;
                
            }
            if (CurrentWP >= waypoints.Length)
            {
                CurrentWP = 0;
            }
        }
    }
    void Rotate(Transform Target,Transform transform,float rotationSpeed)
    {
        Vector3 direction = Target.position - transform.position;
        direction.y = 0;
        direction=direction.normalized;
        Quaternion lookRotaton = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotaton,rotationSpeed*Time.deltaTime);
    }
}
