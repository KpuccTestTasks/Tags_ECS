using System.Collections;
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
        UpdatePosition(posX, posY, false);
    }
    
    public void UpdatePosition(int x, int y, bool animated)
    {
        float newX = x - 1.5f;
        float newY = 1.5f - y;

        if (animated)
            StartCoroutine(MoveToNewPosition(new Vector3(newX, newY, 0f)));
        else
            transform.localPosition = new Vector3(newX, newY, 0f);
    }

    private IEnumerator MoveToNewPosition(Vector3 newPosition)
    {
        var startPos = transform.localPosition;

        float passedTime = 0f;
        float moveTime = .33f;

        while (moveTime > passedTime)
        {
            passedTime += Time.deltaTime;

            if (passedTime > moveTime)
                passedTime = moveTime;
            
            transform.localPosition = Vector3.LerpUnclamped(startPos, newPosition, 1 - (moveTime - passedTime) / moveTime);

            yield return null;
        }
    }
}