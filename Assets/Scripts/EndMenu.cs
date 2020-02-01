using UnityEngine;

public class EndMenu : MonoBehaviour
{

    public Animator Animator;
    public ParticleSystem SparklesParticle;
    public ParticleSystem SparklesParticle2;
    public Vector3 EndToothOffset;
    public Vector3 EndFrontToothOffset;

    private void OnEnable()
    {
        var allTooths = SetupToothsForUI();
        
        Animator.ResetTrigger("Goodjob");
        Animator.ResetTrigger("Toobad");
        
        bool goodJob = CalculateScores(allTooths);
        
        if (goodJob) {
            SparklesParticle.Play();
            SparklesParticle2.Play();
            Animator.SetTrigger("Goodjob");
            return;
        }
        SparklesParticle.Stop();
        SparklesParticle2.Stop();
        Animator.SetTrigger("Toobad");
    }

    private Tooth[] SetupToothsForUI()
    {
        var allTooths = FindObjectsOfType<Tooth>();

        foreach (var t in allTooths)
        {
            var renderers = t.GetComponentsInChildren<SpriteRenderer>();
            foreach (var r in renderers)
            {
                r.sortingLayerName = "UI";
            }

            if (t.gameObject.name.Contains("Top"))
            {
                t.transform.position = t.transform.position + EndFrontToothOffset;    
                continue;
            }
            t.transform.position = t.transform.position + EndToothOffset;
        }
        
        return allTooths;
    }

    private bool CalculateScores(Tooth[] allTooths)
    {
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
