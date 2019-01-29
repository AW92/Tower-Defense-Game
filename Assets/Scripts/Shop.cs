using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void purchaseStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void purchaseAnotherTurret()
    {
        Debug.Log("Another Turret Selected");
        buildManager.SetTurretToBuild(buildManager.anotherTurretPrefab);
    }

}
