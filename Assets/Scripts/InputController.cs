using System;
using UnityEngine;

public class InputController : MonoBehaviour, IInputService
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public int GetSelectedTag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out ITag tagComponent))
                {
                    return tagComponent.Number;
                }
            }
        }

        return -1;
    }
}