using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScript : MonoBehaviour
{
    private Texture2D Tree, Rock, Flower, Herb, Mushroom, defaultCursor;
    private UIManager Canvas;
    private Character player;

    private void Awake()
    {
        Tree = Resources.Load<Texture2D>("Icon/Cursor/wood");
        Rock = Resources.Load<Texture2D>("Icon/Cursor/granite");
        Flower = Resources.Load<Texture2D>("Icon/Cursor/flower");
        Herb = Resources.Load<Texture2D>("Icon/Cursor/herbs");
        Mushroom = Resources.Load<Texture2D>("Icon/Cursor/mushroom");
        defaultCursor = Resources.Load<Texture2D>("Icon/Cursor/crosshair");
    }

    private void Start()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<UIManager>();
        player = Character.instance;
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, Character.instance.transform.position) <= 3.0f)
        {
            Canvas.EPanelActive(true);

            string name = this.gameObject.name;

            if (name.Contains("Tree"))
                Cursor.SetCursor(Tree, Vector2.zero, CursorMode.Auto);
            else if (name.Contains("Rock"))
                Cursor.SetCursor(Rock, Vector2.zero, CursorMode.Auto);
            else if (name.Contains("Mushroom"))
                Cursor.SetCursor(Mushroom, Vector2.zero, CursorMode.Auto);
            else if (name.Contains("Flower"))
                Cursor.SetCursor(Flower, Vector2.zero, CursorMode.Auto);
            else if (name.Contains("LightingPlant"))
                Cursor.SetCursor(Herb, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        Canvas.EPanelActive(false);
        Canvas.TimePanelOnOff(false);
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
