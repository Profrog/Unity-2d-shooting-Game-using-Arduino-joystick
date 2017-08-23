using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class NewBehaviourScript : MonoBehaviour
{

    //public AudioSource music;
    //public AudioClip a;
    public GameObject bullet;
    public AudioClip sndEXP;
    /// //////////////
    /**/

    //public Text text1;
    public GameObject monster;
    private GameObject[] bullet1 = new GameObject[100];
    private int count = 0;
    private bool startCheck = true;
    private static AndroidJavaObject _plugins;
    SerialPort arduino1;
    
    
    public Text message;
    public Text message2;
    public Text message3;
    public GameObject enemy;
    public float X_value = 0;
    public float Y_value = 0;
    public string arduinoString = "";
    private bool count1 = true;
    public bool okay = false;
    private Stopwatch sw = new Stopwatch();
    AndroidJavaClass jc;
    AndroidJavaObject _activity;
    private Vector2 moving;
    int speedCount = 7;


    public Slider slie1;



    void androidAwake_()
    {
        message2.text = "";
        
    }


    public void androidGet_(string arg) // android use this funiction 
    {
        arduinoString = arg;

    
        if (arg == "")
        {
            setMessage_("Error");
            return;
        }

        else
        {
                    setMessage_(arg);
                    making_string(arg); //for text_writting
                    setText_andMoving(arg);             
        }
    }



    void setMessage_(string line)
    {
        message2.text = line;
        Debug.Log(line);
    }


    void setMessage_(int line)
    {
        if (line > 0)
        {
            message.text = "1";
        }

        else
        {
            message.text = "0";
        }

    }

    void going()
    {

        if (arduino1.IsOpen)
        {
            try
            {
                string line = arduino1.ReadLine();
                androidGet_(line);
                Debug.Log(line);
            }

            catch (System.Exception)
            {
                //text1.text = "error";

            }
        }

    }

  //setting position
    // Use this for initialization
    void Start()
    {
        //AndroidJavaClass jc = new AndroidJavaClass("com.example.unity0000.myapplication5555");
        //jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //_activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        

        sw.Start();
        if (SerialPort.GetPortNames().Length != 0)
        {
            arduino1 = new SerialPort(SerialPort.GetPortNames()[0], 9600, Parity.None, 8, StopBits.None);
            arduino1.Close();
            arduino1.Open();
            arduino1.ReadTimeout = 1;
            makingbullet_();
            
            bullet.SetActive(false);
            startCheck = true;
        }

        else
        {
            Debug.Log(SerialPort.GetPortNames());
            setMessage_(1);
            startCheck = false;
        }
        //music.loop = true;
    }

    


    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            going();
            string send = monster.transform.position.ToString();
            //Debug.Log("sending" + send); //****
            enemy.SendMessage("getting", send);
           
        }


        else
        {
            // _activity.Call("onCreate");
        }


        if (sw.ElapsedMilliseconds >= 1000)
        {
            sw.Reset();
            sw.Start();
            speedCount = 7;
            okay = false; //initialing shooting
        }


        settingPosition();

    }

    //

    private void settingPosition() //각종 부하 물체들의 위치 재정의
    {
        slie1.transform.position = gameObject.transform.position;
        slie1.transform.position+= new Vector3(0,0.2f,0);
    }



    void making_string(string line)
    {
        string[] result = new string[3];


        try
        {
            result = line.Split(' '); //split x_value, y_value


            if(System.Int32.Parse(result[1]) > 1000 || System.Int32.Parse(result[1]) > 1000)
            {
                if(sw.ElapsedMilliseconds % 200 == 0)
                {
                    speedCount--;
                }
            }


            else
            {
                speedCount = 7;
            }


            if (result[1] != null)
            {
                Y_value = (System.Int32.Parse(result[1]) - 500) / 500; //getting_result[1] value: (0~1023) ->(-1.x ~1.x)            


                if (result[2] != null)
                {
                    X_value = (System.Int32.Parse(result[2]) - 500) / 500; //getting_result[2] value: (0~1023) -> (-1.x ~ 1.x)
                }
            }


            if (System.Int32.Parse(result[0]) == 1)
            {
                setMessage_(1);
                okay = true;
                shooting_();
            }

            else
            {
                setMessage_(0);
            }

        }

        catch (System.Exception e)
        {
            // text1.text = "error2";

        }
    }


    void setText_andMoving(string line)//캐릭터 움직임을 구현
    {
        if (line != null)
        {
            //text1.text = "X value is" + X_value + "Y_value is" + Y_value;
        }

        else
        {
            //text1.text = "there is not string";
        }


        if (X_value < 2 && Y_value < 2)
        {
            moving = new Vector2(X_value, Y_value);
            monster.transform.Translate(moving * Time.smoothDeltaTime * speedCount);
           
        }
    }


    public void makingbullet_()//총알 생성
    {
        bullet1 = new GameObject[100];
        GameObject gameobj = Instantiate(bullet, monster.transform.position, monster.transform.rotation) as GameObject;
        gameobj.SetActive(false);

        for (int i = 0; i < 100; i++)
        {
            bullet1[i] = Instantiate(bullet, monster.transform.position, monster.transform.rotation) as GameObject;
            bullet1[i].transform.parent = gameObject.transform;
            bullet1[i].SetActive(false);
        }

    }

    

    IEnumerator delay()
    {
        count1 = false;
        yield return new WaitForSeconds(0.1f);
        count1 = true;

    }


    public void shooting_()
    {
        if (count < 100)
        {
            //Debug.Log(count);
            bullet1[count].transform.position = monster.transform.position;
            bullet1[count].SetActive(true);
            Rigidbody2D obj = bullet1[count].GetComponent<Rigidbody2D>();
            AudioSource.PlayClipAtPoint(sndEXP, obj.transform.position);
            Vector3 arrow = new Vector3(0, 100f, 0) - gameObject.transform.position;
            obj.AddForce(1800 * obj.transform.up); //회전을 하면 이상한 방향으로 튀김
            Destroy(bullet1[count], 2f);
            count++;
        }


        else
        {
            count = 0;
            makingbullet_();
        }

        //Destroy(gameobj, 3.0f);
    }



    public void OnTriggerStay(Collider coll)
    {

        
       // Debug.Log("name" + damage);
        
        if (coll.gameObject.tag == "block")
            monster.transform.position = new Vector3(0, 0,6);


        else if(coll.gameObject.tag == "attack2")
        {
            var someScript = coll.transform.parent.GetComponent<NewBehaviourScript1>(); //적은 시간이 지날수록 강해진다.
            float damage = someScript.damage;
            if (slie1.value > 0)
                   slie1.value  -= 0.001f*damage ;
        }


        Debug.Log("collision" + coll.gameObject.tag);
    }

}