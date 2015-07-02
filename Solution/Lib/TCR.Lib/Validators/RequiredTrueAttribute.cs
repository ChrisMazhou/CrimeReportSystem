using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCR.Lib.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class RequiredTrueAttribute : ValidationAttribute
    {
        // Internal field to hold the mask value.
        readonly bool accepted;

        public bool Accepted
        {
            get { return accepted; }
        }

        public RequiredTrueAttribute(bool accepted)
        {
            this.accepted = accepted;
        }

        public RequiredTrueAttribute()
        {
            this.accepted = true;
        }


        public override bool IsValid(object value)
        {
            bool isAccepted = (bool)value;
            return (isAccepted == true);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,   ErrorMessageString, name, this.Accepted);
        }
    }
}
