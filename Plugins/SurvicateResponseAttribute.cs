using System;
using System.Globalization;

namespace Plugins.Survicate
{
    public class ResponseAttribute
    {
        public string name { get; private set; }
        public string value { get; private set; }
        public string provider { get; private set; }

        public ResponseAttribute(string name, string value, string provider = null)
        {
            if (name == null || value == null)
            {
                throw new ArgumentException("Name and value cannot be null");
            }

            this.name = name;
            this.value = value;
            this.provider = provider;
        }

        public ResponseAttribute(string name, DateTime value, string provider = null)
        {
            if (name == null)
            {
                throw new ArgumentException("Name cannot be null");
            }

            this.name = name;
            this.value = SurvicateDateUtils.FormatDateToTimeZoneIso(value);
            this.provider = provider;
        }

        public ResponseAttribute(string name, int value, string provider = null)
        {
            if (name == null)
            {
                throw new ArgumentException("Name cannot be null");
            }

            this.name = name;
            this.value = value.ToString();
            this.provider = provider;
        }

        public ResponseAttribute(string name, double value, string provider = null)
        {
            if (name == null)
            {
                throw new ArgumentException("Name cannot be null");
            }

            this.name = name;
            this.value = value.ToString(CultureInfo.InvariantCulture);
            this.provider = provider;
        }

        public ResponseAttribute(string name, bool value, string provider = null)
        {
            if (name == null)
            {
                throw new ArgumentException("Name cannot be null");
            }

            this.name = name;
            this.value = value.ToString().ToLower();
            this.provider = provider;
        }

    }
}
