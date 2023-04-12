using System;

public class Events
{
    public static Action EnemyKilled;

    public static void SendEnemyKilled()
    {
        if (EnemyKilled != null) EnemyKilled.Invoke();
    }

    
    
}
