using UnityEngine;

public class HunterController : MonoBehaviour
{
    private Hunter hunter;
    private HunterStateMachine _hunterStateMachine;
    void Start()
    {
        hunter = GetComponent<Hunter>();
        _hunterStateMachine = new HunterStateMachine(hunter);
    }
    void Update()
    {
        _hunterStateMachine.FSMUpdate();
    }
    void FixedUpdate()
    {
        _hunterStateMachine.FSMFixedUpdate();
    }

}