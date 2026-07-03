using TMPro;
using UnityEngine;

public class DefeatMenu : BaseScreen
{
    [SerializeField] private TextMeshProUGUI _defeatBoids;
    [SerializeField] private TextMeshProUGUI _defeatFood;
    private string reason;

    public void SetDefeatReason(string reason)
    {
        this.reason = reason;
    }
    public override void Show()
    {
        base.Show();
        if (reason == "Boids")
        {
            _defeatBoids.gameObject.SetActive(true);
            _defeatFood.gameObject.SetActive(false);
        }
        else if (reason == "Food")
        {
            _defeatBoids.gameObject.SetActive(false);
            _defeatFood.gameObject.SetActive(true);
        }
    }

}