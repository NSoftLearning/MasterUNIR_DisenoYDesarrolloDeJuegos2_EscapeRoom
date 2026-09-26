using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public KeyCode toggle;
    public Light flashlightLight;

    private void Start()
    {
        flashlightLight.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightLight.enabled = !flashlightLight.enabled;
        }
    }
}
