using UnityEngine;

[CreateAssetMenu(fileName = "New Item of Interest", menuName = "Game/Items of Interest")]
public class ItemOfInterestData : ScriptableObject
{
    public enum Category
    {
        Map,
        Newspaper,
        Document,
        Letter,
        Code,
        Photograph,
        Dynamite,
        Other
    }

    [Header("Identification")]
    [SerializeField] private string itemID;
    [SerializeField] private string itemTitle;
    [SerializeField] private Category category;

    [Header("Content")]
    [SerializeField] private Sprite itemImage;
    [TextArea(5, 20)]
    [SerializeField] private string itemDescription;
    [TextArea(5, 30)]
    [SerializeField] private string itemContent;

    public string ItemID => itemID;
    public string ItemTitle => itemTitle;
    public Category ItemCategory => category;
    public Sprite ItemImage => itemImage;
    public string ItemDescription => itemDescription;
    public string ItemContent => itemContent;
}