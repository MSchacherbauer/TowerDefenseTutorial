using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private TurretBlueprint _turretToBuild;
    public bool CanBuild => _turretToBuild != null;
    public bool HasMoney => PlayerStats.Money >= _turretToBuild.cost;
    public GameObject buildEffectPrefab;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SetTurretToBuild(TurretBlueprint turret)
    {
        _turretToBuild = turret;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < _turretToBuild.cost) return;
        node.turret = Instantiate(_turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        var buildEffect = Instantiate(buildEffectPrefab, node.GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5);
        PlayerStats.Money -= _turretToBuild.cost;
        Debug.Log("Turret built! Money left: " + PlayerStats.Money);
    }
}