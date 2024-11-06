using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;
using TMPro;


//Scripted by Alva
public class DoubleMoneyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool over;

    [SerializeField] float time;
    [SerializeField] Counting counting;
    [SerializeField] float multiplierTimeout;
    [SerializeField] int doubleMoneyPrize;
    [SerializeField] Button Button;

    void Start()
    {
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > multiplierTimeout)
        {
            counting.multipierValue = 1;
        }

        if (counting.count >= doubleMoneyPrize)
        {
            // Enable button
            Button.interactable = true;
            Button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            Button.interactable = false;
            if (counting.multipierValue == 2)
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
                    counting.multipierValue = 2;
                    counting.count -= 100;
                    time = 0;
                    doubleMoneyPrize *= 2;
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
