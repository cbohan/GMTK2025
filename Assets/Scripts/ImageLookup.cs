using UnityEngine;

[CreateAssetMenu(fileName = "ImageLookup", menuName = "Scriptable Objects/ImageLookup")]
public class ImageLookup : ScriptableObject
{
    [Header("Race Textures")]
    public Texture HoneyRace;
    public Texture LoopRace;
    public Texture OreoRace;
    public Texture AppleRace;

    [Header("Non Race Textures")]
    public Texture Honey;
    public Texture Loop;
    public Texture Oreo;
    public Texture Apple;

    public Texture GetRaceTexture(CatImage image)
    {
        switch (image)
        {
            case CatImage.Honey:
                return HoneyRace;
            case CatImage.Loop:
                return LoopRace;
            case CatImage.Oreo:
                return OreoRace;
            case CatImage.Apple:
            default:
                return AppleRace;
        }
    }

    public Texture GetNonRaceTexture(CatImage image)
    {
        switch (image)
        {
            case CatImage.Honey:
                return Honey;
            case CatImage.Loop:
                return Loop;
            case CatImage.Oreo:
                return Oreo;
            case CatImage.Apple:
            default:
                return Apple;
        }
    }
}
