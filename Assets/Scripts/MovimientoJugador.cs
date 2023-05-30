using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 1;
    [SerializeField] private float estamina = 100;
    [SerializeField] private float estaminaMaxima = 100;
    [SerializeField] private float aumentoVelocidad = 1.5f;

    private float modificadorVelocidad;
    private Rigidbody rb;

    private Slider estaminaSlider;

    // Variables de animacion
    [SerializeField] private Animator animator;
    private bool enMovimiento;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        modificadorVelocidad = 1;
        estaminaSlider = GameObject.Find("Estamina slider").GetComponent<Slider>();
        estaminaSlider.value = estamina;
    }

    // Update is called once per frame
    void Update()
    {
        MoverJugador();
        ActualizarUI();
    }


    //Rota la posision del jugador segun los inputs y lo mueve hacia adelante
    private void MoverJugador()
    {
        //Actualizacion de parametros del animator
        animator.SetBool("bool_moviendose", enMovimiento);
        animator.SetFloat("float_Speed", rb.velocity.magnitude);

        //Lectura de la entrada
        Vector2 valoresEjes = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (valoresEjes.x == 0 && valoresEjes.y == 0)
        {
            rb.velocity = Vector3.zero;
            modificadorVelocidad = 1;
            enMovimiento = false;
            return;
        }
        enMovimiento = true;

        // Si hay estamina, es posible aumentar la velocidad de movimiento
        if (estamina > 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
                modificadorVelocidad = aumentoVelocidad;
            if (Input.GetKeyUp(KeyCode.LeftControl))
                modificadorVelocidad = 1;

            // La estamina se reduce con el paso del tiempo
            if (Input.GetKey(KeyCode.LeftControl))
            {
                estamina -= Time.deltaTime;
                // Debug.Log("Estamina restante: " + estamina);
            }
        }
        else
        {
            Debug.Log("No queda estamina");
            modificadorVelocidad = 1;
        }

        //Rotacion del jugador
        transform.forward = new Vector3(valoresEjes.x, 0, valoresEjes.y);

        // Movimiento del jugador
        rb.velocity = modificadorVelocidad * velocidadMovimiento * transform.forward;
    }

    public void RecuperarEstamina(float estaminaRecuperada)
    {
        estamina += estaminaRecuperada;
        if (estamina > estaminaMaxima)
            estamina = estaminaMaxima;
        Debug.Log("Se recupero " + estaminaRecuperada + " puntos de estamina");
    }

    private void ActualizarUI()
    {
        estaminaSlider.value = estamina;
    }

    public void FrenarMovimiento()
    {
        rb.velocity = Vector3.zero;
    }
}
