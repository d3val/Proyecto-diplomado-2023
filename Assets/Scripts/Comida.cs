using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida
{
    public float energia;
    public Sprite sprite;
    public float tiempoDeConsumo;

    public Comida(float energiaProvista, float tiempoParaComerlo, Sprite spriteDelAlimento)
    {
        energia = energiaProvista;
        sprite = spriteDelAlimento;
        tiempoDeConsumo = tiempoParaComerlo;
    }
}
