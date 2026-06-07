using UnityEngine;

public class FurniturePopup : MonoBehaviour
{
    public TextMesh text;
    public Transform lookAtCamera;

    [Header("Popup Child")]
    public GameObject popupChild;   // assign in Inspector

    [Header("Popup Material (URP)")]
    public Material popupMaterial;  // assign in Inspector, URP Lit Shader

    void Awake()
    {
        // Auto-assign TextMesh
        if (text == null)
            text = GetComponent<TextMesh>();

        // Deactivate child BEFORE anything runs
        Transform child = transform.Find("PaletteCanvas");
        if (child != null)
        {
            popupChild = child.gameObject;
            child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (gameObject.activeSelf && lookAtCamera != null)
        {
            transform.LookAt(transform.position + lookAtCamera.forward);
        }
    }

    public void SetText(Furniture furniture)
    {
        if (text == null)
        {
            Debug.LogError("FurniturePopup: TextMesh reference is missing!");
            return;
        }

        text.text =
            $"{furniture.furnitureName}\n" +
            $"Type: {furniture.furnitureType}\n" +
            $"Price: ${furniture.price}";

        if (popupChild != null)
            popupChild.SetActive(true);
    }

    // ---------------- Color Functions ----------------

    public void SetBlue()
    {
        SetColor(new Color(0f / 255f, 12f / 255f, 60f / 255f));
    }

    public void SetPurple()
    {
        SetColor(new Color(49f / 255f, 5f / 255f, 63f / 255f));
    }

    public void SetWhite()
    {
        SetColor(Color.white);
    }

    public void SetBrown()
    {
        SetColor(new Color(63f / 255f, 2f / 255f, 2f / 255f));
    }

    private void SetColor(Color color)
    {
        if (popupMaterial != null)
        {
            // ✅ Set Base Color for URP Lit Shader
            popupMaterial.SetColor("_BaseColor", color);
            
            // Optional: Also set the main color property for compatibility
            popupMaterial.color = color;
        }
        else
        {
            Debug.LogWarning("FurniturePopup: popupMaterial is not assigned!");
        }
    }

    // ---------------- Metallic Function ----------------

    /// <summary>
    /// Set the metallic value of the URP material (0 = non-metal, 1 = fully metallic)
    /// </summary>
    /// <param name="metallicValue">0 to 1</param>
    public void SetMetallic(float metallicValue)
    {
        if (popupMaterial != null)
        {
            metallicValue = Mathf.Clamp01(metallicValue);
            popupMaterial.SetFloat("_Metallic", metallicValue);
        }
        else
        {
            Debug.LogWarning("FurniturePopup: popupMaterial is not assigned!");
        }
    }
}