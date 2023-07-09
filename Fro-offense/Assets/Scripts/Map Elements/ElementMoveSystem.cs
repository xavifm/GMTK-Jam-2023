using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementMoveSystem : MonoBehaviour
{
    public Vector3 destinationVector;
    public bool enableYMovement;
    [SerializeField] float elementSpeed = 5;

    private void Update()
    {
        if(!enableYMovement)
            destinationVector = new Vector3(Mathf.Round(destinationVector.x), transform.position.y, Mathf.Round(destinationVector.z));
        else
            destinationVector = new Vector3(Mathf.Round(destinationVector.x), destinationVector.y, Mathf.Round(destinationVector.z));

        transform.position = Vector3.Lerp(transform.position, destinationVector, Time.deltaTime * elementSpeed);
    }
}
