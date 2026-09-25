using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]CharacterController characterController;
    [SerializeField] float speed;
    [SerializeField] LayerMask enemyLayer;
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

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Vector3 position = hitInfo.point;
            position.y = transform.position.y;

            transform.LookAt(position);
        }


        if (Input.GetMouseButtonDown(0))
        {
            Ray fireRay = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit enemyInfo;

            Debug.DrawRay(fireRay.origin, fireRay.direction * 10, Color.yellow, 10f);

            if (Physics.Raycast(fireRay.origin, fireRay.direction,out enemyInfo, 100f, enemyLayer))
            {
                Debug.Log(enemyInfo.transform.gameObject.name);
            }
            else
            {
                Debug.Log("Mi loco dele pafuera");
            }
        }

    }
}
