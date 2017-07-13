using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    
    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                


    private void OnEnable()
    {
        //When the tank is turned on, reset the launch force and the UI
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        //The fire Axis is based on the player number
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce; // The slider will change it's value based on the amount of force

        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            //At max charge, not fired
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }

        else if (Input.GetButtonDown(m_FireButton))
        {
            //have we pressed fire button for the first time?
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            //Play the sound
            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
        }
        else if(Input.GetButton(m_FireButton) && !m_Fired)
        {
            //Holding the fire button, not yet fired
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            m_AimSlider.value = m_CurrentLaunchForce;

        }
        else if(Input.GetButtonUp(m_FireButton) && !m_Fired)
        {
            // We released the button, having not fired yet
            Fire();
        }

    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        m_Fired = true;

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
     
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        //Changing the clip to the firing clip and play it
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
       
        //Reset the launch force. This is a precaution in case of missing button events
        m_CurrentLaunchForce = m_MinLaunchForce;

    }
}