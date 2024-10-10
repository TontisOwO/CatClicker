using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] Color originalColor;
    [SerializeField] Color slightlyDarker = new Color (0.1f, 0.1f, 0.1f, 0f);

    private void Awake()
    {
        Image = GetComponent<Image>();
        originalColor = Image.color;
    }
    private void OnMouseOver()
    {
        Image.color = originalColor - slightlyDarker;
        Debug.Log("bullshit, do not get called you fucking bi-");
    }
}
