using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpHeight;
    [SerializeField] private int dashdistance;
    [SerializeField] public float jumpGracePeriod;
    [SerializeField] public int UnlockedZodiacsignIdx;
    public string UnlockedCharname;
    [SerializeField] private GameObject Projectile;
    private static   GameObject ProjectileInstance;
    [SerializeField] private GameObject Reticle;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private CircleCollider2D circlecollider2D;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] public ParticleSystem dust;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject EventSystem;
    GameObject EvenSystemInstance = null;
    GameObject pauseMenuInstance = null;
    [SerializeField] private Sprite[] ZSignSprites = new Sprite[12];
    public List<string> UnlockedSigns = new List<string>();
    enum ZodiacSigns { ARIES,TAURUS,GEMINI,CANCER,LEO,VIRGO,LIBRA,SCORPIO,SAGITTARIUS,CAPRICORN,AQUARIUS,PISCES}
    public string curLevel;
    public string lastCompletedlevel;
    private Vector2 aim;
    private Vector2 moveVal;
    private Vector2 last;
    private bool jumped = false;
    private bool isDebugged = true;
    private bool isPaused = false;
    private float? lastGroundedTime;
    private float? lastJumpedTime;
    private int CharEnumIdx;
    public void Awake()
    {
        UnlockedCharname = UnlockedSigns[UnlockedZodiacsignIdx];
        CharEnumIdx = (int)Enum.Parse(typeof(ZodiacSigns), UnlockedCharname, true);
        curLevel =SceneManager.GetActiveScene().name;
        
        //temporary
        if(curLevel == "Level 3" || curLevel =="Level 4")
            UnlockedSigns.Add(ZodiacSigns.SAGITTARIUS.ToString());

        if (!UnlockedSigns.Contains(ZodiacSigns.ARIES.ToString()))
            UnlockedSigns.Add(ZodiacSigns.ARIES.ToString());
        sr.sprite = ZSignSprites[CharEnumIdx];
        
    }
    public void OnMove(InputAction.CallbackContext cntx)
    {
        moveVal = cntx.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext cntx)
    {
        if (cntx.started)
        {
            lastJumpedTime = Time.time;
            if (IsGrounded() == true)
            {
                lastGroundedTime = Time.time;
                Jump();
            }
        }
    }
    public void OnRestart(InputAction.CallbackContext cntx)
    {
        if(cntx.started &&isDebugged == true)
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void OnUseAbility(InputAction.CallbackContext cntx)
    {
        if (cntx.started)
        {
            switch (CharEnumIdx)
            {
                case (int)ZodiacSigns.ARIES:
                    if (moveVal.x>0)
                    {
                        rb.AddForce(new Vector2(dashdistance,0), ForceMode2D.Impulse);
                        CreateDust();

                    }
                    else if (moveVal.x < 0)
                    {
                        rb.AddForce(new Vector2(-dashdistance, 0), ForceMode2D.Impulse);
                        CreateDust();

                    }
                    break;
                case (int)ZodiacSigns.TAURUS:

                case (int)ZodiacSigns.GEMINI:

                case (int)ZodiacSigns.CANCER:
                    break;
                case (int)ZodiacSigns.LEO:
                    break;
                case (int)ZodiacSigns.VIRGO:
                    break;
                case (int)ZodiacSigns.LIBRA:
                    break;
                case (int)ZodiacSigns.SCORPIO:
                    break;
                case (int)ZodiacSigns.SAGITTARIUS:
                    if(ProjectileInstance == null)
                        ProjectileInstance = Instantiate(Projectile, transform.position,Quaternion.identity);
                     
                    break;
                case (int)ZodiacSigns.CAPRICORN:
                    break;
                case (int)ZodiacSigns.AQUARIUS:
                    break;
                case (int)ZodiacSigns.PISCES:
                default: break;
            }
        }
    }
   
    public void OnSwitchCharacter(InputAction.CallbackContext cntx)
    {
        if (cntx.started)
        {
            Reticle.SetActive(false);
            Cursor.visible = true;
            UnlockedZodiacsignIdx = (UnlockedZodiacsignIdx + 1) % UnlockedSigns.Count;
            UnlockedCharname = UnlockedSigns[UnlockedZodiacsignIdx];
            CharEnumIdx = (int)Enum.Parse(typeof(ZodiacSigns), UnlockedCharname, true);
            sr.sprite = ZSignSprites[CharEnumIdx];
            
            switch (CharEnumIdx)
            {
                case (int)ZodiacSigns.ARIES:
                    break;
                case (int)ZodiacSigns.TAURUS:
                    break;
                case (int)ZodiacSigns.GEMINI:
                    break;
                case (int)ZodiacSigns.CANCER:
                    break;
                case (int)ZodiacSigns.LEO:
                    break;
                case (int)ZodiacSigns.VIRGO:
                    break;
                case (int)ZodiacSigns.LIBRA:
                    break;
                case (int)ZodiacSigns.SCORPIO:
                    break;
                case (int)ZodiacSigns.SAGITTARIUS:
                    if(Reticle.activeSelf == false)
                    {
                        Reticle.SetActive(true);
                        Cursor.visible = false;
                    }
                    break;
                case (int)ZodiacSigns.CAPRICORN:
                    break;
                case (int)ZodiacSigns.AQUARIUS:
                    break;
                case (int)ZodiacSigns.PISCES:
                    break;
                default: break;
            }
        }
    }
    public void OnPause(InputAction.CallbackContext cntx)
    {
        
        if (!isPaused)
        {

            pauseMenuInstance = Instantiate(pauseMenu, Vector3.zero, Quaternion.identity);
            EvenSystemInstance = Instantiate(EventSystem, Vector3.zero, Quaternion.identity);
            Time.timeScale = 0;
            Cursor.visible = true;
            isPaused = true;
        }
        else if(isPaused == true && pauseMenuInstance != null)
        {
            Cursor.visible = false;
            Destroy(pauseMenuInstance);
            Destroy(EvenSystemInstance);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    void Update()    
    {
        transform.Translate((Vector3)moveVal*moveSpeed*Time.deltaTime);
        if (Reticle.activeSelf == true)
        { 
            
                Reticle.transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
        }
        
    }
    void Jump()
    {
        if (Time.time - lastGroundedTime<= jumpGracePeriod)
        {
            if(Time.time - lastJumpedTime <= jumpGracePeriod)
            {

                rb.velocity = Vector2.up * jumpHeight;
                lastJumpedTime = null;
                lastGroundedTime = null;
            }
        }
        jumped = false;
    }
    private bool IsGrounded()
    {
        float extraHeight = .01f;
        RaycastHit2D raycasthit = Physics2D.Raycast(circlecollider2D.bounds.center, Vector2.down, circlecollider2D.bounds.extents.y+extraHeight,platformLayerMask);
        Color rayColor = Color.green;
        if (raycasthit.collider != null)
        {
            rayColor = Color.green;  
        }
        else
        {
            rayColor = Color.red;
        }
        return raycasthit.collider!=null;
    }
    void CreateDust()
    {
        dust.Play();
    }
   
    string DetermineSignString(int ZodiacSign)
    {
        switch (ZodiacSign)
        {
            case (int)ZodiacSigns.ARIES:
                return ZodiacSigns.ARIES.ToString();              
            case (int)ZodiacSigns.TAURUS:
                return ZodiacSigns.TAURUS.ToString();
            case (int)ZodiacSigns.GEMINI:
                return ZodiacSigns.GEMINI.ToString();
            case (int)ZodiacSigns.CANCER:
                return ZodiacSigns.CANCER.ToString();
            case (int)ZodiacSigns.LEO:
                return ZodiacSigns.LEO.ToString();
            case (int)ZodiacSigns.VIRGO:
                return ZodiacSigns.VIRGO.ToString();
            case (int)ZodiacSigns.LIBRA:
                return ZodiacSigns.LIBRA.ToString();
            case (int)ZodiacSigns.SCORPIO:
                return ZodiacSigns.SCORPIO.ToString();
            case (int)ZodiacSigns.SAGITTARIUS:
                return ZodiacSigns.SAGITTARIUS.ToString();
            case (int)ZodiacSigns.CAPRICORN:
                return ZodiacSigns.CAPRICORN.ToString();
            case (int)ZodiacSigns.AQUARIUS:
                return ZodiacSigns.AQUARIUS.ToString();
            case (int)ZodiacSigns.PISCES:
                return ZodiacSigns.PISCES.ToString();
            default: return ZodiacSigns.ARIES.ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal") == true)
        {
            lastCompletedlevel = SceneManager.GetActiveScene().name;
        }

    }
    
}
