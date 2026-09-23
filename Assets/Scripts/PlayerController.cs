using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]CharacterController characterController;
    [SerializeField] float speed;
    private void Update()
    {
        Vector3 movementVector = Vector3.zero;  

        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = 0;
        movementVector.z = Input.GetAxisRaw("Vertical");

        characterController.GetComponent<CharacterController>().Move(movementVector * Time.deltaTime * speed);

        //characterController.Move(movementVector * Time.deltaTime * speed);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray,out hitInfo, 100f))
        {
            Vector3 position = hitInfo.point;
            position.y = 0;    
            transform.LookAt(position);
        }

    }
}
