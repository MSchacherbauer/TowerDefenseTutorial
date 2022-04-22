using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
    }

    public void SelectStandardTurret()
    {
        _buildManager.SetTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        _buildManager.SetTurretToBuild(missileLauncher);
    }
    
    public void SelectLaserBeamer()
    {
        _buildManager.SetTurretToBuild(laserBeamer);
    }
}