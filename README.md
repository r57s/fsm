fsm
=======

- FSM is a Fixed State Machine 
- Only single file of 99 lines of code.
- Easy to integrate
- Unity example included
- FSM is MIT licensed.

### Example Usage

```c#
FSM fsm = new FSM();

fsm.OnMove(string.Empty, "Start", (state, eventName, args) => {
    Debug.Log("I've moved to Start!");
});

fsm.OnEvent("Start.Hello", Hello);
fsm.OnEvent("Start.GoodBye, Start.Farewell", (state, eventName, args) => {
    Debug.Log("Goodbye and Farewell!");    
});


fsm.Move("Start");
fsm.Trigger("Hello");
fsm.Trigger("Farewell");

```

### States

States are represented as a case-sensitive String. The default State is `string.Empty`.

### Movers

Movers are functions (as Lambda functions or Methods) that are called when a state is changed.

```c#
void Move(string newState, params object[] args);

fsm.Move("Run");
fsm.Move("Sprint", 5.0f);

```

```c#
void OnMove(string pattern, MoveFunction fn);

fsm.OnMove("Walk>Run", SomeFunction);
fsm.OnMove("Walk>Run, Walk>Sprint", SomeFunction);
fsm.OnMove("Walk>Run, Walk>Spring, Walk>Jog", (from, to, args) => {
        // Some code.
    });
```

```c#
void OnMove(string[] moveInfos, MoveFunction fn);

fsm.OnMove(new[]{"Walk>Run", "Walk>Sprint"}, SomeFunction);
fsm.OnMove(new[]{"Walk>Run", "Walk>Spring", "Walk>Jog"}, (from, to, args) => {
        // Some code.
    });
```

```c#
void OnMove(string from, string to, MoveFunction fn);

fsm.OnMove("Walk", "Run", SomeFunction);
fsm.OnMove("Walk", "Run", (from, to, args) => {
        // Some code.
    });
```

### Events

Events are functions (as Lambda functions or Methods) that are triggered when a State receives an event.

```c#
void Trigger(string eventName, params object[] args);

fsm.Trigger("Jump");
fsm.Trigger("Leap", 5.0f);
```

```c#
void OnEvent(string pattern, EventFunction fn);

fsm.OnEvent("Run.Jump", SomeFunction);
fsm.OnEvent("Run.Jump, Run.Hop", SomeFunction);
fsm.OnEvent("Run.Jump, Run.Hop, Run.Leap", (state, eventName, args) => {
        // Some code.
    });
```

```c#
void OnEvent(string[] eventInfos, EventFunction fn);

fsm.OnEvent(new[]{"Run.Jump", "Run.Hop"}, SomeFunction);
fsm.OnEvent(new[]{"Run.Jump", "Run.Hop", "Run.Leap"}, (state, eventName, args) => {
        // Some code.
    });
```

```c#
void OnEvent(string state, string eventName, EventFunction fn);

fsm.OnEvent("Run", "Jump", SomeFunction);
fsm.OnEvent("Run", "Jump", (state, eventName, args) => {
        // Some code.
    });
```

### Other API

- `FSM.State` current State name
- `FSM.OnAnyEvent` bind a function to an event belonging to any (`*`) state.

## Releases
- v1.0.0 (08/01/2016)
  - Initial Release