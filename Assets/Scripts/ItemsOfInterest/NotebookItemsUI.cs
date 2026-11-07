using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotebookItemsUI : MonoBehaviour
{
    [Header("Collection References")]
    [SerializeField] private TMP_Dropdown categoryDropdown;
    [SerializeField] private Transform itemListContent;
    [SerializeField] private Button itemButtonPrefab;

    [Header("Item Preview")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemTitle;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private TMP_Text itemContent;

    private readonly List<Button> itemButtons = new List<Button>();

    private ItemOfInterestData.Category? selectedCategory;
    private ItemOfInterestData selectedItem;

    private void OnEnable()
    {
        if (NotebookCollection.Instance != null)
        {
            NotebookCollection.Instance.ItemCollected += OnItemCollected;
        }

        if (categoryDropdown != null)
        {
            categoryDropdown.onValueChanged.AddListener(OnCategoryChanged);
        }

        SetupDropdown();
        RefreshItemList();
        ClearPreview();
    }

    private void OnDisable()
    {
        if (NotebookCollection.Instance != null)
        {
            NotebookCollection.Instance.ItemCollected -= OnItemCollected;
        }

        if (categoryDropdown != null)
        {
            categoryDropdown.onValueChanged.RemoveListener(OnCategoryChanged);
        }
    }

    private void SetupDropdown()
    {
        if (categoryDropdown == null)
        {
            return;
        }

        categoryDropdown.ClearOptions();

        List<string> options = new List<string>
        {
            "All Items"
        };

        foreach (ItemOfInterestData.Category category in
                 System.Enum.GetValues(typeof(ItemOfInterestData.Category)))
        {
            options.Add(category.ToString());
        }

        categoryDropdown.AddOptions(options);
        categoryDropdown.value = 0;
        categoryDropdown.RefreshShownValue();

        selectedCategory = null;
    }

    private void OnCategoryChanged(int index)
    {
        if (index == 0)
        {
            selectedCategory = null;
        }
        else
        {
            selectedCategory = (ItemOfInterestData.Category)(index - 1);
        }

        RefreshItemList();
        ClearPreview();
    }

    private void OnItemCollected(ItemOfInterestData item)
    {
        RefreshItemList();
    }

    private void RefreshItemList()
    {
        ClearItemButtons();

        if (NotebookCollection.Instance == null)
        {
            return;
        }

        foreach (ItemOfInterestData item in
                 NotebookCollection.Instance.CollectedItems)
        {
            if (selectedCategory.HasValue &&
                item.ItemCategory != selectedCategory.Value)
            {
                continue;
            }

            CreateItemButton(item);
        }
    }

    private void CreateItemButton(ItemOfInterestData item)
    {
        Button button = Instantiate(itemButtonPrefab, itemListContent);

        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        if (buttonText != null)
        {
            buttonText.text = item.ItemTitle;
        }

        button.onClick.AddListener(() => SelectItem(item));

        itemButtons.Add(button);
    }

    private void SelectItem(ItemOfInterestData item)
    {
        selectedItem = item;

        if (itemImage != null)
        {
            itemImage.sprite = item.ItemImage;
            itemImage.enabled = item.ItemImage != null;
        }

        if (itemTitle != null)
        {
            itemTitle.text = item.ItemTitle;
        }

        if (itemDescription != null)
        {
            itemDescription.text = item.ItemDescription;
        }

        if (itemContent != null)
        {
            itemContent.text = item.ItemContent;
        }
    }

    private void ClearPreview()
    {
        selectedItem = null;

        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }

        if (itemTitle != null)
        {
            itemTitle.text = "";
        }

        if (itemDescription != null)
        {
            itemDescription.text = "";
        }

        if (itemContent != null)
        {
            itemContent.text = "";
        }
    }

    private void ClearItemButtons()
    {
        foreach (Button button in itemButtons)
        {
            if (button != null)
            {
                Destroy(button.gameObject);
            }
        }

        itemButtons.Clear();
    }
}