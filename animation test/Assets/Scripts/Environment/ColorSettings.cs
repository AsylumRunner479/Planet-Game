using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorSettings : ScriptableObject
{
    public Gradient gradient;
    public Material planetMaterial;
    public BiomeColourSettings biomeColourSettings;
    public Gradient oceanColour;
    public GradientAlphaKey[] alphakey;
    public GradientColorKey[] colorkey;
    public void Start()
    {
        alphakey = new GradientAlphaKey[0];
        alphakey[0].alpha = 0.5f;
        colorkey = new GradientColorKey[0];
        colorkey[0].color = Color.blue;
        oceanColour.SetKeys(colorkey, alphakey);
    }
    [System.Serializable]
    public class BiomeColourSettings
    {
        public Biome[] biomes;
        public NoiseSettings noise;
        public float noiseOffset;
        public float noiseStrength;
        [Range(0,1)]
        public float blendAmount;
        [System.Serializable]
        public class Biome
        {
            public Gradient gradient;
            
            public Color tint;
            [Range(0,1)]
            public float startHeight;
            [Range(0, 1)]
            public float tintPercent;
        }
    }
}
