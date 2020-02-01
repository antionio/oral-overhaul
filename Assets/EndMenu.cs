using UnityEngine;

public class EndMenu : MonoBehaviour
{

    public Animator Animator;

    private void OnEnable()
    {
        Animator.ResetTrigger("Goodjob");
        Animator.ResetTrigger("Toobad");
        
        bool goodJob = CalculateScores();
        
        if (goodJob) {
            Animator.SetTrigger("Goodjob");
            return;
        }
        Animator.SetTrigger("Toobad");
    }

    private bool CalculateScores()
    {
        var allTooths = FindObjectsOfType<Tooth>();

        int score = 0;
        foreach (var t in allTooths)
        {
            var toothCondition = t.GetToothCondition();
            switch (toothCondition)
            {
                case Tooth.ToothCondition.Broken:
                    // nothing
                    break;
                case Tooth.ToothCondition.Cracked:
                    score -= 1;
                    break;
                case Tooth.ToothCondition.Shattered:
                    score -= 1;
                    break;
                case Tooth.ToothCondition.Decay:
                    score -= 1;
                    break;
                case Tooth.ToothCondition.Filled:
                    score += 1;
                    break;
                case Tooth.ToothCondition.Healthy:
                    score += 1;
                    break;
            }
        }

        return score > -1;
    }
}
