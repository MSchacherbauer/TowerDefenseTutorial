using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
    }

    public void PurchaseStandardTurret()
    {
        _buildManager.SetTurretToBuild(_buildManager.standardTurretPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        _buildManager.SetTurretToBuild(_buildManager.missileLauncherPrefab);
    }
}