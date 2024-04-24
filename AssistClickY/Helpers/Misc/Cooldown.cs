using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace AssistClickY.Helpers.Misc
{
    public class Cooldown
    {
        //The Lazy<T> class encapsulates a factory method (in this case, () => new Cooldown()) that creates an instance of Cooldown when needed.
        //The Lazy<T> object initializes the singleton when the Cooldown.Instance property is accessed for the first time.
        private static readonly Lazy<Cooldown> _instance = new Lazy<Cooldown>(() => new Cooldown());
        private bool _isCooldownActive;
        private readonly object _lock = new object();

        //This property returns the value of _instance.Value.
        //Because _instance is a Lazy<Cooldown>, accessing _instance.Value initializes the singleton instance if it hasn't been initialized yet.
        //Once initialized, _instance.Value always returns the same instance of Cooldown.
        public static Cooldown Instance => _instance.Value;

        private Cooldown()
        {
            // Private constructor for Singleton pattern
        }

        public void TriggerCooldown(int durationInMiliseconds)
        {
            lock (_lock)
            {
                if (_isCooldownActive)
                    return;

                _isCooldownActive = true;

                // Start the cooldown timer
                Task.Delay(durationInMiliseconds).ContinueWith(t =>
                {
                    lock (_lock)
                    {
                        _isCooldownActive = false;
                    }
                });
            }
        }

        public bool IsCooldownActive()
        {
            lock (_lock)
            {
                return _isCooldownActive;
            }
        }
    }
}
