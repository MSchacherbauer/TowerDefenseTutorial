using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private readonly Vector3 buildOffset = new(0, 0.5f, 0);
    private BuildManager _buildManager;
    private Color _defaultColor;
    private Renderer _renderer;
    private GameObject _turret;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
        _buildManager = BuildManager.Instance;
    }

    private void OnMouseDown()
    {
        if (IsPointerOverGameObject()) return;
        if (_turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }
        var turretToBuild = _buildManager.GetTurretToBuild();
        if (turretToBuild != null)
        {
            var transform1 = transform;
            _turret = Instantiate(turretToBuild, transform1.position + buildOffset, transform1.rotation);
        }
        else
        {
            Debug.Log("No Turret selected");
        }
    }


    private void OnMouseEnter()
    {
        if (IsPointerOverGameObject()) return;
        if (_buildManager.GetTurretToBuild() == null) return;
        _renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _defaultColor;
    }

    private static bool IsPointerOverGameObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}