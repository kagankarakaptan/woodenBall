using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMind : MonoBehaviour
{
    public LayerMask mask;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, distance, mask) && Input.GetMouseButtonDown(0))
        {
            GameObject knife = Instantiate(Resources.Load("Prefabs/Knife"), transform.position, Quaternion.identity) as GameObject;
            KnifeMind knifeMind = knife.GetComponent<KnifeMind>();
            knifeMind.movementDirection = (hit.point - transform.position).normalized;
            knifeMind.endPoint = hit.point;


        }
    }
}
