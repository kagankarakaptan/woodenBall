using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMind : MonoBehaviour
{
    Vector3 rotation;
    public float rotationSpeed;

    private void Start()
    {
        rotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rotationSpeed = Random.Range(30f, 50f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }

}
