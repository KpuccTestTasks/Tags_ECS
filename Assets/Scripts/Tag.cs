using TMPro;
using UnityEngine;

public class Tag : MonoBehaviour, ITag
{
    public int Number { get; private set; }
    
    [SerializeField] private TextMeshPro numberText;

    public void SetupTag(int number, int posX, int posY)
    {
        Number = number;
        numberText.text = (number + 1).ToString();
        UpdatePosition(posX, posY);
    }
    
    public void UpdatePosition(int x, int y)
    {
        float newX = (x - 1) / 4f - 0.125f;
        float newY = (1 - y) / 4f + 0.125f;

        transform.localPosition = new Vector3(newX, newY, 0f);
    }
}