namespace Scripts.Utility
{
    public delegate void EventHandler();
    
    public delegate void EventHandler<in T1>(T1 param1);
    
    public delegate void EventHandler<in T1, in T2>(T1 param1, T2 param2);
    
    public delegate void EventHandler<in T1, in T2, in T3>(T1 param1, T2 param2, T3 param3);
}