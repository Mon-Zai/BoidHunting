using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField] private int _requiredBoids = 5;
    [SerializeField] TextMeshPro _goalText;
    [SerializeField] TextMeshPro _currentText;
    [SerializeField] private GameEventSO victoryEvent;
    private int _currentBoids = 0;
    void Start()
    {
        if (_goalText != null)
        {
            _goalText.text = "Goal\n" + _requiredBoids.ToString() + "\nBoids";
        }
        UpdateText();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boid"))
        {
            _currentBoids++;
            UpdateText();
            CheckGoal();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boid"))
        {
            _currentBoids--;
            UpdateText();
        }
    }
    private void CheckGoal()
    {
        if (_currentBoids >= _requiredBoids)
        {
            Debug.Log("Goal reached!");
            victoryEvent?.Raise();
        }
    }
    void UpdateText()
    {
        if (_currentText != null)
        {
            _currentText.text = "Current\n" + _currentBoids.ToString() + "\nBoids";
        }
    }
}
