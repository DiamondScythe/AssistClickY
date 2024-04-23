using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.Helpers.Misc
{
    public class Cooldown
    {
        private static readonly Lazy<Cooldown> _instance = new Lazy<Cooldown>(() => new Cooldown());
        private bool _isCooldownActive;
        private readonly object _lock = new object();

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
