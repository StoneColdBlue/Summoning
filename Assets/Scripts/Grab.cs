using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{

    [SerializeField] Transform CamStartPos;
    [SerializeField] Transform CamFinalPos;
    [SerializeField] Transform CamHolder;

    [SerializeField] Renderer GhostOne;
    [SerializeField] Renderer GhostTwo;
    [SerializeField] Renderer GhostThree;

    [SerializeField] Material HighlightOne;
    [SerializeField] Material HighlightTwo;
    [SerializeField] Material HighlightThree;

    [SerializeField] Combat board;

    Dictionary<int, string> Cards = new Dictionary<int, string>()
    {
        { 0, "Dragon" },
        { 1, "Demon" },
        { 2, "Knight" },
        { 3, "Archer" },
        { 4, "Wizard" }
    };

    bool IsHoldingMouse;

    Image HeldCard;

    void Start()
    {

    }

    void Update()
    {
        Image[] cards = GetComponentsInChildren<Image>();
        Transform[] transforms = GetComponentsInChildren<Transform>();
        Vector3 pos;
        Rect rect, bounds;
        Vector2 lowerBounds, size;
        IsHoldingMouse = Input.GetMouseButton(0);
        bool holdingCard = false;
        foreach (Image card in cards)
        {
            if (card.sprite == null)
            {
                System.Random random = new System.Random();
                int dicIndex = random.Next(0, Cards.Count);
                string imageName = Cards[dicIndex];
                // Add below with images
                //card.sprite = Resources.Load<Sprite>($"Images/{imageName}");
            }
            pos = card.transform.position;
            rect = card.rectTransform.rect;
            lowerBounds = new Vector2(pos.x - rect.width / 2f, pos.y - rect.height / 2f);
            size = new Vector2(rect.width, rect.height);
            bounds = new Rect(lowerBounds, size);
            if (bounds.Contains(Input.mousePosition)) card.enabled = (IsHoldingMouse || bounds.Contains(Input.mousePosition)) && !holdingCard;
            else card.enabled = !IsHoldingMouse && !holdingCard;
            if (IsHoldingMouse && bounds.Contains(Input.mousePosition) && !holdingCard)
            {
                HeldCard = card;
                holdingCard = true;
                card.transform.position = Input.mousePosition;
                MoveCam();
                GhostOne.enabled = true;
                GhostTwo.enabled = true;
                GhostThree.enabled = true;
            }
            else if (!IsHoldingMouse)
            {
                Place();
                foreach (Transform transform in transforms)
                {
                    if (transform.name == card.name + " Slot")
                    {
                        card.transform.position = transform.position;
                        break;
                    }
                }
                MoveCam();
                HeldCard = null;
                GhostOne.enabled = false;
                GhostTwo.enabled = false;
                GhostThree.enabled = false;
            }
        }

    }

    void MoveCam()
    {
        float speed = 8f * Time.deltaTime;
        if (IsHoldingMouse) CamHolder.Translate(0, (CamFinalPos.position.y - CamHolder.position.y) * speed, (CamFinalPos.position.z - CamHolder.position.z) * speed);
        else CamHolder.Translate(0, (CamStartPos.position.y - CamHolder.position.y) * 0.25f * speed, (CamStartPos.position.z - CamHolder.position.z) * 0.25f * speed);
    }

    void Place()
    {
        float widthScale = ScaleUI.WidthScale;
        float heightScale = ScaleUI.HeightScale;
        Rect rect, bounds1, bounds2, bounds3;
        Vector2 screenSlot1 = new Vector2(416.5f * widthScale, 206.5f * heightScale);
        Vector2 screenSlot2 = new Vector2(480.5f * widthScale, 206.5f * heightScale);
        Vector2 screenSlot3 = new Vector2(544.5f * widthScale, 206.5f * heightScale);
        Vector2 size, lowerBounds1, lowerBounds2, lowerBounds3, mousePos;
        if (HeldCard != null)
        {
            if (HeldCard.sprite != null)
            {
                if (!board.PlacedThisRound[0])
                {
                    rect = HeldCard.rectTransform.rect;
                    size = new Vector2(rect.width, rect.height);
                    lowerBounds1 = new Vector2(screenSlot1.x - size.x / 2f, screenSlot1.y - size.y / 2f);
                    lowerBounds2 = new Vector2(screenSlot2.x - size.x / 2f, screenSlot2.y - size.y / 2f);
                    lowerBounds3 = new Vector2(screenSlot3.x - size.x / 2f, screenSlot3.y - size.y / 2f);
                    bounds1 = new Rect(lowerBounds1, size);
                    bounds2 = new Rect(lowerBounds2, size);
                    bounds3 = new Rect(lowerBounds3, size);
                    mousePos = Input.mousePosition;
                    if (bounds1.Contains(mousePos))
                    {
                        board.SetCard(true, 1, HeldCard.mainTexture.name);
                        HeldCard.sprite = null;
                        HighlightOne.color = new Color(1f, 0f, 0f, 120f / 255f);
                    }
                    else if (bounds2.Contains(mousePos))
                    {
                        board.SetCard(true, 2, HeldCard.mainTexture.name);
                        HeldCard.sprite = null;
                        HighlightTwo.color = new Color(1f, 0f, 0f, 120f / 255f);
                    }
                    else if (bounds3.Contains(mousePos))
                    {
                        board.SetCard(true, 3, HeldCard.mainTexture.name);
                        HeldCard.sprite = null;
                        HighlightThree.color = new Color(1f, 0f, 0f, 120f / 255f);
                    }
                }
                if (board.PlacedThisRound[0]) HeldCard.sprite = null;
            }
        }
    }
}
