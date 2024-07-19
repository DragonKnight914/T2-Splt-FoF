using UnityEngine;


namespace FSR
{
    [RequireComponent(typeof(AudioSource))]
    public class FSR_Player : MonoBehaviour
    {
        public AudioSource m_AudioSource;
        public Transform foot;
        public int raycastSize = 10;
        [SerializeField] private FSR_Data data;
        public PlayerAnimation walkAnimation;

        public void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            if (foot == null)
            {
                Debug.Log("unassigned foot ");
            }
            step();
        }


        public void step()
        {
            RaycastHit2D hit = (Physics2D.Raycast(foot.position, -foot.up, raycastSize));
            Debug.DrawRay(foot.position, -foot.up * raycastSize, Color.red, 1.0f);

            if (hit.collider != null)
            { 
                Debug.Log("Hit!");
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log(hit.collider.gameObject.tag);
                try {

                   FSR_SimpleSurface surface =  hit.collider.gameObject.GetComponent<FSR_SimpleSurface>();
                    foreach (FSR_Data.SurfaceType surfaceData in data.surfaces)
                    {
                        if (surfaceData.name.Equals(surface.GetSurface()))
                        {
                            Debug.Log(surfaceData.name);
                            playSound(surfaceData);
                        }
                    }
                }
                catch
                {
                    try
                    {
                        FSR_TagedSurface surface = hit.transform.GetComponent<FSR_TagedSurface>();
                        foreach (FSR_Data.SurfaceType surfaceData in data.surfaces)
                        {
                            if (surfaceData.name.Equals(surface.GetSurface()))
                            {
                                Debug.Log(surfaceData.name);
                                playSound(surfaceData);
                            }
                        }
                    }
                    catch
                    {
                        try
                        {
                            FSR_TerrainSurface surface = hit.transform.GetComponent<FSR_TerrainSurface>();
                            foreach (FSR_Data.SurfaceType surfaceData in data.surfaces)
                            {
                                if (surfaceData.name.Equals(surface.GetSurface(transform.position)))
                                {
                                    playSound(surfaceData);
                                }
                            }

                        }
                        catch {

                            foreach (FSR_Data.SurfaceType surfaceData in data.surfaces)
                            {
                                if (surfaceData.name.Equals("GENERIC"))
                                {
                                    Debug.Log("no surface detected");
                                    playSound(surfaceData);
                                }
                            }

                        }
                    }


                }

            }
        }



        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        private void playSound(FSR_Data.SurfaceType surfaceType)
        {
            AudioClip[] soundEffects= surfaceType.soundEffects;

            int n = Random.Range(0, soundEffects.Length);
            m_AudioSource.clip = soundEffects[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            soundEffects[n] = soundEffects[0];
            soundEffects[0] = m_AudioSource.clip;
        }

    }
}
