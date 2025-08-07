using Game.Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class FloatingJoystick : Joystick
{
    private Image _image;

    [Inject]
    public void Construct(GameController gameController)
    {
        _image = GetComponent<Image>();
        gameController.OnStartPlay += Enable;
        gameController.OnWinGame += Disable;
        gameController.OnLoseGame += Disable;
        Start();
        Disable();       
    }

    private void Enable()
    {
        _image.enabled = true;
        enabled = true;
    }

    private void Disable()
    {
        _image.enabled = false;
        enabled = false;
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Vector2.zero;
        OnPointerUp(eventData);
    }

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}