using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;
using TMPro;

//Scripted by Alva

public class AutoClickerButton1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool over;

    [SerializeField] float time;
    [SerializeField] Counting counting;
    [SerializeField] float autoclickingTimeout;
    [SerializeField] int autoclickPrize;
    [SerializeField] Button Button;

    void Start()
    {
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > autoclickingTimeout)
        {
            counting.autoclicking = false;
        }

        if (counting.count >= autoclickPrize)
        {
            // Enable button
            Button.interactable = true;
            Button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            Button.interactable = false;
            if (counting.autoclicking)
            {
                // Highlight button
                Button.GetComponent<Image>().color = Color.red;
            }
            else
            {
                // Disable button
                Button.GetComponent<Image>().color = Color.gray;
            }
        }
        {
            if (over && Input.GetKeyDown(KeyCode.Mouse0))
            {
                // If button is enabled
                if (Button.interactable)
                {
                    // Enable double money
                    counting.autoclicking = true;
                    counting.count -= 100;
                    time = 0;
                    autoclickPrize *= 2;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse is over");
        over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        over = false;
    }


}
