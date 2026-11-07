using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NotebookItemsUI : MonoBehaviour
{
    [Header("Collection References")]
    [SerializeField] private TMP_Dropdown categoryDropdown;
    [SerializeField] private Transform itemListContent;
    [SerializeField] private Button itemButtonPrefab;

    [Header("Item Preview")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Button itemImageButton;
    [SerializeField] private TMP_Text itemTitle;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private TMP_Text itemContent;

    [Header("Image Zoom")]
    [SerializeField] private GameObject imageZoomOverlay;
    [SerializeField] private Image zoomImage;
    [SerializeField] private Button zoomCloseButton;
    [SerializeField] private Vector2 maximumZoomSize = new Vector2(1100f, 750f);

    private readonly List<Button> itemButtons = new List<Button>();

    private ItemOfInterestData.Category? selectedCategory;
    private ItemOfInterestData selectedItem;

    private void Awake()
    {
        if (itemImageButton == null && itemImage != null)
        {
            itemImageButton = itemImage.GetComponent<Button>();

            if (itemImageButton == null)
            {
                itemImageButton = itemImage.GetComponentInParent<Button>();
            }
        }
    }

    private void OnEnable()
    {
        if (NotebookCollection.Instance != null)
        {
            NotebookCollection.Instance.ItemCollected += HandleItemCollected;
        }

        if (categoryDropdown != null)
        {
            categoryDropdown.onValueChanged.AddListener(OnCategoryChanged);
        }

        if (itemImageButton != null)
        {
            itemImageButton.onClick.AddListener(OpenImageZoom);
        }
        else
        {
            Debug.LogError("Item Image Button reference is missing.");
        }

        if (zoomCloseButton != null)
        {
            zoomCloseButton.onClick.AddListener(CloseImageZoom);
        }

        if (NotebookCollection.Instance != null)
        {
            NotebookCollection.Instance.DynamiteCountChanged += UpdateDynamiteCount;
        }

        SetupDropdown();
        RefreshItemList();
        ClearPreview();

        if (imageZoomOverlay != null)
        {
            imageZoomOverlay.SetActive(false);
        }
        else
        {
            Debug.LogError("Image Zoom Overlay reference is missing.");
        }
    }

    private void OnDisable()
    {
        if (NotebookCollection.Instance != null)
        {
            NotebookCollection.Instance.ItemCollected -= HandleItemCollected;
        }

        if (categoryDropdown != null)
        {
            categoryDropdown.onValueChanged.RemoveListener(OnCategoryChanged);
        }

        if (itemImageButton != null)
        {
            itemImageButton.onClick.RemoveListener(OpenImageZoom);
        }

        if (zoomCloseButton != null)
        {
            zoomCloseButton.onClick.RemoveListener(CloseImageZoom);
        }

        if (NotebookCollection.Instance != null)
        {
            NotebookCollection.Instance.DynamiteCountChanged -= UpdateDynamiteCount;
        }
    }

    private void Update()
    {
        if (imageZoomOverlay != null &&
            imageZoomOverlay.activeSelf &&
            Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseImageZoom();
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

    private void HandleItemCollected(ItemOfInterestData item)
    {
        RefreshItemList();
    }

    private void RefreshItemList()
    {
        ClearItemButtons();

        if (NotebookCollection.Instance == null || itemListContent == null)
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
        if (itemButtonPrefab == null)
        {
            Debug.LogError("Item Button Prefab is missing.");
            return;
        }

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
            itemImage.preserveAspect = true;
            itemImage.enabled = item.ItemImage != null;
        }

        if (itemImageButton != null)
        {
            itemImageButton.interactable = item.ItemImage != null;
        }

        if (itemTitle != null)
        {
            if (item.ItemCategory == ItemOfInterestData.Category.Dynamite)
            {
                itemTitle.text = item.ItemTitle + " x" + NotebookCollection.Instance.DynamiteCount;
            }
            else
            {
                itemTitle.text = item.ItemTitle;
            }
        }

        if (itemDescription != null)
        {
            itemDescription.text = item.ItemDescription;
        }

        if (itemContent != null)
        {
            itemContent.text = item.ItemContent;
        }

        CloseImageZoom();
    }

    private void OpenImageZoom()
    {
        Debug.Log("Image zoom button clicked.");

        if (selectedItem == null)
        {
            Debug.LogWarning("No item is currently selected.");
            return;
        }

        if (selectedItem.ItemImage == null)
        {
            Debug.LogWarning("The selected item has no image assigned.");
            return;
        }

        if (imageZoomOverlay == null || zoomImage == null)
        {
            Debug.LogError("Image Zoom Overlay or Zoom Image reference is missing.");
            return;
        }

        zoomImage.sprite = selectedItem.ItemImage;
        zoomImage.preserveAspect = true;
        zoomImage.enabled = true;

        Rect spriteRect = selectedItem.ItemImage.rect;

        float scale = Mathf.Min(
            maximumZoomSize.x / spriteRect.width,
            maximumZoomSize.y / spriteRect.height,
            1f
        );

        RectTransform zoomRect = zoomImage.GetComponent<RectTransform>();

        zoomRect.sizeDelta = new Vector2(
            spriteRect.width * scale,
            spriteRect.height * scale
        );

        zoomRect.anchoredPosition = Vector2.zero;

        imageZoomOverlay.SetActive(true);

        Debug.Log("Image zoom overlay enabled.");
    }

    public void CloseImageZoom()
    {
        if (imageZoomOverlay != null)
        {
            imageZoomOverlay.SetActive(false);
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

        if (itemImageButton != null)
        {
            itemImageButton.interactable = false;
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

        CloseImageZoom();
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

    private void UpdateDynamiteCount(int count)
    {
        if (selectedItem != null &&
            selectedItem.ItemCategory == ItemOfInterestData.Category.Dynamite &&
            itemTitle != null)
        {
            itemTitle.text = selectedItem.ItemTitle + " x" + count;
        }
    }
}