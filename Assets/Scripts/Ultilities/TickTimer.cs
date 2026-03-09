using System;

public class TickTimer
{
    float interval;
    float timer;

    public Action OnTick;

    public TickTimer(float interval)
    {
        this.interval = interval;
    }

    public void Update(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= interval)
        {
            timer -= interval;
            OnTick?.Invoke();
        }
    }
}