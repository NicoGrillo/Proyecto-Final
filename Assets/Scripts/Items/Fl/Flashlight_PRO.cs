﻿using UnityEngine;
using System.Collections;




public class Flashlight_PRO : MonoBehaviour
{
    [Space(10)]
    [SerializeField()] GameObject Lights; // all light effects and spotlight
    [SerializeField()] AudioSource switch_sound; // audio of the switcher
    [SerializeField()] ParticleSystem dust_particles; // dust particles
    [SerializeField()][Range(1, 30)] int maxIntensity = 20;



    private Light spotlight;
    private Material ambient_light_material;
    private Color ambient_mat_color;
    private bool is_enabled = false;









    // Use this for initialization
    void Awake()
    {
        // cache components
        spotlight = Lights.transform.Find("Spotlight").GetComponent<Light>();
        ambient_light_material = Lights.transform.Find("ambient").GetComponent<Renderer>().material;
        ambient_mat_color = ambient_light_material.GetColor("_TintColor");
    }






    /// <summary>
    /// changes the intensivity of lights from 0 to 100.
    /// call this from other scripts.
    /// </summary>
    public void Change_Intensivity(float percentage)
    {
        percentage = Mathf.Clamp(percentage, 0, 100);

        spotlight.intensity = (maxIntensity * percentage) / 100;

        ambient_light_material.SetColor("_TintColor", new Color(ambient_mat_color.r, ambient_mat_color.g, ambient_mat_color.b, percentage / 2000));
    }




    /// <summary>
    /// switch current state  ON / OFF.
    /// call this from other scripts.
    /// </summary>
    public void Switch()
    {
        is_enabled = !is_enabled;

        Lights.SetActive(is_enabled);

        if (switch_sound != null)
            switch_sound.Play();
    }


    /// <summary>
    /// switch, without sound, current state  ON / OFF.
    /// call this from other scripts.
    /// </summary>
    public void SwitchFailure()
    {
        is_enabled = !is_enabled;

        Lights.SetActive(is_enabled);
    }


    /// <summary>
    /// enables the particles.
    /// </summary>
    public void Enable_Particles(bool value)
    {
        if (dust_particles != null)
        {
            if (value)
            {
                dust_particles.gameObject.SetActive(true);
                dust_particles.Play();
            }
            else
            {
                dust_particles.Stop();
                dust_particles.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        if (switch_sound != null)
            switch_sound.Play();
    }

    private void OnDisable()
    {
        if (switch_sound != null)
            switch_sound.Play();
    }

}
