using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Renderer _renderer;
    private Color _defaultColor;
    private GameObject _turret;
    private Vector3 buildOffset = new Vector3(0, 0.5f, 0);

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = hoverColor;
    }

    private void OnMouseDown()
    {
        if (_turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }
        else
        {
            var turretToBuild = BuildManager.Instance.GetTurretToBuild();
            if (turretToBuild != null)
            {
                _turret=Instantiate(turretToBuild, transform.position+buildOffset,transform.rotation);
            }
            else Debug.Log("No Turret selected");
        }
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _defaultColor;
    }
}