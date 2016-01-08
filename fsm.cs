// FSM
// 
// Copyright (c) 2016 Robin Southern -- github.com/r57s/fsm
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using System.Collections.Generic;

public class FSM
{
    
    public delegate void MoveFunction(string from, string to, params object[] args);
    
    public delegate void EventFunction(string state, string eventName, params object[] args);
    
    public FSM()
    {
        State = string.Empty;
    }
    
    public string State { get; private set; }
    
    public void Move(string newState, params object[] args)
    {
        string old = State;
        State = newState;
        foreach(var m in Moves)
        {
            if (m.f == old && m.t == State)
            {
                m.fn(old, State, args);
            }
        }
    }
    
    public void Trigger(string eventName, params object[] args)
    {
        string c = State;
        foreach(var e in Events)
        {
            if ((e.s == c || e.s == "*") && e.e == eventName)
            {
                e.fn(c, eventName, args);
            }
        }
    }
    
    public void OnMove(string pattern, MoveFunction fn)
    {
        OnMove(pattern.Split(','), fn);
    }
    
    public void OnMove(string[] moveInfos, MoveFunction fn)
    {
        foreach(var moveInfo in moveInfos)
        {
            var rule = moveInfo.Split('>');
            if (rule.Length != 2) {
                return;
            }
            OnMove(rule[0], rule[1], fn);
        }
    }
    public void OnMove(string from, string to, MoveFunction fn)
    {
        Moves.Add(new MoveInfo() {
            f = from,
            t = to,
            fn = fn
        });
    }
    
    public void OnEvent(string pattern, EventFunction fn)
    {
        OnEvent(pattern.Split(','), fn);
    }
    
    public void OnEvent(string[] eventInfos, EventFunction fn)
    {
        foreach(var eventInfo in eventInfos)
        {
            var rule = eventInfo.Split('.');
            if (rule.Length != 2) {
                return;
            }
            OnEvent(rule[0], rule[1], fn);
        }
    }
    
    public void OnEvent(string state, string eventName, EventFunction fn)
    {
        state = state.Trim();
        eventName = eventName.Trim();
        Events.Add(new EventInfo() {
            s = state,
            e = eventName,
            fn = fn
        });
    }
    
    public void OnAnyEvent(string eventName, EventFunction fn)
    {
        OnEvent("*", eventName, fn);
    }
    
#region Internal
    
    private class MoveInfo
    {
        public string f, t;    
        public MoveFunction fn;
    }
    
    private class EventInfo
    {
        public string s, e;
        public EventFunction fn;
    }
    
    private List<MoveInfo> Moves = new List<MoveInfo>();
    private List<EventInfo> Events = new List<EventInfo>();
    
#endregion
    
}