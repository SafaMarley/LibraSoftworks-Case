using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    [SerializeField] private Sprite spriteEmpty;
    [SerializeField] private Sprite spriteCrossed;
    private SpriteRenderer spriteRenderer;

    private List<BoardCell> neighbors = new List<BoardCell>();
    public List<BoardCell> Neighbors { get => neighbors; }

    private bool isActive = false;
    public bool IsActive { get => isActive; }

    public void Initialize(float x, float y, float scaleFactor)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteEmpty;
        transform.position = new Vector2(x / scaleFactor, y / scaleFactor);
        transform.localScale = Vector3.one / scaleFactor;
    }

    private void OnMouseDown()
    {
        if (!isActive)
        {
            Activate();
        }
    }

    public void AddToNeighbors(BoardCell neighborCell)
    {
        neighbors.Add(neighborCell);
    }

    private void Activate()
    {
        if (Board.Instance.FindMatches(this))
        {

        }
        else
        {
            spriteRenderer.sprite = spriteCrossed;
            isActive = true;
        }
    }
    public void Deactivate()
    {
        spriteRenderer.sprite = spriteEmpty;
        isActive = false;
    }
}
