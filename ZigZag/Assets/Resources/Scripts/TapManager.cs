using UnityEngine;

public class TapManager : MonoBehaviour
{
    [SerializeField] private GameObject StartText;

    void OnMouseDown()
    {
        if (Player.isLife)
        {
            // изменение направления
            Player.isRightDim = !Player.isRightDim;
        }
        else
        {
            if (!Player.isDie)
            {
                // начало движения
                StartText.SetActive(false);
                Player.isLife = true;
            }
        }
    }
}