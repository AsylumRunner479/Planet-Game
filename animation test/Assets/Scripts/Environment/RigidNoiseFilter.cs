﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    NoiseSettings settings;
    Noise noise = new Noise();

    public RigidNoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }
    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        //float noiseValue = (noise.Evaluate(point * settings.roughness + settings.centre)  + 1) * 0.5f;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;
        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier);
            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }
        noiseValue = noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }
}
