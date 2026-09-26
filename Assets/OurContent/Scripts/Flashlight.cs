using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public KeyCode toggle = KeyCode.F;
    public Light flashlightLight;

    private void Start()
    {
        flashlightLight.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggle))
        {
            flashlightLight.enabled = !flashlightLight.enabled;
        }
    }
}
