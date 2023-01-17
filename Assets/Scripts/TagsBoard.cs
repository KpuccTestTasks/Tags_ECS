using UnityEngine;

public class TagsBoard : MonoBehaviour, ITagViewService
{
    [SerializeField] private Tag[] tags;

    public ITag GetTagView(int number)
    {
        return tags[number];
    }
}