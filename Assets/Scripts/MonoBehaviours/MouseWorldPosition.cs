using UnityEngine;
using UnityEngine.InputSystem;

public class MouseWorldPosition : MonoBehaviour
{
    
    public static MouseWorldPosition I { get; private set; }

    void Awake() {
        I = this;
    }
    
    public Vector3 GetPosition() {
        Ray mouseCamRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(mouseCamRay, out float dist)) {
            return mouseCamRay.GetPoint(dist);
        }
        else {
            return Vector3.zero;
        }
    }
}