using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FurnitureLookDetector : MonoBehaviour
{
    public NearFarInteractor nearFarInteractorRight;

    // A button (right controller)
    public InputActionProperty aButtonAction;

    private FurniturePopup activePopup;

    void OnEnable()
    {
        aButtonAction.action.Enable();
    }

    void OnDisable()
    {
        aButtonAction.action.Disable();
    }

    void Update()
    {
        // Only respond when A button is pressed
        if (!aButtonAction.action.WasPressedThisFrame())
            return;

        HandleFurnitureInteraction();
    }
    public void Celebrate()
    {
        Debug.Log("Yooo");
    }
    void HandleFurnitureInteraction()
    {
        if (nearFarInteractorRight == null) return;

        Collider collider = nearFarInteractorRight.collider;
        if (collider == null) return;

        Transform furnitureRoot = collider.transform.parent;
        if (furnitureRoot == null) return;

        if (!furnitureRoot.CompareTag("Furniture")) return;

        Furniture furniture = furnitureRoot.GetComponent<Furniture>();
        FurniturePopup popup = furnitureRoot.GetComponentInChildren<FurniturePopup>(true);

        if (furniture == null || popup == null) return;

        //  TOGGLE LOGIC
        if (activePopup == popup)
        {
            // A pressed again → hide
            popup.gameObject.SetActive(false);
            activePopup = null;
            return;
        }

        // Hide previous popup if another furniture is selected
        if (activePopup != null)
            activePopup.gameObject.SetActive(false);

        // Show new popup
            popup.lookAtCamera = transform;
            popup.SetText(furniture);
            popup.gameObject.SetActive(true);
            activePopup = popup;
    }

}
