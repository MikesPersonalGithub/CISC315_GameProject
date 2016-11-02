using UnityEngine;
using System.Collections;

public class VillainAI : MonoBehaviour {

    // Enums
    private enum eMotionType
    {
        NONE,
        LINEAR_TOWARD_PLAYER,
        LINEAR_RANDOM,
        NUMBER_OF_TYPES
    }
    //private eMotionType motion_type = (eMotionType)Random.Range(1, (float)eMotionType.NUMBER_OF_TYPES);
    private eMotionType motion_type = eMotionType.LINEAR_TOWARD_PLAYER;

    // State Members
    private float fstate = 0.0f;

    // Attribute Members
    private float speed = 0.005f;

    // Idle Members
    //private float idle_cap = 1.0f;
    //private float idle_time_in_seconds = 0.0f;

    // Time Members
    float time_elapsed = 0.0f;

    // Object Reference Members
    private GameObject player = null;
    private MyPlayer data_player = null;

    private GameObject my_camera = null;
    private ProgramManager data_program_manager = null;

    // Saved Members
    Vector3 dir_to_original_loc = new Vector3(0, 0, 0);
    Vector3 original_player_loc = new Vector3(0, 0, 0);   // original loc of player
    Vector3 original_random_loc = new Vector3(0, 0, 0);   // original random location

    // Use this for initialization
    void Start () {
        // player1
        player = GameObject.Find("Player1");
        data_player = player.GetComponent(typeof(MyPlayer)) as MyPlayer;

        // program manager
        my_camera = GameObject.Find("Main Camera");
        data_program_manager = my_camera.GetComponent(typeof(ProgramManager)) as ProgramManager;
    }
	
	// Update is called once per frame
	void Update () {
        switch(motion_type )
        {
            case eMotionType.NONE:
                {
                }
                break;

            case eMotionType.LINEAR_RANDOM:
                {
                    Move_LINEAR_RANDOM();
                }
                break;

            case eMotionType.LINEAR_TOWARD_PLAYER:
                {
                    Move_LINEAR_TOWARD_PLAYER();
                }
                break;
        }
	}

    void Move_LINEAR_RANDOM()
    {
        if (fstate == 0.0f) // calculate normalized direction vector toward the random destination
        {
            // store original locations
            Vector3 source = transform.position;
            Vector3 dest = new Vector3(Random.Range(-2, 2), transform.position.y, Random.Range(-2, 2));
            Vector3 dir = (dest - source).normalized;

            dir_to_original_loc = dir;
            original_random_loc = dest;
            original_player_loc = player.transform.position;
            fstate = 1.0f;
        }

        if (fstate == 1.0f) // move toward destination
        {
            transform.transform.Translate(speed * dir_to_original_loc);
            fstate = 2.0f;
        }

        if (fstate == 2.0f) // check if w/in tolerance
        {
            bool within_tolerance_of_player = WithinToleranceOfThisDestination(player.transform.position);
            bool within_tolerance_of_random_dest = WithinToleranceOfThisDestination(original_random_loc);

            if (within_tolerance_of_random_dest)
            {
                fstate = 10.0f;
            }
            else if (within_tolerance_of_player)
            {
                fstate = 20.0f;
            }
            else
            {
                fstate = 1.0f;
            }
        }

        if (fstate == 10.0f) // reached random dest
        {

        }

        if (fstate == 20.0f) // reached player
        {
            data_player.PlaySound_HitByVillain();
            fstate = 21.0f;
        }


    }

    void Move_LINEAR_TOWARD_PLAYER()
    {
        if (fstate == 0.0f) // calculate normalized direction vector toward player
        {
            // store original locations
            Vector3 source = transform.position;
            original_player_loc =  player.transform.position;
            dir_to_original_loc = (original_player_loc - source).normalized;
            fstate = 1.0f;
        }

        if (fstate == 1.0f) // move toward destination
        {
            transform.transform.Translate(speed * dir_to_original_loc);
            fstate = 2.0f;
        }

        if (fstate == 2.0f) // check if w/in tolerance
        {
            print(player.transform.position + "," + original_player_loc);
            // w/in tolerance of player?
            if (WithinToleranceOfThisDestination(player.transform.position))
            {
                fstate = 20.0f;
            }
            // w/in tolerance of original location?
            else if (WithinToleranceOfThisDestination(original_player_loc))
            {
                fstate = 40.0f;
            }

            else
            {
                fstate = 1.0f;
            }
        }

        if (fstate == 20.0f) // reached player
        {
            data_player.PlaySound_HitByVillain();
            fstate = 100.0f;
        }



        if (fstate == 40.0f) // reached original location; player likely moved away, but to determine this we must give the player code a chance since original_loc and player pos are too close
        {
            fstate = 100.0f;
        }

        if (fstate == 100.0f) // wait
        {
            time_elapsed += Time.deltaTime;
            if (time_elapsed >= 5.0)
            {
                time_elapsed = 0.0f;
                fstate = 0.0f;
            }
        }



        //print(DistanceToPlayer());

    }

    bool WithinToleranceOfThisDestination(Vector3 destination)
    {
        Vector3 myself = transform.position;
        float distance = (destination - myself).magnitude;
        return distance<=0.25f;
    }
}
