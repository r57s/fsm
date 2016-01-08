using UnityEngine;

class FSMTests : MonoBehaviour
{
    
    private FSM t;
    
    float time;
    
    void Awake()
    {
        t = new FSM();
        
        t.OnMove(string.Empty, "Start", OnStarted);
        
        t.OnEvent("Start.FixedUpdate, Start2.FixedUpdate", (s, e, args) => {
           Debug.LogFormat("State: {0}, Event: {1}, Args[0]= {2}, t = {3}", s, e, args[0], time); 
           if (s == "Start") 
           {
            time += (float) args[0];
            if (time > 10.0f) {
                time = 0.0f;
                t.Move("Start2");
            }
           }
        });
        
        t.OnEvent("Start2.FixedUpdate", Start2FixedUpdate);
        
    }
    
    void Start()
    {
        t.Move("Start");
    }
    
    void FixedUpdate()
    {
        t.Trigger("FixedUpdate", Time.deltaTime);
    }
    
    void OnStarted(string from, string to, params object[] args)
    {
        Debug.Log("I have started.");
    }
    
    void Start2FixedUpdate(string state, string eventName, params object[] args) {
        time += (float) args[0];
        if (time > 10.0f) {
            time = 0.0f;
            t.Move("Start");
        }
    }
    
    
}