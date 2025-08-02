using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ImageLookup", menuName = "Scriptable Objects/ImageLookup")]
public class ImageLookup : ScriptableObject
{
    [Header("Race Textures")]
    public Texture HoneyRace;
    public Texture HoneyRaceFront;
    public Texture HoneyRaceBack;
    public Texture LoopRace;
    public Texture LoopRaceFront;
    public Texture LoopRaceBack;
    public Texture OreoRace;
    public Texture OreoRaceFront;
    public Texture OreoRaceBack;
    public Texture AppleRace;
    public Texture AppleRaceFront;
    public Texture AppleRaceBack;

    [Header("Non Race Textures")]
    public Texture Honey;
    public Texture Loop;
    public Texture Oreo;
    public Texture Apple;

    [Header("Level-Up Textures")]
    public Texture Honey1;
    public Texture Honey2;
    public Texture Honey3;
    public Texture Honey4;
    public Texture Loop1;
    public Texture Loop2;
    public Texture Loop3;
    public Texture Loop4;
    public Texture Oreo1;
    public Texture Oreo2;
    public Texture Oreo3;
    public Texture Oreo4;
    public Texture Apple1;
    public Texture Apple2;
    public Texture Apple3;
    public Texture Apple4;

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

    public Texture GetRaceTextureFront(CatImage image)
    {
        switch (image)
        {
            case CatImage.Honey:
                return HoneyRaceFront;
            case CatImage.Loop:
                return LoopRaceFront;
            case CatImage.Oreo:
                return OreoRaceFront;
            case CatImage.Apple:
            default:
                return AppleRaceFront;
        }
    }

    public Texture GetRaceTextureBack(CatImage image)
    {
        switch (image)
        {
            case CatImage.Honey:
                return HoneyRaceBack;
            case CatImage.Loop:
                return LoopRaceBack;
            case CatImage.Oreo:
                return OreoRaceBack;
            case CatImage.Apple:
            default:
                return AppleRaceBack;
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

    public void GetLevelTexture(CatImage image, RawImage[] textures)
    {
        switch (image)
        {
            case CatImage.Honey:
                textures[0].texture = Honey1;
                textures[1].texture = Honey2;
                textures[2].texture = Honey3;
                textures[3].texture = Honey4;
                break;
            case CatImage.Loop:
                textures[0].texture = Loop1;
                textures[1].texture = Loop2;
                textures[2].texture = Loop3;
                textures[3].texture = Loop4;
                break;
            case CatImage.Oreo:
                textures[0].texture = Oreo1;
                textures[1].texture = Oreo2;
                textures[2].texture = Oreo3;
                textures[3].texture = Oreo4;
                break;
            case CatImage.Apple:
            default:
                textures[0].texture = Apple1;
                textures[1].texture = Apple2;
                textures[2].texture = Apple3;
                textures[3].texture = Apple4;
                break;
        }
    }
}
