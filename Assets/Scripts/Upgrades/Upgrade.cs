using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] protected float activeTime = 0;
    protected AudioSource audioSource;
    protected SpriteRenderer spriteRenderer;
    protected bool active = true;

    void Start()
    {
        active = true;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
