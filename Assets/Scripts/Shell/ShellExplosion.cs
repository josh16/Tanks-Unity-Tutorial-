using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);

    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.

		//An Array of collders that detect the pos, radius, and layer mask of what type of
		//game object that is going to collide.
		Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);
			
		// Have a for loop to check each rigidbody "The tanks"
		for (int i = 0; i < colliders.Length; i++) 
		{

			//Setting the targetRigidbody equal to all the collders with a rigidbody
			Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>;
		
			//If there is nothing there (rigidbody), continue on the next loop.
			if (!targetRigidbody) 
			{
				continue;
			}
		
			//Applying an Explosion force to a rigidbody (The Tanks) to push them away
		    targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);


			//Damage the tanks health by getting it's component
			TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
		
		
			if (!targetHealth) 
			{
				continue; // if it's not a target health, continue on to the next interation.
			}


			float damage = CalculateDamage (targetRigidbody.position);

		}



    
	
	}


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        return 0f;
    }
}