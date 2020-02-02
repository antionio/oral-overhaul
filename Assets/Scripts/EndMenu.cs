using UnityEngine;

public class EndMenu : MonoBehaviour
{

    public AudioSource AudioSource;
    public AudioClip GoodjobClip;
    public AudioClip ToobadClip;
    public Animator Animator;
    public ParticleSystem SparklesParticle;
    public ParticleSystem SparklesParticle2;
    public Vector3 EndToothOffset;
    public Vector3 EndFrontToothOffset;

    private void OnEnable()
    {
        ToolManager.Instance.AudioSource.Stop(); // make sure tool sounds will be off
        
        var allTooths = SetupToothsForUI();
        
        Animator.ResetTrigger("Goodjob");
        Animator.ResetTrigger("Toobad");
        
        bool goodJob = CalculateScores(allTooths);
        
        if (goodJob)
        {
            AudioSource.clip = GoodjobClip;
            AudioSource.Play();
            
            SparklesParticle.Play();
            SparklesParticle2.Play();
            Animator.SetTrigger("Goodjob");
            return;
        }
        
        AudioSource.clip = ToobadClip;
        AudioSource.Play();
        
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

            //scraps give minus too
            var scrap = t.GetComponentInChildren<Scrap>(true);
            if (scrap != null && scrap.isActiveAndEnabled)
            {
                score -= 1;
            }
        }

        return score > -1;
    }
}
