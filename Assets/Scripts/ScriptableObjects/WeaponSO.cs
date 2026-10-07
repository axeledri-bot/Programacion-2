using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public ShootingMode ShootingMode;
    public int projectilAmount;
    public float shootingSpeed;
    public float scatter;
}

public enum ShootingMode
{
    Automatic,
    Semiautomatic,
}
