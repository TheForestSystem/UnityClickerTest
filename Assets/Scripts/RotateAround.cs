using Unity.VisualScripting;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * rotationSpeed);
    }
}
