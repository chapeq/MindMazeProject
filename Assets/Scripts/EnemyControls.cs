using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControls : MonoBehaviour
{
    public float TimeBeforeReborn = 60f;
    public float TimeBetweenPursuit = 10f;
    public float DistanceStartMusic = 40f;
    [FMODUnity.EventRef]
    public string AttackSound;
    [FMODUnity.EventRef]
    public string DeathSound;

    private GameObject target;
    private PlayerStats player;
    private NavMeshAgent agent;
    private Animator anim;
    private float timerPursuit;
    private bool isDead;
    private GameObject canvas;
    private FMODUnity.StudioEventEmitter EnemyMusic;


    private void Start()
    {
        isDead = false;
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        canvas = GameObject.Find("CanvasScreen");
        timerPursuit = TimeBetweenPursuit;
        EnemyMusic = gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cone")
        {
            if (EnemyMusic.IsPlaying())
                EnemyMusic.Stop();
            FMODUnity.RuntimeManager.PlayOneShot(DeathSound, gameObject.transform.position);
            anim.SetTrigger("Die");
            isDead = true;
            canvas.GetComponent<UIController>().DangerUIOff();
            StartCoroutine(Reborn());
        }
    }


    float cornersCalculation()
    {
        Color c = Color.white;
        NavMeshPath path = agent.path;

        switch (path.status)
        {
            case NavMeshPathStatus.PathComplete:
                c = Color.white;
                break;
            case NavMeshPathStatus.PathInvalid:
                c = Color.red;
                break;
            case NavMeshPathStatus.PathPartial:
                c = Color.yellow;
                break;
        }

        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length)
        {
            Vector3 currentCorner = path.corners[i];
            Debug.DrawLine(previousCorner, currentCorner, c);
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }

        return lengthSoFar;
    }

    void FixedUpdate()
    {
        if (!Timer.instance.isTimerOn)
        {
            return;
        }

        if (timerPursuit >= TimeBetweenPursuit)
        {
            MoveToTarget();
        }
        else
        {
            timerPursuit += Time.fixedDeltaTime;
        }

        float speedEnemy = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speedEnemy);

    }

    IEnumerator Reborn()
    {
        yield return new WaitForSeconds(TimeBeforeReborn);
        anim.SetTrigger("Reborn");
        agent.ResetPath();
        isDead = false;
        timerPursuit = TimeBetweenPursuit;
    }

  
    void MoveToTarget()
    {
        if (isDead)
            return;

        agent.destination = target.transform.position;
        float distance = cornersCalculation();

        if (distance <= DistanceStartMusic && distance != 0)
        {
            canvas.GetComponent<UIController>().DangerUIOn();
            if (!EnemyMusic.IsPlaying())
                EnemyMusic.Play();
        }
        else
            canvas.GetComponent<UIController>().DangerUIOff();

        if (distance <= agent.stoppingDistance && distance != 0)
        {
            transform.LookAt(target.transform);
            anim.SetTrigger("Attack");
            EnemyMusic.Stop();

            FMODUnity.RuntimeManager.PlayOneShot(AttackSound, gameObject.transform.position);
            player.RemovePtsVie(15);
            timerPursuit = 0f;
            agent.ResetPath();
            canvas.GetComponent<UIController>().DangerUIOff();
        }
    }
}




