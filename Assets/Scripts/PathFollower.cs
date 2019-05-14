using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMaster;

public class PathFollower : MonoBehaviour
{
    public float step;
    public float speed = 5.0f;
    public float nodeCloseEnough = 0.2f;

    public List<Transform> myPath = new List<Transform>();


    public int currentWaypoint = 0;
    public float rotationSpeed = 5.0f;
    private float reachDistance = 1.0f;
    public string pathName;

    Vector3 lastPosition;
    Vector3 currentPosition;




    void Start()
    {
        step = speed * Time.deltaTime; // calculate distance to move
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (myPath.Count != 0)
        {

            float distance = Vector3.Distance(myPath[currentWaypoint].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, myPath[currentWaypoint].position, Time.deltaTime * speed);

            var rotation = Quaternion.LookRotation(myPath[currentWaypoint].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            if (distance <= reachDistance)
            {
                myPath.RemoveAt(currentWaypoint);
                currentWaypoint++;
            }

            if (currentWaypoint >= myPath.Count)
            {
                //loop
                currentWaypoint = 0;
            }

            Debug.DrawLine(transform.position, myPath[currentWaypoint].transform.position);

        }



        // if (myPath.Count != 0)
        // {
        //     moveToNextNode(myPath[1]);
        // }

        // for (int i = 0; i < myPath.Count; i++)
        // {
        //     if (Vector3.Distance(gameObject.transform.position, myPath[i].position) > nodeCloseEnough)
        //     {
        //         myPath.RemoveAt(i);
        //     }

        // }
    }

    public void addToMyPath(Transform node)
    {
        myPath.Add(node.transform);
    }


    private void moveToNextNode(Transform node)
    {
        // addToMyPath(node);
        for (int i = 0; i < myPath.Count; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, myPath[i].transform.position, step);
        }

    }




}

