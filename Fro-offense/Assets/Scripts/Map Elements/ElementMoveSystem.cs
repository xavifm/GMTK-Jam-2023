using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementMoveSystem : MonoBehaviour
{
    public Vector3 destinationVector;
    [SerializeField] float elementSpeed = 5;

    private void Update()
    {
        destinationVector = new Vector3(Mathf.Round(destinationVector.x), transform.position.y, Mathf.Round(destinationVector.z));
        transform.position = Vector3.Lerp(transform.position, destinationVector, Time.deltaTime * elementSpeed);
    }
}
