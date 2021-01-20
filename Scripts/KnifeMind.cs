using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMind : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 movementDirection;
    public float movementSpeed;
    public GameObject potentialMother;
    public Vector3 endPoint;

    public bool canHit;
    public Vector3 colliderCenter;
    public Vector3 colliderSize;

    public GameObject master;
    public GameObject timer;
    public GameObject score;



    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer");
        master = GameObject.Find("Master");
        score = GameObject.Find("Score");


        colliderCenter = gameObject.GetComponent<BoxCollider>().center;
        colliderSize = gameObject.GetComponent<BoxCollider>().size;

        potentialMother = GameObject.Find("WoodenBall");
        transform.LookAt(endPoint);
        rb.AddForce(movementDirection * movementSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision with foods
        if (collision.collider.gameObject.CompareTag("food") && canHit)
        {
            canHit = false;
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(rb);

            transform.position = collision.collider.gameObject.transform.position;
            transform.SetParent(collision.collider.gameObject.transform);
            collision.collider.gameObject.transform.SetParent(null);
            collision.collider.gameObject.AddComponent<Rigidbody>();

            master.GetComponent<MasterMind>().score += master.GetComponent<MasterMind>().combo;
            if (master.GetComponent<MasterMind>().combo != 2048) master.GetComponent<MasterMind>().combo *= 2;
            master.GetComponent<MasterMind>().time += 3f;
            if (master.GetComponent<MasterMind>().time > 10f) master.GetComponent<MasterMind>().time = 10f;


            //animating timer,score and combo
            timer.GetComponent<Animator>().Play("shakeTimer", -1, 0f);
            score.GetComponent<Animator>().Play("shakeScore", -1, 0f);


            //extra force to up
            collision.collider.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
            collision.collider.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 200f);



        }
        //collision with wood
        else if (collision.collider.gameObject.CompareTag("wood") && canHit)
        {
            canHit = false;

            transform.SetParent(potentialMother.transform);
            Destroy(rb);

            //repositioning the collider and knife
            gameObject.GetComponent<BoxCollider>().center = new Vector3(colliderCenter.x, colliderCenter.y, colliderCenter.z * 2);
            gameObject.GetComponent<BoxCollider>().size = new Vector3(colliderSize.x, colliderSize.y, colliderSize.z / 2);
            transform.position = endPoint;


            master.GetComponent<MasterMind>().combo = 1;

            //animating wood
            collision.collider.gameObject.GetComponent<Animator>().Play("shakeWood", -1, 0f);

        }

        //collision with other knifes
        else if (collision.collider.gameObject.CompareTag("knife") && canHit)
        {
            canHit = false;
            //Destroy(gameObject.GetComponent<BoxCollider>());
            rb.useGravity = true;
            rb.freezeRotation = false;


            rb.angularVelocity = new Vector3(2, 2, 2);
            Destroy(gameObject, 5);

            master.GetComponent<MasterMind>().combo = 1;

            master.GetComponent<MasterMind>().Death();
        }

    }



}
