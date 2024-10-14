using UnityEngine;
using System.Collections;

public class ShopExpand : MonoBehaviour
{
    // Reference to the canvas that you want to hide
    public GameObject ExpandedShop;

    // Reference to the animation component
    private Animation canvasAnimation;

    private void Start()
    {
        // Get the animation component attached to the canvas
        canvasAnimation = ExpandedShop.GetComponent<Animation>();
    }

    // Method to hide the canvas
    public void HideCanvas()
    {
        if (ExpandedShop != null)
        {
            ExpandedShop.SetActive(false); // This will deactivate the canvas
        }
    }

    public void ShowCanvas()
    {
        if (ExpandedShop != null)
        {
            // Play the animation
            canvasAnimation.Play();

            // Show the canvas after the animation has finished playing
            StartCoroutine(ShowCanvasAfterAnimation());
        }
    }

    private IEnumerator ShowCanvasAfterAnimation()
    {
        // Wait for the animation to finish playing
        yield return new WaitForSeconds(canvasAnimation.clip.length);

        // Show the canvas
        ExpandedShop.SetActive(true);
    }
}

