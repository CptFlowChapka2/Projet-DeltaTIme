using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grabber[] grabbers;
    public Hex[,] hexes = new Hex[25, 25];
    public Material popupBaseMat;
    public LvlInfos LvlInfos;
    public SoundManager soundManager;
}
