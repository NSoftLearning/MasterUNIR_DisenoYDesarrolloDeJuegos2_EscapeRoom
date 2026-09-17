using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float velocidadCorrer = 9f;
    public float gravedad = -9.81f;

    [Header("Cámara")]
    public Transform camara;
    public float sensibilidadMouse = 2f;
    public float limiteCamara = 80f;

    private CharacterController controller;
    private float rotacionX = 0f;
    private float velocidadVertical = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Mover();
        Mirar();
    }

    void Mover()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movimiento = transform.right * x + transform.forward * z;

        float velocidadActual = Input.GetKey(KeyCode.LeftShift)
            ? velocidadCorrer
            : velocidad;

        if (controller.isGrounded)
        {
            velocidadVertical = -2f;
        }
        else
        {
            velocidadVertical += gravedad * Time.deltaTime;
        }

        movimiento *= velocidadActual;
        movimiento.y = velocidadVertical;

        controller.Move(movimiento * Time.deltaTime);
    }

    void Mirar()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse;

        transform.Rotate(Vector3.up * mouseX);

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -limiteCamara, limiteCamara);

        camara.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    }
}