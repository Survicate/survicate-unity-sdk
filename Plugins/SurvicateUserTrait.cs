using System;

namespace Plugins.Survicate
{
    public class UserTrait
    {
        public string key { get; private set; }
        public string value { get; private set; }

        public UserTrait(string key, string value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentException("Key and value cannot be null");
            }

            this.key = key;
            this.value = value;
        }

        public UserTrait(string key, DateTime value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentException("Key and value cannot be null");
            }

            this.key = key;
            this.value = FormatDateToTimeZoneIso(value);
        }

        public UserTrait(string key, int value)
        {
            if (key == null)
            {
                throw new ArgumentException("Key cannot be null");
            }

            this.key = key;
            this.value = value.ToString();
        }

        public UserTrait(string key, double value)
        {
            if (key == null)
            {
                throw new ArgumentException("Key cannot be null");
            }

            this.key = key;
            this.value = value.ToString();
        }

        public UserTrait(string key, bool value)
        {
            if (key == null)
            {
                throw new ArgumentException("Key cannot be null");
            }

            this.key = key;
            this.value = value.ToString().ToLower();
        }

        private string FormatDateToTimeZoneIso(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }
}
