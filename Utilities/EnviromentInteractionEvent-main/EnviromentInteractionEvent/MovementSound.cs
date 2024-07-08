using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicAudio;
[RequireComponent(typeof(AudioSource))]
public class MovementSound : MonoBehaviour
{
    public DynamicAudio.EnviromentEventProperty EnviromentEvent;
    public AudioSource audioSource;
    public LayerMask layer;
    // Start is called before the first frame update

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position+ Vector3.up * 0.01f, Vector3.down * 0.1f, Color.red);
       
    }

    public void PlaySound()
    {
        RaycastHit ray;
        if (Physics.Raycast(this.transform.position + Vector3.up * 0.01f, Vector3.down, out ray, 5f, layer))
        {
            if (ray.collider != null)
            {
                if (ray.collider.GetComponent<ObjectInfo>())
                {
                    var map = ray.collider.GetComponent<ObjectInfo>();
                        
                    
                    EnviromentInteractionEvent.PlaySound(map.materialType, EnviromentEvent, this.gameObject);
                    // Create a new GameObject and set its position



                    //EnviromentInteractionEvent.PlayPaticleAtPosition(map, EnviromentEvent, ray.point);

                }
            }
        }
    }
    public void PLaySoundAt(Vector3 position)
    {
        RaycastHit ray;
        if (Physics.Raycast(position+Vector3.up *0.01f, Vector3.down, out ray, 0.05f, layer))
        {
            if (ray.collider != null)
            {
                if (ray.collider.GetComponent<ObjectInfo>())
                {
                    var map = ray.collider.GetComponent<ObjectInfo>();

                    Debug.Log(map.materialType);
                    EnviromentInteractionEvent.PlaySound(map.materialType, EnviromentEvent, this.gameObject);
                    // Create a new GameObject and set its position
                        
                    

                    //EnviromentInteractionEvent.PlayPaticleAtPosition(map, EnviromentEvent, ray.point);

                }
            }
        }
        
    }
    
    
}
