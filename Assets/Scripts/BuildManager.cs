using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject standardTurretPrefab;
    [FormerlySerializedAs("anotherTurretPrefab")]
    public GameObject missileLauncherPrefab;
    private GameObject _turretToBuild;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        _turretToBuild = turret;
    }

    public GameObject GetTurretToBuild()
    {
        return _turretToBuild;
    }
}