using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] private WeaponSO currentWeapon;
    [SerializeField] private WeaponSO primaryWeapon;
    [SerializeField] private WeaponSO secondaryWeapon;
    [HideInInspector]
    [SerializeField] private float shootTimer;
    [SerializeField] private int primaryBullet;
    private void Update()
    {
        Movement();
        Mouse();
        Shooting();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentWeapon == primaryWeapon)
            {
                currentWeapon = secondaryWeapon;
            }
            else if (currentWeapon == secondaryWeapon)
            {
                currentWeapon = primaryWeapon;
            }
        }

        if (shootTimer > 0)
        {

            shootTimer -= Time.deltaTime;
        }
    }

    private void Movement()
    {
        Vector3 movementVector = Vector3.zero;

        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = 0;
        movementVector.z = Input.GetAxisRaw("Vertical");

        characterController.GetComponent<CharacterController>().Move(movementVector * Time.deltaTime * speed);

        //characterController.Move(movementVector * Time.deltaTime * speed);
    }
    private void Shooting()
    {
        if (currentWeapon.ShootingMode == ShootingMode.Automatic)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (!canShoot) { return; }

                if (currentWeapon == primaryWeapon & primaryBullet <= 0) { return; }
                if (currentWeapon == primaryWeapon)
                {
                    primaryBullet -= 1;
                }

                for (int i = 0; i < currentWeapon.projectilAmount; i++)
                {
                    float angle = Random.Range(-currentWeapon.scatter, currentWeapon.scatter);
                    Quaternion angleRotation = Quaternion.AngleAxis(angle, Vector3.up);

                    Ray fireRay = new Ray(transform.position + Vector3.up, angleRotation * transform.forward);
                    RaycastHit enemyInfo;

                    Debug.DrawRay(fireRay.origin, fireRay.direction * 10, Color.yellow, 10f);

                    if (Physics.Raycast(fireRay.origin, fireRay.direction, out enemyInfo, 100f, enemyLayer))
                    {
                        Debug.Log(enemyInfo.transform.gameObject.name);
                    }
                    else
                    {
                        Debug.Log("Mi loco dele pafuera");
                    }
                }
                canShoot = false;
                StartCoroutine(ResetAttack());

            }
        }

        else if (currentWeapon.ShootingMode == ShootingMode.Semiautomatic)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (!canShoot) { return; }
                if (currentWeapon == primaryWeapon & primaryBullet <= 0) { return; }
                {
                    primaryBullet -= 1;
                }

                for (int i = 0; i < currentWeapon.projectilAmount; i++)
                {
                    float angle = Random.Range(-currentWeapon.scatter, currentWeapon.scatter);
                    Quaternion angleRotation = Quaternion.AngleAxis(angle, Vector3.up);

                    Ray fireRay = new Ray(transform.position + Vector3.up, angleRotation * transform.forward);
                    RaycastHit enemyInfo;

                    Debug.DrawRay(fireRay.origin, fireRay.direction * 10, Color.yellow, 10f);

                    if (Physics.Raycast(fireRay.origin, fireRay.direction, out enemyInfo, 100f, enemyLayer))
                    {
                        Debug.Log(enemyInfo.transform.gameObject.name);
                    }
                    else
                    {
                        Debug.Log("Mi loco dele pafuera");
                    }
                }
                canShoot = false;
                StartCoroutine(ResetAttack());


            }
        }
    }
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(currentWeapon.shootingSpeed);
        canShoot = true;
    }

    public bool canShoot = true;
    private void Mouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Vector3 position = hitInfo.point;
            position.y = transform.position.y;

            transform.LookAt(position);
        }
    }
}
