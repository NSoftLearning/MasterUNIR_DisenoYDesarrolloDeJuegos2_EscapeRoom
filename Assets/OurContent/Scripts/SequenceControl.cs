using System.Linq;
using UnityEngine;

public class SequenceControl : MonoBehaviour
{
    [SerializeField] GameObject[] disableOnStart;

    [SerializeField] SequenceStep[] sequenceSteps;


    int currentStep = 0;    

    private void Start()
    {
        sequenceSteps[0].EnterStep();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            GoToNextStep();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentStep = 0;
            sequenceSteps[currentStep].EnterStep();
        }

    }

    [ContextMenu (nameof (GoToNextStep))]
    public void GoToNextStep ()
    {
        if (currentStep == sequenceSteps.Count() - 1)
            return;

        sequenceSteps[currentStep].ExitStep();
        currentStep++;
        sequenceSteps[currentStep].EnterStep();

        Debug.Log("Entered step " + currentStep + 1);
    }

    [ContextMenu(nameof(GoToPreviousStep))]
    public void GoToPreviousStep ()
    {
        if (currentStep == 0)
            return;

        sequenceSteps[currentStep].ExitStep();
        currentStep--;
        sequenceSteps[currentStep].EnterStep();
        Debug.Log("Entered step " + currentStep +  1);
    }
}


[System.Serializable]
public class SequenceStep 
{
    [SerializeField] GameObject[] activateOnEnterAndPersist;
    [SerializeField] GameObject[] deactivateOnEnter;
    [SerializeField] GameObject[] deactivateOnExit;


    
    public void EnterStep ()
    {
        foreach (GameObject item in activateOnEnterAndPersist)
        {
            item.SetActive(true);
        }

        foreach (GameObject item in deactivateOnEnter)
        {
            item.SetActive(false);
        }
    }

    public void ExitStep()
    {
        foreach (GameObject item in deactivateOnExit)
        {
            item.SetActive(false);
        }

    }
    
}
