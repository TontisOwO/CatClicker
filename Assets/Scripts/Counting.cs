using UnityEngine;

[CreateAssetMenu(fileName = "Counting", menuName = "Scriptable Objects/Counting")]
public class Counting : ScriptableObject
{
    public float count;
    public int displayedNumber;
    public float multipierValue = 1;
    public bool autoclicking = false;
}
