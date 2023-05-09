using System.Reflection;

namespace ConsoleApp73
{
    [Serializable]
    public struct Worker : IComparable<Worker>
    {
        private const int MIN_POSSIBLE_APPLYING_YEAR = 1970;

        private string _surname;
        private string _name;
        private string _middlename;
        private string _occupation;
        private int _applyingYear;

        public string? Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }

                _surname = value;
            }
        }
        public string? Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }

                _name = value;
            }
        }
        public string? Middlename
        {
            get
            {
                return _middlename;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }

                _middlename = value;
            }
        }
        public string? Occupation
        {
            get
            {
                return _occupation;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }

                _occupation = value;
            }
        }
        public int ApplyingYear
        {
            get
            {
                return _applyingYear;
            }
            set
            {
                if (value < MIN_POSSIBLE_APPLYING_YEAR)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _applyingYear = value;
            }
        }

        public int Experience => DateTime.Today.Year - _applyingYear;

        public Worker(string[]? fullName, string? occupation, int applyingYear)
        {
            if (fullName is null || occupation is null)
            {
                return;
            }

            Surname = fullName[0];
            Name = fullName[1];
            Middlename = fullName[2];
            Occupation = occupation;
            ApplyingYear = applyingYear;
        }

        public override string ToString()
        {
            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            var thisObj = this;

            var values = fields.Select(x => x.GetValue(thisObj));

            return string.Join(' ', values);
        }

        public int CompareTo(Worker other)
        {
            return _surname.CompareTo(other._surname);
        }
    }
}
