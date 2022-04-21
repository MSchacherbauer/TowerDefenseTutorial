using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    [Header("Optional")]
    [FormerlySerializedAs("_turret")]
    public GameObject turret;
    private readonly Vector3 _buildOffset = new(0, 0.5f, 0);
    private BuildManager _buildManager;
    private Color _defaultColor;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
        _buildManager = BuildManager.Instance;
    }

    private void OnMouseDown()
    {
        if (IsPointerOverGameObject()) return;
        if (!_buildManager.CanBuild) return;
        if (turret != null) return;
        _buildManager.BuildTurretOn(this);
    }


    private void OnMouseEnter()
    {
        if (IsPointerOverGameObject()) return;
        if (!_buildManager.CanBuild) return;
        if (turret != null) return;
        _renderer.material.color = _buildManager.HasMoney ? hoverColor : Color.red;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _defaultColor;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + _buildOffset;
    }

    private static bool IsPointerOverGameObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject + " hit GroundPlane");
    }
}