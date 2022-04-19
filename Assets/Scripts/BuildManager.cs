using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject standardTurretPrefab;

    private GameObject _turretToBuild;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return _turretToBuild;
    }
}